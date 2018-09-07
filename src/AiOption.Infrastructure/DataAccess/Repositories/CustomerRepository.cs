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
        private readonly UserManager<CustomerDto> _customerManager;


        public CustomerRepository(Func<DbContext> db, IMapper mapper) : base(db) {
            _mapper = mapper;
        }

        public async Task<AuthorizedCustomer> GetAuthorizedCustomerAsync(string emailAddress) {

            var user = await this.FirstOrDefaultAsync(x => x.NormalizedEmail == emailAddress.ToUpper());

            if (user != null) {
                return _mapper.Map<AuthorizedCustomer>(user);
            }

            return null;
        }

    }

}