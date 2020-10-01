using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly Context _dbContext;

        public ItemRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _dbContext.Set<Item>()
                .ToListAsync();
        }     
        
        public async Task<Item> GetById(Guid id)
        {
            return await _dbContext.Set<Item>()
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}