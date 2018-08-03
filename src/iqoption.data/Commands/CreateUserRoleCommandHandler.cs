using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventFlow.Commands;
using iqoption.core;
using iqoption.domain.Users;
using iqoption.domain.Users.Commands;
using Microsoft.AspNetCore.Identity;

namespace iqoption.data.Commands {
    public class CreateUserRoleCommandHandler : ICommandHandler<UserAggregrate, UserIdentity, UserRoleInfo, CreateUserRoleCommand> {
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;


        public CreateUserRoleCommandHandler(RoleManager<IdentityRole> roleManager, IMapper mapper) {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        
        public async Task<UserRoleInfo> ExecuteCommandAsync(UserAggregrate aggregate, CreateUserRoleCommand request,
            CancellationToken cancellationToken) {
            var role = await _roleManager.RoleExistsAsync(request.RoleName);

            if (!role) {
                var identityResult = await _roleManager.CreateAsync(new IdentityRole(request.RoleName));

                if (!identityResult.Succeeded)
                    throw new Exception(string.Join(", ", identityResult.Errors));
            }

            var query = await _roleManager.GetRoleIdAsync(new IdentityRole(request.RoleName));
            return new UserRoleInfo(request.RoleName, query, true);
        }
    }
}