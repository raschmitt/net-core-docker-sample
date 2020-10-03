using System;
using Domain.Entities;
using Tests.Builder.Dtos.Item;

namespace Tests.Builder.Entities
{
    public class ItemBuilder
    {
        private Guid _id;
        private string _description = "Bacon";
        private double _price = 10.50;

        public Item Build()
        {
            var itemRequest = new ItemRequestBuilder()
                .WithDescription(_description)
                .WithPrice(_price)
                .Build();

            var item = new Item(itemRequest);
            
            return item;
        }

        public ItemBuilder WithDescription(string value)
        {
            _description = value;
            return this;
        }
        
        public ItemBuilder WithPrice(double value)
        {
            _price = value;
            return this;
        }
    }
}