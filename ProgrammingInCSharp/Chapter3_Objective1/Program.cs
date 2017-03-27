namespace Chapter3_Objective1
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using DataAccess;
    using Models;
    using Validation;

    class Program
    {
        static void Main(string[] args)
        {

        }

        private static bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}") ||
                   input.StartsWith("[") && input.EndsWith("]");
        }

        private static void RemoveExcessWhitespace()
        {
            var regex = new Regex(@"[ ]{2,}", RegexOptions.None);

            var input = "A B  C   D";
            var result = regex.Replace(input, " ");

            Console.WriteLine(result);
        }

        private static bool ValidateZipCode(string zipCode)
        {
            Match match = Regex.Match(zipCode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$", RegexOptions.IgnoreCase);
            return match.Success;
        }

        private static void UseConvertWithNull()
        {
            var i = Convert.ToInt32(null);
            Console.WriteLine(i);
        }

        private static void ParseCurrency()
        {
            var english = new CultureInfo("En");
            var dutch = new CultureInfo("Nl");

            var value = "19,95€";
            var number = decimal.Parse(value, NumberStyles.Currency, dutch);
            Console.WriteLine(number.ToString(english));
        }

        private static void UseTryParse()
        {
            var value = Console.ReadLine();
            int result;
            var success = int.TryParse(value, out result);

            if (success)
            {
                Console.WriteLine("The value is an integer");
            }
            else
            {
                Console.WriteLine("The value is not an integer");
            }
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
