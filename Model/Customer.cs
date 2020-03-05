using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Model
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required, MaxLength(50)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please specify a name that is between 3 and 50 characters")]
        public string Name { get; set; }

        [MaxLength(60)]
        public string Address { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(15)]
        public string City { get; set; }

        [MaxLength(15)]
        public string Province { get; set; }

        public IEnumerable<Repair> Repairs { get; set; }
    }
}
