using fix_it_tracker_back_end.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fix_it_tracker_back_end_unit_tests.Repositories
{
    public static class TestData
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

        private static Customer GetCustomer(int id) => GetCustomers().FirstOrDefault(c => c.CustomerID == id);

        public static List<Fault> GetFaults()
        {
            return new List<Fault>()
            {
                new Fault
                {
                    FaultID = 1,
                    Name = "Battery",
                    Description = "The battery will not charge"
                },
                new Fault
                {
                    FaultID = 2,
                    Name = "Damaged Keyboard",
                    Description = "The keyboard has physical damage."
                },
                new Fault
                {
                    FaultID = 3,
                    Name = "Damaged Screen",
                    Description = "The screen has physical damage."
                },
                new Fault
                {
                    FaultID = 4,
                    Name = "Black Screen",
                    Description = "Will turn on but the screen is black."
                },
                new Fault
                {
                    FaultID = 5,
                    Name = "Dead",
                    Description = "Will not turn on."
                },
                new Fault
                {
                    FaultID = 6,
                    Name = "Hot",
                    Description = "Device gets very hot after being powered on."
                },
                new Fault
                {
                    FaultID = 7,
                    Name = "No Sound",
                    Description = "The device has no sound."
                }
            };
        }

        public static List<ItemType> GetItemTypes()
        {
            return new List<ItemType>()
            {
                new ItemType
                {
                    ItemTypeID = 1,
                    Name = "65 Inch  UHD HDR LED TV",
                    Model = "XBR65X950G",
                    Manufacturer = "Sony"
                },
                new ItemType
                {
                    ItemTypeID = 2,
                    Name = "G8X ThinQ Dual Screen",
                    Model = "LG G8X ThinQ",
                    Manufacturer = "LG"
                },
                new ItemType
                {
                    ItemTypeID = 3,
                    Name = "Galaxy Tab A 8",
                    Model = "SM-T290NZKAXAC",
                    Manufacturer = "Samsung"
                },
                new ItemType
                {
                    ItemTypeID = 4,
                    Name = "MacBook Pro 16 Inch",
                    Model = " MVVJ2LL/A",
                    Manufacturer = "Apple"
                },
                new ItemType
                {
                    ItemTypeID = 5,
                    Name = "OptiPlex",
                    Model = "3060 SFF",
                    Manufacturer = "Dell"
                }
            };
        }

        private static ItemType GetItemType(int id) => GetItemTypes().FirstOrDefault(i => i.ItemTypeID == id);

        public static List<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item
                {
                    ItemID = 1,
                    Serial = "116996",
                    ItemType = GetItemType(1)
                },
                new Item
                {
                    ItemID = 2,
                    Serial = "829684",
                    ItemType = GetItemType(2)
                },
                new Item
                {
                    ItemID = 3,
                    Serial = "582427",
                    ItemType = GetItemType(3)
                },
                new Item
                {
                    ItemID = 4,
                    Serial = "305892",
                    ItemType = GetItemType(4)
                },
                new Item
                {
                    ItemID = 5,
                    Serial = "991161",
                    ItemType = GetItemType(5)
                },
                new Item
                {
                    ItemID = 6,
                    Serial = "211020",
                    ItemType = GetItemType(1)
                },
                new Item
                {
                    ItemID = 7,
                    Serial = "962453",
                    ItemType = GetItemType(2)
                },
                new Item
                {
                    ItemID = 8,
                    Serial = "402389",
                    ItemType = GetItemType(3)
                },
                new Item
                {
                    ItemID = 9,
                    Serial = "688021",
                    ItemType = GetItemType(4)
                },
                new Item
                {
                    ItemID = 10,
                    Serial = "918294",
                    ItemType = GetItemType(5)
                }
            };
        }

        public static List<Repair> GetRepairs()
        {
            return new List<Repair>()
            {
                new Repair
                {
                    RepairID = 1,
                    DateOpened = DateTime.Parse("2019-10-19T15:58:43.3603324"),
                    Customer = GetCustomer(1)
                },
                new Repair
                {
                    RepairID = 2,
                    DateOpened = DateTime.Parse("2019-08-19T15:58:43.3613501"),
                    Customer = GetCustomer(2)
                },
                new Repair
                {
                    RepairID = 3,
                    DateOpened = DateTime.Parse("2019-10-19T15:58:43.3612719"),
                    DateCompleted = DateTime.Parse("2019-10-25T15:58:43.3612719"),
                    Customer = GetCustomer(3)
                },
                new Repair
                {
                    RepairID = 4,
                    DateOpened = DateTime.Parse("2019-12-19T15:58:43.3611711"),
                    DateCompleted = DateTime.Parse("2019-12-25T15:58:43.3611711"),
                    Customer = GetCustomer(4)
                },
                new Repair
                {
                    RepairID = 5,
                    DateOpened = DateTime.Parse("2019-05-19T15:58:43.3610984"),
                    Customer = GetCustomer(5)
                }
            };
        }
    }
}
