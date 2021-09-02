using System.Threading.Tasks;
using Api.Controllers.v2;
using Domain.Entities;
using Domain.Interfaces.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Api.Controllers.v2
{
    public class ItemsControllerTests
    {
        private readonly IItemService _itemService;
        private readonly ItemsController _itemsController;
        
        public ItemsControllerTests()
        {
            _itemService = Substitute.For<IItemService>();
            _itemsController = new ItemsController(_itemService);
        }
        
        [Fact]
        public async Task Should_get_all_items_ordered()
        {
            // Arrange
            var propertyName = nameof(Item.Price);
            
            //Act
            var result = await _itemsController.GetAllOrdered(propertyName);

            //Assert
            await _itemService.Received(1).GetAllOrdered(propertyName);
            
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}