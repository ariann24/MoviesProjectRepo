using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MoviesProjectStandard.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly MoviesDBEntities movieEntity;
        private static string Secret = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";

        public UserController()
        {
            movieEntity = new MoviesDBEntities();
        }

        [HttpGet]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            return Ok(new { message = "logged out successfully!" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> login([FromBody] User user)
        {
            HttpRequestMessage request = new HttpRequestMessage();

            try
            {
                if (user == null)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "You need to input your email and password"));
                }

                if ((user.EmailAddress == null || user.EmailAddress == string.Empty) ||
                    (user.Password == null || user.Password == string.Empty))
                {
                    throw new Exception("You need to input your email and password");
                }

                var userEntity = await movieEntity.Users.Where(x => x.EmailAddress == user.EmailAddress && x.Password == user.Password).FirstOrDefaultAsync();
      
                if (userEntity == null)
                {
                    // HttpContext.Session.SetString("token", token);
                    // HttpContext.Session.SetString("IsLoggedIn", "true");
                    throw new Exception("You need to input your email and password");
                    
                }

                //var identity = new GenericIdentity(user.EmailAddress);
                //SetPrincipal(new GenericPrincipal(identity, null));
                var token = TokenValidator.TokenValidator.GenerateToken(user.EmailAddress);
                return Ok(new { message = "Successfully logged in", token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("create_user")]
        public async Task<IHttpActionResult> Post([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    throw new Exception("You need to input your email and password");
                }

                if ((user.FullName == null || user.FullName == string.Empty) ||
                    (user.EmailAddress == null || user.EmailAddress == string.Empty) ||
                    (user.Password == null || user.Password == string.Empty))
                {
                    throw new Exception("Fullname, EmailAddress, and Password is required!");
                }

                var userEntity = await movieEntity.Users.Where(x => x.EmailAddress == user.EmailAddress).FirstOrDefaultAsync();

                if (userEntity != null)
                {
                    throw new Exception("Email already exist");
                }

                movieEntity.Users.Add(user);
                movieEntity.SaveChanges();
                return Ok(new { message = "Account created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
