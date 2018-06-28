namespace iqoption.core {
    public interface INotification : MediatR.INotification {
    }

    public interface IRequestHandler<in TCommand, TResult> : MediatR.IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult> {
    }
}