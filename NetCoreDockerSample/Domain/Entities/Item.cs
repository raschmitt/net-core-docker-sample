using System;
using Domain.Dtos.Item;

namespace Domain.Entities
{
    public class Item
    {
        public Guid Id { get; private set; } 
        public string Description { get; private set; } 
        public double Price { get; private set; } 
        public bool Active { get; private set; }

        public Item(ItemRequest itemRequest)
        {
            Id = Guid.NewGuid();
            Description = itemRequest.Description;
            Price = itemRequest.Price;
            Active = true;
        }

        public Item(string description, double price)
        {
            Id = Guid.NewGuid();
            Description = description;
            Price = price;
            Active = true;
        }
        
        public void InactivateItem()
        {
            Active = false;
        }
    }
}