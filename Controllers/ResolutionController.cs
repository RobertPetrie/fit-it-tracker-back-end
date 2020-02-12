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
    public class ResolutionController : ControllerBase
    {
        private DataContext _dataContext;

        public ResolutionController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET api/resolution
        [HttpGet]
        public ActionResult GetResolutions()
        {
            var resolutions = _dataContext.Resolutions;

            if (resolutions == null)
            {
                return NotFound("No resolutions found.");
            }
            else
            {
                return Ok(resolutions);
            }
        }

        // GET api/resolution/5
        [HttpGet("{id}")]
        public ActionResult GetResolution(int id)
        {
            var resolution = _dataContext.Resolutions.FirstOrDefault(r => r.ResolutionID == id);

            if (resolution == null)
            {
                return NotFound($"No resolution found for id: {id}");
            }
            else
            {
                return Ok(resolution);
            }
        }
    }
}