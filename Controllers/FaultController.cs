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
    public class FaultController : ControllerBase
    {
        private DataContext _dataContext;

        public FaultController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET api/fault
        [HttpGet]
        public ActionResult GetFaults()
        {
            var faults = _dataContext.Faults;

            if (faults == null)
            {
                return NotFound("No faults found.");
            }
            else
            {
                return Ok(faults);
            }
        }

        // GET api/fault/5
        [HttpGet("{id}")]
        public ActionResult GetFault(int id)
        {
            var fault = _dataContext.Faults.FirstOrDefault(f => f.FaultID == id);

            if (fault == null)
            {
                return NotFound($"No fault found for id: {id}");
            }
            else
            {
                return Ok(fault);
            }
        }
    }
}