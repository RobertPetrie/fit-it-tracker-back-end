using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end.Model.BindingTargets;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Data.Repositories
{
    public interface IFixItTrackerRepository
    {
        IEnumerable<Customer> GetCustomers(string name, string city, string province);
        Customer GetCustomer(int id);

        IEnumerable<Fault> GetFaults();
        Fault GetFault(int id);

        IEnumerable<Item> GetItems();
        Item GetItem(int id);

        IEnumerable<ItemType> GetItemTypes();
        ItemType GetItemType(int id);

        IEnumerable<Repair> GetRepairs(DateTime? dateOpented, DateTime? dateCompleted, int? pageNumber, int? pageSize);
        IEnumerable<Repair> GetCustomerRepairs(int id);
        Repair GetRepair(int id);

        IEnumerable<Resolution> GetResolutions();
        Resolution GetResolution(int id);

        Customer AddCustomer(Customer customer);
        bool CustomerExists(Customer customer);

        Fault AddFault(Fault fault);
        bool FaultExists(Fault fault);

        ItemType AddItemType(ItemType itemType);
        bool ItemTypeExists(ItemType itemType);

        Item AddItem(Item item);
        bool ItemExists(Item item);

        Resolution AddResolution(Resolution resolution);
        bool ResolutionExists(Resolution resolution);

        Repair AddRepair(Repair repair);

        void ReplaceCustomer(int customerId, Customer customer);

        void ReplaceItemType(int itemTypeId, ItemType itemType);

        void UpdateRepair(Repair repair);

        void RemoveFault(Fault fault);

        void RemoveCustomer(Customer customer);
    }
}
