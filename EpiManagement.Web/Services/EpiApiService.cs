using EpiManagement.Web.Models;
using System.Text.Json;
using System.Text;

namespace EpiManagement.Web.Services
{
    public class EpiApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EpiApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<Epi>> GetEpisAsync(string? nome = null, int? ca = null)
        {
            var query = new List<string>();
            if (!string.IsNullOrEmpty(nome))
                query.Add($"nome={Uri.EscapeDataString(nome)}");
            if (ca.HasValue)
                query.Add($"ca={ca.Value}");

            var queryString = query.Count > 0 ? "?" + string.Join("&", query) : "";
            var response = await _httpClient.GetAsync($"api/epis{queryString}");
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Epi>>(json, _jsonOptions) ?? new List<Epi>();
            }
            
            return new List<Epi>();
        }

        public async Task<Epi?> GetEpiAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/epis/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Epi>(json, _jsonOptions);
            }
            
            return null;
        }

        public async Task<bool> CreateEpiAsync(Epi epi)
        {
            var json = JsonSerializer.Serialize(epi, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("api/epis", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEpiAsync(int id, Epi epi)
        {
            var json = JsonSerializer.Serialize(epi, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync($"api/epis/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEpiAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/epis/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<DashboardViewModel?> GetDashboardMetricsAsync()
        {
            var response = await _httpClient.GetAsync("api/epis/dashboard");
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DashboardViewModel>(json, _jsonOptions);
            }
            
            return null;
        }
    }
}

