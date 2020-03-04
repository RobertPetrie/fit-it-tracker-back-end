using fix_it_tracker_back_end.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Data.Repositories
{
    public interface IFixItTrackerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(int id);

        IEnumerable<Fault> GetFaults();
        Fault GetFault(int id);

        IEnumerable<Item> GetItems();
        Item GetItem(int id);

        IEnumerable<ItemType> GetItemTypes();
        ItemType GetItemType(int id);

        IEnumerable<Repair> GetRepairs();
        IEnumerable<Repair> GetCustomerRepairs(int id);
        Repair GetRepair(int id);

        IEnumerable<Resolution> GetResolutions();
        Resolution GetResolution(int id);

        Customer AddCustomer(Customer customer);
        bool CustomerExists(Customer customer);

        Fault AddFault(Fault fault);
        bool FaultExists(Fault fault);
    }
}
