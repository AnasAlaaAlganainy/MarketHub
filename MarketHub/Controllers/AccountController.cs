using MarketHub.BLL.DTOs.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<GenResponse>> Register([FromBody]RegisterDTO registerDTO)
        {
            GenResponse response = new GenResponse();

            response = await _accountService.Register(registerDTO);

            if (response.IsSuccess == false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        [Route("[Action]")]// ديه بتلقط اسم الميثود اللى تحته
        public async Task<ActionResult<GenResponse>>Login([FromBody] LoginDTO loginDTO)
        {
            GenResponse response = new GenResponse();
            response = await _accountService.Login(loginDTO);

            if (response.IsSuccess == false)
                return Unauthorized(response);

            return Ok(response);
        }
    }
}
