namespace CustomerOrderSystem.Presentation.DTOs
{
    /// <summary>
    /// Response de una orden
    /// </summary>
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}