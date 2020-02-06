using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Fault> Faults { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<RepairItem> RepairItems { get; set; }
        public DbSet<Resolution> Resolutions { get; set; }
        public DbSet<RepairItemFault> RepairItemFaults { get; set; }
        public DbSet<RepairItemResolution> RepairItemResolutions { get; set; }

        //Note: you have to manually enter this fluent API to setup the many-to-many relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepairItemFault>()
                .HasKey(rif => new { rif.RepairItemID, rif.FaultID });

            modelBuilder.Entity<RepairItemResolution>()
                .HasKey(rir => new { rir.RepairItemID, rir.ResolutionID });
        }
    }
}
