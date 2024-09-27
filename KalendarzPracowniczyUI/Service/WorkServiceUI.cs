using KalendarzPracowniczyApplication.CQRS.Commands.Works.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Works.Update;
using KalendarzPracowniczyApplication.Dto;

namespace KalendarzPracowniczyUI.Service
{
    public class WorkServiceUI
    {
        private readonly HttpClient _httpClient;

        public WorkServiceUI(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task Create(CreateWorkCommand createWorkCommand)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/Worker", createWorkCommand);
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
                var response = await _httpClient.DeleteAsync($"https://localhost:7164/api/Work/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas usuwania zadania {ex.Message}");
            }
        }

        public async Task Update(UpdateWorkCommand updateWorkCommand)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7164/api/Work/", updateWorkCommand);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas aktualizacji zadania {ex.Message}");
            }
        }

        public async Task GetUserTaskById(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7164/api/Work/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas pobierania numeru ID zadania {ex.Message}");
            }
        }

        public async Task<List<WorkDto>> GetUserAllTasks()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<WorkDto>>($"https://localhost:7164/api/Work/");
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