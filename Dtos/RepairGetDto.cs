using fix_it_tracker_back_end.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Dtos
{
    public class RepairGetDto
    {
        [JsonPropertyName("repairId")]
        public int RepairID { get; set; }

        [JsonPropertyName("repairDateOpened")]
        public DateTime DateOpened { get; set; }

        [JsonPropertyName("repairDateCompleted")]
        public DateTime? DateCompleted { get; set; }
    }
}
