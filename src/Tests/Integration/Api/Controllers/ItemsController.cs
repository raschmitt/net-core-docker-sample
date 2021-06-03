using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Domain.Dtos.ItemDtos;
using Domain.Entities;
using FluentAssertions;
using Infra.Data.Repositories;
using Tests.Builders.Dtos;
using Tests.Builders.Entities;
using Xunit;

namespace Tests.Integration.Api.Controllers
{
    public class ItemsController : BaseTestClient
    {
        private readonly ItemRepository _itemRepository;
        
        private readonly Item _itemA;
        private readonly Item _itemB;
        private readonly Item _itemC;

        private readonly List<Item> _activeItems;
        
        private readonly ItemRequest _itemRequest;
        
        public ItemsController() : base("/items")
        {
            _itemA = new ItemBuilder().Build();
            _itemB = new ItemBuilder().Build();
            _itemC = new ItemBuilder()
                .WithInactiveStatus()
                .Build();
                
            _activeItems = new List<Item> {_itemA, _itemB};
            
            Factory.SeedData(_itemA, _itemB, _itemC);

            _itemRequest = new ItemRequestBuilder().Build();

            _itemRepository = new ItemRepository(Factory.GetContext());
        }

        [Fact]
        public async Task Should_add_an_item()
        {
            // Act
            var response = await Client.PostAsJsonAsync(ControllerUri, _itemRequest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            
            var responseItem = await DeserializeResponse<ItemResponse>(response);
            
            responseItem.Should().BeEquivalentTo(_itemRequest);
            
            var items = await _itemRepository.GetAll();

            items.Should().HaveCount(_activeItems.Count + 1);
        }

        [Fact]
        public async Task Should_delete_an_item()
        {
            // Act
            var response = await Client.DeleteAsync($"{ControllerUri}/{_itemA.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var item = await _itemRepository.GetById(_itemA.Id);

            item.Active.Should().BeFalse();
            item.Description.Should().Be(_itemA.Description);
            item.Price.Should().Be(_itemA.Price);
        }

        [Fact]
        public async Task Should_get_all_items()
        {
            // Act
            var response = await Client.GetAsync(ControllerUri);

            //Assert
            var responseItems = await DeserializeResponse<List<ItemResponse>>(response);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            responseItems.Should().HaveCount(_activeItems.Count);
            responseItems.Should().ContainEquivalentOf(_itemA);
            responseItems.Should().ContainEquivalentOf(_itemB);
            responseItems.Should().NotContainEquivalentOf(_itemC);
        }        
     
        [Fact]
        public async Task Should_get_an_item_by_id()
        {
            // Act
            var response = await Client.GetAsync($"{ControllerUri}/{_itemC.Id}");

            //Assert
            var responseItem = await DeserializeResponse<ItemResponse>(response);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            responseItem.Should().BeEquivalentTo(_itemC);
        }

        [Fact]
        public async Task Should_update_an_item()
        {
            // Act
            var response = await Client.PutAsJsonAsync($"{ControllerUri}/{_itemA.Id}", _itemRequest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseItem = await DeserializeResponse<ItemResponse>(response);
            
            responseItem.Should().BeEquivalentTo(_itemRequest);
            
            var item = await _itemRepository.GetById(_itemA.Id);

            item.Should().BeEquivalentTo(_itemRequest);
        }
    }
}