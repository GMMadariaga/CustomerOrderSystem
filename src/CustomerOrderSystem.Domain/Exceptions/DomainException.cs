namespace CustomerOrderSystem.Domain.Exceptions
{
    /// <summary>
    /// Excepcion base para errores del dominio
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}