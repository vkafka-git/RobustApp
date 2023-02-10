namespace Robust.WebApi.ExceptionHandling
{
    public class InvalidMovieException : Exception
    {
        public InvalidMovieException(string message) : base(message)
        {
        }
    }
}
