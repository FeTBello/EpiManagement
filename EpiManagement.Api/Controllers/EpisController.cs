using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EpiManagement.Api.Data;
using EpiManagement.Api.Models;
using EpiManagement.Api.DTOs;

namespace EpiManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisController : ControllerBase
    {
        private readonly EpiDbContext _context;

        public EpisController(EpiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Epi>>> GetEpis(
            [FromQuery] string? nome = null,
            [FromQuery] int? ca = null)
        {
            var query = _context.Epis.AsQueryable();

            // Filtro por nome
            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(e => e.Nome.Contains(nome));
            }

            // Filtro por CA
            if (ca.HasValue)
            {
                query = query.Where(e => e.CA == ca.Value);
            }

            var epis = await query.OrderBy(e => e.Nome).ToListAsync();
            return Ok(epis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Epi>> GetEpi(int id)
        {
            var epi = await _context.Epis.FindAsync(id);

            if (epi == null)
            {
                return NotFound(new { message = $"EPI com ID {id} não encontrado." });
            }

            return Ok(epi);
        }

        [HttpPost]
        public async Task<ActionResult<Epi>> CreateEpi(CreateEpiDto createEpiDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar se CA já existe
            var existingEpi = await _context.Epis.FirstOrDefaultAsync(e => e.CA == createEpiDto.CA);
            if (existingEpi != null)
            {
                return Conflict(new { message = $"Já existe um EPI com CA {createEpiDto.CA}." });
            }

            var epi = new Epi
            {
                Nome = createEpiDto.Nome,
                CA = createEpiDto.CA,
                Descricao = createEpiDto.Descricao,
                Validade = createEpiDto.Validade,
                Categoria = createEpiDto.Categoria
            };

            _context.Epis.Add(epi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEpi), new { id = epi.Id }, epi);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEpi(int id, UpdateEpiDto updateEpiDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var epi = await _context.Epis.FindAsync(id);
            if (epi == null)
            {
                return NotFound(new { message = $"EPI com ID {id} não encontrado." });
            }

            // Verificar se CA já existe em outro EPI
            var existingEpi = await _context.Epis.FirstOrDefaultAsync(e => e.CA == updateEpiDto.CA && e.Id != id);
            if (existingEpi != null)
            {
                return Conflict(new { message = $"Já existe outro EPI com CA {updateEpiDto.CA}." });
            }

            epi.Nome = updateEpiDto.Nome;
            epi.CA = updateEpiDto.CA;
            epi.Descricao = updateEpiDto.Descricao;
            epi.Validade = updateEpiDto.Validade;
            epi.Categoria = updateEpiDto.Categoria;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpi(int id)
        {
            var epi = await _context.Epis.FindAsync(id);
            if (epi == null)
            {
                return NotFound(new { message = $"EPI com ID {id} não encontrado." });
            }

            _context.Epis.Remove(epi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardMetricsDto>> GetDashboardMetrics()
        {
            var epis = await _context.Epis.ToListAsync();
            var today = DateTime.Today;

            var metrics = new DashboardMetricsDto
            {
                TotalEpis = epis.Count,
                EpisVencidos = epis.Count(e => e.Validade < today),
                EpisProximosDoVencimento = epis.Count(e => e.Validade <= today.AddDays(30) && e.Validade >= today),
                EpisPorCategoria = epis.GroupBy(e => e.Categoria)
                                     .ToDictionary(g => g.Key, g => g.Count()),
                EpisVencendoEm30Dias = epis.Where(e => e.Validade <= today.AddDays(30))
                                          .OrderBy(e => e.Validade)
                                          .Select(e => new EpiVencimentoDto
                                          {
                                              Id = e.Id,
                                              Nome = e.Nome,
                                              CA = e.CA,
                                              Validade = e.Validade,
                                              Categoria = e.Categoria,
                                              DiasParaVencimento = (e.Validade - today).Days
                                          })
                                          .ToList()
            };

            return Ok(metrics);
        }

        private bool EpiExists(int id)
        {
            return _context.Epis.Any(e => e.Id == id);
        }
    }
}

