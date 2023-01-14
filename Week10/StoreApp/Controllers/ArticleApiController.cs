using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApp.DTOs;
using StoreApp.Services;

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

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ArticleDto> Get() => _repository.All;

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ArticleDto> Get(int id) => _repository[id];

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ArticleDto> Post([FromBody] CreateArticleDto dto) =>
            _repository.Add(dto);

        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ArticleDto> Put([FromBody] UpdateArticleDto dto) =>
            _repository.Update(dto);

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id) => _repository.Delete(id);
    }
}
