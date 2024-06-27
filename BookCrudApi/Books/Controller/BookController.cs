using BookCrudApi.Books.Controller.Interfaces;
using BookCrudApi.Books.Model;
using BookCrudApi.Books.Repository.interfaces;
using BookCrudApi.Books.Service.Interfaces;
using BookCrudApi.Dto;
using BookCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BookCrudApi.Books.Controller
{
    public class BookController:BookApiController
    {
        private IBookCommandService _bookCommandService;
        private IBookQueryService _bookQueryService;

        public BookController(IBookCommandService bookCommandService, IBookQueryService bookQueryService)
        {
           _bookCommandService = bookCommandService;
            _bookQueryService = bookQueryService;
        }

        public override async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookRequest bookRequest)
        {
            try
            {
                var books = await _bookCommandService.CreateBook(bookRequest);

                return Created("Cartea a fost adaugata",books);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<BookDto>> DeleteBook([FromRoute] int id)
        {
            try
            {
                var books = await _bookCommandService.DeleteBookById(id);

                return Accepted("", books);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ListBookDto>> GetAll()
        {
            try
            {
                var books = await _bookQueryService.GetAll();
                return Ok(books);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<BookDto>> GetByTitleRoute([FromRoute] string title)
        {
            try
            {
                var books = await _bookQueryService.GetByTitle(title);
                return Ok(books);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<BookDto>> GetByIdRoute(int id)
        {
            try
            {
                var book = await _bookQueryService.GetById(id);
                return Ok(book);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<BookDto>> UpdateBook([FromRoute] int id, [FromBody] UpdateBookRequest bookRequest)
        {
            try
            {
                var books = await _bookCommandService.UpdateBook(id,bookRequest);

                return Ok(books);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
