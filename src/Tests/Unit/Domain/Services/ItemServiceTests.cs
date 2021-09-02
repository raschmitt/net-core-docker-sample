using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.ItemDtos;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Mappers;
using Domain.Services;
using FluentAssertions;
using NSubstitute;
using Tests.Builders.Dtos;
using Tests.Builders.Entities;
using Xunit;

namespace Tests.Unit.Domain.Services
{
    public class ItemServiceTests
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemService _itemService;

        private readonly ItemRequest _itemRequest;
        private readonly Item _itemA;
        private readonly Item _itemB;
        
        public ItemServiceTests()
        {
            _itemRepository = Substitute.For<IItemRepository>();
            
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ItemMapper>();
            })); 
            
            _itemService = new ItemService(_itemRepository, mapper);

            _itemRequest = new ItemRequestBuilder()
                .Build();

            _itemA = new ItemBuilder()
                .WithPrice(5.45)
                .Build();
            
            _itemB = new ItemBuilder()
                .WithPrice(7.50)
                .Build();
        }
        
        [Fact]
        public async Task Should_add_an_item()
        {
            //Act
            var itemResponse = await _itemService.Add(_itemRequest);

            //Assert
            itemResponse.Id.Should().BeEmpty();
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
            _itemRepository.GetById(_itemA.Id).Returns(_itemA);
            
            //Act
            await _itemService.Delete(_itemA.Id);
        
            //Assert
            _itemA.Active.Should().BeFalse();
            
            await _itemRepository.Received(1).Update(_itemA);
        }   
        
        [Fact]
        public async Task Should_get_all_items()
        {
            //Arrange
            var items = new List<Item>{ _itemA };

            _itemRepository.GetAll().Returns(items);
            
            //Act
            var itemsResponse = await _itemService.GetAll();
        
            //Assert
            itemsResponse.Should().HaveCount(items.Count);

            itemsResponse.First().Id.Should().Be(_itemA.Id);
            itemsResponse.First().Description.Should().Be(_itemA.Description);
            itemsResponse.First().Price.Should().Be(_itemA.Price);
            itemsResponse.First().Active.Should().Be(_itemA.Active);
            
            await _itemRepository.Received(1).GetAll();
        } 
        
        [Fact]
        public async Task Should_get_all_items_ordered()
        {
            //Arrange
            var propertyName = nameof(Item.Price);

            var items = new List<Item>{ _itemA, _itemB };

            _itemRepository.GetAll().Returns(items);
            
            //Act
            var itemsResponse = await _itemService.GetAllOrdered(propertyName);
        
            //Assert
            itemsResponse.Should().HaveCount(items.Count);

            itemsResponse.First().Id.Should().Be(_itemA.Id);
            itemsResponse.First().Description.Should().Be(_itemA.Description);
            itemsResponse.First().Price.Should().Be(_itemA.Price);
            itemsResponse.First().Active.Should().Be(_itemA.Active);
            
            itemsResponse.Last().Id.Should().Be(_itemB.Id);
            itemsResponse.Last().Description.Should().Be(_itemB.Description);
            itemsResponse.Last().Price.Should().Be(_itemB.Price);
            itemsResponse.Last().Active.Should().Be(_itemB.Active);
            
            await _itemRepository.Received(1).GetAll();
        }     
        
        [Fact]
        public async Task Should_get_an_item_by_id()
        {
            //Arrange
            _itemRepository.GetById(_itemA.Id).Returns(_itemA);
            
            //Act
            var itemResponse = await _itemService.GetById(_itemA.Id);
        
            //Assert
            itemResponse.Id.Should().Be(_itemA.Id);
            itemResponse.Description.Should().Be(_itemA.Description);
            itemResponse.Price.Should().Be(_itemA.Price);
            itemResponse.Active.Should().Be(_itemA.Active);
            
            await _itemRepository.Received(1).GetById(_itemA.Id);
        }    
        
        [Fact]
        public async Task Should_update_an_item()
        {
            //Arrange
            _itemRepository.GetById(_itemA.Id).Returns(_itemA);
            
            //Act
            var itemResponse = await _itemService.Update(_itemA.Id, _itemRequest);
        
            //Assert
            itemResponse.Id.Should().Be(_itemA.Id);
            itemResponse.Description.Should().Be(_itemRequest.Description);
            itemResponse.Price.Should().Be(_itemRequest.Price);
            itemResponse.Active.Should().Be(_itemA.Active);
            
            await _itemRepository.Received(1).Update(_itemA);
        }
    }
}