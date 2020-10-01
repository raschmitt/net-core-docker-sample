using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Item;

namespace Domain.Interfaces.Services
{
    public interface IItemService
    {
        Task<List<ItemResponse>> GetAll();
        Task<ItemResponse> GetById(Guid id);
    }
}