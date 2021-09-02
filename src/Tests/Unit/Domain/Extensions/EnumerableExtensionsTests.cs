using System.Linq;
using Domain.Entities;
using Domain.Extensions;
using FluentAssertions;
using Tests.Builders.Entities;
using Xunit;

namespace Tests.Unit.Domain.Extensions
{
    public class EnumerableExtensionsTests
    {
        private readonly Item[] _items;

        public EnumerableExtensionsTests()
        {
            var itemA = new ItemBuilder()
                .WithDescription("AAA")
                .WithPrice(9.99)
                .Build();
            
            var itemB = new ItemBuilder()
                .WithDescription("BBB")
                .WithPrice(4.50)
                .Build();            
            
            var itemC = new ItemBuilder()
                .WithDescription("CCC")
                .WithPrice(7.00)
                .Build();

            _items = new[] { itemA, itemB, itemC };
        }

        [Fact]
        public void Should_order_by_price()
        {
            // Arrange
            var propertyName = nameof(Item.Price);
            
            // Act
            var result = _items.OrderBy(propertyName).ToList();

            // Assert
            result.Should().BeInAscendingOrder(x => x.Price);
        }
        
        [Fact]
        public void Should_order_by_description()
        {
            // Arrange
            var propertyName = nameof(Item.Description);
            
            // Act
            var result = _items.OrderBy(propertyName).ToList();

            // Assert
            result.Should().BeInAscendingOrder(x => x.Description);
        }     
        
        [Fact]
        public void Should_not_order_with_empty_string()
        {
            // Arrange
            var propertyName = string.Empty;
            
            // Act
            var result = _items.OrderBy(propertyName).ToList();

            // Assert
            result.Should().BeEquivalentTo(_items);
        }        
        
        [Fact]
        public void Should_not_order_with_null_string()
        {
            // Arrange
            string propertyName = null;
            
            // Act
            var result = _items.OrderBy(propertyName).ToList();

            // Assert
            result.Should().BeEquivalentTo(_items);
        }
    }
}