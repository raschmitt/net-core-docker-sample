using AutoMapper;
using Domain.Entities;
using Domain.Mappers;
using Tests.Builders.Dtos;

namespace Tests.Builders.Entities
{
    public class ItemBuilder
    {
        private IMapper _mapper;

        private string _description = "Bacon";
        private double _price = 10.90;
        private bool _active = true;
        
        public ItemBuilder()
        {
            _mapper = new Mapper(new MapperConfiguration(
                cfg => cfg.AddProfile<ItemMapper>())
            );
        }

        public Item Build()
        {
            var itemRequest = new ItemRequestBuilder()
                .WithPrice(_price)
                .WithDescription(_description)
                .Build();

            var item = _mapper.Map<Item>(itemRequest);

            if (_active is false)
                item.Inactivate();

            return item;
        }

        public ItemBuilder WithInactiveStatus()
        {
            _active = false;
            return this;
        }
        
        public ItemBuilder WithPrice(double value)
        {
            _price = value;
            return this;
        }   
        
        public ItemBuilder WithDescription(string value)
        {
            _description = value;
            return this;
        }
    }
}