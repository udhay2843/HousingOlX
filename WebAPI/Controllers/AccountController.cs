using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Data.Interfaces;
using WebAPI.Dtos;
using WebAPI.Errors;
using WebAPI.Extensions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
 
    public class AccountController : BaseController
    {
        private readonly IUnitofWork uow;
         private readonly IConfiguration configuration;

        public AccountController(IUnitofWork uow,IConfiguration configuration)
        {
            this.uow = uow;
            this.configuration = configuration;
        }

      //  public IConfiguration Configuration { get; }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq){
           var user=await uow.userRepository.Authenticate(loginReq.Username,loginReq.Password);
           if(user==null){
            ApiError apierror=new ApiError();
            apierror.ErrorCode=Unauthorized().StatusCode;
            apierror.ErrorMessage="Invalid password or username try others";
            apierror.ErrorDetails="goodluck";
            return Unauthorized(apierror);
           }
           var loginRes = new LoginResDto();
           loginRes.Username=user.Username;
           loginRes.Token=CreateJWT(user);
           return Ok(loginRes);
        }
         [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq){
                ApiError apierror=new ApiError();
            if(loginReq.Username.IsEmpty()||loginReq.Password.IsEmpty()){
                 apierror.ErrorCode=BadRequest().StatusCode;
            apierror.ErrorMessage="Username and password cannot empty";
            apierror.ErrorDetails="goodluck";
                return BadRequest(apierror);
            }
            if(await uow.userRepository.UserAlreadyExists(loginReq.Username)) {
             
            apierror.ErrorCode=BadRequest().StatusCode;
            apierror.ErrorMessage="User exists already";
            apierror.ErrorDetails="goodluck";
                return BadRequest(apierror);
            }
            uow.userRepository.Register(loginReq.Username,loginReq.Password);
            
           await uow.SaveAsync();
           return StatusCode(201);
        }
        private string CreateJWT(User user){
           var secretKey=configuration.GetSection("AppSettings:key").Value;
var key=new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(secretKey));
            var claims = new Claim[]{
                new Claim(ClaimTypes.Name,user.Username),
                 new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var signingCredentials=new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor =new  SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.UtcNow.AddDays(1),
                SigningCredentials=signingCredentials
            };
            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            
        }
    }
}