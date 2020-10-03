using System;
using System.Threading.Tasks;
using Api.Controllers;
using Domain.Dtos.ItemDtos;
using Domain.Interfaces.Services;
using NSubstitute;
using Tests.Builder.Dtos.Item;
using Xunit;

namespace Tests.Unit.Api.Controllers
{
    public class ItemsControllerTests
    {
        private readonly IItemService _itemService;
        private readonly ItemsController _itemsController;

        private readonly ItemRequest _itemRequest;
        private readonly Guid _itemId;
        
        public ItemsControllerTests()
        {
            _itemService = Substitute.For<IItemService>();
            _itemsController = new ItemsController(_itemService);
            
            _itemRequest = new ItemRequestBuilder().Build();
            _itemId = Guid.NewGuid();
        }
        
        [Fact]
        public async Task Should_add_an_item()
        {
            //Act
            await _itemsController.Add(_itemRequest);

            //Assert
            await _itemService.Received(1).Add(_itemRequest);
        }        
        
        [Fact]
        public async Task Should_delete_an_item()
        {
            //Act
            await _itemsController.Delete(_itemId);

            //Assert
            await _itemService.Received(1).Delete(_itemId);
        }   
        
        [Fact]
        public async Task Should_get_all_items()
        {
            //Act
            await _itemsController.GetAll();

            //Assert
            await _itemService.Received(1).GetAll();
        }     
        
        [Fact]
        public async Task Should_get_an_item_by_id()
        {
            //Act
            await _itemsController.GetById(_itemId);

            //Assert
            await _itemService.Received(1).GetById(_itemId);
        }    
        
        [Fact]
        public async Task Should_update_an_item()
        {
            //Act
            await _itemsController.Update(_itemId, _itemRequest);

            //Assert
            await _itemService.Received(1).Update(_itemId, _itemRequest);
        }
    }
}