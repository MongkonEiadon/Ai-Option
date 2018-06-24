using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iqoption.domain
{
    public interface IMessage { }
    public interface ICommand { }

    public interface ICommandHandler<T> where T : ICommand {
        Task ExecuteAsync(T command);
    }
}
