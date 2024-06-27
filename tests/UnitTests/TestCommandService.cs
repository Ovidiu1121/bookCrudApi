using BookCrudApi.Books.Model;
using BookCrudApi.Books.Repository.interfaces;
using BookCrudApi.Books.Service;
using BookCrudApi.Books.Service.Interfaces;
using BookCrudApi.Dto;
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
    public class TestCommandService
    {
        Mock<IBookRepository> _mock;
        IBookCommandService _service;

        public TestCommandService()
        {

            _mock = new Mock<IBookRepository>();
            _service = new BookCommandService(_mock.Object);

        }

        [Fact]
        public async Task Create_InvalidData()
        {
            var create = new CreateBookRequest
            {
                Title="Test",
                Author="test",
                Genre="genretest"
            };

            var book = TestBookFactory.CreateBook(5);

            _mock.Setup(repo => repo.GetByTitleAsync("Test")).ReturnsAsync(book);

            var exception = await Assert.ThrowsAsync<ItemAlreadyExists>(() => _service.CreateBook(create));

            Assert.Equal(Constants.BOOK_ALREADY_EXIST, exception.Message);



        }

        [Fact]
        public async Task Create_ReturnBook()
        {

            var create = new CreateBookRequest
            {
                Title="Test",
                Author="test",
                Genre="genretest"
            };

            var book = TestBookFactory.CreateBook(5);

            book.Title=create.Title;
            book.Author=create.Author;
            book.Genre=create.Genre;

            _mock.Setup(repo => repo.CreateBook(It.IsAny<CreateBookRequest>())).ReturnsAsync(book);

            var result = await _service.CreateBook(create);

            Assert.NotNull(result);
            Assert.Equal(result, book);
        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.DeleteBookById(It.IsAny<int>())).ReturnsAsync((BookDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteBookById(5));

            Assert.Equal(exception.Message, Constants.BOOK_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var book = TestBookFactory.CreateBook(5);

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(book);

            var result = await _service.DeleteBookById(5);

            Assert.NotNull(result);
            Assert.Equal(book, result);


        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {

            var update = new UpdateBookRequest
            {
                Title="Test",
                Author="test",
                Genre="genretest"
            };

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((BookDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateBook(1, update));

            Assert.Equal(Constants.BOOK_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateBookRequest
            {
                Title="Test",
                Author="test",
                Genre="genretest"
            };

            var book = TestBookFactory.CreateBook(5);

            book.Title=update.Title;
            book.Author=update.Author;
            book.Genre=update.Genre;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(book);
            _mock.Setup(repoo => repoo.UpdateBook(It.IsAny<int>(), It.IsAny<UpdateBookRequest>())).ReturnsAsync(book);

            var result = await _service.UpdateBook(5, update);

            Assert.NotNull(result);
            Assert.Equal(book, result);

        }


    }
}
