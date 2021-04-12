using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WepApi.ModelBinders;
using WepApi.Validators;

namespace WepApi.Controllers
{
    // GET    http://somesite.com/books/
    // GET    http://somesite.com/books/12
    // POST   http://somesite.com/books/
    // PUT    http://somesite.com/books/12 with model in body
    // DELETE http://somesite.com/books/12
    // GET    http://somesite.com/books/authors?name with name in query string
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Name = "CLR VIA C#", Author = "Jeffrey Richter", ReleaseDate = new DateTime(2010, 5, 8) },
            new Book { Id = 2, Name = "ASP.NET Core in Action", Author = "Andrew Lock", ReleaseDate = new DateTime(2021, 4, 1) },
            new Book { Id = 3, Name = "The Clean Coder: A Code of Conduct for Professional Programmers", Author = "Robert Cecil Martin", ReleaseDate = new DateTime(2015, 10, 10) },
            new Book { Id = 4, Name = "Refactoring", Author = "Martin Fowler", ReleaseDate = new DateTime(2017, 8, 9) },
            new Book { Id = 5, Name = "Clean Code", Author = "Robert Cecil Martin", ReleaseDate = new DateTime(2008, 3, 28) },
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = Books.FirstOrDefault(x => x.Id == id);
            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            Books.Add(book);

            return Created(nameof(GetById), new { id = book.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book newBook)
        {
            var book = Books.FirstOrDefault(x => x.Id == newBook.Id);
            if (book is null)
            {
                return NotFound();
            }

            book = newBook;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = Books.FirstOrDefault(x => x.Id == id);
            if (book is null)
            {
                return NotFound();
            };

            Books.Remove(book);

            return NoContent();
        }

        [HttpGet("authors")]
        public IActionResult GetBooksForAuthor(string name)
        {
            var books = Books.Where(x => x.Author == name);

            return Ok(books);
        }

        [HttpGet("filtering")]
        public IActionResult GetByDateRange(DateRange dateRange)
        {
            var books = Books.Where(x => x.ReleaseDate < dateRange.EndDate && x.ReleaseDate > dateRange.StartDate);
            return Ok(books);
        }

        [HttpGet("filteringByName")]
        public IActionResult GetByName([FromQuery]string[] names)
        {
            var books = Books.Where(x => names.Contains(x.Name));
            return Ok(books);
        }
    }

    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
