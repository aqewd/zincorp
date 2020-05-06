using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ZinCorp.BE;
using ZinCorp.BE.Enums;
using ZinCorp.BL.Customers;
using ZinCorp.WebAPI.Auth;
using ZinCorp.WebAPI.Models;
using ZinCorp.WebAPI.Helpers;

namespace ZinCorp.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IDataProtector _dataProtector;
        private readonly IJwtFactory _jwtFactory;
        private readonly ICustomersBL _customersBL;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(IDataProtectionProvider dataProtectionProvider, IJwtFactory jwtFactory, ICustomersBL customersBl, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _dataProtector = dataProtectionProvider.CreateProtector(Constants.DataProtectorPurpose);
            _jwtFactory = jwtFactory;
            _customersBL = customersBl;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                return BadRequest(ErrorsHelper.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            var jwt = await TokenHelper.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var customer = _customersBL.Auth(login, password);

            if (customer == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            HttpContext.Session.Set("customer", customer);
            HttpContext.Response.Cookies.Append("login", _dataProtector.Protect(login));
            HttpContext.Response.Cookies.Append("password", _dataProtector.Protect(password));

            return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(login, customer.Role.ToString(), customer.Id.ToString()));
        }
    }
}
