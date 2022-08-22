using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using chatApplication.Models;
using chatApplication.Api;
using chatApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace chatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SigInManager _authService;

        //public AuthController(SigInManager authService)
        //{
        //    _authService = authService;
        //}

        //[HttpGet("tt")]
        //[Authorize]
        //public async Task<IActionResult> tt()
        //{
        //    return Ok("its access");
        //}


        //[HttpGet("tt1")]
        //public async Task<IActionResult> tt1(string PhoneNumber)
        //{

        //    await _authService.SinoutAsync();
        //    return Ok("Is SignOut Now");
        //}

        //[HttpGet("tt2")]
        //public async Task<IActionResult> tt2(string PhoneNumber)
        //{

        //    await _authService.SininAsync(PhoneNumber);
        //    return Ok("Is SignIn Now");
        //}



        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterAsync([FromBody] myUser model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _authService.RegisterAsync(model);

        //    if (!result.IsAuthenticated)
        //        return BadRequest(result.Message);

        //    return Ok(result);
        //}

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
    }
}