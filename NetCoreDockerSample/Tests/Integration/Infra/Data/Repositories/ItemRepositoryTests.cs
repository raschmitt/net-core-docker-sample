using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using FluentAssertions;
using Infra.Data;
using Infra.Data.Repositories;
using Tests.Builder.Dtos.Item;
using Tests.Builder.Entities;
using Xunit;

namespace Tests.Integration.Infra.Data.Repositories
{
    public class ItemRepositoryTests : IntegrationContext
    {
        private readonly IItemRepository _actRepository;
        private readonly IItemRepository _assertRepository;

        private readonly Item _itemA;
        private readonly Item _itemB;
        private readonly List<Item> _items;
        
        public ItemRepositoryTests()
        {
            _actRepository = new ItemRepository(_actContext);
            _assertRepository = new ItemRepository(_assertContext);
            
            _itemA = new ItemBuilder().Build();
            _itemB = new ItemBuilder().Build();

            _items = new List<Item> {_itemA, _itemB};
            
            _context.AddRange( _itemA, _itemB );
            _context.SaveChanges();
        }
        
        [Fact]
        public async Task Should_add_an_item()
        {
            //Arrange
            var item = new ItemBuilder().Build();
            
            //Act
            await _actRepository.Add(item);
            await _actContext.SaveChangesAsync();
                
            //Assert
            var addedItem = await _assertRepository.GetById(item.Id);

            addedItem.Should().BeEquivalentTo(item);
        }
        
        [Fact]
        public async Task Should_get_all()
        {
            //Act
            var items = await _actRepository.GetAll();
                
            //Assert
            items.Should().HaveCount(_items.Count);
            items.Should().ContainEquivalentOf(_itemA);
            items.Should().ContainEquivalentOf(_itemB);
        }        
        
        [Fact]
        public async Task Should_get_an_item_by_id()
        {
            //Act
            var item = await _actRepository.GetById(_itemA.Id);
                
            //Assert
            item.Should().BeEquivalentTo(_itemA);
        }       
        
        [Fact]
        public async Task Should_update_an_item()
        {
            //Arrange
            var itemRequest = new ItemRequestBuilder()
                .WithDescription("Potato")
                .WithPrice(2.25)
                .Build();
            
            _itemA.Update(itemRequest);
            
            //Act
            await _actRepository.Update(_itemA);
            await _actContext.SaveChangesAsync();
                
            //Assert
            var updatedItem = await _assertRepository.GetById(_itemA.Id);

            updatedItem.Description.Should().Be(itemRequest.Description);
            updatedItem.Price.Should().Be(itemRequest.Price);
            updatedItem.Active.Should().BeTrue();
        }
    }
}