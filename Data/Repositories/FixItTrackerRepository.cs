using fix_it_tracker_back_end.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Data.Repositories
{
    public class FixItTrackerRepository : IFixItTrackerRepository
    {
        private readonly DataContext _dataContext;

        public FixItTrackerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Customer GetCustomer(int id)
        {
            var customer = _dataContext.Customers.FirstOrDefault(c => c.CustomerID == id);
            return customer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            var customers = _dataContext.Customers;
            return customers;
        }

        public Fault GetFault(int id)
        {
            var fault = _dataContext.Faults.FirstOrDefault(f => f.FaultID == id);
            return fault;
        }

        public IEnumerable<Fault> GetFaults()
        {
            var faults = _dataContext.Faults;
            return faults;
        }

        public Item GetItem(int id)
        {
            var item = _dataContext.Items
                .Include(i => i.ItemType)
                .FirstOrDefault(i => i.ItemID == id);

            // use this to break the circular reference
            if (item != null)
            {
                if (item.ItemType != null)
                {
                    item.ItemType.Items = null;
                }
            }

            return item;
        }

        public IEnumerable<Item> GetItems()
        {
            var items = _dataContext.Items
                .Include(i => i.ItemType);

            // use this to break the circular reference
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.ItemType.Items = null;
                }
            }

            return items;
        }

        public ItemType GetItemType(int id)
        {
            var itemType = _dataContext.ItemTypes.FirstOrDefault(i => i.ItemTypeID == id);
            return itemType;
        }

        public IEnumerable<ItemType> GetItemTypes()
        {
            var itemTypes = _dataContext.ItemTypes;
            return itemTypes;
        }

        public Repair GetRepair(int id)
        {
            var repair = _dataContext.Repairs.FirstOrDefault(r => r.RepairID == id);
            return repair;
        }

        public IEnumerable<Repair> GetRepairs()
        {
            var repairs = _dataContext.Repairs;
            return repairs;
        }

        public IEnumerable<Repair> GetCustomerRepairs(int customerId)
        {
            var repairs = _dataContext.Repairs.Where(c => c.Customer.CustomerID == customerId);
            return repairs;
        }


        public Resolution GetResolution(int id)
        {
            var resolution = _dataContext.Resolutions.FirstOrDefault(r => r.ResolutionID == id);
            return resolution;
        }

        public IEnumerable<Resolution> GetResolutions()
        {
            var resolutions = _dataContext.Resolutions;
            return resolutions;
        }

        public Customer AddCustomer(Customer customer)
        {
            _dataContext.Add(customer);
            _dataContext.SaveChanges();
            return customer;
        }

        public bool CustomerExists(Customer customer) =>
            _dataContext.Customers.Any(c => c.Name.ToUpper() == customer.Name.ToUpper()) ? true : false;

        public Fault AddFault(Fault fault)
        {
            _dataContext.Add(fault);
            _dataContext.SaveChanges();
            return fault;
        }

        public bool FaultExists(Fault fault) =>
            _dataContext.Faults.Any(f => f.Name.ToUpper() == fault.Name.ToUpper()) ? true : false;

        public ItemType AddItemType(ItemType itemType)
        {
            _dataContext.Add(itemType);
            _dataContext.SaveChanges();
            return itemType;
        }

        public bool ItemTypeExists(ItemType itemType)
        {
            if (_dataContext.ItemTypes.Any(
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

    }
}
