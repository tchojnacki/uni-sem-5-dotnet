using System.Collections.Generic;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data;
using StoreApp.DTOs;
using StoreApp.Services;
using StoreApp.Util;

namespace StoreApp.Controllers
{
    [Route("api/articles")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ArticleApiController : ControllerBase
    {
        private readonly IArticleRepository _repository;

        public ArticleApiController(IArticleRepository repository) => _repository = repository;

        /// <summary>
        /// Gets all Articles.
        /// </summary>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ArticleDto> Get() => _repository.All;

        /// <summary>
        /// Gets a specific Article.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ArticleDto> Get(int id) => _repository[id];

        /// <summary>
        /// Creates a new Article.
        /// </summary>
        [AdminOnlyEndpoint]
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<ArticleDto> Post([FromBody] CreateArticleDto dto) =>
            _repository.Add(dto);

        /// <summary>
        /// Updates a specific Article.
        /// </summary>
        [AdminOnlyEndpoint]
        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ArticleDto> Put([FromBody] UpdateArticleDto dto) =>
            _repository.Update(dto);

        /// <summary>
        /// Deletes a specific Article.
        /// </summary>
        [AdminOnlyEndpoint]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id) => _repository.Delete(id);

        /// <summary>
        /// Gets a specific page of Articles for lazy loading.
        /// </summary>
        [HttpGet("paginated/{page:int:min(0)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ArticleDto> Page(
            [FromServices] StoreDbContext context,
            int page,
            int? categoryId
        ) =>
            (
                categoryId.HasValue
                    ? context.Articles.Where(a => a.CategoryId == categoryId.Value)
                    : context.Articles
            )
                .GetPage(page)
                .Adapt<IEnumerable<ArticleDto>>();
    }
}
