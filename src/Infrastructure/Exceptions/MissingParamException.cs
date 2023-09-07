namespace Infrastructure.Exceptions
{
    public class MissingParamException : Exception
    {
        public MissingParamException(string paramName)
            : base(string.Format(Resources.Exceptions.Exceptions.MissingParam, paramName))
        {
        }
    }
}
