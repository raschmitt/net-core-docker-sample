using Bogus;
using Domain.Dtos.ItemDtos;

namespace Tests.Builders.Dtos
{
    public class ItemResponseBuilder
    {
        public ItemResponse Build()
        {
            var itemResponse = new Faker<ItemResponse>()
                .StrictMode(true)
                .RuleFor(i => i.Description, f => f.Commerce.Product())
                .RuleFor(i => i.Price, f => f.Random.Double(1.0, 25.0))
                .RuleFor(i => i.Active, true)
                .RuleFor(i => i.Id, f => f.Random.Guid())
                .Generate();

            return itemResponse;
        }
    }
}