using System.Linq;
using System.Threading.Tasks;
using iqoption.data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace iqoption.data.Services {
    public interface ISeedService {
        Task Seed();
    }
    public class SeedService : ISeedService {
        private readonly AiOptionContext _context;
        private readonly UserManager<UserDto> _userManager;

        public SeedService(AiOptionContext context, UserManager<UserDto> userManager) {
            _context = context;
            _userManager = userManager;
        }


        public async Task Seed() {

            //seed users
            await SeedUsers();
            await SeedTraderFollowers();
            _context.SaveChanges();

        }

        private async Task SeedUsers() {
            var user = new UserDto() {
                UserName = "liie.m@excelbangkok.com",
                Email = "liie.m@excelbangkok.com"
            };

            if (await _userManager.FindByEmailAsync(user.Email) == null) {
                await _userManager.CreateAsync(user, "Code11054");
                await _userManager.SetLockoutEnabledAsync(user, false);
            }

            var user2 = new UserDto()
            {
                UserName = "mongkon.eiadon@gmail.com",
                Email = "mongkon.eiadon@gmail.com"
            };

            if (await _userManager.FindByEmailAsync(user2.Email) == null)
            {
                await _userManager.CreateAsync(user2, "Code11054");
                await _userManager.SetLockoutEnabledAsync(user2, false);
            }
            
        }

        private async Task SeedTraderFollowers() {

            var trader = new TraderDto() {
                IqOptionUserDto = new IqOptionUserDto() {
                    IqOptionUserId = 31773196,
                    Password = "Code11054",
                    IqOptionUserName = "mongkon.eiadon@gmail.com"
                }
            };

            if (_context.IqOptionUsers.Find(trader.IqOptionUserDto.Id) == null) {

                trader.IqOptionUserDto.User = await _userManager.FindByEmailAsync(trader.IqOptionUserDto.IqOptionUserName);
                _context.IqOptionUsers.Add(trader.IqOptionUserDto);
               
            }


            var follower = new FollowerDto() {
                IqOptionUserDto = new IqOptionUserDto() {
                    IqOptionUserId = 21853876,
                    Password = "Code11054",
                    IqOptionUserName = "liie.m@excelbangkok.com"
                }
            };

            if (_context.IqOptionUsers.Find(follower.IqOptionUserDto.Id) == null)
            {

                follower.IqOptionUserDto.User = await _userManager.FindByEmailAsync(follower.IqOptionUserDto.IqOptionUserName);
                _context.IqOptionUsers.Add(follower.IqOptionUserDto);
            }
        }
        
    }
}