using BookCrudApi.Books.Controller;
using BookCrudApi.Books.Controller.Interfaces;
using BookCrudApi.Books.Model;
using BookCrudApi.Books.Service.Interfaces;
using BookCrudApi.Dto;
using BookCrudApi.System.Constant;
using BookCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;
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
    public class TestController
    {
        Mock<IBookCommandService> _command;
        Mock<IBookQueryService> _query;
        BookApiController _controller;

        public TestController()
        {

            _command = new Mock<IBookCommandService>();
            _query = new Mock<IBookQueryService>();
            _controller = new BookController(_command.Object, _query.Object);

        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {

            _query.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemDoesNotExist(Constants.BOOK_DOES_NOT_EXIST));

            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.BOOK_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {

            var books = TestBookFactory.CreateBooks(5);

            _query.Setup(repo => repo.GetAll()).ReturnsAsync(books);

            var result = await _controller.GetAll();
            var okresult = Assert.IsType<OkObjectResult>(result.Result);
            var booksAll = Assert.IsType<ListBookDto>(okresult.Value);

            Assert.Equal(5, booksAll.bookList.Count);
            Assert.Equal(200, okresult.StatusCode);

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

            _command.Setup(repo => repo.CreateBook(It.IsAny<CreateBookRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.BOOK_ALREADY_EXIST));

            var result = await _controller.CreateBook(create);
            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, bad.StatusCode);
            Assert.Equal(Constants.BOOK_ALREADY_EXIST, bad.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {

            var create = new CreateBookRequest
            {
                Title="Test",
                Author="test",
                Genre="genretest"
            };

            var book = TestBookFactory.CreateBook(1);

            book.Title=create.Title;
            book.Author=create.Author;
            book.Genre=create.Genre;

            _command.Setup(repo => repo.CreateBook(create)).ReturnsAsync(book);

            var result = await _controller.CreateBook(create);
            var okResult = Assert.IsType<CreatedResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 201);
            Assert.Equal(book, okResult.Value);
        }

        [Fact]
        public async Task Update_InvalidDate()
        {

            var update = new UpdateBookRequest
            {
                Title="",
                Author="",
                Genre=""
            };

            _command.Setup(repo => repo.UpdateBook(2, update)).ThrowsAsync(new ItemDoesNotExist(Constants.BOOK_DOES_NOT_EXIST));

            var result = await _controller.UpdateBook(2, update);
            var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 404);
            Assert.Equal(bad.Value, Constants.BOOK_DOES_NOT_EXIST);

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

            var book = TestBookFactory.CreateBook(1);

            book.Title=update.Title;
            book.Author=update.Author;
            book.Genre=update.Genre;

            _command.Setup(repo => repo.UpdateBook(1, update)).ReturnsAsync(book);

            var result = await _controller.UpdateBook(1, update);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, book);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _command.Setup(repo => repo.DeleteBookById(1)).ThrowsAsync(new ItemDoesNotExist(Constants.BOOK_DOES_NOT_EXIST));

            var result = await _controller.DeleteBook(1);
            var notfound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notfound.StatusCode, 404);
            Assert.Equal(notfound.Value, Constants.BOOK_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var book = TestBookFactory.CreateBook(1);

            _command.Setup(repo => repo.DeleteBookById(1)).ReturnsAsync(book);

            var result = await _controller.DeleteBook(1);
            var okResult = Assert.IsType<AcceptedResult>(result.Result);

            Assert.Equal(202, okResult.StatusCode);
            Assert.Equal(book, okResult.Value);

        }

        [Fact]
        public async Task GetByTitle_ItemDoesNotExist()
        {
            _query.Setup(repo => repo.GetByTitle("")).ThrowsAsync(new ItemDoesNotExist(Constants.BOOK_DOES_NOT_EXIST));

            var result = await _controller.GetByTitleRoute("");

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.BOOK_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetByTitle_ReturnBook()
        {

            var book = TestBookFactory.CreateBook(1);

            book.Title="test";

            _query.Setup(repo => repo.GetByTitle("test")).ReturnsAsync(book);

            var result = await _controller.GetByTitleRoute("test");

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);


        }



    }
}
