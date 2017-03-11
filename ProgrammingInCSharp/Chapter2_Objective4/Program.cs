namespace Chapter2_Objective4
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {

        }

        private static void UseBaseDerived()
        {
            var b = new Base();
            b.Execute();
            b = new Derived();
            b.Execute();
        }

        private static void UseOrderRepository()
        {
            var orders = new List<Order>
            {
                new Order(125.37M),
                new Order(593.69M),
                new Order(29.99M)
            };

            var repository = new OrderRepository(orders);
            repository.FilterOrdersOnAmount(200M);

            foreach (var order in repository.FilterOrdersOnAmount(200M))
            {
                Console.WriteLine(order.Amount);
            }
        }

        private static void UseImplementer()
        {
            IContractA implementer = new Implementer();
            implementer.MethodFromA();
            (implementer as IContractB).MethodFromB();
        }
    }
}
