using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventFlow.Queries;
using iqoption.core.data;
using iqoption.domain.Users.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data.User.Queries
{
    public class UsersLevelQueryHandler : IQueryHandler<UsersLevelQuery, UsersLevelQueryResult> {
        private readonly UserManager<UserDto> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<UserDto, string> _userRepository;
        private readonly IMapper _mapper;


        public UsersLevelQueryHandler(UserManager<UserDto> userManager, RoleManager<IdentityRole> roleManager, IRepository<UserDto, string> userRepository, IMapper mapper) {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UsersLevelQueryResult> ExecuteQueryAsync(UsersLevelQuery query, CancellationToken cancellationToken) {


            var resultUsers = new List<domain.Users.User>();
            var allUsers = _userRepository.GetAll().ToList();

            resultUsers.AddRange(_mapper.Map<List<domain.Users.User>>(allUsers));




            foreach (var role in await _roleManager.Roles.Select(x => x.Name).ToListAsync(cancellationToken: cancellationToken)) {
                var userRole = await _userManager.GetUsersInRoleAsync(role);

                foreach (var user in resultUsers.Where(x => userRole.Select(u => u.Id).Contains(x.Id))) {
                    user.Level = role;
                }
            }


            return new UsersLevelQueryResult(resultUsers, query.UserLevel, true);


        }
    }
    
}
