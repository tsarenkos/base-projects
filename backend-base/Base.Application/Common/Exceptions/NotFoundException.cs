namespace Base.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base() { }

        public NotFoundException(string name, object key)
            : base($"The requested resource \"{name}\" ({key}) hasn't been found") { }

        public NotFoundException(string message)
            : base(message) { }
    }
}
