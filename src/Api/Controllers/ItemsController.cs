using System;
using System.Threading.Tasks;
using Domain.Dtos.ItemDtos;
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
        
        /// <summary>
        /// Adds an item
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ItemRequest itemRequest)
        {
            var result = await _itemService.Add(itemRequest);
            
            return Created($"/{result.Id}", result);
        }
        
        /// <summary>
        /// Deletes a specific item
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _itemService.Delete(id);

            return NoContent();
        }
    
        /// <summary>
        /// Gets all items
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _itemService.GetAll();
            
            return Ok(result);
        }        
        
        /// <summary>
        /// Gets a specific item
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _itemService.GetById(id);
            
            return Ok(result);
        }        
        
        /// <summary>
        /// Updates a specific item
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ItemRequest itemRequest)
        {
            var result = await _itemService.Update(id, itemRequest);
            
            return Ok(result);
        } 
    }
}