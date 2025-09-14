using Microsoft.AspNetCore.Mvc;
using EpiManagement.Web.Models;
using EpiManagement.Web.Services;

namespace EpiManagement.Web.Controllers
{
    public class EpisController : Controller
    {
        private readonly EpiApiService _apiService;

        public EpisController(EpiApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string? nome, int? ca)
        {
            var epis = await _apiService.GetEpisAsync(nome, ca);
            var viewModel = new EpiFilterViewModel
            {
                Nome = nome,
                CA = ca,
                Epis = epis
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var epi = await _apiService.GetEpiAsync(id);
            if (epi == null)
            {
                return NotFound();
            }
            return View(epi);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Epi epi)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiService.CreateEpiAsync(epi);
                if (success)
                {
                    TempData["SuccessMessage"] = "EPI criado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Erro ao criar EPI. Verifique se o CA já não existe.";
                }
            }
            return View(epi);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var epi = await _apiService.GetEpiAsync(id);
            if (epi == null)
            {
                return NotFound();
            }
            return View(epi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Epi epi)
        {
            if (id != epi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _apiService.UpdateEpiAsync(id, epi);
                if (success)
                {
                    TempData["SuccessMessage"] = "EPI atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Erro ao atualizar EPI. Verifique se o CA já não existe.";
                }
            }
            return View(epi);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var epi = await _apiService.GetEpiAsync(id);
            if (epi == null)
            {
                return NotFound();
            }
            return View(epi);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _apiService.DeleteEpiAsync(id);
            if (success)
            {
                TempData["SuccessMessage"] = "EPI excluído com sucesso!";
            }
            else
            {
                TempData["ErrorMessage"] = "Erro ao excluir EPI.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Dashboard()
        {
            var dashboard = await _apiService.GetDashboardMetricsAsync();
            if (dashboard == null)
            {
                dashboard = new DashboardViewModel();
            }
            return View(dashboard);
        }
    }
}

