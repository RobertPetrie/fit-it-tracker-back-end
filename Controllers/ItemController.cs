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
    public class ItemController : ControllerBase
    {
        private DataContext _dataContext;

        public ItemController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET api/item
        [HttpGet]
        public ActionResult GetItems()
        {
            var items = _dataContext.Items
                .Include(i => i.ItemType);

            // use this to break the circular reference
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.ItemType.Items = null;
                }
            }

            if (items == null)
            {
                return NotFound("No items found.");
            }
            else
            {
                return Ok(items);
            }
        }

        // GET api/item/5
        [HttpGet("{id}")]
        public ActionResult GetItem(int id)
        {
            var item = _dataContext.Items
                .Include(i => i.ItemType)
                .FirstOrDefault(i => i.ItemID == id);

            // use this to break the circular reference
            if (item != null)
            {
                if (item.ItemType != null)
                {
                    item.ItemType.Items = null;
                }
            }

            if (item == null)
            {
                return NotFound($"No item found for id: {id}");
            }
            else
            {
                return Ok(item);
            }
        }
    }
}
