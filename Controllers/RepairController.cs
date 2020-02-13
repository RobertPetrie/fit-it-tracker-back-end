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
    public class RepairController : ControllerBase
    {
        private DataContext _dataContext;

        public RepairController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET api/repair
        [HttpGet]
        public ActionResult GetRepairs()
        {

            var repairs = _dataContext.Repairs;

            if (repairs == null)
            {
                return NotFound("No repairs found.");
            }
            else
            {
                return Ok(repairs);
            }
        }

        // GET api/repair/5
        [HttpGet("{id}")]
        public ActionResult GetRepair(int id)
        {
            var repair = _dataContext.Repairs.FirstOrDefault(r => r.RepairID == id);

            if (repair == null)
            {
                return NotFound($"No repair found for id: {id}");
            }
            else
            {
                return Ok(repair);
            }
        }

        [HttpGet("[action]")]
        public ActionResult GetCustomerRepairs(int id)
        {
            var repair = _dataContext.Repairs.Where(c => c.Customer.CustomerID == id);

            if (repair.Count() == 0)
            {
                return NotFound($"No repair found for customer id: {id}");
            }
            else
            {
                return Ok(repair);
            }
        }
    }
}
