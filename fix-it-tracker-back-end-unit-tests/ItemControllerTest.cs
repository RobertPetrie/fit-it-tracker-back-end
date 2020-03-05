using fix_it_tracker_back_end.Controllers;
using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Dtos;
using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end.Model.BindingTargets;
using fix_it_tracker_back_end_unit_tests.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace fix_it_tracker_back_end_unit_tests
{
    public class ItemControllerTest
    {
        private ItemController _itemController;
        private IFixItTrackerRepository _fixItTrackerRepository;

        private static int EXISTING_ITEM_ID = 1;
        private static int NON_EXISTING_ITEM_ID = 25;
        private static int NUM_OF_ITEM = 10;

        public ItemControllerTest()
        {
            _fixItTrackerRepository = new UnitTestsRepository();
            _itemController = new ItemController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());
        }

        // GET api/item
        [Fact]
        public void GetItems_ReturnsOkResult()
        {
            var okResult = _itemController.GetItems();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetItems_ReturnsRightItem()
        {
            var okResult = _itemController.GetItems().Result as OkObjectResult;
            Assert.Equal(EXISTING_ITEM_ID, (okResult.Value as List<ItemGetDto>).FirstOrDefault(i => i.ItemID == EXISTING_ITEM_ID).ItemID);
        }

        [Fact]
        public void GetItems_ReturnsRightChildItem()
        {
            var okResult = _itemController.GetItems().Result as OkObjectResult;
            var itemType = typeof(ItemTypeGetDto);
            Assert.IsType(itemType, (okResult.Value as List<ItemGetDto>).FirstOrDefault(i => i.ItemID == EXISTING_ITEM_ID).ItemType);
        }

        [Fact]
        public void GetItems_ReturnsAllItems()
        {
            var okResult = _itemController.GetItems().Result as OkObjectResult;
            var items = Assert.IsType<List<ItemGetDto>>(okResult.Value);
            Assert.Equal(NUM_OF_ITEM, items.Count);
        }

        [Fact]
        public void GetItems_ReturnsNotFound()
        {
            _fixItTrackerRepository = new UnitTestsRepository(noItems: true);
            _itemController = new ItemController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());

            var okResult = _itemController.GetItems();
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }

        // GET api/item/5
        [Fact]
        public void GetItem_ReturnsOkResult()
        {
            var okResult = _itemController.GetItem(EXISTING_ITEM_ID);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetItem_ReturnsRightItem()
        {
            var okResult = _itemController.GetItem(EXISTING_ITEM_ID).Result as OkObjectResult;
            Assert.IsType<ItemGetDto>(okResult.Value);
            Assert.Equal(EXISTING_ITEM_ID, (okResult.Value as ItemGetDto).ItemID);
        }

        [Fact]
        public void GetItem_ReturnsRightChildItem()
        {
            var okResult = _itemController.GetItem(EXISTING_ITEM_ID).Result as OkObjectResult;
            var itemType = typeof(ItemTypeGetDto);
            Assert.IsType(itemType, (okResult.Value as ItemGetDto).ItemType);
        }

        [Fact]
        public void GetItem_ReturnsNotFound()
        {
            var okResult = _itemController.GetItem(NON_EXISTING_ITEM_ID);
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }

        // POST api/item/
        [Fact]
        public void AddItem_ReturnsCreatedResponse()
        {
            ItemData item = new ItemData
            {
                Serial = "123456",
                ItemType = 1
            };

            var createdResponse = _itemController.CreateItem(item);

            Assert.IsType<CreatedResult>(createdResponse);
        }

        [Fact]
        public void AddItem_ReturnedResponseHasCreatedMessage()
        {
            ItemData item = new ItemData
            {
                Serial = "123456",
                ItemType = 1
            };

            ActionResult<Item> actionResult = _itemController.CreateItem(item);
            CreatedResult createdResult = actionResult.Result as CreatedResult;
            var result = createdResult.Value;

            Assert.Equal("Item Created", result);
        }

        [Fact]
        public void AddItem_ReturnsBadRequest()
        {
            ItemData item = new ItemData();

            _itemController.ModelState.AddModelError("Serial", "Required");

            var badResponse = _itemController.CreateItem(item);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void AddItem_ExistingItemReturnsBadRequest()
        {
            ItemData firstItem = new ItemData
            {
                Serial = "123456",
                ItemType = 1
            };

            ItemData secondItem = new ItemData
            {
                Serial = "123456",
                ItemType = 1
            };

            _itemController.CreateItem(firstItem);

            var badResponse = _itemController.CreateItem(secondItem);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void AddItem_ItemTypeNotExistReturnsBadRequest()
        {
            ItemData item = new ItemData
            {
                Serial = "123456",
                ItemType = 100
            };

            var badResponse = _itemController.CreateItem(item);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}
