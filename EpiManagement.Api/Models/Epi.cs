using System.ComponentModel.DataAnnotations;

namespace EpiManagement.Api.Models
{
    public class Epi
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "CA é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "CA deve ser maior que 0")]
        public int CA { get; set; }

        [StringLength(500, ErrorMessage = "Descrição deve ter no máximo 500 caracteres")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Validade é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime Validade { get; set; }

        [Required(ErrorMessage = "Categoria é obrigatória")]
        [StringLength(100, ErrorMessage = "Categoria deve ter no máximo 100 caracteres")]
        public string Categoria { get; set; } = string.Empty;
        public bool EstaVencido => Validade < DateTime.Today;

        //Verificar se está próximo do vencimento (30 dias)
        public bool ProximoDoVencimento => Validade <= DateTime.Today.AddDays(30) && !EstaVencido;
    }
}

