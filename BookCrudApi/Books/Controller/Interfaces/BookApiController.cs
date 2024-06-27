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
        public abstract Task<ActionResult<ListBookDto>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Book))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookRequest bookRequest);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Book))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<BookDto>> UpdateBook([FromRoute]int id, [FromBody] UpdateBookRequest bookRequest);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Book))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<BookDto>> DeleteBook([FromRoute] int id);

        [HttpGet("title/{title}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Book))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<BookDto>> GetByTitleRoute([FromRoute] string title);
        
        [HttpGet("id/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Book))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<BookDto>> GetByIdRoute([FromRoute] int id);

    }
}
