using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.ItemDtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        
        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<ItemResponse> Add(ItemRequest itemRequest)
        {
            var item = _mapper.Map<Item>(itemRequest);

            await _itemRepository.Add(item);

            var itemResponse = _mapper.Map<ItemResponse>(item);
            
            return itemResponse;
        }
        
        public async Task Delete(Guid id)
        {
            var item = await _itemRepository.GetById(id);
            
            item.Inactivate();
            
            await _itemRepository.Update(item);
        }
        
        public async Task<List<ItemResponse>> GetAll()
        {
            var items = await _itemRepository.GetAll();
            
            return items.Select(_mapper.Map<ItemResponse>).ToList();
        }
        
        public async Task<ItemResponse> GetById(Guid id)
        {
            var item = await _itemRepository.GetById(id);
            
            var itemResponse = _mapper.Map<ItemResponse>(item);

            return itemResponse;
        }
        
        public async Task<ItemResponse> Update(Guid id, ItemRequest itemRequest)
        {
            var item = await _itemRepository.GetById(id);
            
            item.Update(itemRequest);
            
            await _itemRepository.Update(item);
            
            var itemResponse = _mapper.Map<ItemResponse>(item);

            return itemResponse;
        }
    }
}