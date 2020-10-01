using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Item;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }
        
        [HttpPost]
        public void Create()
        {

        }
        
        [HttpPost]
        public void Update()
        {

        }     
        
        [HttpGet]
        public async Task<List<ItemResponse>> GetAll()
        {
            return await _itemService.GetAll();
        }        
        
        [HttpGet("{id}")]
        public async Task<ItemResponse> GetById([FromRoute] Guid id)
        {
            return await _itemService.GetById(id);
        }        
        
        [HttpDelete]
        public void Delete()
        {

        }
    }
}