using FluentValidation.Results;

namespace Base.Application.Common.Exceptions
{
    public class ApplicationValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ApplicationValidationException(IEnumerable<ValidationFailure> failures)
            : base("One or more validation errors occured.")
        {
            this.Errors = failures
                .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
                .ToDictionary(errorsGroup => errorsGroup.Key, errorsGroup => errorsGroup.ToArray());
        }

        public ApplicationValidationException(string propertyName, string message)
            : base("One or more validation errors occured.")
        {
            this.Errors = new Dictionary<string, string[]>()
            {
                { propertyName, new string[] { message } }
            };
        }
    }
}
