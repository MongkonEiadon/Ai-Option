using System;
using System.Threading.Tasks;
using AiOption.Application.ApplicationServices;
using AiOption.Domain.Customers;
using AiOption.WebPortal.Shared;
using AutoMapper;
using EventFlow.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AiOption.WebPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerProcessManagerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(
            ICustomerProcessManagerService customerService,
            IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomerTask([FromBody] CustomerViewModel model)
        {
            try
            {
                var result = await _customerService.RegisterCustomerAsync(
                    model.EmailAddress,
                    model.Password,
                    model.InvitationCode);

                var vm = _mapper.Map<CustomerViewModel>(result);

                return Ok(vm);
            }
            catch (DomainError domainError)
            {
                return BadRequest(domainError.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerTask([FromBody] string customerId)
        {
            var result = await _customerService.GetCustomerAsync(CustomerId.With(customerId));

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeLevelTask([FromBody] CustomerViewModel viewModel)
        {
            //var customer = _mapper.Map<Customer>(viewModel);

            //var result =
            //    await _customerService.ChangeCustomerLevel(customer.Id,
            //        new Level(UserLevel.Administrator));

            return Ok(viewModel);
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteCustomerTask([FromBody] string customerId)
        {
            try
            {

                await _customerService.DeleteCustomerAsync(CustomerId.With(customerId));

                return Ok();
            }
            catch (DomainError domainError)
            {
                return BadRequest(domainError.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}