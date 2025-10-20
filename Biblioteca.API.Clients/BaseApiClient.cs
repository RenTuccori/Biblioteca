using System.Net.Http.Headers;

namespace Biblioteca.API.Clients
{
    public abstract class BaseApiClient
    {
        private readonly HttpClient _httpClient;

        protected BaseApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async Task<HttpClient> CreateHttpClientAsync()
        {
            var token = await AuthServiceProvider.Instance.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return _httpClient;
        }

        protected async Task EnsureAuthenticatedAsync()
        {
            var ok = await AuthServiceProvider.Instance.IsAuthenticatedAsync();
            if (!ok)
                throw new UnauthorizedAccessException("No autenticado");
        }

        protected async Task<bool> HandleUnauthorizedResponseAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await AuthServiceProvider.Instance.LogoutAsync();
                return true;
            }
            return false;
        }

        // Helpers centralizados
        protected async Task<HttpResponseMessage> GetAsync(string url)
        {
            var client = await CreateHttpClientAsync();
            var res = await client.GetAsync(url);
            if (await HandleUnauthorizedResponseAsync(res)) throw new UnauthorizedAccessException();
            return res;
        }

        protected async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            var client = await CreateHttpClientAsync();
            var res = await client.PostAsync(url, content);
            if (await HandleUnauthorizedResponseAsync(res)) throw new UnauthorizedAccessException();
            return res;
        }

        protected async Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            var client = await CreateHttpClientAsync();
            var res = await client.PutAsync(url, content);
            if (await HandleUnauthorizedResponseAsync(res)) throw new UnauthorizedAccessException();
            return res;
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var client = await CreateHttpClientAsync();
            var res = await client.DeleteAsync(url);
            if (await HandleUnauthorizedResponseAsync(res)) throw new UnauthorizedAccessException();
            return res;
        }
    }
}
