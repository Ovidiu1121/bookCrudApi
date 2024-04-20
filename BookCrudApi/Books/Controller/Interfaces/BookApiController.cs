using BookCrudApi.Books.Model;
using BookCrudApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookCrudApi.Books.Controller.Interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BookApiController:ControllerBase
    {

        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Book>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<IEnumerable<Book>>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Book))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Book>> CreateBook([FromBody] CreateBookRequest bookRequest);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Book))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Book>> UpdateBook([FromRoute]int id, [FromBody] UpdateBookRequest bookRequest);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Book))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Book>> DeleteBook([FromRoute] int id);

        [HttpGet("{title}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Book))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Book>> GetByTitleRoute([FromRoute] string title);

    }
}
