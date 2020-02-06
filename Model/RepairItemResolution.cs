using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Model
{
    //Note: this is a many-to-many 
    public class RepairItemResolution
    {
        public int RepairItemID { get; set; }
        public int ResolutionID { get; set; }

        public RepairItem RepairItem { get; set; }
        public Resolution Resolution { get; set; }
    }
}
