using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace iqoption.core {
    public abstract class ValidatorHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : ICommand<TResponse> {
        public virtual Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken) {
            var validator = CreateValidatorForCommand(request);
            if (validator == null)
                return HandleRequestAsync(request, cancellationToken);

            var context = new ValidationContext(request);
            var failures = validator
                .Validate(context)
                .Errors
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);

            return HandleRequestAsync(request, cancellationToken);
        }

        public abstract Task<TResponse> HandleRequestAsync(TRequest request,
            CancellationToken ct = default(CancellationToken));

        private IValidator CreateValidatorForCommand(TRequest request) {
            var vt = typeof(AbstractValidator<>);
            var et = request.GetType();
            var evt = vt.MakeGenericType(et);

            var validatorType = Assembly.GetAssembly(et).GetTypes().FirstOrDefault(t => t.IsSubclassOf(evt));
            ;
            if (validatorType == null)
                return null;

            var validatorInstance = (IValidator) Activator.CreateInstance(validatorType);
            return validatorInstance;
        }
    }
}