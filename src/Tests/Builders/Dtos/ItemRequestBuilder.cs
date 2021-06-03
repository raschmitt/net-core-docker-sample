using Bogus;
using Domain.Dtos.ItemDtos;

namespace Tests.Builders.Dtos
{
    public class ItemRequestBuilder
    {
        public ItemRequest Build()
        {
            var itemRequest = new Faker<ItemRequest>()
                .StrictMode(true)
                .RuleFor(i => i.Description, f => f.Commerce.Product())
                .RuleFor(i => i.Price, f => f.Random.Double(1.0, 25.0))
                .Generate();

            return itemRequest;
        }
    }
}