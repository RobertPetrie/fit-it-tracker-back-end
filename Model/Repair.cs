using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Model
{
    public class Repair
    {
        public int RepairID { get; set; }

        public DateTime DateOpened { get; set; }

        public DateTime? DateCompleted { get; set; }

        [Required]
        public Customer Customer { get; set; }

        public IEnumerable<RepairItem> RepairItems { get; set; }
    }
}
