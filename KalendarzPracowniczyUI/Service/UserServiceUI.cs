using KalendarzPracowniczyApplication.CQRS.Commands.Users.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Update;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.Login;
using KalendarzPracowniczyApplication.Dto;
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

        public async Task<UserDto> Login(LoginQuery loginCommand)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7164/api/User/login", loginCommand);
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Status odpowiedzi: {response.StatusCode}");
                Console.WriteLine($"Treść odpowiedzi: {responseContent}");
                if (response.IsSuccessStatusCode)
                {
                    var userDto = JsonSerializer.Deserialize<UserDto>(responseContent);
                    Console.WriteLine($"Zalogowany użytkownik: {userDto?.Email}");
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
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Błąd deserializacji JSON: {jsonEx.Message}");
                throw;
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