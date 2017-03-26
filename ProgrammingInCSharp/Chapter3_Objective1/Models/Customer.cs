namespace Chapter3_Objective1.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Customer : IEntity
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public Address ShippingAddress { get; set; }

        [Required]
        public Address BillingAddress { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {BillingAddress.City}";
        }
    }
}
