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

        public UnitTestsRepository(
            bool noCustomers = false,
            bool noFaults = false
            )
        {
            _customers = noCustomers == true ? new List<Customer>() : TestData.GetCustomers();
            _faults = noFaults == true ? new List<Fault>() : TestData.GetFaults();
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
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public ItemType GetItemType(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemType> GetItemTypes()
        {
            throw new NotImplementedException();
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
