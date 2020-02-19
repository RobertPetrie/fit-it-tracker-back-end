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
using fix_it_tracker_back_end.Data.Repositories;

namespace fix_it_tracker_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IFixItTrackerRepository _dataContext;
        private readonly IMapper _mapper;

        public CustomerController(IFixItTrackerRepository dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of customers: customerId, customerName, customerAddress, customerPostalCode, customerCity, customerProvince
        /// </summary>
        // GET api/customer
        [HttpGet]
        public ActionResult GetCustomers()
        {
            var customers = _dataContext.GetCustomers();
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

        /// <summary>
        /// Returns a customer by a specific id: customerId, customerName, customerAddress, customerPostalCode, customerCity, customerProvince
        /// </summary>
        /// <param name="id">The customer id</param>
        // GET api/customer/5
        [HttpGet("{id}")]
        public ActionResult GetCustomer(int id)
        {
            var customer = _dataContext.GetCustomer(id);
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