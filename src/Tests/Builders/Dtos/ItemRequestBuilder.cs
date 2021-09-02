using Domain.Dtos.ItemDtos;

namespace Tests.Builders.Dtos
{
    public class ItemRequestBuilder
    {
        private string _description = "Bacon";
        private double _price = 10.90;
        
        public ItemRequest Build()
        {
            var itemRequest = new ItemRequest
            {
                Description = _description,
                Price = _price
            };
            
            return itemRequest;
        }
        
        public ItemRequestBuilder WithPrice(double value)
        {
            _price = value;
            return this;
        }           
        
        public ItemRequestBuilder WithDescription(string value)
        {
            _description = value;
            return this;
        }   
    }
}