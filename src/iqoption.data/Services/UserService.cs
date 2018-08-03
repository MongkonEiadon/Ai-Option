using System.Threading.Tasks;
using iqoption.core.data;
using iqoption.data.User;

namespace iqoption.data.Services {
    public interface IUserService {
        Task<UserDto> GetUserByNameAsync(string name);
    }

    public class UserService : IUserService {
        private readonly IRepository<UserDto, string> _userRepository;

        public UserService(IRepository<UserDto, string> userRepository) {
            _userRepository = userRepository;
        }

        public Task<UserDto> GetUserByNameAsync(string name) {
            return _userRepository.FirstOrDefaultAsync(x => x.UserName == name);
        }
    }
}