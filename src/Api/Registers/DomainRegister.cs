using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Registers
{
    public static class DomainRegister
    {
        public static void AddDomain(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IItemService, ItemService>();
        }
    }
}