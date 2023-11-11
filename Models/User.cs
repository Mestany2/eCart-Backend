namespace eCart_Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Uid { get; set; }
        public bool isSeller { get; set; }
        public List<Order> Orders { get; set; }
        public List<Item> Items { get; set; }
    }
}
