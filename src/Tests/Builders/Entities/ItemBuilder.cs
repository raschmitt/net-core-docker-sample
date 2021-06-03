using AutoMapper;
using Domain.Entities;
using Domain.Mappers;
using Tests.Builders.Dtos;

namespace Tests.Builders.Entities
{
    public class ItemBuilder
    {
        private IMapper _mapper;

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
    }
}