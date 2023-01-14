using Microsoft.AspNetCore.Mvc;
using StoreApp.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using StoreApp.Data;
using StoreApp.DTOs;
using StoreApp.Util;

namespace StoreApp.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoryApiController(ICategoryRepository repository) => _repository = repository;

        /// <summary>
        /// Gets all Categories.
        /// </summary>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<CategoryDto> Get() => _repository.All;

        /// <summary>
        /// Gets a specific Category.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDto> Get(int id) => _repository[id];

        /// <summary>
        /// Creates a new Category.
        /// </summary>
        [AdminOnlyEndpoint]
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<CategoryDto> Post([FromBody] CreateCategoryDto dto) =>
            _repository.Add(dto);

        /// <summary>
        /// Updates a specific Category.
        /// </summary>
        [AdminOnlyEndpoint]
        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDto> Put([FromBody] UpdateCategoryDto dto) =>
            _repository.Update(dto);

        /// <summary>
        /// Deletes a specific Category.
        /// </summary>
        [AdminOnlyEndpoint]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id) => _repository.Delete(id);
    }
}
