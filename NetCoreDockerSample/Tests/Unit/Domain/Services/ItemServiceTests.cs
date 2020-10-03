using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos.ItemDtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using FluentAssertions;
using NSubstitute;
using Tests.Builder.Dtos.Item;
using Tests.Builder.Entities;
using Xunit;

namespace Tests.Unit.Domain.Services
{
    public class ItemServiceTests
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemService _itemService;

        private readonly ItemRequest _itemRequest;
        private readonly Item _item;
        
        public ItemServiceTests()
        {
            _itemRepository = Substitute.For<IItemRepository>();
            _itemService = new ItemService(_itemRepository);
            
            _itemRequest = new ItemRequestBuilder().Build();
            _item = new ItemBuilder().Build();
        }
        
        [Fact]
        public async Task Should_add_an_item()
        {
            //Act
            var itemResponse = await _itemService.Add(_itemRequest);

            //Assert
            itemResponse.Id.Should().NotBeEmpty();
            itemResponse.Description.Should().Be(_itemRequest.Description);
            itemResponse.Price.Should().Be(_itemRequest.Price);
            itemResponse.Active.Should().BeTrue();
            
            await _itemRepository.Received(1).Add(Arg.Is<Item>(i => 
                i.Description == _itemRequest.Description &&
                i.Price == _itemRequest.Price &&
                i.Active
                ));
        }        
        
        [Fact]
        public async Task Should_delete_an_item()
        {
            //Arrange
            _itemRepository.GetById(_item.Id).Returns(_item);
            
            //Act
            await _itemService.Delete(_item.Id);
        
            //Assert
            _item.Active.Should().BeFalse();
            
            await _itemRepository.Received(1).Update(_item);
        }   
        
        [Fact]
        public async Task Should_get_all_items()
        {
            //Arrange
            var items = new List<Item>{ _item };

            _itemRepository.GetAll().Returns(items);
            
            //Act
            var itemsResponse = await _itemService.GetAll();
        
            //Assert
            itemsResponse.Should().HaveCount(items.Count);

            itemsResponse.First().Id.Should().Be(_item.Id);
            itemsResponse.First().Description.Should().Be(_item.Description);
            itemsResponse.First().Price.Should().Be(_item.Price);
            itemsResponse.First().Active.Should().Be(_item.Active);
            
            await _itemRepository.Received(1).GetAll();
        }     
        
        [Fact]
        public async Task Should_get_an_item_by_id()
        {
            //Arrange
            _itemRepository.GetById(_item.Id).Returns(_item);
            
            //Act
            var itemResponse = await _itemService.GetById(_item.Id);
        
            //Assert
            itemResponse.Id.Should().Be(_item.Id);
            itemResponse.Description.Should().Be(_itemRequest.Description);
            itemResponse.Price.Should().Be(_itemRequest.Price);
            itemResponse.Active.Should().BeTrue();
            
            await _itemRepository.Received(1).GetById(_item.Id);
        }    
        
        [Fact]
        public async Task Should_update_an_item()
        {
            //Arrange
            var itemRequest = new ItemRequestBuilder()
                .WithDescription("Banana")
                .WithPrice(5.75)
                .Build();
            
            _itemRepository.GetById(_item.Id).Returns(_item);
            
            //Act
            var itemResponse = await _itemService.Update(_item.Id, itemRequest);
        
            //Assert
            itemResponse.Id.Should().Be(_item.Id);
            itemResponse.Description.Should().Be(itemRequest.Description);
            itemResponse.Price.Should().Be(itemRequest.Price);
            itemResponse.Active.Should().BeTrue();
            
            await _itemRepository.Received(1).Update(_item);
        }
    }
}