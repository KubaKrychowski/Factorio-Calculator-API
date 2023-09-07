namespace Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string[] args)
            : base(string.Format(Resources.Exceptions.Exceptions.NotFound, args))
        {
        }
    }
}
