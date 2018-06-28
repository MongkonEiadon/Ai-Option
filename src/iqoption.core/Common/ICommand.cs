using MediatR;

namespace iqoption.core {
    public interface ICommand<T> : IRequest<T> {
    }
}