using System.Net.Http;
using System.Threading.Tasks;
using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Tests.Integration.Api
{
    public abstract class BaseTestClient
    {
        protected readonly HttpClient Client;
        protected readonly CustomWebApplicationFactory<Startup> Factory;

        protected readonly string ControllerUri;

        protected BaseTestClient(string controllerUri)
        {
            ControllerUri = controllerUri;

            Factory = new CustomWebApplicationFactory<Startup>();
            Client = Factory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        protected async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}