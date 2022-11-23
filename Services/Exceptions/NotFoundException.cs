namespace SalesWebMVC.Services.Exceptions
{
    // Classe Exception personalizada
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}