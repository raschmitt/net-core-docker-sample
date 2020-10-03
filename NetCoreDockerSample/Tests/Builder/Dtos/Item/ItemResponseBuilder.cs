using System;
using Domain.Dtos.ItemDtos;

namespace Tests.Builder.Dtos.Item
{
    public class ItemResponseBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _description = "Bacon";
        private double _price = 10.50;
        private bool _active = true;

        public ItemResponse Build()
        {
            var itemResponse = new ItemResponse
            {
                Id = _id,
                Description = _description,
                Price = _price,
                Active = _active,
            };

            return itemResponse;
        }
    }
}