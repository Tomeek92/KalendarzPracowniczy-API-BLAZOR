using KalendarzPracowniczyApplication.Dto;

namespace KalendarzPracowniczyUI.Service
{
    public class DayOffServiceUI
    {
        private readonly HttpClient _httpClient;

        public DayOffServiceUI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(DayOffDto dayOffDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/DayOff", dayOffDto);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, content: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Nieoczekiwany błąd {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7164/api/DayOff/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, content: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Nieoczekiwany błąd {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }
    }
}