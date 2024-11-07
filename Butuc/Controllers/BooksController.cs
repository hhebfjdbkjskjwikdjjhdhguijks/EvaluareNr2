using Butuc.Models;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;

namespace Butuc.Controllers
{
    public class BooksController : Controller
    {
        // Listă în memorie pentru a stoca cărțile
        private static List<Book> books = new List<Book>
        {
            
            // Adaugă mai multe cărți de probă, după nevoie
        };

        // Afișează lista de cărți
        public IActionResult Index()
        {
            return View(books);
        }

        // Creează o carte nouă
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.ID = books.Count > 0 ? books.Max(b => b.ID) + 1 : 1; // Auto-increment ID
                books.Add(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // Exemplar pentru filtrarea după preț
        public IActionResult FilterByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var filteredBooks = books.Where(b => b.Price >= minPrice && b.Price <= maxPrice).ToList();
            return View("Index", filteredBooks);
        }
        public IActionResult Edit(int id)
        {
            var book = books.FirstOrDefault(b => b.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Actualizează informațiile cărții
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.Publisher = updatedBook.Publisher;
                book.Price = updatedBook.Price;
                book.ImageUrl = updatedBook.ImageUrl;

                return RedirectToAction(nameof(Index));
            }

            return View(updatedBook);
        }
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = books.FirstOrDefault(b => b.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            books.Remove(book);
            return RedirectToAction(nameof(Index));
        }
    }
}
