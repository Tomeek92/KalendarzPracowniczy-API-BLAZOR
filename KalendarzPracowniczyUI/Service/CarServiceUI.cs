using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Update;
using KalendarzPracowniczyApplication.Dto;
using Newtonsoft.Json;

namespace KalendarzPracowniczyUI.Service
{
    public class CarServiceUI
    {
        private readonly HttpClient _httpClient;

        public CarServiceUI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(CarDto carCommand)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/Car", carCommand);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7164/api/Worker/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(UpdateCarCommand workerCommand)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7164/api/Worker/", workerCommand);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<CarDto>> GetAll()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7164/api/Car/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var allEvents = JsonConvert.DeserializeObject<IEnumerable<CarDto>>(content);

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

        public async Task<CarDto> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7164/api/Worker/{id}");
            if (response.IsSuccessStatusCode)
            {
                var workerDto = await response.Content.ReadFromJsonAsync<CarDto>();
                if (workerDto != null)
                {
                    return workerDto;
                }
                else
                {
                    throw new Exception($"Nie naleziono zdarzenia z numerem {id}");
                }
            }
            else
            {
                throw new Exception($"Nie można znaleźć pracownika z numerem {id}");
            }
        }
    }
}