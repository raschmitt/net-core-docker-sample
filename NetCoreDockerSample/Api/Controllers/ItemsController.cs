using System;
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
        public async Task<IActionResult> Add([FromBody] ItemRequest itemRequest)
        {
            return Ok(await _itemService.Add(itemRequest));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _itemService.Delete(id);

            return NoContent();
        }
    
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _itemService.GetAll());
        }        
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await _itemService.GetById(id));
        }        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ItemRequest itemRequest)
        {
            return Ok(await _itemService.Update(id, itemRequest));
        } 
    }
}