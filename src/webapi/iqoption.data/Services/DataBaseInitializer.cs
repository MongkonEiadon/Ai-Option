using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iqoption.data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace iqoption.data.Services
{
    public interface IDbSeeding {
        Task SeedAsync();
    }
    public class DbSeedingService : IDbSeeding
    {
        private readonly iqOptionContext _context;
        private readonly UserManager<UserDto> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbSeedingService(
            iqOptionContext context,
            UserManager<UserDto> userManager,
            RoleManager<IdentityRole> roleManager) {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync() {

            //
            await _context.Database.EnsureCreatedAsync();

            //
            await SeedUser();
        }

        private async Task SeedUser() {
  //
            if (_context.Roles.Any(x => x.Name == "Administrator")) {
                return;
            }

            await _roleManager.CreateAsync(new IdentityRole("Administrator"));

            await _userManager.CreateAsync(new UserDto() {
                UserName = "m@email.com",
                EmailConfirmed = true,
                Email = "m@email.com"
            }, "Code11054");

            await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync("m@email.com"), "Administrator");

        }
    }
}
