using System.Linq;
using Domain.Entities;

namespace Infra.Data.Seed
{
    public static class ItemsSeed
    {
        public static void AddItems(this Context dbContext)
        {
            var items = dbContext.Set<Item>();

            if (items.Any())
                return;

            // var itemsFaker = new Faker<ItemRequest>()
            //     .RuleFor(i => i.Description, f => f.Commerce.Product())
            //     .RuleFor(i => i.Price, f => f.Random.Double(1.0, 25.0));
            //
            // var seedItems = itemsFaker
            //     .Generate(10)
            //     .Select(x => new Item(x));
            //
            // dbContext.AddRange(seedItems);
        }
    }
}