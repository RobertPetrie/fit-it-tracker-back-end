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
        public ActionResult GetRepairs()
        {

            var repairs = _dataContext.GetRepairs();
            var repairsToReturn = _mapper.Map<IEnumerable<RepairGetDto>>(repairs);

            if (repairsToReturn == null)
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
        public ActionResult GetRepair(int id)
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
        public ActionResult GetCustomerRepairs(int id)
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
    }
}
