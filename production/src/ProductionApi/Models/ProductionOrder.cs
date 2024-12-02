namespace ProductionApi.Models
{
    public class ProductionOrder
    {
        public string Id { get; set; } // MongoDB usa string para Id
        public int OrderId { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }
        public List<string> Items { get; set; }
    }
}
