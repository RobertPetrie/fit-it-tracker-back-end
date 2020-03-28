using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end.Model.BindingTargets;
using Microsoft.AspNetCore.JsonPatch;
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

        public Item AddItem(Item item)
        {
            _dataContext.Add(item);
            _dataContext.SaveChanges();
            return item;
        }

        public bool ItemExists(Item item)
        {
            if (_dataContext.Items.Any(
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

        public Resolution AddResolution(Resolution resolution)
        {
            _dataContext.Add(resolution);
            _dataContext.SaveChanges();
            return resolution;
        }

        public bool ResolutionExists(Resolution resolution) =>
            _dataContext.Resolutions.Any(r => r.Name.ToUpper() == resolution.Name.ToUpper()) ? true : false;

        public Repair AddRepair(Repair repair)
        {
            _dataContext.Add(repair);
            _dataContext.SaveChanges();
            return repair;
        }

        public void ReplaceCustomer(int customerId, Customer customer)
        {
            var customerToUpdate = _dataContext.Customers.Find(customerId);

            customerToUpdate.Name = customer.Name;
            customerToUpdate.Address = customer.Address;
            customerToUpdate.PostalCode = customer.PostalCode;
            customerToUpdate.City = customer.City;
            customerToUpdate.Province = customer.Province;

            _dataContext.Update(customerToUpdate);
            _dataContext.SaveChanges();
        }

        public void ReplaceItemType(int itemTypeId, ItemType itemType)
        {
            var itemTypeToUpdate = _dataContext.ItemTypes.Find(itemTypeId);

            itemTypeToUpdate.Name = itemType.Name;
            itemTypeToUpdate.Model = itemType.Model;
            itemTypeToUpdate.Manufacturer = itemType.Manufacturer;

            _dataContext.Update(itemTypeToUpdate);
            _dataContext.SaveChanges();
        }

        public void UpdateRepair(Repair repair)
        {
            _dataContext.Update(repair);
            _dataContext.SaveChanges();
        }
    }
}
