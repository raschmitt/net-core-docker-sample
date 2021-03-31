using Domain.Dtos.ItemDtos;
using Domain.Entities;
using FluentAssertions;
using Tests.Builder.Dtos.Item;
using Tests.Builder.Entities;
using Xunit;

namespace Tests.Unit.Domain.Entities
{
    public class ItemTests
    {
        private readonly ItemRequest _itemRequest;
        private readonly Item _item;
        
        public ItemTests()
        {
            _itemRequest = new ItemRequestBuilder().Build();
            _item = new ItemBuilder().Build();
        }
        
        [Fact]
        public void Should_map_an_item_request_to_an_item()
        {
            //Act
            var item = new Item(_itemRequest);

            //Assert
            item.Id.Should().Be(item.Id);
            item.Description.Should().Be(item.Description);
            item.Price.Should().Be(item.Price);
            item.Active.Should().Be(item.Active);
        }     
        
        [Fact]
        public void Should_update_an_item()
        {
            var itemRequest = new ItemRequestBuilder()
                .WithDescription("Banana")
                .WithPrice(5.75)
                .Build();
            
            //Act
            _item.Update(itemRequest);

            //Assert
            _item.Description.Should().Be(itemRequest.Description);
            _item.Price.Should().Be(itemRequest.Price);
        }  
        
        [Fact]
        public void Should_inactivate_an_item()
        {
            //Act
            _item.Inactivate();

            //Assert
            _item.Active.Should().BeFalse();
        }  
    }
}