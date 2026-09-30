using MarketHub.BLL.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.Service.Abstraction
{
    public interface IAccountService
    {
        public Task<GenResponse> Register(RegisterDTO registerDTO);
        public Task<GenResponse> Login(LoginDTO loginDTO);
    }
}
