using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Model
{
    public class ItemType
    {
        public int ItemTypeID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        public string Model { get; set; }

        [Required, MaxLength(50)]
        public string Manufacturer { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}
