namespace MVVMNovaPoshta.DAL.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NpDbContext : DbContext
    {
        public NpDbContext()
            : base("name=NpDbContext")
        {
        }

        public virtual DbSet<Delivery> Delivery { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Delivery>()
                .Property(e => e.DeliveryDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.CustomerPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.TTN)
                .IsUnicode(false);
        }
    }
}
