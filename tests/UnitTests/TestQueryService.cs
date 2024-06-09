using BookCrudApi.Books.Model;
using BookCrudApi.Books.Repository.interfaces;
using BookCrudApi.Books.Service;
using BookCrudApi.Books.Service.Interfaces;
using BookCrudApi.System.Constant;
using BookCrudApi.System.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests
{
    public class TestQueryService
    {

        Mock<IBookRepository> _mock;
        IBookQueryService _service;

        public TestQueryService()
        {
            _mock=new Mock<IBookRepository>();
            _service=new BookQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Book>());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAll());

            Assert.Equal(exception.Message, Constants.NO_BOOKS_EXIST);

        }

        [Fact]
        public async Task GetAll_ReturnAllBooks()
        {

            var books = TestBookFactory.CreateBooks(5);

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(books);

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Contains(books[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

            Assert.Equal(Constants.BOOK_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetById_ReturnBook()
        {

            var book = TestBookFactory.CreateBook(1);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

            var result = await _service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(book, result);

        }

        [Fact]
        public async Task GetByTitle_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByTitleAsync("")).ReturnsAsync((Book)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByTitle(""));

            Assert.Equal(Constants.BOOK_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetByTitle_ReturnBook()
        {
            var book = TestBookFactory.CreateBook(1);

            book.Title="test";

            _mock.Setup(repo => repo.GetByTitleAsync("test")).ReturnsAsync(book);

            var result = await _service.GetByTitle("test");

            Assert.NotNull(result);
            Assert.Equal(book, result);
        }




    }
}
