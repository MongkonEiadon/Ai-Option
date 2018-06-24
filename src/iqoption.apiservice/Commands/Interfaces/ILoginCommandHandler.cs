using System.Threading.Tasks;
using iqoption.domain;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using Newtonsoft.Json;

namespace iqoption.apiservice
{

    public interface ILoginCommandHandler  {
        Task<string> ExecuteAsync(ILoginCommand command);
    }


}
