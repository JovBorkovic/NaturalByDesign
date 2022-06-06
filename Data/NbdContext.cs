using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NbdAplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NbdAplication.Data
{
    public class NbdContext: IdentityDbContext
    {
        public NbdContext (DbContextOptions<NbdContext> options)
            : base(options)
        {
        }

        public DbSet<NbdAplication.Models.Client> Client { get; set; }
        public DbSet<NbdAplication.Models.Projects> Projects { get; set; }
        public DbSet<NbdAplication.Models.Bids> Bids { get; set; }
        public DbSet<NbdAplication.Models.Inventory> Inventories { get; set; }
        public DbSet<NbdAplication.Models.Material> Materials { get; set; }
        public DbSet<NbdAplication.Models.Nbdstaff> Nbdstaffs { get; set; }
        public DbSet<NbdAplication.Models.Occupation> Occupations { get; set; }
        public DbSet<NbdAplication.Models.Labour> Labours { get; set; }
        public DbSet<NbdAplication.Models.Task> Tasks { get; set; }
        public DbSet<NbdAplication.Models.Sale> Sale { get; set; }
        public DbSet<NbdAplication.Models.Designer> Designer { get; set; }
        public DbSet<NbdAplication.Models.BidInventory> BidInventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Many to Many Intersection
            modelBuilder.Entity<BidInventory>()
            .HasKey(t => new { t.InventoryID, t.BidsID });

            modelBuilder.Entity<Bids>()
                .Property(E => E.EstAmount)
                .HasConversion<double>();
            modelBuilder.Entity<Bids>()
                .Property(E => E.ActAmount)
                .HasConversion<double>();

            //Add this so you don't get Cascade Delete
            modelBuilder.Entity<BidInventory>()
                .HasOne(pc => pc.Inventory)
                .WithMany(c => c.BidInventories)
                .HasForeignKey(pc => pc.InventoryID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
