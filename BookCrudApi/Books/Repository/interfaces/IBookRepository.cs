using BookCrudApi.Books.Model;
using BookCrudApi.Dto;

namespace BookCrudApi.Books.Repository.interfaces
{
    public interface IBookRepository
    {
        Task<ListBookDto> GetAllAsync();
        Task<BookDto> GetByIdAsync(int id);
        Task<BookDto> GetByTitleAsync(string title);
        Task<BookDto> CreateBook(CreateBookRequest request);
        Task<BookDto> UpdateBook(int id, UpdateBookRequest request);
        Task<BookDto> DeleteBookById(int id);

    }
}
