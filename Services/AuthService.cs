using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using chatApplication.Helpers;
using chatApplication.Models;
using chatApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace chatApplication.Services
{
    public class AuthService : SigInManager
    {
        private readonly UserManager<myUser> _userManager;
        private readonly SignInManager<myUser> _signinManager;
        private readonly RoleManager<myRoles> _roleManager;
        private readonly JWT _jwt;

        public AuthService(SignInManager<myUser> signInManager, UserManager<myUser> userManager, RoleManager<myRoles> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }
        //public async Task SinoutAsync()
        //{
        //    await _signinMaanager.SignOutAsync();
        //}
        //public async Task SininAsync(string PhoneNumber)
        //{
        //   myUser user = _userManager.Users.Where(q => q.PhoneNumber == PhoneNumber).FirstOrDefault();
        //    await _signinMaanager.SignInAsync(user, isPersistent: false);
        //}

        public JsonResult Error(string message)
        {
            return new JsonResult(new Exception(message));
        }



        public JsonResult Success(string message)
        {
            return new JsonResult(new Exception(message));
        }


        public async Task<AuthModel> RegisterAsync(VM_CreateUser user,string roleName)
        {

            AuthModel authModel = new AuthModel();
            var UsersCount = _userManager.Users.Where(q => q.PhoneNumber == user.PhoneNumber).Count();
            if (UsersCount > 0)
            {
                authModel.Message = "هذا الحساب موجود بالفعل";
                return authModel;
            }
            
            try
            {
                myUser myuser = new myUser
                {
                    UserName = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    BranchId = user.BranchID,
                    Address = user.Address,
                };
                IdentityResult rezult =  _userManager.CreateAsync(myuser, user.Password).Result;
                if (!rezult.Succeeded)
                {
                    var errors = string.Empty;

                    foreach (var error in rezult.Errors)
                        errors += $"{error.Description},";

                     authModel.Message = errors ;
                    return authModel;
                }
                await _signinManager.SignInAsync(myuser, isPersistent: false);
                await _userManager.AddToRoleAsync(myuser, roleName);
                var jwtSecurityToken = await CreateJwtToken(myuser);
                return authModel = new AuthModel
                {
                    UserId = myuser.Id,
                    ExpiresOn = jwtSecurityToken.ValidTo.Date,
                    IsAuthenticated = true,
                    Roles = new List<string> { roleName },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    PhoneNumber = myuser.PhoneNumber,
                    Username = myuser.UserName,
                    BranchId = user.BranchID

                };

            }
            catch
            {
            }
            authModel.Message = "حدث خطاء غير متوقع";
            return authModel;

        }


        private async Task<JwtSecurityToken> CreateJwtToken(myUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays).Date,
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            myUser user =  _userManager.Users.Where(u => u.PhoneNumber == model.PhoneNumber).FirstOrDefault();

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "رقم الهاتف أو كلمة المرور غير صحيحة";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.UserId = user.Id;
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();
            authModel.PhoneNumber = user.PhoneNumber;
            authModel.BranchId = user.BranchId;
             await _signinManager.SignInAsync(user, isPersistent: false);
            return authModel;
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }


    }
}