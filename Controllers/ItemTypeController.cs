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
    public class ItemTypeController : ControllerBase
    {
        private IFixItTrackerRepository _dataContext;
        private readonly IMapper _mapper;

        public ItemTypeController(IFixItTrackerRepository dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        // GET api/itemtype
        [HttpGet]
        public ActionResult GetItemTypes()
        {
            var itemType = _dataContext.GetItemTypes();
            var itemTypesToReturn = _mapper.Map<IEnumerable<ItemTypeGetDto>>(itemType);

            if (itemTypesToReturn == null)
            {
                return NotFound("No item types found.");
            }
            else
            {
                return Ok(itemTypesToReturn);
            }
        }

        // GET api/itemtype/5
        [HttpGet("{id}")]
        public ActionResult GetItemType(int id)
        {
            var itemType = _dataContext.GetItemType(id);
            var itemTypeToReturn = _mapper.Map<ItemTypeGetDto>(itemType);

            if (itemTypeToReturn == null)
            {
                return NotFound($"No item type found for id: {id}");
            }
            else
            {
                return Ok(itemTypeToReturn);
            }
        }
    }
}