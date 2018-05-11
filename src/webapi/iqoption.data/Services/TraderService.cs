using System;
using System.Threading.Tasks;
using iqoption.core.data;
using iqoption.data.Model;

namespace iqoption.data.Services {

    public interface ITraderService {

    }
    public class TraderService : ITraderService {
        private readonly IRepository<TraderFollwerDto> _traderFollowerDto;

        public TraderService(IRepository<TraderFollwerDto> traderFollowerDto) {
            _traderFollowerDto = traderFollowerDto;
        }
        


        

    }
}