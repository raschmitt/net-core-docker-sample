using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos.Item;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<ItemResponse>> GetAll()
        {
            var items = await _itemRepository.GetAll();
            
            return items.Select(i => new ItemResponse(i)).ToList();
        }
        
        public async Task<ItemResponse> GetById(Guid id)
        {
            var item = await _itemRepository.GetById(id);
            
            return new ItemResponse(item);
        }
    }
}