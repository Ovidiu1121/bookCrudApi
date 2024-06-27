using BookCrudApi.Books.Model;
using BookCrudApi.Books.Repository.interfaces;
using BookCrudApi.Books.Service.Interfaces;
using BookCrudApi.Dto;
using BookCrudApi.System.Constant;
using BookCrudApi.System.Exceptions;

namespace BookCrudApi.Books.Service
{
    public class BookCommandService: IBookCommandService
    {
        private IBookRepository _repository;

        public  BookCommandService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<BookDto> CreateBook(CreateBookRequest request)
        {
            BookDto book = await _repository.GetByTitleAsync(request.Title);

            if (book!=null)
            {
                throw new ItemAlreadyExists(Constants.BOOK_ALREADY_EXIST);
            }

            book=await _repository.CreateBook(request);
            return book;
        }

        public async Task<BookDto> DeleteBookById(int id)
        {
            BookDto book = await _repository.GetByIdAsync(id);

            if (book==null)
            {
                throw new ItemDoesNotExist(Constants.BOOK_DOES_NOT_EXIST);
            }

            await _repository.DeleteBookById(id);
            return book;
        }

        public async Task<BookDto> UpdateBook(int id,UpdateBookRequest request)
        {
            BookDto book = await _repository.GetByIdAsync(id);

            if (book==null)
            {
                throw new ItemDoesNotExist(Constants.BOOK_DOES_NOT_EXIST);
            }

            book = await _repository.UpdateBook(id,request);
            return book;
        }
    }
}
