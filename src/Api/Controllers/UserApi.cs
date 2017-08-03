using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swagger.PoC.ViewModels;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swagger.PoC.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    public class UserApiController : Controller
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <remarks>This can only be done by the logged in user.</remarks>
        /// <param name="body">Created user object</param>
        /// <response code="201">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpPost]
        [Route("/v2/users")]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Uri))]
        public virtual IActionResult CreateUser([FromBody]UserViewModel body)
        {
            return CreatedAtRoute(nameof(GetUserByName), new {username = body.Username});
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <remarks>This can only be done by the logged in user.</remarks>
        /// <param name="username">The name that needs to be deleted</param>
        /// <response code="400">Invalid username supplied</response>
        /// <response code="404">UserViewModel not found</response>
        [HttpDelete]
        [Route("/v2/users/{username}")]
        public virtual void DeleteUser([FromRoute]string username)
        { 
            throw new NotImplementedException();
        }


        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <remarks></remarks>
        /// <param name="username">The name that needs to be fetched. Use user1 for testing.</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid username supplied</response>
        /// <response code="404">UserViewModel not found</response>
        [HttpGet]
        [Route("/v2/users/{username}")]
        [SwaggerResponse(200, typeof(UserViewModel))]
        public virtual IActionResult GetUserByName([FromRoute]string username)
        { 
            string exampleJson = null;
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<UserViewModel>(exampleJson)
            : default(UserViewModel);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Logs user into the system
        /// </summary>
        /// <remarks></remarks>
        /// <param name="username">The user name for login</param>
        /// <param name="password">The password for login in clear text</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid username/password supplied</response>
        [HttpGet]
        [Route("/v2/users/login")]
        [SwaggerResponse(200, typeof(string))]
        public virtual IActionResult LoginUser([FromQuery]string username, [FromQuery]string password)
        { 
            string exampleJson = null;
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<string>(exampleJson)
            : default(string);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Logs out current logged in user session
        /// </summary>
        /// <remarks></remarks>
        /// <response code="0">successful operation</response>
        [HttpGet]
        [Route("/v2/users/logout")]
        public virtual void LogoutUser()
        { 
            throw new NotImplementedException();
        }


        /// <summary>
        /// Updated user
        /// </summary>
        /// <remarks>This can only be done by the logged in user.</remarks>
        /// <param name="username">name that need to be deleted</param>
        /// <param name="body">Updated user object</param>
        /// <response code="400">Invalid user supplied</response>
        /// <response code="404">UserViewModel not found</response>
        [HttpPut]
        [Route("/v2/users/{username}")]
        public virtual void UpdateUser([FromRoute]string username, [FromBody]UserViewModel body)
        { 
            throw new NotImplementedException();
        }
    }
}
