using GUM.DAL;
using GUM.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace GUM.WebApi.Controllers
{
    public class AuthController : ApiController
    {
        public enum CustomStatusCode 
        {
            DataNotFound =1000
        }
        AccountManager accountManager = null;
        public AuthController() 
        {
            accountManager = new AccountManager();
        }
        [HttpPost]
        [Route("api/Auth/Login/")]
        public HttpResponseMessage PostLogin(User user)
        {
            User userDataFromDB = accountManager.ValidateUser(user);
            if (userDataFromDB == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }          
            else 
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://192.168.43.12:9000",
                    audience: "http://192.168.43.12:9000",
                    claims: new List<Claim>() 
                    {
                        new Claim(ClaimTypes.Role,userDataFromDB.RoleName),
                        new Claim("Role",userDataFromDB.RoleName),
                        new Claim("Email",userDataFromDB.Email)
                    },
                    expires: DateTime.Now.AddDays(28),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Token = tokenString });
            }
            //else
            //{
            //    return this.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //}
        }

        [HttpPost]
        [Route("api/Auth/Signup/")]

        public HttpResponseMessage PostSignup(User user) 
        {
            var result = accountManager.Register(user);
            if (result == true)
                return Request.CreateResponse(HttpStatusCode.OK, new { Saved = "True" });
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Saved = "False" });
        }
    }
}
