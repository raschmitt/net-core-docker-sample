using System;
using Domain.Entities;

namespace Domain.Dtos.ItemDtos
{
    public class ItemResponse : ItemBase
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }

        public ItemResponse() { }
        
        public ItemResponse(Item item)
        {
            Id = item.Id;
            Description = item.Description;
            Price = item.Price;
            Active = item.Active;
        }
    }
}