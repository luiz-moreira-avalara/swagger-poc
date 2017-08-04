using System.Collections.Generic;
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
    [Authorize("pet")]
    [Route("[controller]s")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PetController : Controller
    {
        /// <summary>
        /// Add a new pet to the store
        /// </summary>
        /// <remarks></remarks>
        /// <param name="body">Pet object that needs to be added to the store</param>
        /// <response code="201">Return GetPet URL</response>
        /// <response code="405">Invalid input</response>
        [HttpPost]
        public virtual IActionResult AddPet([FromBody]PetViewModel body)
        {
            return CreatedAtAction(nameof(GetPetById), new { id = body.Id }, body);
        }

        /// <summary>
        /// Deletes a pet
        /// </summary>
        /// <remarks></remarks>
        /// <param name="apiKey"></param>
        /// <param name="petId">Pet id to delete</param>
        /// <response code="204">successful operation</response>
        /// <response code="400">Invalid pet value</response>
        [HttpDelete("{petId}")]
        public virtual IActionResult DeletePet([FromHeader]string apiKey, [FromRoute]long? petId)
        {
            return NoContent();
        }

        /// <summary>
        /// Finds Pets by status
        /// </summary>
        /// <remarks>Multiple status values can be provided with comma seperated strings</remarks>
        /// <param name="status">Status values that need to be considered for filter</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid status value</response>
        [HttpGet("findByStatus")]
        [SwaggerResponse(200, typeof(List<PetViewModel>))]
        public virtual IActionResult FindPetsByStatus([FromQuery]List<string> status)
        {
            return Ok(new[] { FakeViewModels.Pet });
        }


        /// <summary>
        /// Finds Pets by tags
        /// </summary>
        /// <remarks>Muliple tags can be provided with comma seperated strings. Use tag1, tag2, tag3 for testing.</remarks>
        /// <param name="tags">Tags to filter by</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid tag value</response>
        [HttpGet("findByTags")]
        [SwaggerResponse(200, typeof(List<PetViewModel>))]
        public virtual IActionResult FindPetsByTags([FromQuery]List<string> tags)
        {
            return Ok(new[] { FakeViewModels.Pet });
        }


        /// <summary>
        /// Find pet by ID
        /// </summary>
        /// <remarks>Returns a pet when ID &lt; 10.  ID &gt; 10 or nonintegers will simulate API error conditions</remarks>
        /// <param name="petId">ID of pet that needs to be fetched</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Pet not found</response>
        [HttpGet]
        [Route("{petId}")]
        [SwaggerResponse(200, typeof(PetViewModel))]
        public virtual IActionResult GetPetById([FromRoute]long? petId)
        {
            return Ok(FakeViewModels.Pet);
        }


        /// <summary>
        /// Update an existing pet
        /// </summary>
        /// <remarks></remarks>
        /// <param name="body">Pet object that needs to be added to the store</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Pet not found</response>
        /// <response code="405">Validation exception</response>
        [HttpPut]
        [Route("pets")]
        [SwaggerResponse(200, typeof(PetViewModel))]
        public virtual IActionResult UpdatePet([FromBody]PetViewModel body)
        {
            return Ok(FakeViewModels.Pet);
        }


        /// <summary>
        /// Updates a pet in the store with form data
        /// </summary>
        /// <remarks></remarks>
        /// <param name="petId">ID of pet that needs to be updated</param>
        /// <param name="name">Updated name of the pet</param>
        /// <param name="status">Updated status of the pet</param>
        /// <response code="200">successful operation</response>
        /// <response code="405">Invalid input</response>
        [HttpPost]
        [Route("{petId}")]
        [SwaggerResponse(200, typeof(PetViewModel))]
        public virtual IActionResult UpdatePetWithForm([FromRoute]string petId, [FromForm]string name, [FromForm]string status)
        {
            return Ok(FakeViewModels.Pet);
        }
    }
}
