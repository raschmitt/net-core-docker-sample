using System;
using Domain.Dtos.ItemDtos;

namespace Domain.Entities
{
    public class Item
    {
        public Guid Id { get; private set; } 
        public string Description { get; private set; } 
        public double Price { get; private set; } 
        public bool Active { get; private set; }

        private Item() { }
        
        public Item(ItemRequest itemRequest)
        {
            Id = Guid.NewGuid();
            Description = itemRequest.Description;
            Price = itemRequest.Price;
            Active = true;
        }

        public void Update(ItemRequest itemRequest)
        {
            Description = itemRequest.Description;
            Price = itemRequest.Price;
        }

        public void Inactivate()
        {
            Active = false;
        }
    }
}