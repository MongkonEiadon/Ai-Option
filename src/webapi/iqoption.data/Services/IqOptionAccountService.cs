using System;
using System.Threading.Tasks;
using iqoption.core.data;
using iqoption.data.Model;

namespace iqoption.data.Services {
    public class IqOptionAccountService {
        private readonly IRepository<IqOptionUserDto> _iqOptionUserRepository;

        public IqOptionAccountService(IRepository<IqOptionUserDto> iqOptionUserRepository) {
            _iqOptionUserRepository = iqOptionUserRepository;
        }

        public Task<Guid> CreateUserTask(IqOptionUserDto user) {
            return _iqOptionUserRepository.InsertAndGetIdAsync(user);
        }
    }
}