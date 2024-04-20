using BookCrudApi.Books.Model;
using BookCrudApi.Books.Repository.interfaces;
using BookCrudApi.Books.Service.Interfaces;
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

        public async Task<IEnumerable<Book>> GetAll()
        {
            IEnumerable<Book> books = await _repository.GetAllAsync();

            if (books.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_BOOKS_EXIST);
            }

            return books;
        }

        public async Task<Book> GetById(int id)
        {

            Book book = await _repository.GetByIdAsync(id);

            if (book == null)
            {
                throw new ItemDoesNotExist(Constants.BOOK_DOES_NOT_EXIST);
            }

            return book;
        }

        public async Task<Book> GetByTitle(string title)
        {
            Book book = await _repository.GetByTitleAsync(title);

            if (book == null)
            {
                throw new ItemDoesNotExist(Constants.BOOK_DOES_NOT_EXIST);
            }

            return book;
        }
    }
}
