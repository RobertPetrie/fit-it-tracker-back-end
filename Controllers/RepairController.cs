using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Dtos;
using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end.Model.BindingTargets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fix_it_tracker_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private IFixItTrackerRepository _dataContext;
        private readonly IMapper _mapper;

        public RepairController(IFixItTrackerRepository dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of repairs : repairId, repairDateOpened, repairDateCompleted
        /// </summary>
        // GET api/repair
        [HttpGet]
        public ActionResult<IEnumerable<Repair>> GetRepairs()
        {

            var repairs = _dataContext.GetRepairs();
            var repairsToReturn = _mapper.Map<IEnumerable<RepairGetDto>>(repairs);

            if (repairsToReturn.Count() == 0)
            {
                return NotFound("No repairs found.");
            }
            else
            {
                return Ok(repairsToReturn);
            }
        }

        /// <summary>
        /// Returns a repair by specific id : repairId, repairDateOpened, repairDateCompleted
        /// </summary>
        /// <param name="id">The repair id</param>
        // GET api/repair/5
        [HttpGet("{id}")]
        public ActionResult<Repair> GetRepair(int id)
        {
            var repair = _dataContext.GetRepair(id);
            var repairToReturn = _mapper.Map<RepairGetDto>(repair);

            if (repairToReturn == null)
            {
                return NotFound($"No repair found for id: {id}");
            }
            else
            {
                return Ok(repairToReturn);
            }
        }

        /// <summary>
        /// Returns repairs by a specific customer id : repairId, repairDateOpened
        /// </summary>
        /// <param name="id">The cuustomer id</param>
        /// <returns></returns>
        // GET api/repair/GetCustomerRepairs/5
        [HttpGet("[action]/{id}")]
        public ActionResult<IEnumerable<Repair>> GetCustomerRepairs(int id)
        {
            var repair = _dataContext.GetCustomerRepairs(id);
            var repairToReturn = _mapper.Map<IEnumerable<RepairGetDto>>(repair);

            if (repairToReturn.Count() == 0)
            {
                return NotFound($"No repair found for customer id: {id}");
            }
            else
            {
                return Ok(repairToReturn);
            }
        }

        /// <summary>
        /// Creates a single repair.
        /// </summary>
        /// <param name="repairData">The customer Id you want to create a repair for </param>
        /// <returns>A message confirming the repair has been created</returns>
        // POST api/repair
        [HttpPost]
        public ActionResult CreateRepair([FromBody] RepairData repairData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = _dataContext.GetCustomer(repairData.Customer);

            if (customer == null)
            {
                return BadRequest("Customer doesn't exist");
            }

            Repair repair = repairData.Repair;
            repair.Customer = customer;

            var createdRepair = _dataContext.AddRepair(repair);

            var uri = Request != null ? Request.GetDisplayUrl().ToString() + createdRepair.RepairID : "";

            return Created(uri, "Repair Created");
        }

        /// <summary>
        /// Updates a single repair.
        /// </summary>
        /// <param name="id">The repair Id you want to update.</param>
        /// <param name="patch">The individual properties that you want to update.</param>
        /// <returns>A message confirming that the repair has been updated</returns>
        // PATCH api/repair/5
        [HttpPatch("{id}")]
        public ActionResult UpdateRepair(int id, [FromBody]JsonPatchDocument<RepairPatchData> patch)
        {
            Repair existingRepair = _dataContext.GetRepair(id);

            if (existingRepair == null)
            {
                return BadRequest($"Repair ID: {id} doesn't exist");
            }

            RepairPatchData repairPatchData = new RepairPatchData { Repair = existingRepair };

            patch.ApplyTo(repairPatchData, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (repairPatchData.DateCompleted != null)
            {
                if (DateTime.Compare(existingRepair.DateOpened, repairPatchData.DateCompleted.Value) > 0)
                {
                    return BadRequest("Date Completed is earlier than Date Opened");
                }
            }

            _dataContext.UpdateRepair(repairPatchData.Repair);
            return Ok("The repair has been updated.");
        }
    }
}
