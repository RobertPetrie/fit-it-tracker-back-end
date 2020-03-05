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
using Microsoft.AspNetCore.Mvc;

namespace fix_it_tracker_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResolutionController : ControllerBase
    {
        private IFixItTrackerRepository _dataContext;
        private readonly IMapper _mapper;

        public ResolutionController(IFixItTrackerRepository dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of resolutions : resolutionId, resolutionName, resolutionDescription
        /// </summary>
        // GET api/resolution
        [HttpGet]
        public ActionResult<IEnumerable<Resolution>> GetResolutions()
        {
            var resolutions = _dataContext.GetResolutions();
            var resolutionsToReturn = _mapper.Map<IEnumerable<ResolutionGetDto>>(resolutions);

            if (resolutionsToReturn.Count() == 0)
            {
                return NotFound("No resolutions found.");
            }
            else
            {
                return Ok(resolutionsToReturn);
            }
        }

        /// <summary>
        /// Returns a resolution by specific id : resolutionId, resolutionName, resolutionDescription
        /// </summary>
        /// <param name="id">The resolution id</param>
        // GET api/resolution/5
        [HttpGet("{id}")]
        public ActionResult<Resolution> GetResolution(int id)
        {
            var resolution = _dataContext.GetResolution(id);
            var resolutionToReturn = _mapper.Map<ResolutionGetDto>(resolution);

            if (resolutionToReturn == null)
            {
                return NotFound($"No resolution found for id: {id}");
            }
            else
            {
                return Ok(resolutionToReturn);
            }
        }

        /// <summary>
        /// Creates a single resolution.
        /// </summary>
        /// <param name="resolutionData">The resolution object that you want to create.</param>
        /// <returns>A message confirming the resolution has been created</returns>
        // POST api/resolution
        [HttpPost]
        public ActionResult CreateResolution([FromBody] ResolutionData resolutionData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_dataContext.ResolutionExists(resolutionData.Resolution))
            {
                return BadRequest("Resolution name already exists");
            }

            var resolution = _dataContext.AddResolution(resolutionData.Resolution);

            var uri = Request != null ? Request.GetDisplayUrl().ToString() + resolution.ResolutionID : "";

            return Created(uri, "Resolution Created");
        }
    }
}