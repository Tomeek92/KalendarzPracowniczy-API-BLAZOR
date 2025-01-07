using KalendarzPracowniczyApplication.Dto;
using Newtonsoft.Json;

namespace KalendarzPracowniczyUI.Service
{
    public class EventServiceUI
    {
        private readonly HttpClient _httpClient;

        public EventServiceUI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(EventDto eventDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/Event/Create", eventDto);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Błąd: {response.StatusCode}, Szczegóły: {errorContent}");
                }

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd {ex.Message}");
            }
        }

        public async Task Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7164/api/Event/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<EventDto> GetById(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7164/api/Event/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var eventDto = await response.Content.ReadFromJsonAsync<EventDto>();
                    if (eventDto != null)
                    {
                        return eventDto;
                    }
                    else
                    {
                        throw new Exception($"Nie znaleziono zdarzenia z numerem {id}");
                    }
                }
                else
                {
                    throw new Exception($"Nie można znaleźć zdarzenia z ID {id}. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }

        public async Task<List<EventDto>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7164/api/Event/GetAll");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    var allEvents = JsonConvert.DeserializeObject<List<EventDto>>(content);

                    if (allEvents == null)
                    {
                        throw new Exception("Nie udało się zdeserializować odpowiedzi z API.");
                    }

                    return allEvents;
                }
                else
                {
                    throw new Exception($"Błąd podczas pobierania danych: {response.StatusCode}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Problem z połączeniem HTTP: {httpEx.Message}");
            }
            catch (Newtonsoft.Json.JsonException jsonEx)
            {
                throw new Exception($"Błąd podczas deserializacji odpowiedzi: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas pobierania wydarzeń: {ex.Message}");
            }
        }

        public async Task Update(EventDto eventDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7164/api/Event/Update", eventDto);
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Błąd API: {response.StatusCode}, Treść odpowiedzi: {responseContent}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Problem z połączeniem HTTP: {httpEx.Message}");
            }
        }
    }
}