using fix_it_tracker_back_end.Controllers;
using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Dtos;
using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end.Model.BindingTargets;
using fix_it_tracker_back_end_unit_tests.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace fix_it_tracker_back_end_unit_tests
{
    public class CustomerControllerTest
    {
        private CustomerController _customerController;
        private IFixItTrackerRepository _fixItTrackerRepository;

        private static int EXISTING_CUSTOMER_ID = 1;
        private static int NON_EXISTING_CUSTOMER_ID = 25;
        private static int NUM_OF_CUSTOMERS = 6;

        public CustomerControllerTest()
        {
            _fixItTrackerRepository = new UnitTestsRepository();
            _customerController = new CustomerController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());
        }

        // GET api/customer
        [Fact]
        public void GetCustomers_ReturnsOkResult()
        {
            var okResult = _customerController.GetCustomers(null, null, null);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetCustomers_ReturnsRightItem()
        {
            var okResult = _customerController.GetCustomers(null, null, null).Result as OkObjectResult;
            Assert.Equal(EXISTING_CUSTOMER_ID, (okResult.Value as List<CustomerGetDto>).FirstOrDefault(c => c.CustomerID == EXISTING_CUSTOMER_ID).CustomerID);
        }

        [Fact]
        public void GetCustomers_ReturnsAllItems()
        {
            var okResult = _customerController.GetCustomers(null, null, null).Result as OkObjectResult;
            var items = Assert.IsType<List<CustomerGetDto>>(okResult.Value);
            Assert.Equal(NUM_OF_CUSTOMERS, items.Count);
        }

        [Fact]
        public void GetCustomers_ReturnsNotFound()
        {
            _fixItTrackerRepository = new UnitTestsRepository(noCustomers: true);
            _customerController = new CustomerController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());

            var okResult = _customerController.GetCustomers(null, null, null);
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }

        // GET api/customer/5
        [Fact]
        public void GetCustomer_ReturnsOkResult()
        {
            var okResult = _customerController.GetCustomer(EXISTING_CUSTOMER_ID);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetCustomer_ReturnsRightItem()
        {
            var okResult = _customerController.GetCustomer(EXISTING_CUSTOMER_ID).Result as OkObjectResult;
            Assert.IsType<CustomerGetDto>(okResult.Value);
            Assert.Equal(EXISTING_CUSTOMER_ID, (okResult.Value as CustomerGetDto).CustomerID);
        }

        [Fact]
        public void GetCustomer_ReturnsNotFound()
        {
            var okResult = _customerController.GetCustomer(NON_EXISTING_CUSTOMER_ID);
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }

        // POST api/customer/
        [Fact]
        public void AddCustomer_ReturnsCreatedAtRouteResponse()
        {
            CustomerData customer = new CustomerData
            {
                Name = "John Doe",
                Address = "123 Somewhere Drive",
                City = "Toronto",
                Province = "ON",
                PostalCode = "A1B 2C3"
            };

            var createdResponse = _customerController.CreateCustomer(customer);

            Assert.IsType<CreatedAtRouteResult>(createdResponse);
        }

        [Fact]
        public void AddCustomer_ReturnedResponseHasCreatedItem()
        {
            CustomerData customer = new CustomerData
            {
                Name = "John Doe",
                Address = "123 Somewhere Drive",
                City = "Toronto",
                Province = "ON",
                PostalCode = "A1B 2C3"
            };

            ActionResult<CustomerGetDto> actionResult = _customerController.CreateCustomer(customer);
            CreatedAtRouteResult createdAtRouteResult = actionResult.Result as CreatedAtRouteResult;
            CustomerGetDto result = createdAtRouteResult.Value as CustomerGetDto;

            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public void AddCustomer_ReturnsBadRequest()
        {
            CustomerData customer = new CustomerData();

            _customerController.ModelState.AddModelError("Name", "Required");

            var badResponse = _customerController.CreateCustomer(customer);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void AddCustomer_ExistingCustomerReturnsBadRequest()
        {
            CustomerData firstCustomer = new CustomerData
            {
                Name = "John Doe",
                Address = "123 Somewhere Drive",
                City = "Toronto",
                Province = "ON",
                PostalCode = "A1B 2C3"
            };

            CustomerData secondCustomer = new CustomerData
            {
                Name = "John Doe",
                Address = "123 Somewhere Drive",
                City = "Toronto",
                Province = "ON",
                PostalCode = "A1B 2C3"
            };

            _customerController.CreateCustomer(firstCustomer);

            var badResponse = _customerController.CreateCustomer(secondCustomer);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        // PUT api/customer/
        [Fact]
        public void ReplaceCustomer_ReturnsOkResult()
        {
            CustomerData customer = new CustomerData
            {
                Name = "John Doe",
                Address = "123 Somewhere Drive",
                City = "Toronto",
                Province = "ON",
                PostalCode = "A1B 2C3"
            };

            var okResult = _customerController.ReplaceCustomer(1, customer);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void ReplaceCustomer_ReturnedResponseHasResponseMessage()
        {
            CustomerData customer = new CustomerData
            {
                Name = "John Doe",
                Address = "123 Somewhere Drive",
                City = "Toronto",
                Province = "ON",
                PostalCode = "A1B 2C3"
            };

            ActionResult<CustomerData> actionResult = _customerController.ReplaceCustomer(1, customer);
            OkObjectResult createdResult = actionResult.Result as OkObjectResult;
            var result = createdResult.Value;

            Assert.Equal("The customer has been updated.", result);
        }

        [Fact]
        public void ReplaceCustomer_ReturnsBadRequest()
        {
            CustomerData customer = new CustomerData();

            _customerController.ModelState.AddModelError("Name", "Required");

            var badResponse = _customerController.ReplaceCustomer(1, customer);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void ReplaceCustomer_NonExistingCustomerReturnsBadRequest()
        {
            CustomerData customer = new CustomerData
            {
                Name = "John Doe",
                Address = "123 Somewhere Drive",
                City = "Toronto",
                Province = "ON",
                PostalCode = "A1B 2C3"
            };

            var badResponse = _customerController.ReplaceCustomer(1234, customer);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void ReplaceCustomer_ExistingCustomerReturnsBadRequest()
        {
            CustomerData firstCustomer = new CustomerData
            {
                Name = "John Doe",
                Address = "123 Somewhere Drive",
                City = "Toronto",
                Province = "ON",
                PostalCode = "A1B 2C3"
            };

            CustomerData secondCustomer = new CustomerData
            {
                Name = "John Doe",
                Address = "123 Somewhere Drive",
                City = "Toronto",
                Province = "ON",
                PostalCode = "A1B 2C3"
            };

            _customerController.ReplaceCustomer(1, firstCustomer);

            var badResponse = _customerController.ReplaceCustomer(2, secondCustomer);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        // DELETE api/customer/5
        [Fact]
        public void RemoveCustomer_ReturnsOkResult()
        {
            var okResult = _customerController.RemoveCustomer(1);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void RemoveCustomer_ReturnedResponseHasResponseMessage()
        {
            ActionResult<CustomerData> actionResult = _customerController.RemoveCustomer(1);
            OkObjectResult removeResult = actionResult.Result as OkObjectResult;
            var result = removeResult.Value;

            Assert.Equal("The Customer has been deleted along with all associated repairs.", result);
        }

        [Fact]
        public void RemoveCustomer_NonExistingCustomerReturnsBadRequest()
        {

            var badResponse = _customerController.RemoveCustomer(1234);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}
