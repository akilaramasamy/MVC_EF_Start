using Microsoft.AspNetCore.Mvc;
using MVC_EF_Start.DataAccess;
using MVC_EF_Start.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EF_Start.Controllers
{
    public class BookController : Controller
    {

        public ApplicationDbContext dbContext;

        public BookController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> DatabaseOperations()
        {
            // CREATE operation
            Book myBook = new Book();
            //myBook.BookId =123;
            myBook.Title = "Book1";
            myBook.Author = "ISM";


            Category myCategory = new Category();
            //myCategory.CategoryId = 456;
            myCategory.CategoryName = "cat1";

            BookCategory bookCategory = new BookCategory();
            bookCategory.Book = myBook;
            bookCategory.Category = myCategory;

            dbContext.Books.Add(myBook);
            dbContext.Categories.Add(myCategory);
            dbContext.BookCategories.Add(bookCategory);

            dbContext.SaveChanges();

            // READ operation
            Book book = dbContext.Books
                                    .Where(c => c.Title == "Book1")
                                    .First();


            // UPDATE operation
            myBook.Title = "Title2";
            dbContext.Books.Update(myBook);
            //dbContext.SaveChanges();
            await dbContext.SaveChangesAsync();

            // DELETE operation
            //dbContext.Companies.Remove(CompanyRead1);
            //await dbContext.SaveChangesAsync();

            return View("Index");
        }

        public ViewResult LINQOperations()
        {
            Book book = dbContext.Books
                                            .Where(c => c.Title == "Title2")
                                            .First();

            Category category = dbContext.Categories
                                            .Where(c => c.CategoryName == "Cat1")
                                            .First();

            BookCategory bookCategory = dbContext.BookCategories
                                    .Where(c => c.BookId == 123)
                                    .FirstOrDefault();
                                    

            return View();
        }
    }
}
