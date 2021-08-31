using System.Collections.Generic;
using System.Linq;
using BooksCatalog.Models;
using BooksCatalog.Repositories.Interfaces;

namespace BooksCatalog.Repositories
{
    public class BookRepository : IBookRepository<Book, int>
    {
        private readonly ApplicationContext _ctx;
        public BookRepository(ApplicationContext c)
        {
            _ctx = c;
        }

        public Book Get(int id)
        {
            var book = _ctx.Books.FirstOrDefault(b => b.Id == id);
            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            var book = _ctx.Books.ToList();
            return book;
        }

        public int Add(Book book)
        {
            _ctx.Books.Add(book);
            return _ctx.SaveChanges();
        }

        public int Delete(int id)
        {
            var book = _ctx.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return 0;
            _ctx.Books.Remove(book);
            return _ctx.SaveChanges();
        }

        public int Update(int id, Book item)
        {
            var book = _ctx.Books.Find(id);
            if (book == null) return 0;
            book.OriginalTitle = item.OriginalTitle;
            book.Author = item.Author;
            book.Isbn10 = item.Isbn10;
            return _ctx.SaveChanges();
        }
    }
}
