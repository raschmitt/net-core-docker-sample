using Domain.Dtos.ItemDtos;

namespace Tests.Builder.Dtos.Item
{
    public class ItemRequestBuilder
    {
        private string _description = "Bacon";
        private double _price = 10.50;

        public ItemRequest Build()
        {
            var itemRequest = new ItemRequest
            {
                Description = _description,
                Price = _price,
            };

            return itemRequest;
        }

        public ItemRequestBuilder WithDescription(string value)
        {
            _description = value;
            return this;
        }
        
        public ItemRequestBuilder WithPrice(double value)
        {
            _price = value;
            return this;
        }
    }
}