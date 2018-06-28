using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using iqoption.core;
using iqoption.domain.Users.Commands;
using Microsoft.AspNetCore.Identity;

namespace iqoption.data.Commands {
    public class CreateUserRoleCommandHandler : ValidatorHandler<CreateUserRoleCommand, UserRoleInfo> {
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;


        public CreateUserRoleCommandHandler(RoleManager<IdentityRole> roleManager, IMapper mapper) {
            _roleManager = roleManager;
            _mapper = mapper;
        }


        public override async Task<UserRoleInfo> HandleRequestAsync(CreateUserRoleCommand request,
            CancellationToken ct = default(CancellationToken)) {
            var role = await _roleManager.RoleExistsAsync(request.UserLevel);

            if (!role) {
                var identityResult = await _roleManager.CreateAsync(new IdentityRole(request.UserLevel));

                if (identityResult.Succeeded)
                    return new UserRoleInfo {
                        UserLevel = request.UserLevel
                    };

                throw new Exception(string.Join(", ", identityResult.Errors));
            }

            return new UserRoleInfo {
                UserLevel = await _roleManager.GetRoleNameAsync(new IdentityRole(request.UserLevel))
            };
        }
    }
}