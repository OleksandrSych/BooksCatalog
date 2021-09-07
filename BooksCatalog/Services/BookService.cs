using System.Collections.Generic;
using BooksCatalog.Models;
using BooksCatalog.Repositories.Interfaces;
using BooksCatalog.Services.Interfaces;

namespace BooksCatalog.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository<Book, int> _bookRepo;
        public BookService(IBookRepository<Book, int> bookRepo)
        {
            _bookRepo = bookRepo;
        }
        public int Add(Book b) => _bookRepo.Add(b);
        public int Add(Book[] b) => _bookRepo.Add(b);

        public int Delete(int id) => _bookRepo.Delete(id);

        public Book Get(int id) => _bookRepo.Get(id);

        public IEnumerable<Book> GetAll() => _bookRepo.GetAll();

        public int Update(int id, Book b) => _bookRepo.Update(id, b);

    }
}
