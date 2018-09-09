using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using AiOption.Application.Repositories;
using AiOption.Domain.Customers;

using AutoMapper;

using Microsoft.AspNetCore.Identity;

namespace AiOption.Infrastructure.DataAccess.Identities
{

    public interface ICustomerIdentityRepository {

        Task<Customer> SigninWithPasswordAsync(string email, string password);

    }
    public class CustomerIdentityRepository  : ICustomerIdentityRepository {

        private readonly UserManager<CustomerDto> _customerManager;
        private readonly IRepository<CustomerDto, Guid> _customerRepository;
        private readonly SignInManager<CustomerDto> _signInManager;
        private readonly IMapper _mapper;

        public CustomerIdentityRepository(IRepository<CustomerDto, Guid> customerRepository, 
            SignInManager<CustomerDto> signInManager, IMapper mapper) {
            _customerRepository = customerRepository;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<Customer> SigninWithPasswordAsync(string email, string password) {

            var user = _customerRepository.FirstOrDefault(x => x.NormalizedEmail == email.ToUpper());

            if (user == null) return null;

            var sigin = await _signInManager.PasswordSignInAsync(user, password, true, false);

            return _mapper.Map<Customer>(user);
        }

        
    }
}
