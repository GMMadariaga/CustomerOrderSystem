namespace CustomerOrderSystem.Presentation.DTOs
{
    /// <summary>
    /// Request para crear una nueva orden
    /// </summary>
    public class OrderCreateRequest
    {
        public Guid CustomerId { get; set; }
    }
}