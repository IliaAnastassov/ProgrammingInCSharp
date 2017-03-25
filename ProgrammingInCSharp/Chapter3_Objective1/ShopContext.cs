namespace Chapter3_Objective1
{
    using System.Data.Entity;

    public class ShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasRequired(c => c.BillingAddress)
                                           .WithMany()
                                           .WillCascadeOnDelete(false);
        }
    }
}
