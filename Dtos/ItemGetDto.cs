using fix_it_tracker_back_end.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Dtos
{
    public class ItemGetDto
    {
        [JsonPropertyName("itemId")]
        public int ItemID { get; set; }

        [JsonPropertyName("serial")]
        public string Serial { get; set; }

        [JsonPropertyName("itemType")]
        public ItemTypeGetDto ItemType { get; set; }
    }
}
