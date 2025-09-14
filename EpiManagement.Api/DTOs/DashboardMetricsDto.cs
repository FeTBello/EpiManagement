namespace EpiManagement.Api.DTOs
{
    public class DashboardMetricsDto
    {
        public int TotalEpis { get; set; }
        public int EpisVencidos { get; set; }
        public int EpisProximosDoVencimento { get; set; }
        public Dictionary<string, int> EpisPorCategoria { get; set; } = new();
        public List<EpiVencimentoDto> EpisVencendoEm30Dias { get; set; } = new();
    }

    public class EpiVencimentoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int CA { get; set; }
        public DateTime Validade { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public int DiasParaVencimento { get; set; }
    }
}

