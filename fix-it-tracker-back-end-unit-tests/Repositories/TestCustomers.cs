using fix_it_tracker_back_end.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace fix_it_tracker_back_end_unit_tests.Repositories
{
    public static class TestCustomers
    {
        public static List<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
                new Customer
                {
                    CustomerID = 1,
                    Name = "Trillum Health Partners - Credit Valley",
                    Address = "2200 Eglinton Ave W",
                    PostalCode = "L5M 2N1",
                    City = "Mississauga",
                    Province = "ON"
                },
                new Customer
                {
                    CustomerID = 2,
                    Name = "The Rosedale Day School",
                    Address = "131 Bloor St W #426",
                    PostalCode = "M5S 1R1",
                    City = "Toronto",
                    Province = "ON"
                },
                new Customer
                {
                    CustomerID = 3,
                    Name = "Marc-Favreau Library",
                    Address = "500 Boulevard Rosemont",
                    PostalCode = "H2S 0C4",
                    City = "Montréal",
                    Province = "QC"
                },
                new Customer
                {
                    CustomerID = 4,
                    Name = "Samesun Vancouver Hotel",
                    Address = "1018 Granville St",
                    PostalCode = "V6Z 1L5",
                    City = "Vancouver",
                    Province = "BC"
                },
                new Customer
                {
                    CustomerID = 5,
                    Name = "MacEwan University",
                    Address = "10700 104 Ave NW",
                    PostalCode = "T5J 4S2",
                    City = "Edmonton",
                    Province = "AB"
                },
                new Customer
                {
                    CustomerID = 6,
                    Name = "Carey Price",
                    Address = "123 Best Goalie Ever Drive",
                    PostalCode = "G0A 4V0",
                    City = "Montréal",
                    Province = "QC"
                }
            };
        }
    }
}
