using AutoMapper;
using BookCrudApi.Books.Model;
using BookCrudApi.Books.Repository.interfaces;
using BookCrudApi.Data;
using BookCrudApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace BookCrudApi.Books.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListBookDto> GetAllAsync()
        {
            List<Book> result = await _context.Books.ToListAsync();
            
            ListBookDto listBookDto = new ListBookDto()
            {
                bookList = _mapper.Map<List<BookDto>>(result)
            };

            return listBookDto;
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await _context.Books.Where(b => b.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<BookDto>(book);
            
        }

        public async Task<BookDto> GetByTitleAsync(string title)
        {
            var book = await _context.Books.Where(b => b.Title.Equals(title)).FirstOrDefaultAsync();
            
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateBook(CreateBookRequest request)
        {
            var book = _mapper.Map<Book>(request);

            _context.Books.Add(book);

            await _context.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateBook(int id, UpdateBookRequest request)
        {
            var book = await _context.Books.FindAsync(id);

            book.Title= request.Title ?? book.Title;
            book.Author=request.Author ?? book.Author;
            book.Genre=request.Genre??book.Genre;

            _context.Books.Update(book);

            await _context.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);

        }

        public async Task<BookDto> DeleteBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);

        }
    }
}
