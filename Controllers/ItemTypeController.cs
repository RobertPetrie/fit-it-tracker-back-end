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

        /// <summary>
        /// Creates a single item type.
        /// </summary>
        /// <param name="itemTypeData">The item type object that you want to create.</param>
        /// <returns>A message confirming the item type has been created</returns>
        // POST api/itemtype
        [HttpPost]
        public ActionResult CreateItemType([FromBody] ItemTypeData itemTypeData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_dataContext.ItemTypeExists(itemTypeData.ItemType))
            {
                return BadRequest("Item Type name already exists");
            }

            var itemType = _dataContext.AddItemType(itemTypeData.ItemType);

            var uri = Request != null ? Request.GetDisplayUrl().ToString() + itemType.ItemTypeID : "";

            return Created(uri, "Item Type Created");
        }

        /// <summary>
        /// Replace a value of a specific property for a single item type.
        /// </summary>
        /// <param name="id">The existing item type id.</param>
        /// <param name="itemTypeData">The item type object the values that you want updated.</param>
        /// <returns>Returns OK with a confirmation message</returns>
        /// PUT api/itemtype/5
        [HttpPut("{id}")]
        public ActionResult ReplaceItemType(int id, [FromBody] ItemTypeData itemTypeData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingItemType = _dataContext.GetItemType(id);

            if (existingItemType == null)
            {
                return BadRequest($"Item Type ID: {id} doesn't exist");
            }

            _dataContext.ReplaceItemType(id, itemTypeData.ItemType);

            return Ok("The Item Type has been updated.");
        }

    }
}