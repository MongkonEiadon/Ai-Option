using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AiOption.Domain.Common;

namespace AiOption.Application.Services
{
    public interface IJwtTokenService
    {
        Task<string> BuildTokenAsync(string email);

        Task<string> BuildTokenAsync(Email email);
    }
}
