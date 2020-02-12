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
    public class ItemTypeController : ControllerBase
    {
        private DataContext _dataContext;

        public ItemTypeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET api/itemtype
        [HttpGet]
        public ActionResult GetItemTypes()
        {
            var itemType = _dataContext.ItemTypes;

            if (itemType == null)
            {
                return NotFound("No item types found.");
            }
            else
            {
                return Ok(itemType);
            }
        }

        // GET api/itemtype/5
        [HttpGet("{id}")]
        public ActionResult GetItemType(int id)
        {
            var itemType = _dataContext.ItemTypes.FirstOrDefault(i => i.ItemTypeID == id);

            if (itemType == null)
            {
                return NotFound($"No item type found for id: {id}");
            }
            else
            {
                return Ok(itemType);
            }
        }
    }
}