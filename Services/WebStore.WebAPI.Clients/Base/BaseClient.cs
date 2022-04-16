using System.Net;
using System.Net.Http.Json;

namespace WebStore.WebAPI.Clients.Base
{
    public abstract class BaseClient
    {
        protected HttpClient Client { get; }
        protected string Address { get; }

        protected BaseClient(HttpClient client, string address)
        {
            Client = client;
            Address = address;
        }

        protected async Task<T?> GetAsync<T>(string url)
        {
            var response = await Client.GetAsync(url).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                case HttpStatusCode.NotFound:
                    return default;
            }

            var result = await response
                .Content
                .ReadFromJsonAsync<T>()
                .ConfigureAwait(false);

            return result;
        } 
        protected T? Get<T>(string url) => GetAsync<T>(url).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value)
        {
            var response = await Client.PostAsJsonAsync(url, value).ConfigureAwait(false);

            return response.EnsureSuccessStatusCode();
        }
        protected HttpResponseMessage Post<T>(string url, T value) => PostAsync<T>(url, value).Result;


        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value)
        {
            var response = await Client.PutAsJsonAsync(url, value).ConfigureAwait(false);

            return response.EnsureSuccessStatusCode();
        }
        protected HttpResponseMessage Put<T>(string url, T value) => PutAsync<T>(url, value).Result;


        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var response = await Client.DeleteAsync(url).ConfigureAwait(false);

            return response;
        }
        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

    }
}
