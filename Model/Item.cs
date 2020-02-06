using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Model
{
    public class Item
    {
        public int ItemID { get; set; }

        [Required, MaxLength(50)]
        public string Serial { get; set; }

        [Required]
        public ItemType ItemType { get; set; }

        public IEnumerable<RepairItem> RepairItems { get; set; }
    }
}
