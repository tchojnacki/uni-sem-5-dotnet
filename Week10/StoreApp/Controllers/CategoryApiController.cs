using Microsoft.AspNetCore.Mvc;
using StoreApp.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using StoreApp.DTOs;

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
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CategoryDto> Post([FromBody] CreateCategoryDto dto) =>
            _repository.Add(dto);

        /// <summary>
        /// Updates a specific Category.
        /// </summary>
        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDto> Put([FromBody] UpdateCategoryDto dto) =>
            _repository.Update(dto);

        /// <summary>
        /// Deletes a specific Category.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id) => _repository.Delete(id);
    }
}
