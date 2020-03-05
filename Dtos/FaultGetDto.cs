using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace fix_it_tracker_back_end.Dtos
{
    public class FaultGetDto
    {
        [JsonPropertyName("faultId")]
        public int FaultID { get; set; }

        [JsonPropertyName("faultName")]
        public string Name { get; set; }

        [JsonPropertyName("faultDescription")]
        public string Description { get; set; }
    }
}
