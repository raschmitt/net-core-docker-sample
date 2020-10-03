using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IItemRepository
    {
        Task Add(Item item);
        Task<IEnumerable<Item>> GetAll();
        Task<Item> GetById(Guid id);
        Task Update(Item item);
    }
}