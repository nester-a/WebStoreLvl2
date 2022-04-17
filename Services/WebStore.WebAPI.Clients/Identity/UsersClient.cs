using WebStore.Interfaces;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Identity;

public class UsersClient : BaseClient
{
    public UsersClient(HttpClient client) : base(client, WebAPIAddresses.V1.Identity.Users)
    {
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}
