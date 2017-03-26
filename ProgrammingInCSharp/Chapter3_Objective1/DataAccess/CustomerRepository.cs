namespace Chapter3_Objective1.DataAccess
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class CustomerRepository : IRepository<Customer>
    {
        public void AddEntity(Customer entity)
        {
            using (var context = new ShopContext())
            {
                context.Customers.Add(entity);
                context.SaveChanges();
            }
        }

        public IEnumerable<Customer> GetAllEntities()
        {
            IEnumerable<Customer> customers;

            using (var context = new ShopContext())
            {
                customers = context.Customers.Include(c => c.BillingAddress)
                                             .ToList();
            }

            return customers;
        }

        public Customer GetEntity(int id)
        {
            Customer customer;

            using (var context = new ShopContext())
            {
                customer = context.Customers.Include(c => c.BillingAddress)
                                            .Where(c => c.Id == id)
                                            .FirstOrDefault();
            }

            return customer;
        }
    }
}
