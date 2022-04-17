using System.Net.Http.Json;
using WebStore.Interfaces;
using WebStore.Interfaces.TestAPI;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Values
{
    public class ValuesClient : BaseClient, IValuesService
    {
        public ValuesClient(HttpClient client) : base(client, WebAPIAddresses.V1.Values) { }
        public void Add(string value)
        {
            var response = Client.PostAsJsonAsync(Address, value).Result;
            response.EnsureSuccessStatusCode();
        }

        public int Count()
        {
            var response = Client.GetAsync($"{Address}/count").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<int>().Result!;

            return -1;
        }

        public bool Delete(int id)
        {
            var response = Client.DeleteAsync($"{Address}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public void Edit(int id, string value)
        {
            var response = Client.PutAsJsonAsync($"{Address}/{id}", value).Result;
            response.EnsureSuccessStatusCode();
        }

        public string? GetById(int id)
        {
            var response = Client.GetAsync($"{Address}/{id}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<string>().Result!;

            return null;
        }

        public IEnumerable<string> GetValues()
        {
            var response = Client.GetAsync(Address).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result!;

            return Enumerable.Empty<string>();
        }
    }
}
