using BookCrudApi.Books.Model;
using BookCrudApi.Dto;

namespace BookCrudApi.Books.Service.Interfaces
{
    public interface IBookQueryService
    {
        Task<ListBookDto> GetAll();
        Task<BookDto> GetById(int id);
        Task<BookDto> GetByTitle(string title);
    }
}
