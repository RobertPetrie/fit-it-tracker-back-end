using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Model
{
    //Note: this is a many-to-many 
    public class RepairItemFault
    {
        public int RepairItemID { get; set; }
        public int FaultID { get; set; }

        public RepairItem RepairItem { get; set; }
        public Fault Fault { get; set; }
    }
}
