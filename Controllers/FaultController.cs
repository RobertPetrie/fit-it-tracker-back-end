using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private DataContext _dataContext;
        private readonly IMapper _mapper;

        public FaultController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        // GET api/fault
        [HttpGet]
        public ActionResult GetFaults()
        {
            var faults = _dataContext.Faults;
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

        // GET api/fault/5
        [HttpGet("{id}")]
        public ActionResult GetFault(int id)
        {
            var fault = _dataContext.Faults.FirstOrDefault(f => f.FaultID == id);
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