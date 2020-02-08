using fix_it_tracker_back_end.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Models
{
    public static class SeedData
    {
        private static Random random = new Random();

        public static void SeedCustomers(DataContext dataContext)
        {
            if (!dataContext.Customers.Any())
            {
                var data = File.ReadAllText("Data/CustomersSeedData.json");
                var items = JsonConvert.DeserializeObject<List<Customer>>(data);

                foreach (var item in items)
                    dataContext.Add(item);

                dataContext.SaveChanges();
            }
        }

        public static void SeedFaults(DataContext dataContext)
        {
            if (!dataContext.Faults.Any())
            {
                var data = File.ReadAllText("Data/FaultsSeedData.json");
                var items = JsonConvert.DeserializeObject<List<Fault>>(data);

                foreach (var item in items)
                    dataContext.Add(item);

                dataContext.SaveChanges();
            }
        }

        public static void SeedItemTypes(DataContext dataContext)
        {
            if (!dataContext.ItemTypes.Any())
            {
                var data = File.ReadAllText("Data/ItemTypesSeedData.json");
                var items = JsonConvert.DeserializeObject<List<ItemType>>(data);

                foreach (var item in items)
                    dataContext.Add(item);

                dataContext.SaveChanges();
            }
        }

        public static void SeedResolutions(DataContext dataContext)
        {
            if (!dataContext.Resolutions.Any())
            {
                var data = File.ReadAllText("Data/ResolutionsSeedData.json");
                var items = JsonConvert.DeserializeObject<List<Resolution>>(data);

                foreach (var item in items)
                    dataContext.Add(item);

                dataContext.SaveChanges();
            }
        }

        public static void SeedRepairs(DataContext dataContext)
        {
            if (!dataContext.Repairs.Any())
            {
                var customers = dataContext.Customers.ToList();

                foreach (var customer in customers)
                {

                    for (int i = 0; i < 4; i++)
                    {
                        Repair repair = new Repair();
                        repair.DateOpened = DateTime.Now.AddMonths(random.Next(-12, -1));

                        if (random.Next(1, 1000) % 2 == 0)
                        {
                            repair.DateCompleted = repair.DateOpened.AddDays(random.Next(1, 7));
                        }

                        repair.Customer = customer;

                        dataContext.Add(repair);
                    }
                }

                dataContext.SaveChanges();
            }
        }

        public static void SeedItems(DataContext dataContext)
        {
            if (!dataContext.Items.Any())
            {
                var randomSerials = new List<string>();

                for (int i = 0; i <= 99; i++)
                {
                    var randomSerial = random.Next(100000, 999999).ToString();
                    var executeAgain = true;

                    do
                    {
                        if (!randomSerials.Contains(randomSerial))
                        {
                            randomSerials.Add(randomSerial);
                            executeAgain = false;
                        }
                    } while (executeAgain);
                }

                var itemTypes = dataContext.ItemTypes.ToList();

                foreach (var serial in randomSerials)
                {
                    Item item = new Item();
                    item.ItemType = itemTypes[random.Next(itemTypes.Count)];
                    item.Serial = serial;
                    dataContext.Add(item);
                }

                dataContext.SaveChanges();
            }
        }

        public static void SeedRepairItems(DataContext dataContext)
        {
            if (!dataContext.RepairItems.Any())
            {
                var repairs = dataContext.Repairs.ToList();
                var items = dataContext.Items.ToList();
                var repairIndex = 0;

                foreach (var item in items)
                {
                    var repairItem = new RepairItem();
                    repairItem.Repair = repairs[repairIndex];
                    repairItem.Item = item;

                    if (repairItem.Repair.DateCompleted.HasValue)
                    {
                        repairItem.DateRepaired = repairItem.Repair.DateCompleted;
                        repairItem.DateShipped = repairItem.Repair.DateCompleted;
                        repairItem.CourierTrackingID = random.Next(10000000, 99999999).ToString();
                    }

                    dataContext.Add(repairItem);

                    if (repairIndex < 19)
                    {
                        repairIndex++;
                    }
                    else
                    {
                        repairIndex = 0;
                    }
                }

                dataContext.SaveChanges();
            }
        }

        public static void SeedRepairItemFaultsAndResolutions(DataContext dataContext)
        {
            if (!dataContext.RepairItemFaults.Any())
            {
                var repairs = dataContext.Repairs.ToList();
                var repairItems = dataContext.RepairItems.ToList();
                var faults = dataContext.Faults.ToList();
                var resolutions = dataContext.Resolutions.ToList();

                foreach (var repairItem in repairItems)
                {
                    var numberOfFaults = random.Next(1, 4);

                    for (int i = 0; i < numberOfFaults; i++)
                    {
                        var randomFault = faults[random.Next(0, faults.Count - 1)];

                        var repairItemFault = new RepairItemFault
                        {
                            RepairItemID = repairItem.RepairItemID,
                            FaultID = randomFault.FaultID,
                            RepairItem = repairItem,
                            Fault = randomFault
                        };

                        if (repairItem.RepairItemFaults == null ||
                            !repairItem.RepairItemFaults.Any(x => x.FaultID == repairItemFault.FaultID))
                        {
                            dataContext.Add(repairItemFault);
                        }

                        if (repairItem.Repair.DateCompleted.HasValue)
                        {
                            var randomResolution = resolutions[random.Next(0, resolutions.Count - 1)];

                            var repairItemResolution = new RepairItemResolution
                            {
                                RepairItemID = repairItem.RepairItemID,
                                ResolutionID = randomResolution.ResolutionID,
                                RepairItem = repairItem,
                                Resolution = randomResolution
                            };

                            if (repairItem.RepairItemResolutions == null ||
                                !repairItem.RepairItemResolutions.Any(x => x.ResolutionID == repairItemResolution.ResolutionID))
                            {
                                dataContext.Add(repairItemResolution);
                            }
                        }
                    }
                }

                dataContext.SaveChanges();
            }
        }

    }
}
