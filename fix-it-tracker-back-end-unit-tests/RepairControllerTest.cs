using fix_it_tracker_back_end.Controllers;
using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Dtos;
using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end.Model.BindingTargets;
using fix_it_tracker_back_end_unit_tests.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace fix_it_tracker_back_end_unit_tests
{
    public class RepairControllerTest
    {
        private RepairController _repairController;
        private IFixItTrackerRepository _fixItTrackerRepository;

        private static int EXISTING_REPAIR_ID = 1;
        private static int NON_EXISTING_REPAIR_ID = 25;
        private static int NUM_OF_REPAIRS = 5;

        private static int EXISTING_CUSTOMER_ID = 1;
        private static int NON_EXISTING_CUSTOMER_ID = 25;
        private static int NUM_OF_CUSTOMER_REPAIRS = 1;

        public RepairControllerTest()
        {
            _fixItTrackerRepository = new UnitTestsRepository();
            _repairController = new RepairController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());
        }

        // GET api/repair
        [Fact]
        public void GetRepairs_ReturnsOkResult()
        {
            var okResult = _repairController.GetRepairs(null, null);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetRepairs_ReturnsRightItem()
        {
            var okResult = _repairController.GetRepairs(null, null).Result as OkObjectResult;
            Assert.Equal(EXISTING_REPAIR_ID, (okResult.Value as List<RepairGetDto>).FirstOrDefault(r => r.RepairID == EXISTING_REPAIR_ID).RepairID);
        }

        [Fact]
        public void GetRepairs_ReturnsAllItems()
        {
            var okResult = _repairController.GetRepairs(null, null).Result as OkObjectResult;
            var items = Assert.IsType<List<RepairGetDto>>(okResult.Value);
            Assert.Equal(NUM_OF_REPAIRS, items.Count);
        }

        [Fact]
        public void GetRepairs_ReturnsNotFound()
        {
            _fixItTrackerRepository = new UnitTestsRepository(noRepairs: true);
            _repairController = new RepairController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());

            var okResult = _repairController.GetRepairs(null, null);
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }

        // GET api/repair/5
        [Fact]
        public void GetRepair_ReturnsOkResult()
        {
            var okResult = _repairController.GetRepair(EXISTING_REPAIR_ID);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetRepair_ReturnsRightItem()
        {
            var okResult = _repairController.GetRepair(EXISTING_REPAIR_ID).Result as OkObjectResult;
            Assert.IsType<RepairGetDto>(okResult.Value);
            Assert.Equal(EXISTING_REPAIR_ID, (okResult.Value as RepairGetDto).RepairID);
        }

        [Fact]
        public void GetRepair_ReturnsNotFound()
        {
            var okResult = _repairController.GetRepair(NON_EXISTING_REPAIR_ID);
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }

        // GET api/repair/GetCustomerRepairs/5
        [Fact]
        public void GetCustomerRepairs_ReturnsOkResult()
        {
            var okResult = _repairController.GetCustomerRepairs(EXISTING_CUSTOMER_ID);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetCustomerRepairs_ReturnsRightItem()
        {
            var okResult = _repairController.GetCustomerRepairs(EXISTING_CUSTOMER_ID).Result as OkObjectResult;
            Assert.Equal(EXISTING_REPAIR_ID, (okResult.Value as List<RepairGetDto>).FirstOrDefault(r => r.RepairID == EXISTING_REPAIR_ID).RepairID);
        }

        [Fact]
        public void GetCustomerRepairs_ReturnsAllItems()
        {
            var okResult = _repairController.GetCustomerRepairs(EXISTING_CUSTOMER_ID).Result as OkObjectResult;
            var items = Assert.IsType<List<RepairGetDto>>(okResult.Value);
            Assert.Equal(NUM_OF_CUSTOMER_REPAIRS, items.Count);
        }

        [Fact]
        public void GetCustomerRepairs_ReturnsNotFound()
        {
            var okResult = _repairController.GetCustomerRepairs(NON_EXISTING_CUSTOMER_ID);
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }

        // POST api/repair/
        [Fact]
        public void AddRepair_ReturnsCreatedResponse()
        {
            RepairData repair = new RepairData
            {
                Customer = 1
            };

            var createdResponse = _repairController.CreateRepair(repair);

            Assert.IsType<CreatedResult>(createdResponse);
        }

        [Fact]
        public void AddRepair_ReturnedResponseHasCreatedMessage()
        {
            RepairData repair = new RepairData
            {
                Customer = 1
            };

            ActionResult<Repair> actionResult = _repairController.CreateRepair(repair);
            CreatedResult createdResult = actionResult.Result as CreatedResult;
            var result = createdResult.Value;

            Assert.Equal("Repair Created", result);
        }

        [Fact]
        public void AddRepair_ReturnsBadRequest()
        {
            RepairData repair = new RepairData();

            _repairController.ModelState.AddModelError("Customer", "Required");

            var badResponse = _repairController.CreateRepair(repair);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void AddRepair_CustomerNotExistReturnsBadRequest()
        {
            RepairData repair = new RepairData
            {
                Customer = 100
            };

            var badResponse = _repairController.CreateRepair(repair);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        //  PATCH api/repair/5
        [Fact]
        public void UpdateRepair_ReturnsOkResult()
        {
            var jsonPatchDocument = new JsonPatchDocument<RepairPatchData>();
            var jsonPatchOperation = new Operation<RepairPatchData>("replace", "DateCompleted", null, "2020-03-27");

            jsonPatchDocument.Operations.Add(jsonPatchOperation);

            var okResult = _repairController.UpdateRepair(1, jsonPatchDocument);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void UpdateRepair_ReturnedResponseHasResponseMessage()
        {
            var jsonPatchDocument = new JsonPatchDocument<RepairPatchData>();
            var jsonPatchOperation = new Operation<RepairPatchData>("replace", "DateCompleted", null, "2020-03-27");

            jsonPatchDocument.Operations.Add(jsonPatchOperation);

            ActionResult<RepairPatchData> actionResult = _repairController.UpdateRepair(1, jsonPatchDocument);

            OkObjectResult createdResult = actionResult.Result as OkObjectResult;
            var result = createdResult.Value;

            Assert.Equal("The repair has been updated.", result);
        }

        [Fact]
        public void UpdateRepair_ReturnsBadRequest()
        {
            var jsonPatchDocument = new JsonPatchDocument<RepairPatchData>();
            var jsonPatchOperation = new Operation<RepairPatchData>("replace", "DateCompleted", null, "2020-03-27");

            jsonPatchDocument.Operations.Add(jsonPatchOperation);

            _repairController.ModelState.AddModelError("Name", "Required");

            var badResponse = _repairController.UpdateRepair(1, jsonPatchDocument);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void UpdateRepair_NonExistingrepairPatchDataReturnsBadRequest()
        {
            var jsonPatchDocument = new JsonPatchDocument<RepairPatchData>();
            var jsonPatchOperation = new Operation<RepairPatchData>("replace", "DateCompleted", null, "2020-03-27");

            jsonPatchDocument.Operations.Add(jsonPatchOperation);

            var badResponse = _repairController.UpdateRepair(123456, jsonPatchDocument);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void UpdateRepair_DateCompletedIsBeforeDateOpenedReturnsBadRequest()
        {
            RepairData repair = new RepairData
            {
                Customer = 1
            };

            var createdRepair = _fixItTrackerRepository.AddRepair(repair.Repair);

            var jsonPatchDocument = new JsonPatchDocument<RepairPatchData>();
            var jsonPatchOperation = new Operation<RepairPatchData>("replace", "DateCompleted", null, createdRepair.DateOpened.AddYears(-1).ToShortDateString());

            jsonPatchDocument.Operations.Add(jsonPatchOperation);

            var badResponse = _repairController.UpdateRepair(createdRepair.RepairID, jsonPatchDocument);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}
