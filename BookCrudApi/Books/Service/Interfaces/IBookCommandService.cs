using BookCrudApi.Books.Model;
using BookCrudApi.Dto;

namespace BookCrudApi.Books.Service.Interfaces
{
    public interface IBookCommandService
    {
        Task<BookDto> CreateBook(CreateBookRequest request);
        Task<BookDto> UpdateBook(int id, UpdateBookRequest request);
        Task<BookDto> DeleteBookById(int id);
    }
}
