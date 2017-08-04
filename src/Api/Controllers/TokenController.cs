using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Swagger.PoC.ViewModels;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swagger.PoC.Controllers
{
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public enum Scope
        {
            Pet,
            Store,
            User
        }

        /// <summary>
        /// Generate sample Auth JWT
        /// </summary>
        /// <param name="scopes"></param>
        /// <response code="200">successful operation</response>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(TokenViewModel))]
        public IActionResult Get(List<Scope> scopes)
        {
            var claims = scopes.Select(x => new Claim("scope", x.ToString().ToLower()));
            var secretKey = _configuration["SecretKey"];

            if (secretKey == null)
                throw new ArgumentNullException(nameof(secretKey));

            return Ok(new TokenViewModel { Token = GenerateJwt(secretKey, claims) });
        }

        private static string GenerateJwt(string secretKey, IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtHeader = new JwtHeader(signingCredentials);
            var jwtPayload = new JwtPayload(claims: claims);
            var jwtSecurityToken = new JwtSecurityToken(jwtHeader, jwtPayload);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
