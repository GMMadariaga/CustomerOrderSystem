namespace CustomerOrderSystem.Domain.Entities
{
    /// <summary>
    /// Estados posibles de una orden
    /// </summary>
    public enum OrderStatus
    {
        Created,
        Completed,
        Cancelled
    }
}