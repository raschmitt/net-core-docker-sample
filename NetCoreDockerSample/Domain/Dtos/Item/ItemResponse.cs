using System;

namespace Domain.Dtos.Item
{
    public class ItemResponse : ItemBase
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }

        public ItemResponse(Entities.Item item)
        {
            Id = item.Id;
            Description = item.Description;
            Price = item.Price;
            Active = item.Active;
        }
    }
}