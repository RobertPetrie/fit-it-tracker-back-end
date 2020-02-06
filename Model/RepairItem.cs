using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Model
{
    public class RepairItem
    {
        public int RepairItemID { get; set; }

        [Required]
        public Repair Repair { get; set; }

        [Required]
        public Item Item { get; set; }

        public DateTime? DateRepaired { get; set; }

        public DateTime? DateShipped { get; set; }

        [MaxLength(50)]
        public string CourierTrackingID { get; set; }

        public ICollection<RepairItemFault> RepairItemFaults { get; set; }

        public ICollection<RepairItemResolution> RepairItemResolutions { get; set; }
    }
}
