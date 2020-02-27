using fix_it_tracker_back_end.Controllers;
using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Dtos;
using fix_it_tracker_back_end_unit_tests.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            var okResult = _customerController.GetCustomers();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetCustomers_ReturnsRightItem()
        {
            var okResult = _customerController.GetCustomers().Result as OkObjectResult;
            Assert.Equal(EXISTING_CUSTOMER_ID, (okResult.Value as List<CustomerGetDto>).FirstOrDefault(c => c.CustomerID == EXISTING_CUSTOMER_ID).CustomerID);
        }

        [Fact]
        public void GetCustomers_ReturnsAllItems()
        {
            var okResult = _customerController.GetCustomers().Result as OkObjectResult;
            var items = Assert.IsType<List<CustomerGetDto>>(okResult.Value);
            Assert.Equal(NUM_OF_CUSTOMERS, items.Count);
        }

        [Fact]
        public void GetCustomers_ReturnsNotFound()
        {
            _fixItTrackerRepository = new UnitTestsRepository(noCustomers: true);
            _customerController = new CustomerController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());

            var okResult = _customerController.GetCustomers();
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
    }
}
