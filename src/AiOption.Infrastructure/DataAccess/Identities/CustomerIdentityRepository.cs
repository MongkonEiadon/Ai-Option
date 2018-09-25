using System;
using System.Linq;
using System.Threading.Tasks;

using AiOption.Application.Repositories;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.DomainServices;

using AutoMapper;

using Microsoft.AspNetCore.Identity;

namespace AiOption.Infrastructure.DataAccess.Identities {

    public class AuthorizationDomainService : IAuthorizationDomainService {

        private readonly UserManager<CustomerDto> _customerManager;
        private readonly IRepository<CustomerDto, Guid> _customerRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<CustomerDto> _signInManager;
        private readonly UserManager<CustomerDto> _userManager;

        public AuthorizationDomainService(
            IRepository<CustomerDto, Guid> customerRepository,
            SignInManager<CustomerDto> signInManager,
            IMapper mapper,
            UserManager<CustomerDto> userManager) {

            _customerRepository = customerRepository;
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CustomerReadModel> SigninWithPasswordAsync(string email, string password) {

            var user = _customerRepository.FirstOrDefault(x => x.NormalizedEmail == email.ToUpper());

            if (user == null) return null;

            var sigin = await _signInManager.PasswordSignInAsync(user, password, true, false);

            return _mapper.Map<CustomerReadModel>(user);
        }

        public async Task<CustomerReadModel> RegisterCustomerAsync(CustomerReadModel newCustomer) {

            var userDto = _mapper.Map<CustomerDto>(newCustomer);
            return newCustomer;


            //var result = await _userManager.CreateAsync(userDto, newCustomer.Password);

            //if (result.Succeeded) return newCustomer;

            //throw new CustomerException(newCustomer.Id, string.Join(", ", result.Errors.Select(x => x.Description)));
        }

    }

}