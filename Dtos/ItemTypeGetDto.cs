using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Dtos
{
    public class ItemTypeGetDto
    {
        [JsonPropertyName("itemTypeId")]
        public int ItemTypeID { get; set; }

        [JsonPropertyName("name")]
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [JsonPropertyName("model")]
        [Required, MaxLength(30)]
        public string Model { get; set; }

        [JsonPropertyName("manufacturer")]
        [Required, MaxLength(50)]
        public string Manufacturer { get; set; }
    }
}
