using BookCrudApi.Books.Model;
using BookCrudApi.Books.Repository.interfaces;
using BookCrudApi.Books.Service.Interfaces;
using BookCrudApi.Dto;
using BookCrudApi.System.Constant;
using BookCrudApi.System.Exceptions;

namespace BookCrudApi.Books.Service
{
    public class BookQueryService: IBookQueryService
    {
        private IBookRepository _repository;

        public BookQueryService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListBookDto> GetAll()
        {
            ListBookDto books = await _repository.GetAllAsync();

            if (books.bookList.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_BOOKS_EXIST);
            }

            return books;
        }

        public async Task<BookDto> GetById(int id)
        {

            BookDto book = await _repository.GetByIdAsync(id);

            if (book == null)
            {
                throw new ItemDoesNotExist(Constants.BOOK_DOES_NOT_EXIST);
            }

            return book;
        }

        public async Task<BookDto> GetByTitle(string title)
        {
            BookDto book = await _repository.GetByTitleAsync(title);

            if (book == null)
            {
                throw new ItemDoesNotExist(Constants.BOOK_DOES_NOT_EXIST);
            }

            return book;
        }
    }
}
