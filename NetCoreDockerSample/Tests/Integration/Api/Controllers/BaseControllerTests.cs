using System.Net.Http;
using System.Threading.Tasks;
using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Tests.Integration.Api.Controllers
{
    public abstract class BaseControllerTests
    {
        protected readonly HttpClient Client;
        protected readonly CustomWebApplicationFactory<Startup> Factory;

        protected readonly string ControllerUri;

        public BaseControllerTests(string controllerUri)
        {
            ControllerUri = controllerUri;

            Factory = new CustomWebApplicationFactory<Startup>();
            Client = Factory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        public async Task<T> DescerializeResponse<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}