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

        public UserServiceUI(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task LogoutAsync()
        {
            var response = await _httpClient.PostAsync("/api/User/logout", null);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Wylogowywanie nie powiodło się.");
            }
        }

        public async Task<UserDto> GetCurrentUserAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/User/me");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDto>();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Nie udało się pobrać informacji o zalogowanym użytkowniku.");
            }
        }

        public async Task<UserDto> Login(LoginQuery loginCommand)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7164/api/User/login")
                {
                    Content = JsonContent.Create(loginCommand)
                };

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var userDto = JsonSerializer.Deserialize<UserDto>(responseContent);
                    if (userDto == null)
                    {
                        throw new Exception("Odpowiedź nie zawiera poprawnych danych JSON.");
                    }

                    return userDto;
                }
                else
                {
                    throw new Exception($"Błąd przy logowaniu: {response.StatusCode}, Treść odpowiedzi: {responseContent}");
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