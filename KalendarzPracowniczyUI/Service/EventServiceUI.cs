using KalendarzPracowniczyApplication.CQRS.Commands.Events.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Update;
using KalendarzPracowniczyApplication.Dto;

namespace KalendarzPracowniczyUI.Service
{
    public class EventServiceUI
    {
        private readonly HttpClient _httpClient;

        public EventServiceUI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(CreateEventCommand command)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/Event", command);
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

        public async Task<IEnumerable<EventDto>> GetAll()
        {
            var allEvents = await _httpClient.GetFromJsonAsync<IEnumerable<EventDto>>($"https://localhost:7164/api/Event");
            if (allEvents != null)
            {
                return allEvents;
            }
            else
            {
                throw new Exception($"Brak Zadań");
            }
        }

        public async Task Update(UpdateEventCommand command)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Event", command);
            response.EnsureSuccessStatusCode();
        }
    }
}