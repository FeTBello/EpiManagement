namespace EpiManagement.Web.Models
{
    public class EpiFilterViewModel
    {
        public string? Nome { get; set; }
        public int? CA { get; set; }
        public List<Epi> Epis { get; set; } = new();
    }
}

