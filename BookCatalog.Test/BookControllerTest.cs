using System.Collections.Generic;
using BooksCatalog.Controllers;
using BooksCatalog.Models;
using BooksCatalog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BooksCatalog.Tests
{
    public class BookControllerTest
    {

        [Fact]
        public void GetAllBooksTest()
        {
            //Arrange
            var mock = new Mock<IBookService>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestBooks());
            var controller = new BookController(mock.Object);
            //Act
            var result = controller.Get();
            //Asset
            var viewResult = Assert.IsAssignableFrom<List<Book>>(result);
            var model = Assert.IsAssignableFrom<List<Book>>(
                viewResult);
            Assert.Equal(GetTestBooks().Count, model.Count);
        }


        [Fact]
        public void GetBookReturnsNull()
        {
            //Arrange
            var mock = new Mock<IBookService>();
            var controller = new BookController(mock.Object);
            //Act
            var result = controller.Get(-1);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetBookReturnsNotNull()
        {
            //Arrange
            var mock = new Mock<IBookService>();
            mock.Setup(repo => repo.Get(1)).Returns(GetTestBooks()[0]);
            var controller = new BookController(mock.Object);
            //Act
            var result = controller.Get(1);
            //Assert
            Assert.IsType<Book>(result);
        }

        [Fact]
        public void GetPostReturnsNotNull()
        {
            //Arrange
            var mock = new Mock<IBookService>();
            var controller = new BookController(mock.Object);
            //Act
            var result = controller.Post(new Book());
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetAddBooksReturnsNotNull()
        {
            //Arrange
            var mock = new Mock<IBookService>();
            var controller = new BookController(mock.Object); 
            //Act
            var result = controller.AddBooks(GetTestBooks().ToArray());
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetPostReturnsBadRequestObjectResult()
        {
            //Arrange
            var mock = new Mock<IBookService>();
            var controller = new BookController(mock.Object);
            controller.ModelState.AddModelError("Error", "test Error");
            //Act
            var result = controller.Post(new Book());
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetPutReturnsNotNull()
        {
            //Arrange
            var mock = new Mock<IBookService>();
            var controller = new BookController(mock.Object);
            //Act
            var result = controller.Put(new Book());
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetPutReturnsBadRequestObjectResult()
        {
            //Arrange
            var mock = new Mock<IBookService>();
            var controller = new BookController(mock.Object);
            controller.ModelState.AddModelError("Error", "test Error");
            //Act
            var result = controller.Put(new Book());
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void GetDeleteReturnsOkObjectResult()
        {
            //Arrange
            var mock = new Mock<IBookService>();
            var controller = new BookController(mock.Object);
            //Act
            var result = controller.Delete(1);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetDeleteReturnsNotNull()
        {
            //Arrange
            var mock = new Mock<IBookService>();
            mock.Setup(repo => repo.Get(1)).Returns(GetTestBooks()[0]);
            //Act
            var controller = new BookController(mock.Object);
            //Assert
            if (controller.Delete(1) is OkObjectResult result) Assert.IsType<Book>(result.Value);
        }

        private static List<Book> GetTestBooks()
        {
            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    OriginalTitle = "A Life on Our Planet: My Witness Statement and a Vision for the Future",
                    Author = "David Attenborough",
                    Isbn10 = 1529108276
                },
                new Book
                {
                    Id = 2,
                    OriginalTitle = "Life on Air",
                    Author = "David Attenborough",
                    Isbn10 = 1849900010
                },
                new Book
                {
                    Id = 3,
                    OriginalTitle = "An Improbable Life: The Autobiography",
                    Author = "Trevor McDonald",
                    Isbn10 = 1474614779
                }
            };
            return books;
        }

    }
}
