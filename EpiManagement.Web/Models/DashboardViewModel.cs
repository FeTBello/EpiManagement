namespace EpiManagement.Web.Models
{
    public class DashboardViewModel
    {
        public int TotalEpis { get; set; }
        public int EpisVencidos { get; set; }
        public int EpisProximosDoVencimento { get; set; }
        public Dictionary<string, int> EpisPorCategoria { get; set; } = new();
        public List<EpiVencimento> EpisVencendoEm30Dias { get; set; } = new();
    }

    public class EpiVencimento
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int CA { get; set; }
        public DateTime Validade { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public int DiasParaVencimento { get; set; }
    }
}

