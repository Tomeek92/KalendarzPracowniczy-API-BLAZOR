using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Update;
using KalendarzPracowniczyApplication.Dto;
using Newtonsoft.Json;

namespace KalendarzPracowniczyUI.Service
{
    public class CarServiceUI
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CarServiceUI(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

        }

        public async Task Create(CarDto carCommand)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Car", carCommand);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Błąd: {response.StatusCode}, Szczegóły: {errorContent}");
                }
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Samochód już istnieje {ex.Message}");
            }
        }

        public async Task Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Car/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(CarDto carDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Car/", carDto);
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

        public async Task<List<CarDto>> GetAll()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Car/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var allEvents = JsonConvert.DeserializeObject<List<CarDto>>(content);

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
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Car/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var workerDto = await response.Content.ReadFromJsonAsync<CarDto>();
                    if (workerDto != null)
                    {
                        return workerDto;
                    }
                    else
                    {
                        throw new Exception($"Nie naleziono samochodu z numerem {id}");
                    }
                }
                else
                {
                    throw new Exception($"Nie można znaleźć samochodu z numerem {id}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd podczas pobierania samochodu {ex.Message}");
            }

        }
        public async Task<List<CarDto>> GetAvailableCarsAsync(DateTime selectedDate)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Car/available-cars?selectedDate={selectedDate:yyyy-MM-dd}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var allEvents = JsonConvert.DeserializeObject<List<CarDto>>(content);
                    return allEvents;
                }
                return new List<CarDto>();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}