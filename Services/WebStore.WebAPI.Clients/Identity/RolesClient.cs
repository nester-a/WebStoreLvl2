using WebStore.Interfaces;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Identity;
public class RolesClient : BaseClient
{
    public RolesClient(HttpClient client) : base(client, WebAPIAddresses.V1.Identity.Roles)
    {
    }

}
