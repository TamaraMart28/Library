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
    public class FavoriteBooksStorage : IFavoriteBooksStorage
    {
        public List<FavoriteBooksViewModel> GetFullList()
        {
            using var context = new LibraryDatabase();

            return context.FavoriteBooks
               .Select(rec => new FavoriteBooksViewModel
               {
                   Id = rec.Id,
                   ReaderId = rec.ReaderId,
                   BookId = rec.BookId
               })
                .ToList();
        }

        public List<FavoriteBooksViewModel> GetFilteredList(FavoriteBooksBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            return context.FavoriteBooks.Where(rec => rec.ReaderId == model.ReaderId)
            .Select(rec => new FavoriteBooksViewModel
            {
                Id = rec.Id,
                ReaderId = rec.ReaderId,
                BookId = rec.BookId
            })
            .ToList();
        }

        public FavoriteBooksViewModel GetElement(FavoriteBooksBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            var _fb = context.FavoriteBooks.FirstOrDefault(rec => rec.Id == model.Id || (rec.ReaderId == model.ReaderId && rec.BookId == model.BookId));
            return _fb != null ? new FavoriteBooksViewModel
            {
                Id = _fb.Id,
                ReaderId = _fb.ReaderId,
                BookId = _fb.BookId
            } :
            null;
        }

        public FavoriteBooksViewModel GetElementByIds(FavoriteBooksBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            var _fb = context.FavoriteBooks.FirstOrDefault(rec => rec.ReaderId == model.ReaderId && rec.BookId == model.BookId);
            return _fb != null ? new FavoriteBooksViewModel
            {
                Id = _fb.Id,
                ReaderId = _fb.ReaderId,
                BookId = _fb.BookId
            } :
            null;
        }

        public void Insert(FavoriteBooksBindingModel model)
        {
            using var context = new LibraryDatabase();

            context.FavoriteBooks.Add(CreateModel(model, new FavoriteBooks()));
            context.SaveChanges();
        }

        public void Delete(FavoriteBooksBindingModel model)
        {
            using var context = new LibraryDatabase();

            var element = context.FavoriteBooks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.FavoriteBooks.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Ошибка");
            }
        }

        private FavoriteBooks CreateModel(FavoriteBooksBindingModel model, FavoriteBooks _fb)
        {
            _fb.ReaderId = model.ReaderId;
            _fb.BookId = model.BookId;
            return _fb;
        }
    }
}
