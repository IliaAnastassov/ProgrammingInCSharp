namespace Chapter2_Objective4
{
    using System;

    public class Order : IEntity, IComparable
    {
        public Order(decimal amount)
        {
            Amount = amount;
        }

        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Created { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Order o = obj as Order;

            if (o == null)
            {
                throw new ArgumentException("Object is not an Order.");
            }

            return o.Created.CompareTo(obj);
        }
    }
}
