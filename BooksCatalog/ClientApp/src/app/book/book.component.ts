import { Component, OnInit } from "@angular/core";
import { DataService } from "../data.service";
import { Book } from "./book";

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  providers: [DataService]
})
export class BookComponent implements OnInit {

  book: Book = new Book();
  books: Book[];
  tableMode: boolean = true;

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.loadBooks();
  }

  loadBooks() {
    this.dataService.getBooks()
      .subscribe((data: Book[]) => this.books = data);
  }

  save() {
    if (this.book.id == null) {
      this.dataService.createBook(this.book)
        .subscribe((data: Book) => this.books.push(data));
    } else {
      this.dataService.updateBook(this.book)
        .subscribe(data => this.loadBooks());
    }
    this.cancel();
  }
  editBook(b: Book) {
    this.book = b;
  }
  cancel() {
    this.book = new Book();
    this.tableMode = true;
  }
  delete(b: Book) {
    this.dataService.deleteBook(b.id)
      .subscribe(data => this.loadBooks());
  }
  add() {
    this.cancel();
    this.tableMode = false;
  }
}
