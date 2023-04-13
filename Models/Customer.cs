namespace ProvaPub.Models
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
