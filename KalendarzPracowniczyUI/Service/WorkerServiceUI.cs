using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Update;
using KalendarzPracowniczyApplication.Dto;

namespace KalendarzPracowniczyUI.Service
{
    public class WorkerServiceUI
    {
        private readonly HttpClient _httpClient;

        public WorkerServiceUI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(CreateWorkerCommand workerCommand)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/Worker", workerCommand)
                response.EnsureSuccessStatusCode();
        }

        public async Task Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7164/api/Worker/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(UpdateWorkerCommand workerCommand)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Worker", workerCommand);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<WorkerDto>> GetAll()
        {
            var allWorkers = await _httpClient.GetFromJsonAsync<IEnumerable<WorkerDto>>("https://localhost:7164/api/Worker");
            if (allWorkers != null)
            {
                return allWorkers;
            }
            else
            {
                throw new Exception($"Brak pracowników");
            }
        }

        public async Task<WorkerDto> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7164/api/Worker/{id}");
            if (response.IsSuccessStatusCode)
            {
                var workerDto = await response.Content.ReadFromJsonAsync<WorkerDto>();
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