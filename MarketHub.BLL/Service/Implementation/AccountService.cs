
using MarketHub.BLL.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using String = System.String;

namespace MarketHub.BLL.Service.Implementation
{
    public class AccountService: IAccountService
    {
        //DI
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountService(UserManager<AppUser> userManger, SignInManager<AppUser> signInManager, IConfiguration configuration) 
        {
            _userManager = userManger;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        //Register Method
        public async Task<GenResponse> Register(RegisterDTO registerDTO)
        {
            try
            {
                //mapping
                AppUser appUser = new AppUser()
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                    PhoneNumber = registerDTO.PhoneNumber,
                    Address = registerDTO.Address,
                    FullName = registerDTO.FullName,
                };

                //Create User with "User" Role and ckeck Errors
                IdentityResult registerResult = await _userManager.CreateAsync(appUser, registerDTO.Password);

                if(registerResult.Succeeded == false)
                {
                    string errors = String.Join('|', registerResult.Errors.Select(error => error.Description));
                    return CreateResponse(false, errors, registerDTO);
                }

                IdentityResult roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded == false)
                {
                    string errors = String.Join('|', roleResult.Errors.Select(error => error.Description));
                    return CreateResponse(false, errors, registerDTO);
                }

                //return response
                return CreateResponse(true, "Successfully Registerd", DataObject: registerDTO);
            }
            catch(Exception ex)
            {
                return CreateResponse(false, "Internal Server Error", registerDTO);
            }
            
        }
        //Login method
        public async Task<GenResponse> Login(LoginDTO loginDTO)
        {
            try
            {

                AppUser user = await _userManager.FindByEmailAsync(loginDTO.Email);
                //AppUser user = await _userManager.FindByNameAsync(loginDTO.Email);
                
                if (user == null)
                    return CreateResponse(false, "Invalid Email/User Don't Exist", loginDTO);

                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

                if (!result.Succeeded)
                    return CreateResponse(false, "Wrong Password", loginDTO);

                //=============================
                //Claims 
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                //Roles Claims

                IList<string> roles = await _userManager.GetRolesAsync(user);

                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }

                //=============================
                // Secret Kay & Signature Part Configuration
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

                SigningCredentials sc = new SigningCredentials(key: securityKey, algorithm: SecurityAlgorithms.HmacSha256);
                
                //=============================
                //Token 
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["JWT:IssuerIP"],
                    audience: _configuration["JWT:AudienceIP"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: sc
                    );
                
                var retToken = new //JwtSecurityTokenHandler()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                };

                return CreateResponse(true, "Successflly LoggedIn", retToken);

            }
            catch (Exception ex)
            {
                return CreateResponse(false, "Internal Server Error", null);
            }
        }

        //Create General Response Method
        private GenResponse CreateResponse(bool IsSuccess, string Message, dynamic? DataObject)
        {
            GenResponse response = new GenResponse();
            response.IsSuccess = IsSuccess;
            response.Message = Message;
            response.DataObject = DataObject;

            return response;
        }

    }
}
