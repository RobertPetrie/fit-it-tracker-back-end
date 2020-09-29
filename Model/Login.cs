using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Model
{
    public class Login
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string NewName { get; set; }

        public string NewPassword { get; set; }
    }
}
