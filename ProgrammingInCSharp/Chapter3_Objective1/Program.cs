namespace Chapter3_Objective1
{
    using System;
    using DataAccess;
    using Models;
    using Validation;

    class Program
    {
        static void Main(string[] args)
        {
            ValidateAddress();
        }

        private static void ValidateAddress()
        {
            var address = new Address
            {
                AddressLine1 = "Small Street",
                City = "London",
                ZipCode = "66"
            };

            var results = EntityValidator<Address>.Validate(address);

            foreach (var result in results)
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }

        private static void ValidateCustomer()
        {
            var customer = CustomerGenerator();
            customer.FirstName = null;
            customer.BillingAddress = null;
            var results = EntityValidator<Customer>.Validate(customer);

            foreach (var result in results)
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }

        private static Customer CustomerGenerator()
        {
            var address = new Address
            {
                AddressLine1 = "Small Street",
                AddressLine2 = "14th floor",
                City = "New Yourk",
                ZipCode = "6699NY"
            };

            var customer = new Customer
            {
                FirstName = "Jane",
                LastName = "Smith",
                BillingAddress = address,
                ShippingAddress = address
            };

            return customer;
        }

        private static void ReadEntity(int id)
        {
            var customerRepo = new CustomerRepository();
            var customer = customerRepo.GetEntity(id);
            Console.WriteLine(customer);
        }

        private static void ReadAllEntities()
        {
            var customersRepo = new CustomerRepository();
            foreach (var customer in customersRepo.GetAllEntities())
            {
                Console.WriteLine(customer);
            }
        }

        private static void AddCustomer()
        {
            var address = new Address
            {
                AddressLine1 = "Wall Streat",
                AddressLine2 = "58th floor",
                City = "New Yourk",
                ZipCode = "6699NY"
            };

            var customer = new Customer
            {
                FirstName = "David",
                LastName = "Levoleur",
                BillingAddress = address,
                ShippingAddress = address
            };

            var customersRepo = new CustomerRepository();
            customersRepo.AddEntity(customer);
        }
    }
}
