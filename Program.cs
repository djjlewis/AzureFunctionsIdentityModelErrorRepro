using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzureFunctionsWithIdentityModelErrorRepro
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    // Calling AddAccessTokenManagement causes function to fail
                    // This works in other asp.net core projects, webjobs and generic-host projects but not azure functions
                    
                    // source code for AddAccessTokenManagement: https://github.com/IdentityModel/IdentityModel.AspNetCore/blob/be8bd8dc9ce907dbc4b90a0af8d67b507f4cdd7b/src/AccessTokenManagement/TokenManagementServiceCollectionExtensions.cs
                    services.AddAccessTokenManagement(options =>
                    {
                        // client details are irrelevant but would contain configuration on how to connect to identity server
                        options.Client.Clients.Add("test",
                                                   new ClientCredentialsTokenRequest
                                                   {
                                                       Address = "https://localhost:8080",
                                                       ClientId = "test",
                                                       ClientSecret = "abc123"
                                                   });
                    });
                })
                .Build();

            host.Run();
        }
    }
}