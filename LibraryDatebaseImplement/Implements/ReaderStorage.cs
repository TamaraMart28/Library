using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;
using LibraryContracts.StoragesContracts;
using LibraryDatebaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatebaseImplement.Implements
{
    public class ReaderStorage : IReaderStorage
    {
        public List<ReaderViewModel> GetFullList()
        {
            using var context = new LibraryDatabase();

            return context.Readers.ToList()
               .Select(CreateModel)
                .ToList();

            //убрала инклуды
        }

        public List<ReaderViewModel> GetFilteredList(ReaderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            return context.Readers.Where(rec => (rec.Login == model.Login && rec.Password == model.Password) || (rec.Login == model.Login && model.Password == null))
            .ToList().Select(CreateModel)
           .ToList();
            //убрала инклуды
        }

        public ReaderViewModel GetElement(ReaderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            var reader = context.Readers.FirstOrDefault(rec => rec.Id == model.Id || rec.Login == model.Login);
            return reader != null ? CreateModel(reader) : null;
            //убрала инклуды
        }

        public void Insert(ReaderBindingModel model)
        {
            using var context = new LibraryDatabase();

            using var transaction = context.Database.BeginTransaction();

            try
            {
                Reader reader = new Reader
                {
                    Login = model.Login,
                    Password = model.Password,
                    FullName = model.FullName,
                    //FavoriteBooks = model.FavoriteBooks,
                    //ReaderBooks = model.ReaderBooks
                };
                context.Readers.Add(reader);
                context.SaveChanges();
                CreateModel(model, reader, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(ReaderBindingModel model)
        {
            using var context = new LibraryDatabase();

            using var transaction = context.Database.BeginTransaction();

            try
            {
                var element = context.Readers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(ReaderBindingModel model)
        {
            using var context = new LibraryDatabase();

            Reader element = context.Readers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Readers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Reader CreateModel(ReaderBindingModel model, Reader reader, LibraryDatabase context)//
        {
            reader.Login = model.Login;
            reader.Password = model.Password;
            reader.FullName = model.FullName;
            
            var favoriteBooks = new List<Book>();
            /*if (model.FavoriteBooks != null)
            {
                //сделать поверку на то, что книга уже в избранном
                foreach (var bookId in model.FavoriteBooks) 
                {
                    favoriteBooks.Add(context.Books.FirstOrDefault(rec => rec.Id == model.Id));
                }
            }*/
            //reader.FavoriteBooks = favoriteBooks;

            //if (model.ReaderBooks != null)
            //{
            //    var wasteReaderBooks = new List<ReaderBook>();
            //    var newReaderBooks = new List<ReaderBook>();
            //    if (model.Id.HasValue)
            //    {
            //        var readerBooks = context.ReaderBooks.Where(rec => rec.ReaderId == model.Id.Value).ToList();
            //        /*удалили все
            //        context.ReaderBooks.RemoveRange(readerBooks); //ХЗХЗХЗХЗХЗХЗ*/
                    
            //        // удалили те, которых нет в модели
            //        foreach (var obj in readerBooks)//вроде норм
            //        {
            //            if (model.ReaderBooks.ContainsKey((int)obj.Id))
            //            {
            //                newReaderBooks.Add(obj);
            //            }
            //        }
            //        context.ReaderBooks.RemoveRange(readerBooks);
            //        context.SaveChanges();
            //    }
            //    // добавили оставшиеся новые
            //    foreach (var rb in newReaderBooks)
            //    {
            //        context.ReaderBooks.Add(new ReaderBook
            //        {
            //            ReaderId = reader.Id,
            //            BookBranchId = rb.BookBranchId,
            //            DateOut = DateTime.Now,
            //            DateIn = DateTime.Now.AddMonths(1)
            //        });
            //        context.SaveChanges();
            //    }
            //}
            return reader;
        }

        private static ReaderViewModel CreateModel(Reader reader)
        {
            return new ReaderViewModel
            {
                Id = reader.Id,
                Login = reader.Login,
                Password = reader.Password,
                FullName = reader.FullName,
                //FavoriteBooks = reader.FavoriteBooks != null ? reader.FavoriteBooks.ToDictionary(rec => (int)rec.Id, rec => rec.Name) : null,
                //ReaderBooks = reader.ReaderBooks != null ? reader.ReaderBooks.ToDictionary(recRB => recRB.BookBranchId, 
                //recRB => (recRB.BookBranch?.Book?.Name)) : null///name?
            };
        }
    }
}
