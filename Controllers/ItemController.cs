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
using Microsoft.EntityFrameworkCore;

namespace fix_it_tracker_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IFixItTrackerRepository _dataContext;
        private readonly IMapper _mapper;

        public ItemController(IFixItTrackerRepository dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of items : itemId, serial, itemType{ itemTypeId, name, model, manufacturer }
        /// </summary>
        // GET api/item
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            var items = _dataContext.GetItems();

            var itemsToReturn = _mapper.Map<IEnumerable<ItemGetDto>>(items);

            if (itemsToReturn.Count() == 0)
            {
                return NotFound("No items found.");
            }
            else
            {
                return Ok(itemsToReturn);
            }
        }

        /// <summary>
        /// Returns a item by specific id : itemId, serial, itemType{ itemTypeId, name, model, manufacturer }
        /// </summary>
        /// <param name="id">The item id</param>
        // GET api/item/5
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            var item = _dataContext.GetItem(id);

            var itemToReturn = _mapper.Map<ItemGetDto>(item);

            if (itemToReturn == null)
            {
                return NotFound($"No item found for id: {id}");
            }
            else
            {
                return Ok(itemToReturn);
            }
        }
    }
}
