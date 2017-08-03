using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swagger.PoC.ViewModels;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swagger.PoC.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    public class StoreApiController : Controller
    { 

        /// <summary>
        /// Delete purchase order by ID
        /// </summary>
        /// <remarks>For valid response try integer IDs with value &lt; 1000. Anything above 1000 or nonintegers will generate API errors</remarks>
        /// <param name="orderId">ID of the order that needs to be deleted</param>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Order not found</response>
        [HttpDelete]
        [Route("/v2/stores/order/{orderId}")]
        public virtual void DeleteOrder([FromRoute]string orderId)
        { 
            throw new NotImplementedException();
        }


        /// <summary>
        /// Find purchase order by ID
        /// </summary>
        /// <remarks>For valid response try integer IDs with value &lt;&#x3D; 5 or &gt; 10. Other values will generated exceptions</remarks>
        /// <param name="orderId">ID of pet that needs to be fetched</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Order not found</response>
        [HttpGet]
        [Route("/v2/stores/order/{orderId}")]
        [SwaggerResponse(200, typeof(OrderViewModel))]
        public virtual IActionResult GetOrderById([FromRoute]string orderId)
        { 
            string exampleJson = null;
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<OrderViewModel>(exampleJson)
            : default(OrderViewModel);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Place an order for a pet
        /// </summary>
        /// <remarks></remarks>
        /// <param name="body">order placed for purchasing the pet</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid Order</response>
        [HttpPost]
        [Route("/v2/stores/order")]
        [SwaggerResponse(200, typeof(OrderViewModel))]
        public virtual IActionResult PlaceOrder([FromBody]OrderViewModel body)
        { 
            string exampleJson = null;
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<OrderViewModel>(exampleJson)
            : default(OrderViewModel);
            return new ObjectResult(example);
        }
    }
}
