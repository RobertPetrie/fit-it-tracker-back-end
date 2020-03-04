﻿using AutoMapper;
using fix_it_tracker_back_end.Dtos;
using System.Collections.Generic;
using System.Linq;
using fix_it_tracker_back_end.Model;
using Microsoft.AspNetCore.Mvc;
using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Model.BindingTargets;

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
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            var customers = _dataContext.GetCustomers();
            var customersToReturn = _mapper.Map<IEnumerable<CustomerGetDto>>(customers);

            if (customersToReturn.Count() == 0)
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
        [HttpGet("{id}", Name = "CustomerById")]
        public ActionResult<Customer> GetCustomer(int id)
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

        /// <summary>
        /// Creates a single customer.
        /// </summary>
        /// <param name="customerData">The customer object that you want to create</param>
        /// <returns>Created customer with a header on how to access it.</returns>
        // POST api/customer
        [HttpPost]
        public ActionResult CreateCustomer([FromBody] CustomerData customerData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_dataContext.CustomerExists(customerData.Customer))
            {
                return BadRequest("Customer name already exists");
            }

            var newCustomer = _dataContext.AddCustomer(customerData.Customer);

            var customerToReturn = _mapper.Map<CustomerGetDto>(newCustomer);

            return CreatedAtRoute("CustomerById", new { id = customerToReturn.CustomerID }, customerToReturn);
        }
    }
}