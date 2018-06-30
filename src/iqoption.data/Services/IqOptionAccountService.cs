using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iqoption.core.data;
using iqoption.data.Model;

namespace iqoption.data.Services {
    public interface IIqOptionAccountService {
        Task<Guid> CreateAccountTask(IqOptionAccountDto user);
        Task<List<IqOptionAccountDto>> GetIqOptionAccountsByUserIdAsync(string userName);
        Task<IqOptionAccountDto> UpdateAccountTask(IqOptionAccountDto accont);
        Task<IqOptionAccountDto> GetAccountByUserIdAsync(long userId);
        Task<IqOptionAccountDto> GetAccountByIdAsync(Guid id);
    }

    public class IqOptionAccountService : IIqOptionAccountService {
        private readonly IRepository<IqOptionAccountDto> _iqOptionUserRepository;

        public IqOptionAccountService(IRepository<IqOptionAccountDto> iqOptionUserRepository) {
            _iqOptionUserRepository = iqOptionUserRepository;
        }

        public Task<Guid> CreateAccountTask(IqOptionAccountDto user) {
            user.Id = Guid.NewGuid();
            user.LastSyned = DateTime.Now;
            user.CreatedOn = DateTime.Now;

            return _iqOptionUserRepository.InsertAndGetIdAsync(user);
        }

        public Task<IqOptionAccountDto> UpdateAccountTask(IqOptionAccountDto accont) {
            accont.UpdatedOn = DateTime.Now;

            return _iqOptionUserRepository.UpdateAsync(accont);
        }


        public Task<List<IqOptionAccountDto>> GetIqOptionAccountsByUserIdAsync(string userName) {
            return _iqOptionUserRepository.GetAllListAsync(x => x.User.UserName == userName);
        }


        public Task<IqOptionAccountDto> GetAccountByUserIdAsync(long userId) {
            return _iqOptionUserRepository.FirstOrDefaultAsync(x => x.IqOptionUserId == userId);
        }


        public Task<IqOptionAccountDto> GetAccountByIdAsync(Guid id) {
            return _iqOptionUserRepository.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}