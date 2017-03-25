namespace Chapter3_Objective1
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ShopContext())
            {
                var address = new Address
                {
                    AddressLine1 = "Baker Street",
                    AddressLine2 = "Second floor",
                    City = "London",
                    ZipCode = "6666DC"
                };

                var customer = new Customer
                {
                    //FirstName = "Dildo",
                    LastName = "Schwaggins",
                    BillingAddress = address,
                    ShippingAddress = address
                };

                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }
    }
}
