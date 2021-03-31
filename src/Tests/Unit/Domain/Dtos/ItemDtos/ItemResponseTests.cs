using Domain.Dtos.ItemDtos;
using FluentAssertions;
using Tests.Builder.Entities;
using Xunit;

namespace Tests.Unit.Domain.Dtos.ItemDtos
{
    public class ItemResponseTests
    {
        [Fact]
        public void Should_map_an_item_to_an_item_response()
        {
            //Arrange
            var item = new ItemBuilder().Build();
            
            //Act
            var itemResponse = new ItemResponse(item);

            //Assert
            itemResponse.Id.Should().Be(item.Id);
            itemResponse.Description.Should().Be(item.Description);
            itemResponse.Price.Should().Be(item.Price);
            itemResponse.Active.Should().Be(item.Active);
        }    
    }
}