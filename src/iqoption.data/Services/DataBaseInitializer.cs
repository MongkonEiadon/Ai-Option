using System.Linq;
using System.Threading.Tasks;
using iqopoption.core;
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
        private readonly ISession _session;
        private readonly UserManager<UserDto> _userManager;


        public DbSeedingService(
            AiOptionContext context,
            ISession session,
            UserManager<UserDto> userManager,
            RoleManager<IdentityRole> roleManager) {
            _context = context;
            _session = session;
            _userManager = userManager;
            _roleManager = roleManager;
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
                new CreateUserRoleCommand(UserLevel.Administrator),
                new CreateUserRoleCommand(UserLevel.Baned),
                new CreateUserRoleCommand(UserLevel.Gold),
                new CreateUserRoleCommand(UserLevel.Platinum),
                new CreateUserRoleCommand(UserLevel.Standard),
                new CreateUserRoleCommand(UserLevel.Silver)
            };

            foreach (var command in levels) await _session.Send(command);
        }
    }
}