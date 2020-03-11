using System.Collections.Generic;
using IdentityServer4.Models;

namespace TeamX.Sercurity.AuthServer
{
    public class Config
    {
        // clients that are allowed to acess resources from the Auth se
        public static IEnumerable<Client> GetClients()
        {
            // clients credentials, list of clients
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // Clients secrets
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "apil" }
                },
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("apil", "My API")
            };
        }
    }
}
