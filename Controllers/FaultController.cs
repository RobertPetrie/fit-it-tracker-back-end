using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Dtos;
using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end.Model.BindingTargets;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace fix_it_tracker_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaultController : ControllerBase
    {
        private IFixItTrackerRepository _dataContext;
        private readonly IMapper _mapper;

        public FaultController(IFixItTrackerRepository dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of faults: faultId, faultName, faultDescription
        /// </summary>
        // GET api/fault
        [HttpGet]
        public ActionResult<IEnumerable<Fault>> GetFaults()
        {
            var faults = _dataContext.GetFaults();
            var faultsToReturn = _mapper.Map<IEnumerable<FaultGetDto>>(faults);

            if (faultsToReturn.Count() == 0)
            {
                return NotFound("No faults found.");
            }
            else
            {
                return Ok(faultsToReturn);
            }
        }

        /// <summary>
        /// Returns a fault by a specific id: faultId, faultName, faultDescription
        /// </summary>
        /// <param name="id">The fault id</param>
        // GET api/fault/5
        [HttpGet("{id}")]
        public ActionResult<Fault> GetFault(int id)
        {
            var fault = _dataContext.GetFault(id);

            var faultToReturn = _mapper.Map<FaultGetDto>(fault);

            if (faultToReturn == null)
            {
                return NotFound($"No fault found for id: {id}");
            }
            else
            {
                return Ok(faultToReturn);
            }
        }

        /// <summary>
        /// Creates a single fault.
        /// </summary>
        /// <param name="faultData">The fault object that you want to create.</param>
        /// <returns>A message confirming the fault has been created</returns>
        // POST api/fault
        [HttpPost]
        public ActionResult CreateFault([FromBody] FaultData faultData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_dataContext.FaultExists(faultData.Fault))
            {
                return BadRequest("Fault name already exists");
            }

            var fault = _dataContext.AddFault(faultData.Fault);

            var uri = Request != null ? Request.GetDisplayUrl().ToString() + fault.FaultID : "";

            return Created(uri, "Fault Created");
        }

        /// <summary>
        /// Reemove a single fault from the database.
        /// </summary>
        /// <param name="id">The existing fault id.</param>
        /// <returns>Returns OK with a confirmation message.</returns>
        [HttpDelete("{id}")]
        public ActionResult RemoveFault(int id)
        {
            var existingFault = _dataContext.GetFault(id);

            if (existingFault == null)
            {
                return BadRequest($"Fault ID: {id} doesn't exist");
            }

            _dataContext.RemoveFault(existingFault);
            return Ok("The Fault has been deleted along with all associated repairs.");
        }
    }
}