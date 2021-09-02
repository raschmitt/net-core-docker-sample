using System.Threading.Tasks;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }
        
        /// <summary>
        /// Gets all items, ordered by price
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllOrdered([FromQuery] string orderBy)
        {
            var result = await _itemService.GetAllOrdered(orderBy);
            
            return Ok(result);
        }
    }
}