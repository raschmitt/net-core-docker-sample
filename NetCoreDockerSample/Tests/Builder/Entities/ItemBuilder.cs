using Domain.Entities;
using Tests.Builder.Dtos.Item;

namespace Tests.Builder.Entities
{
    public class ItemBuilder
    {
        private string _description = "Bacon";
        private double _price = 10.50;
        private bool _active = true;

        public Item Build()
        {
            var itemRequest = new ItemRequestBuilder()
                .WithDescription(_description)
                .WithPrice(_price)
                .Build();

            var item = new Item(itemRequest);

            if (!_active)
            {
                item.Inactivate();
            }

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

        public ItemBuilder Inactive()
        {
            _active = false;
            return this;
        }
    }
}