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
    }
}
