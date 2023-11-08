namespace eCart_Backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal? OrderTotal { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public List<Item> items { get; set; }
    }
}
