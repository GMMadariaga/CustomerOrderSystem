namespace CustomerOrderSystem.Presentation.DTOs
{
    /// <summary>
    /// Response de un cliente
    /// </summary>
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}