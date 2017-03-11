namespace Chapter2_Objective4
{
    using System.Collections.Generic;
    using System.Linq;

    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(IEnumerable<Order> orders)
            : base(orders)
        {
        }

        public IEnumerable<Order> FilterOrdersOnAmount(decimal amount)
        {
            return elements.Where(o => o.Amount <= amount);
        }
    }
}
