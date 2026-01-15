namespace CustomerOrderSystem.Presentation.DTOs
{
    /// <summary>
    /// Request para crear un nuevo cliente
    /// </summary>
    public class CustomerCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
