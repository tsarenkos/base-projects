using Base.Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace Base.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this._validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (this._validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResult = await Task.WhenAll(this._validators.Select(x => x.ValidateAsync(context, cancellationToken)));

                var failures = validationResult
                    .Where(x => x.Errors.Any())
                    .SelectMany(x => x.Errors)
                    .ToList();

                if (failures.Any())
                {
                    throw new ApplicationValidationException(failures);
                }
            }
            return await next();
        }
    }
}
