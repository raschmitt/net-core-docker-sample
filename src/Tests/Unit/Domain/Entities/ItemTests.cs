using Domain.Entities;
using FluentAssertions;
using Tests.Builders.Dtos;
using Tests.Builders.Entities;
using Xunit;

namespace Tests.Unit.Domain.Entities
{
    public class ItemTests
    {
        private Item _item;
        
        public ItemTests()
        {
            _item = new ItemBuilder().Build();
        }
        
        [Fact]
        public void Should_update_an_item()
        {
            var itemRequest = new ItemRequestBuilder()
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