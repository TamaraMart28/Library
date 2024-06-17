using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.StoragesContracts;
using LibraryContracts.ViewModels;
using LibraryDatebaseImplement.Models;

namespace LibraryDatebaseImplement.Implements
{
    public class BookStorage : IBookStorage
    {
        public void Delete(BookBindingModel model)
        {
            using var context = new LibraryDatabase();

            var element = context.Books.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Books.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Книга не найдена");
            }
        }

        public BookViewModel GetElement(BookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new LibraryDatabase();

            var book = context.Books.FirstOrDefault(rec => rec.Id == model.Id || rec.Name == model.Name);
            return book != null ? CreateModel(book) : null;
        }

        public List<BookViewModel> GetFilteredList(BookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new LibraryDatabase();

            return context.Books.Where(rec => rec.Name == model.Name)
                .Select(CreateModel)
                .ToList();

            /*return context.Books.Where(rec => rec.Name == model.Name)
                .Select(rec => new BookViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Author = rec.Author,
                    Genres = rec.Genres,
                    Annotation = rec.Annotation,
                    Year = rec.Year,
                    Image = rec.Image
                }).ToList();*/
        }

        public List<BookViewModel> GetFullList()
        {
            using var context = new LibraryDatabase();

            return context.Books
               .Select(CreateModel).ToList();
            /*return context.Books
               .Select(rec => new BookViewModel
               {
                   Id = rec.Id,
                   Name = rec.Name,
                   Author = rec.Author,
                   Genres = rec.Genres,
                   Annotation = rec.Annotation,
                   Year = rec.Year,
                   Image = rec.Image
               }).ToList();*/
        }
        
        public void Insert(BookBindingModel model)
        {
            using var context = new LibraryDatabase();
            context.Books.Add(CreateModel(model, new Book()));
            context.SaveChanges();
        }

        public void Update(BookBindingModel model)
        {
            using var context = new LibraryDatabase();

            var element = context.Books.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Книга не найдена");
            }

            CreateModel(model, element);
            context.SaveChanges();
        }

        private static Book CreateModel(BookBindingModel model, Book book)
        {
            book.Name = model.Name;
            book.Author = model.Author;
            book.Genres = model.Genres;
            book.Annotation = model.Annotation;
            book.Year = model.Year;
            book.ImageName = model.ImageName;
            book.ImagePath = model.ImagePath;
            return book;
        }

        private static BookViewModel CreateModel(Book book)
        {
            return new BookViewModel
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Genres = book.Genres,
                Annotation = book.Annotation,
                Year = book.Year,
                ImageName = book.ImageName,
                ImagePath = book.ImagePath
            };
        }
    }
}
