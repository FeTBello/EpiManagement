using System.ComponentModel.DataAnnotations;

namespace EpiManagement.Web.Models
{
    public class Epi
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "CA é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "CA deve ser maior que 0")]
        [Display(Name = "CA")]
        public int CA { get; set; }

        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Validade é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Validade")]
        public DateTime Validade { get; set; }

        [Required(ErrorMessage = "Categoria é obrigatória")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; } = string.Empty;

        public bool EstaVencido => Validade < DateTime.Today;
        public bool ProximoDoVencimento => Validade <= DateTime.Today.AddDays(30) && !EstaVencido;
    }
}

