using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end_unit_tests.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fix_it_tracker_back_end_unit_tests
{
    public class UnitTestsRepository : IFixItTrackerRepository
    {
        private List<Customer> _customers;
        private List<Fault> _faults;
        private List<ItemType> _itemTypes;
        private List<Item> _items;
        private List<Repair> _repairs;
        private List<Resolution> _resolutions;

        public UnitTestsRepository(
            bool noCustomers = false,
            bool noFaults = false,
            bool noItemTypes = false,
            bool noItems = false,
            bool noRepairs = false,
            bool noResolutions = false
            )
        {
            _customers = noCustomers == true ? new List<Customer>() : TestData.GetCustomers();
            _faults = noFaults == true ? new List<Fault>() : TestData.GetFaults();
            _itemTypes = noItemTypes == true ? new List<ItemType>() : TestData.GetItemTypes();
            _items = noItems == true ? new List<Item>() : TestData.GetItems();
            _repairs = noRepairs == true ? new List<Repair>() : TestData.GetRepairs();
            _resolutions = noResolutions == true ? new List<Resolution>() : TestData.GetResolutions();
        }

        public Customer AddCustomer(Customer customer)
        {
            customer.CustomerID = GetCustomers().LastOrDefault().CustomerID++;
            _customers.Add(customer);
            return customer;
        }

        public Fault AddFault(Fault fault)
        {
            fault.FaultID = GetFaults().LastOrDefault().FaultID++;
            _faults.Add(fault);
            return fault;
        }

        public ItemType AddItemType(ItemType itemType)
        {
            itemType.ItemTypeID = GetItemTypes().LastOrDefault().ItemTypeID++;
            _itemTypes.Add(itemType);
            return itemType;
        }

        public Item AddItem(Item item)
        {
            item.ItemID = GetItems().LastOrDefault().ItemID++;
            _items.Add(item);
            return item;
        }

        public Resolution AddResolution(Resolution resolution)
        {
            resolution.ResolutionID = GetResolutions().LastOrDefault().ResolutionID++;
            _resolutions.Add(resolution);
            return resolution;
        }

        public Repair AddRepair(Repair repair)
        {
            repair.RepairID = GetRepairs().LastOrDefault().RepairID++;
            _repairs.Add(repair);
            return repair;
        }

        public bool CustomerExists(Customer customer) =>
            _customers.Any(c => c.Name.ToUpper() == customer.Name.ToUpper()) ? true : false;

        public bool FaultExists(Fault fault) =>
            _faults.Any(f => f.Name.ToUpper() == fault.Name.ToUpper()) ? true : false;

        public bool ItemTypeExists(ItemType itemType)
        {
            if (_itemTypes.Any(
                i => i.Name.ToUpper() == itemType.Name.ToUpper() &&
                i.Model.ToUpper() == itemType.Model.ToUpper() &&
                i.Manufacturer.ToUpper() == itemType.Manufacturer.ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ItemExists(Item item)
        {
            if (_items.Any(
                i => i.Serial.ToUpper() == item.Serial.ToUpper() &&
                i.ItemType.ItemTypeID == item.ItemType.ItemTypeID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ResolutionExists(Resolution resolution) =>
             _resolutions.Any(r => r.Name.ToUpper() == resolution.Name.ToUpper()) ? true : false;

        public Customer GetCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerID == id);
            return customer;
        }

        public IEnumerable<Repair> GetCustomerRepairs(int id)
        {
            return _repairs.Where(c => c.Customer.CustomerID == id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customers;
        }

        public Fault GetFault(int id)
        {
            var fault = _faults.FirstOrDefault(f => f.FaultID == id);
            return fault;
        }

        public IEnumerable<Fault> GetFaults()
        {
            return _faults;
        }

        public Item GetItem(int id)
        {
            var item = _items.FirstOrDefault(i => i.ItemID == id);
            return item;
        }

        public IEnumerable<Item> GetItems()
        {
            return _items;
        }

        public ItemType GetItemType(int id)
        {
            var itemType = _itemTypes.FirstOrDefault(i => i.ItemTypeID == id);
            return itemType;
        }

        public IEnumerable<ItemType> GetItemTypes()
        {
            return _itemTypes;
        }

        public Repair GetRepair(int id)
        {
            var repair = _repairs.FirstOrDefault(r => r.RepairID == id);
            return repair;
        }

        public IEnumerable<Repair> GetRepairs()
        {
            return _repairs;
        }

        public Resolution GetResolution(int id)
        {
            var resolution = _resolutions.FirstOrDefault(r => r.ResolutionID == id);
            return resolution;
        }

        public IEnumerable<Resolution> GetResolutions()
        {
            return _resolutions;
        }

        public void ReplaceCustomer(int id, Customer customer)
        {
            var customerToReplace = _customers.SingleOrDefault(c => c.CustomerID == id);

            customerToReplace.Name = customer.Name;
            customerToReplace.Address = customer.Address;
            customerToReplace.PostalCode = customer.PostalCode;
            customerToReplace.City = customer.City;
            customerToReplace.Province = customer.Province;
        }

        public void ReplaceItemType(int itemTypeId, ItemType itemType)
        {
            var itemTypeToReplace = _itemTypes.SingleOrDefault(i => i.ItemTypeID == itemTypeId);

            itemTypeToReplace.Name = itemType.Name;
            itemTypeToReplace.Model = itemType.Model;
            itemTypeToReplace.Manufacturer = itemType.Manufacturer;
        }
    }
}
