using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DbSet<Item> _dbSet;
        private readonly Context _context;

        public ItemRepository(Context context)
        {
            _dbSet = context.Set<Item>();
            _context = context;
        }

        public async Task Add(Item item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _dbSet.Where(i => i.Active).ToListAsync();
        }     
        
        public async Task<Item> GetById(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(i => i.Id == id);
        }
        
        public async Task Update(Item item)
        {
            _dbSet.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}