import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from "../book/book";

@Injectable()
export class DataService {

  private url = "/api/books";

  constructor(private http: HttpClient
  ) {
  }

  getBooks() {
    return this.http.get(this.url);
  }

  getBook(id: number) {
    return this.http.get(this.url + '/' + id);
  }

  createBook(book: Book) {
    return this.http.post(this.url, book);
  }

  addBooks(books: Book[]) {
    return this.http.post(this.url + '/addbooks', books);
  }

  updateBook(product: Book) {

    return this.http.put(this.url, product);
  }

  deleteBook(id: number) {
    return this.http.delete(this.url + '/' + id);
  }
}
