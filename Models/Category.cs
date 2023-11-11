namespace eCart_Backend.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } 
        public string CategoryImage { get; set; }
        List<Item> Items { get; set; }
    }
}
