using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using iqoption.data.Model;
using iqoption.domain.Users;
using iqoption.domain.Users.Commands;
using Microsoft.AspNetCore.Identity;

namespace iqoption.data.Services {
    public interface IDbSeeding {
        Task SeedAsync();
    }

    public class DbSeedingService : IDbSeeding {
        private readonly AiOptionContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICommandBus _commandBus;
        private readonly UserManager<UserDto> _userManager;


        public DbSeedingService(
            AiOptionContext context,
            UserManager<UserDto> userManager,
            RoleManager<IdentityRole> roleManager, ICommandBus commandBus) {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _commandBus = commandBus;
        }

        public async Task SeedAsync() {
            //
            await _context.Database.EnsureCreatedAsync();

            //
            await SeedUserLevel();

            //
            await SeedUser();
        }

        private async Task SeedUser() {
            if (_context.Roles.Any(x => x.Name == "Administrator")) return;

            await _roleManager.CreateAsync(new IdentityRole("Administrator"));

            await _userManager.CreateAsync(new UserDto {
                UserName = "m@email.com",
                EmailConfirmed = true,
                Email = "m@email.com"
            }, "Code11054");

            await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync("m@email.com"), "Administrator");
        }


        private async Task SeedUserLevel() {
            var levels = new[] {
                new CreateUserRoleCommand(UserIdentity.New, UserLevel.Administrator),
                new CreateUserRoleCommand(UserIdentity.New, UserLevel.Baned),
                new CreateUserRoleCommand(UserIdentity.New, UserLevel.Gold),
                new CreateUserRoleCommand(UserIdentity.New, UserLevel.Platinum),
                new CreateUserRoleCommand(UserIdentity.New, UserLevel.Standard),
                new CreateUserRoleCommand(UserIdentity.New, UserLevel.Silver)
            };

            foreach (var command in levels) await _commandBus.PublishAsync(command, CancellationToken.None);
        }
    }
}