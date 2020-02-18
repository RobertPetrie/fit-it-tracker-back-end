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
    public class ResolutionController : ControllerBase
    {
        private DataContext _dataContext;
        private readonly IMapper _mapper;

        public ResolutionController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        // GET api/resolution
        [HttpGet]
        public ActionResult GetResolutions()
        {
            var resolutions = _dataContext.Resolutions;
            var resolutionsToReturn = _mapper.Map<IEnumerable<ResolutionGetDto>>(resolutions);

            if (resolutionsToReturn == null)
            {
                return NotFound("No resolutions found.");
            }
            else
            {
                return Ok(resolutionsToReturn);
            }
        }

        // GET api/resolution/5
        [HttpGet("{id}")]
        public ActionResult GetResolution(int id)
        {
            var resolution = _dataContext.Resolutions.FirstOrDefault(r => r.ResolutionID == id);
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
    }
}