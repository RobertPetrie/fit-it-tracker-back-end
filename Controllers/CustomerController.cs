using AutoMapper;
using fix_it_tracker_back_end.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fix_it_tracker_back_end.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fix_it_tracker_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private DataContext _dataContext;
        private readonly IMapper _mapper;

        public CustomerController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        // GET api/customer
        [HttpGet]
        public ActionResult GetCustomers()
        {
            var customers = _dataContext.Customers;
            var customersToReturn = _mapper.Map<IEnumerable<CustomerGetDto>>(customers);

            if (customersToReturn == null)
            {
                return NotFound("No customers found.");
            }
            else
            {
                return Ok(customersToReturn);
            }
        }

        // GET api/customer/5
        [HttpGet("{id}")]
        public ActionResult GetCustomer(int id)
        {
            var customer = _dataContext.Customers.FirstOrDefault(c => c.CustomerID == id);
            var customerToReturn = _mapper.Map<CustomerGetDto>(customer);

            if (customerToReturn == null)
            {
                return NotFound($"No customer found for id: {id}");
            }
            else
            {
                return Ok(customerToReturn);
            }
        }
    }
}