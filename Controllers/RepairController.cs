using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private DataContext _dataContext;
        private readonly IMapper _mapper;

        public RepairController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        // GET api/repair
        [HttpGet]
        public ActionResult GetRepairs()
        {

            var repairs = _dataContext.Repairs;
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

        // GET api/repair/5
        [HttpGet("{id}")]
        public ActionResult GetRepair(int id)
        {
            var repair = _dataContext.Repairs.FirstOrDefault(r => r.RepairID == id);
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

        // GET api/repair/GetCustomerRepairs?id=5
        [HttpGet("[action]")]
        public ActionResult GetCustomerRepairs(int id)
        {
            var repair = _dataContext.Repairs.Where(c => c.Customer.CustomerID == id);
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
