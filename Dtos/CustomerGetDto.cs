using fix_it_tracker_back_end.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace fix_it_tracker_back_end.Dtos
{
    public class CustomerGetDto
    {
        [JsonPropertyName("customerId")]
        public int CustomerID { get; set; }

        [JsonPropertyName("customerName")]
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [JsonPropertyName("customerAddress")]
        [MaxLength(60)]
        public string Address { get; set; }

        [JsonPropertyName("customerPostalCode")]
        [MaxLength(10)]
        public string PostalCode { get; set; }

        [JsonPropertyName("customerCity")]
        [MaxLength(15)]
        public string City { get; set; }

        [JsonPropertyName("customerProvince")]
        [MaxLength(15)]
        public string Province { get; set; }
    }
}
