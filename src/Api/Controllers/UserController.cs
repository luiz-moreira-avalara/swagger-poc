using System;
using System.Net;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swagger.PoC.Extension;
using Swagger.PoC.ViewModels;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swagger.PoC.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [Authorize("user")]
    [Route("[controller]s")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : Controller
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <remarks>This can only be done by the logged in user.</remarks>
        /// <param name="body">Created user object</param>
        /// <response code="201">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Uri))]
        public virtual IActionResult CreateUser([FromBody]UserViewModel body)
        {
            return CreatedAtAction(nameof(GetUserByName), new {username = body.Username}, FakeViewModels.User);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <remarks>This can only be done by the logged in user.</remarks>
        /// <param name="username">The name that needs to be deleted</param>
        /// <response code="400">Invalid username supplied</response>
        /// <response code="404">User not found</response>
        [HttpDelete("{username}")]
        public virtual IActionResult DeleteUser([FromRoute]string username)
        {
            return NoContent();
        }


        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <remarks></remarks>
        /// <param name="username">The name that needs to be fetched. Use user1 for testing.</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid username supplied</response>
        /// <response code="404">User not found</response>
        [HttpGet("{username}", Name = nameof(GetUserByName))]
        [SwaggerResponse(200, typeof(UserViewModel))]
        public virtual IActionResult GetUserByName([FromRoute]string username)
        {
            return Ok(FakeViewModels.User);
        }


        /// <summary>
        /// Logs user into the system
        /// </summary>
        /// <remarks></remarks>
        /// <param name="username">The user name for login</param>
        /// <param name="password">The password for login in clear text</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid username/password supplied</response>
        [HttpGet("login")]
        [SwaggerResponse(200, typeof(string))]
        public virtual IActionResult LoginUser([FromQuery]string username, [FromQuery]string password)
        { 
            return Ok(new Faker().Hashids.Encode(1, 2, 3));
        }

        /// <summary>
        /// Logs out current logged in user session
        /// </summary>
        /// <remarks></remarks>
        /// <response code="204">successful operation</response>
        [HttpGet("logout")]
        public virtual IActionResult LogoutUser()
        {
            return NoContent();
        }


        /// <summary>
        /// Updated user
        /// </summary>
        /// <remarks>This can only be done by the logged in user.</remarks>
        /// <param name="username">name that need to be deleted</param>
        /// <param name="body">Updated user object</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid user supplied</response>
        /// <response code="404">User not found</response>
        [HttpPut("{username}")]
        public virtual IActionResult UpdateUser([FromRoute]string username, [FromBody]UserViewModel body)
        {
            return CreatedAtAction(nameof(GetUserByName), new { username = body.Username });
        }
    }
}
