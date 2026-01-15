namespace CustomerOrderSystem.Presentation.DTOs
{
    /// <summary>
    /// Response estándar para errores
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; }
        public int StatusCode { get; set; }
    }
}