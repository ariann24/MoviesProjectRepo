using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieProjectCore.Entities;
using MovieProjectCore.Helpers;
using MovieProjectCore.Interface;

namespace MovieProjectCore.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("token", string.Empty);
            HttpContext.Session.SetString("IsLoggedIn", "true");
            return Ok(new { message = "logged out successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> PostJWTToken([FromBody] UserEntity user)
        {
            try
            {
                if (user == null)
                {
                    return StatusCode(400, new { message = "You need to input your email and password" });
                }

                if ((user.EmailAddress == null || user.EmailAddress == string.Empty) ||
                    (user.Password == null || user.Password == string.Empty))
                {
                    return StatusCode(400, new { message = "You need to input your email and password" });
                }

                UserEntity userEntity = await _unitOfWork.Users.GetAsync(x => x.EmailAddress == user.EmailAddress && x.Password == user.Password);
               
                if (userEntity != null)
                {
                    var token = GenerateJwtToken(userEntity.EmailAddress);
                    HttpContext.Session.SetString("token", token);
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    return Ok(new { jwtToken = token });
                }
                
                return StatusCode(401, new { message = "Invalid credentials." });
            }
            catch (Exception ex)
            {
                return StatusCode(501, new { message = ex.Message });
            }
        }


        [HttpPost("create_user")]
        public async Task<IActionResult> Post([FromBody] UserEntity user)
        {
            try
            {
                if (user == null)
                {
                    return StatusCode(400, new { message = "You need to input your email and password" });
                }

                if ((user.FullName == null || user.FullName == string.Empty) ||
                    (user.EmailAddress == null || user.EmailAddress == string.Empty) ||
                    (user.Password == null || user.Password == string.Empty))
                {
                    return StatusCode(400, new { message = "Fullname, EmailAddress, and Password is required!" });
                }

                UserEntity userEntity = await _unitOfWork.Users.GetAsync(x => x.EmailAddress == user.EmailAddress);
                
                if (userEntity != null)
                {
                    return StatusCode(409, new { message = "Email already exist" });
                }

                await _unitOfWork.Users.AddAsync(user);
                _unitOfWork.CompleteUOW();
                return Ok(new { message = "Account created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(501, new { message = ex.Message });
            }
        }

        private string GenerateJwtToken(string user)
        {
            string tokenResult;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenResult = tokenHandler.WriteToken(token);

            return tokenResult;
        }
    }
}
