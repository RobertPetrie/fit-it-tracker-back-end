using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fix_it_tracker_back_end.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fix_it_tracker_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private DataContext _dataContext;

        public CustomerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET api/customer
        [HttpGet]
        public ActionResult GetCustomers()
        {
            var customers = _dataContext.Customers;

            if (customers == null)
            {
                return NotFound("No customers found.");
            }
            else
            {
                return Ok(customers);
            }
        }

        // GET api/customer/5
        [HttpGet("{id}")]
        public ActionResult GetCustomer(int id)
        {
            var customer = _dataContext.Customers.FirstOrDefault(c => c.CustomerID == id);

            if (customer == null)
            {
                return NotFound($"No customer found for id: {id}");
            }
            else
            {
                return Ok(customer);
            }
        }
    }
}