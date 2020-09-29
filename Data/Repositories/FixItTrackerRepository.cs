using fix_it_tracker_back_end.Helpers;
using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end.Model.BindingTargets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace fix_it_tracker_back_end.Data.Repositories
{
    public class FixItTrackerRepository : IFixItTrackerRepository
    {
        private readonly DataContext _dataContext;

        private UserManager<IdentityUser> userManager;

        private SignInManager<IdentityUser> signInManager;

        public FixItTrackerRepository(DataContext dataContext, IServiceProvider provider)
        {
            _dataContext = dataContext;
            userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            signInManager = provider.GetRequiredService<SignInManager<IdentityUser>>();
        }

        public Customer GetCustomer(int id)
        {
            var customer = _dataContext.Customers.FirstOrDefault(c => c.CustomerID == id);
            return customer;
        }

        public IEnumerable<Customer> GetCustomers(string name, string city, string province)
        {
            IQueryable<Customer> customers = _dataContext.Customers;

            if (!string.IsNullOrWhiteSpace(name))
            {
                customers = customers.Where(c => c.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                customers = customers.Where(c => c.City.ToLower().Contains(city.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(province))
            {
                customers = customers.Where(c => c.Province.ToLower().Contains(province.ToLower()));
            }

            customers = customers.OrderBy(c => c.Name);

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

        public IEnumerable<Repair> GetRepairs(DateTime? dateOpened, DateTime? dateCompleted, int? pageNumber, int? pageSize)
        {
            IQueryable<Repair> repairs = _dataContext.Repairs;

            repairs = _dataContext.Repairs;

            if (dateOpened != null)
            {
                repairs = repairs.Where(r => r.DateOpened >= dateOpened);
            }

            if (dateCompleted != null)
            {
                repairs = repairs.Where(r => r.DateCompleted <= dateCompleted);
            }

            var currentPageNumer = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 10;

            return repairs.Skip((currentPageNumer - 1) * currentPageSize).Take(currentPageSize);
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

        public void RemoveFault(Fault fault)
        {
            _dataContext.Remove(fault);
            _dataContext.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            _dataContext.Remove(customer);
            _dataContext.SaveChanges();
        }

        public async Task<bool> DoLogin(Login login)
        {
            IdentityUser identityUser = await userManager.FindByNameAsync(login.Name);

            if (identityUser != null)
            {
                await signInManager.SignOutAsync();
                SignInResult signInResult = await signInManager.PasswordSignInAsync(identityUser, login.Password, false, false);
                return signInResult.Succeeded;
            }

            return false;
        }

        public async Task<bool> DoLogout(Login login)
        {
            IdentityUser identityUser = await userManager.FindByNameAsync(login.Name);

            if (identityUser != null)
            {
                await signInManager.SignOutAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CreateAccount(Login login)
        {
            IdentityUser identityUser = new IdentityUser(login.Name);
            identityUser.Email = login.Email;

            IdentityResult identityResult = await userManager.CreateAsync(identityUser, login.Password);

            return identityResult.Succeeded;
        }

        public async Task<bool> UpdateAccountName(Login login)
        {
            IdentityUser identityUser = await userManager.FindByNameAsync(login.Name);

            if (identityUser != null && login.Name != IdentitySeedData.adminUser)
            {
                IdentityUser identityUserExists = await userManager.FindByNameAsync(login.NewName);

                if (identityUserExists == null)
                {
                    IdentityResult identityResult = await userManager.SetUserNameAsync(identityUser, login.NewName);

                    return identityResult.Succeeded;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAccountPassword(Login login)
        {
            IdentityUser identityUser = await userManager.FindByNameAsync(login.Name);

            if (identityUser != null)
            {
                bool validPassword = await userManager.CheckPasswordAsync(identityUser, login.Password);

                if (validPassword)
                {
                    IdentityResult identityResult = await userManager.ChangePasswordAsync(identityUser, login.Password, login.NewPassword);

                    return identityResult.Succeeded;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> AddToAdminRole(Login login)
        {
            IdentityUser identityUser = await userManager.FindByNameAsync(login.Name);

            if (identityUser != null)
            {
                IdentityResult identityResult = await userManager.AddToRoleAsync(identityUser, UserRoles.AdminAccess);

                return identityResult.Succeeded;
            }

            return false;
        }

        public async Task<bool> RemoveAccount(Login login)
        {
            IdentityUser identityUser = await userManager.FindByNameAsync(login.Name);

            if (identityUser != null && login.Name != IdentitySeedData.adminUser)
            {
                IdentityResult identityResult = await userManager.DeleteAsync(identityUser);

                return identityResult.Succeeded;
            }

            return false;
        }

        public IEnumerable<string> GetAllAccounts()
        {
            return userManager.Users.Select(x => x.UserName).ToList();
        }
    }
}
