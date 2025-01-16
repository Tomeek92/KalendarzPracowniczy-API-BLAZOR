using KalendarzPracowniczyApplication.Dto;

namespace KalendarzPracowniczyUI.Service
{
    public class WorkServiceUI
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public WorkServiceUI(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];
        }

        public async Task Create(WorkDto workDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Work/Create", workDto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas tworzenia zadania {ex.Message}");
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Work/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas usuwania zadania {ex.Message}");
            }
        }

        public async Task Update(WorkDto workDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Work/", workDto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas aktualizacji zadania {ex.Message}");
            }
        }

        public async Task<List<WorkDto>> GetUserTaskById(string userid)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Work/{userid}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Odpowiedź serwera: {content}");
                if (response.IsSuccessStatusCode)
                {
                    var workDto = await response.Content.ReadFromJsonAsync<List<WorkDto>>();
                    if (workDto != null)
                    {
                        return workDto;
                    }
                    else
                    {
                        throw new Exception($"Nie znaleziono zdarzenia z numerem {userid}");
                    }
                }
                else
                {
                    throw new Exception($"Nie można znaleźć zdarzenia z ID {userid}. Status Code: {response.StatusCode} {response}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas pobierania numeru ID zadania {ex.Message}");
            }
        }

        public async Task<List<WorkDto>> GetAllTasks()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<WorkDto>>($"{_baseUrl}/api/Work/");
                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new KeyNotFoundException($"Nie odnaleziono zadań dla tego użytkownika ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas pobierania zadań {ex.Message}");
            }
        }
    }
}