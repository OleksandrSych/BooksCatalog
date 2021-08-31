using System.Collections.Generic;
using BooksCatalog.Models;

namespace BooksCatalog.Services.Interfaces
{
    public interface IBookService
    {
        public IEnumerable<Book> GetAll();
        public Book Get(int id);
        public int Add(Book b);
        public int Update(int id, Book b);
        public int Delete(int id);
    }
}
