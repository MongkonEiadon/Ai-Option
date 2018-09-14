using System;
using System.Threading.Tasks;

using AiOption.Application.Repositories.ReadOnly;
using AiOption.Domain.Customers;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AiOption.Infrastructure.DataAccess.Repositories {

    public class CustomerRepository : EfCoreRepositoryBase<CustomerDto, Guid>, IReadCustomerRepository {

        private readonly IMapper _mapper;
        private readonly UserManager<CustomerDto> _userManager;
        private readonly UserManager<CustomerDto> _customerManager;


        public CustomerRepository(Func<DbContext> db, IMapper mapper, UserManager<CustomerDto> userManager) : base(db) {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CustomerState> GetAuthorizedCustomerAsync(string emailAddress) {

            var dto = _customerManager.FindByEmailAsync(emailAddress);

            if (dto != null) {
                return _mapper.Map<CustomerState>(dto);
            }

            return null;
        }

    }

}