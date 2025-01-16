using KalendarzPracowniczyApplication.Dto;
using Newtonsoft.Json;

namespace KalendarzPracowniczyUI.Service
{
    public class DayOffServiceUI
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public DayOffServiceUI(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];
        }

        public async Task Create(DayOffDto dayOffDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/DayOff/Create", dayOffDto);
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
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/DayOff/{userId}");
                var content = await response.Content.ReadAsStringAsync();

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
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/DayOff/GetAllDaysOff");
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
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/DayOff/{id}");
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
        public async Task Update(DayOffDto updateDayOff)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/DayOff/", updateDayOff);
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