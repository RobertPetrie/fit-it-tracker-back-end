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

        public UnitTestsRepository(
            bool noCustomers = false,
            bool noFaults = false,
            bool noItemTypes = false,
            bool noItems = false
            )
        {
            _customers = noCustomers == true ? new List<Customer>() : TestData.GetCustomers();
            _faults = noFaults == true ? new List<Fault>() : TestData.GetFaults();
            _itemTypes = noItemTypes == true ? new List<ItemType>() : TestData.GetItemTypes();
            _items = noItems == true ? new List<Item>() : TestData.GetItems();
        }

        public Customer GetCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerID == id);
            return customer;
        }

        public IEnumerable<Repair> GetCustomerRepairs(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IEnumerable<Repair> GetRepairs()
        {
            throw new NotImplementedException();
        }

        public Resolution GetResolution(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resolution> GetResolutions()
        {
            throw new NotImplementedException();
        }
    }
}
