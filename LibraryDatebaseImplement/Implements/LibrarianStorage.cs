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
    public class LibrarianStorage : ILibrarianStorage
    {
        public List<LibrarianViewModel> GetFullList()//
        {
            using var context = new LibraryDatabase();

            return context.Librarians
               .Select(rec => new LibrarianViewModel
               {
                   Id = rec.Id,
                   Login = rec.Login,
                   Password = rec.Password,
                   FullName = rec.FullName,
                   BranchId = rec.BranchId
                   
               }).ToList();
        }

        public List<LibrarianViewModel> GetFilteredList(LibrarianBindingModel model)//
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            return context.Librarians.Where(rec => rec.Login == model.Login && rec.Password == rec.Password)
            .Select(rec => new LibrarianViewModel
            {
                Id = rec.Id,
                Login = rec.Login,
                Password = rec.Password,
                FullName = rec.FullName,
                BranchId = rec.BranchId
            }).ToList();
        }


        public LibrarianViewModel GetElement(LibrarianBindingModel model)//
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            var _librarian = context.Librarians.FirstOrDefault(rec => rec.Id == model.Id || rec.Login == model.Login);
            return _librarian != null ? new LibrarianViewModel
            {
                Id = _librarian.Id,
                FullName = _librarian.FullName,
                BranchId = _librarian.BranchId,
                Login = _librarian.Login,
                Password = _librarian.Password
            } : null;
        }

        public void Insert(LibrarianBindingModel model)//
        {
            using var context = new LibraryDatabase();

            context.Librarians.Add(CreateModel(model, new Librarian()));
            context.SaveChanges();
        }

        public void Update(LibrarianBindingModel model)//
        {
            using var context = new LibraryDatabase();

            var element = context.Librarians.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Библиотекарь не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        public void Delete(LibrarianBindingModel model)//
        {
            using var context = new LibraryDatabase();

            var element = context.Librarians.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Librarians.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Библиотекарь не найден");
            }

        }

        private Librarian CreateModel(LibrarianBindingModel model, Librarian _librarian)//
        {
            _librarian.FullName = model.FullName;
            _librarian.BranchId = model.BranchId;
            _librarian.Login = model.Login;
            _librarian.Password = model.Password;
            return _librarian;
        }
    }
}
