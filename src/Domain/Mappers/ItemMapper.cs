using AutoMapper;
using Domain.Dtos.ItemDtos;
using Domain.Entities;

namespace Domain.Mappers
{
    public class ItemMapper : Profile
    {
        public ItemMapper()
        {
            CreateMap<ItemRequest, Item>();
            CreateMap<Item, ItemResponse>();
        }
    }
}