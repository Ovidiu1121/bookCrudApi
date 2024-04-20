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

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
           return await _context.Books.FirstOrDefaultAsync(x => x.Id.Equals(id));
            
        }

        public async Task<Book> GetByTitleAsync(string title)
        {
           return await _context.Books.FirstOrDefaultAsync(x =>x.Title.Equals(title));
        }

        public async Task<Book> CreateBook(CreateBookRequest request)
        {
            var book = _mapper.Map<Book>(request);

            _context.Books.Add(book);

            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Book> UpdateBook(int id, UpdateBookRequest request)
        {
            var book = await _context.Books.FindAsync(id);

            book.Title= request.Title ?? book.Title;
            book.Author=request.Author ?? book.Author;
            book.Genre=request.Genre??book.Genre;

            _context.Books.Update(book);

            await _context.SaveChangesAsync();

            return book;

        }

        public async Task<Book> DeleteBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return book;

        }
    }
}
