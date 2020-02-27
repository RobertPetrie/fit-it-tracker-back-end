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

        /// <summary>
        /// Returns a list of item types : itemTypeId, name, model, manufacturer
        /// </summary>
        // GET api/itemtype
        [HttpGet]
        public ActionResult<IEnumerable<ItemType>> GetItemTypes()
        {
            var itemType = _dataContext.GetItemTypes();
            var itemTypesToReturn = _mapper.Map<IEnumerable<ItemTypeGetDto>>(itemType);

            if (itemTypesToReturn.Count() == 0)
            {
                return NotFound("No item types found.");
            }
            else
            {
                return Ok(itemTypesToReturn);
            }
        }

        /// <summary>
        /// Returns a item type by specific id : itemTypeId, name, model, manufacturer
        /// </summary>
        /// <param name="id">The Item Type id</param>
        // GET api/itemtype/5
        [HttpGet("{id}")]
        public ActionResult<ItemType> GetItemType(int id)
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