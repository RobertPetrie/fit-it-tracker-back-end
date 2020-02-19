using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Dtos;
using fix_it_tracker_back_end.Model;
using Microsoft.AspNetCore.Http;
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
        public ActionResult GetFaults()
        {
            var faults = _dataContext.GetFaults();
            var faultsToReturn = _mapper.Map<IEnumerable<FaultGetDto>>(faults);

            if (faultsToReturn == null)
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
        public ActionResult GetFault(int id)
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
    }
}