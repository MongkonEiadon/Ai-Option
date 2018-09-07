using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Queries;

using EventFlow.Queries;

using Microsoft.AspNetCore.Identity;

namespace AiOption.Application.Services
{

    public interface ICustomerAccountServices {

        Task<AuthorizedCustomer> LoginAsync(string userName, string password);

    }
    public class CustomerAccountServices : ICustomerAccountServices
    {

        private readonly IQueryProcessor _queryProcessor;

        public CustomerAccountServices(IQueryProcessor queryProcessor) {
            _queryProcessor = queryProcessor;

        }
        

        public async Task<AuthorizedCustomer> LoginAsync(string email, string password) {

            var account = await _queryProcessor.ProcessAsync(new GetAuthorizeCustomerQuery(email), CancellationToken.None);

            VerifyPasswordHash(password, account.Token)

            return default(AuthorizedCustomer);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

    }
}
