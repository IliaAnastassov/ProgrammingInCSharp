namespace Chapter2_Objective4
{
    public class Order : IEntity
    {
        public Order(decimal amount)
        {
            Amount = amount;
        }

        public int Id { get; set; }

        public decimal Amount { get; set; }
    }
}
