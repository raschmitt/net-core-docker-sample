using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Domain.Dtos.ItemDtos;
using Domain.Entities;
using FluentAssertions;
using Infra.Data.Repositories;
using Tests.Builders.Dtos;
using Tests.Builders.Entities;
using Xunit;

namespace Tests.Integration.Api.Controllers.v2
{
    public class ItemsController : BaseTestClient
    {
        private readonly ItemRepository _itemRepository;
        
        private readonly Item _itemA;
        private readonly Item _itemB;
        private readonly Item _itemC;

        private readonly List<Item> _activeItems;
        
        private readonly ItemRequest _itemRequest;
        
        public ItemsController() : base("api/v2/items")
        {
            _itemA = new ItemBuilder()
                .WithPrice(10.00)
                .Build();
            
            _itemB = new ItemBuilder()
                .WithPrice(4.50)
                .Build();
            
            _itemC = new ItemBuilder()
                .WithInactiveStatus()
                .Build();
                
            _activeItems = new List<Item> {_itemA, _itemB};
            
            Factory.SeedData(_itemA, _itemB, _itemC);

            _itemRequest = new ItemRequestBuilder().Build();

            _itemRepository = new ItemRepository(Factory.GetContext());
        }

        [Fact]
        public async Task Should_get_all_items_ordered_by_price()
        {
            // Arrange
            var orderBy = nameof(Item.Price);
            
            // Act
            var response = await Client.GetAsync($"{ControllerUri}?orderBy={orderBy}");

            //Assert
            var responseItems = await DeserializeResponse<List<ItemResponse>>(response);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            responseItems.Should().HaveCount(_activeItems.Count);
            responseItems.Should().BeInAscendingOrder(x => x.Price);
            responseItems.Should().ContainEquivalentOf(_itemA);
            responseItems.Should().ContainEquivalentOf(_itemB);
            responseItems.Should().NotContainEquivalentOf(_itemC);
        }        
     }
}