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
        public string Name { get; set; }

        [JsonPropertyName("customerAddress")]
        public string Address { get; set; }

        [JsonPropertyName("customerPostalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("customerCity")]
        public string City { get; set; }

        [JsonPropertyName("customerProvince")]
        public string Province { get; set; }
    }
}
