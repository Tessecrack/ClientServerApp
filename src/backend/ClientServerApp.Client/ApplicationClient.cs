using System.Net.Http.Json;

using ClientServerApp.Domain;

namespace ClientServerApp.Client
{
    public class ApplicationClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public ApplicationClient(string connectionString)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri($"{connectionString}/");
        }

        public string BaseAddress => _httpClient.BaseAddress!.ToString();

        public async Task<IEnumerable<UserModel>> GetUsers(CancellationToken cancel = default) 
        {
            var response = await _httpClient.GetAsync("api/users").ConfigureAwait(false);

            var listUsers = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<List<UserModel>>(cancel)
                .ConfigureAwait(false);
            if (listUsers == null)
            {
                return [];
            }
            return listUsers;
        }

        public async Task<UserModel> GetUser(string id, CancellationToken cancel = default)
        {
            var repsonse = await _httpClient.GetAsync($"api/users/{id}").ConfigureAwait(false);

            var user = await repsonse
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<UserModel>(cancel)
                .ConfigureAwait(false);

            return user;
        }

        public async Task<UserModel> AddUser(UserModel userModel, CancellationToken cancel = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/users", userModel, cancel).ConfigureAwait(false);

            var user = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<UserModel>(cancel)
                .ConfigureAwait(false);

            return user;
        }

        public async Task<UserModel> EditUser(UserModel userModel, CancellationToken cancel = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/users", userModel, cancel).ConfigureAwait(false);

            var user = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<UserModel>(cancel)
                .ConfigureAwait(false);

            return user;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/users/{id}").ConfigureAwait(false);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}