using KalendarzPracowniczyApplication.CQRS.Commands.Users.Create;
using KalendarzPracowniczyApplication.Dto;
using Newtonsoft.Json;

namespace KalendarzPracowniczyUI.Service
{
    public class UserServiceUI
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UserServiceUI(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];
        }

        public async Task LogoutAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/User/logout", null);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Wylogowywanie nie powiodło się.");
                }
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Problem z połączeniem HTTP: " + httpEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Nie udało się pobrać informacji o użytkownikach: " + ex.Message);
            }
        }

        public async Task<List<UserDto>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/User/getAll");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<UserDto>>(content);
                    return users;
                }
                else
                {
                    throw new Exception("Błąd podczas pobierania danych: " + response.StatusCode);
                }
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Problem z połączeniem HTTP: " + httpEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Nie udało się pobrać informacji o użytkownikach: " + ex.Message);
            }
        }

        public async Task<UserDto> GetCurrentUserAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/User/me");

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
                throw new Exception($"Nie udało się pobrać informacji o zalogowanym użytkowniku {ex.Message}");
            }
        }

        public async Task<LoginDto> Login(LoginDto loginUser)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/api/User/login")
                {
                    Content = JsonContent.Create(loginUser)
                };

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var userDto = System.Text.Json.JsonSerializer.Deserialize<LoginDto>(responseContent);
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
            catch (System.Text.Json.JsonException jsonEx)
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
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/api/User/Create")
                {
                    Content = JsonContent.Create(userCommand)
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Użytkownik utworzony pomyślnie.");
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Błąd podczas tworzenia użytkownika. StatusCode: {response.StatusCode}, Treść odpowiedzi: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                throw new Exception("Nieoczekiwany błąd", ex);
            }
        }

        public async Task Delete(string id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/User/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(UserDto userDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/User/UpdateUser", userDto);
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
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas update user {ex.Message}");
            }
        }

        public async Task<UserDto> GetById(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/User/{id}");
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