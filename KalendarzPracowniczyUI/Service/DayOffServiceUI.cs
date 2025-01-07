using KalendarzPracowniczyApplication.Dto;
using Newtonsoft.Json;

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
        public async Task<List<DayOffDto>> GetElementById(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7164/api/DayOff/{userId}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Odpowiedź serwera: {content}");
                if (response.IsSuccessStatusCode)
                {
                    var dayOffDto = await response.Content.ReadFromJsonAsync<List<DayOffDto>>();

                    if (dayOffDto != null)
                    {
                        return dayOffDto;
                    }
                    else
                    {
                        throw new Exception($"Nie znaleziono zdarzenia z numerem {userId}");
                    }
                }
                else
                {
                    throw new Exception($"Nie można znaleźć zdarzenia z ID {userId}. Status Code: {response.StatusCode} {response}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }
        public async Task<List<DayOffDto>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7164/api/DayOff/GetAllDaysOff");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var dayOff = JsonConvert.DeserializeObject<List<DayOffDto>>(content);

                    if (dayOff == null)
                    {
                        throw new Exception($"Błąd podczas deserializacji");
                    }
                    return dayOff;
                }
                else
                {
                    throw new Exception("Błąd podczas pobierania danych: " + response.StatusCode);
                }
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