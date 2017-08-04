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
    [Authorize("store")]
    [Route("[controller]s")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class StoreController : Controller
    {

        /// <summary>
        /// Delete purchase order by ID
        /// </summary>
        /// <remarks>For valid response try integer IDs with value &lt; 1000. Anything above 1000 or nonintegers will generate API errors</remarks>
        /// <param name="orderId">ID of the order that needs to be deleted</param>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Order not found</response>
        [HttpDelete("order/{orderId}")]
        public virtual IActionResult DeleteOrder([FromRoute] string orderId)
        {
            return NoContent();
        }


        /// <summary>
        /// Find purchase order by ID
        /// </summary>
        /// <remarks>For valid response try integer IDs with value &lt;&#x3D; 5 or &gt; 10. Other values will generated exceptions</remarks>
        /// <param name="orderId">ID of pet that needs to be fetched</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Order not found</response>
        [HttpGet("order/{orderId}")]
        [SwaggerResponse(200, typeof(OrderViewModel))]
        public virtual IActionResult GetOrderById([FromRoute] string orderId)
        {
            return Ok(FakeViewModels.Order);
        }


        /// <summary>
        /// Place an order for a pet
        /// </summary>
        /// <remarks></remarks>
        /// <param name="body">order placed for purchasing the pet</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid Order</response>
        [HttpPost("order")]
        [SwaggerResponse(200, typeof(OrderViewModel))]
        public virtual IActionResult PlaceOrder([FromBody] OrderViewModel body)
        {
            return Ok(FakeViewModels.Order);
        }
    }
}
