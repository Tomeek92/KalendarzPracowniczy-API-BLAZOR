using KalendarzPracowniczyApplication.CQRS.Commands.Users.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Login;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Update;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Users;
using System.Text.Json;

namespace KalendarzPracowniczyUI.Service
{
    public class UserServiceUI
    {
        private readonly HttpClient _httpClient;

        public UserServiceUI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto> Login(LoginCommand userCommand)
        {
            try
            {

                var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/User/login", userCommand);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var userDto = JsonSerializer.Deserialize<UserDto>(responseContent);

                    if (userDto == null)
                    {
                        throw new Exception("Odpowiedź nie zawiera poprawnych danych JSON.");
                    }

                    return userDto;
                }
                else
                {
                    throw new Exception($"Błąd przy logowaniu: {response.StatusCode}, Treść odpowiedzi: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas logowania: {ex.Message}");
            }
        }

        public async Task Create(CreateUserCommand userCommand)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/User", userCommand);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                throw new Exception("Nieoczekiwany błąd");
            }
        }

        public async Task Delete(string id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7164/api/User/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(UpdateUserCommand userCommand)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/User", userCommand);
            response.EnsureSuccessStatusCode();
        }

        public async Task<UserDto> GetById(string id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7164/api/Event/{id}");
            if (response.IsSuccessStatusCode)
            {
                var userDto = await response.Content.ReadFromJsonAsync<UserDto>();
                if (userDto != null)
                {
                    return userDto;
                }
                else
                {
                    throw new Exception($"Nie odnaleziono użytkownika z tym {id}");
                }
            }
            else
            {
                throw new Exception($"Nie odnaleziono zdarzenia z tym użytkownikiem id = {id}");
            }
        }
    }
}