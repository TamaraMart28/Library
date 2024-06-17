using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.StoragesContracts;
using LibraryContracts.ViewModels;
using LibraryContracts.BindingModels;
using LibraryDatebaseImplement.Models;

namespace LibraryDatebaseImplement.Implements
{
    public class AdminStorage : IAdminStorage
    {
        public List<AdminViewModel> GetFullList()
        {
            using var context = new LibraryDatabase();

            return context.Admins
               .Select(rec => new AdminViewModel
               {
                   Id = rec.Id,
                   Login = rec.Login,
                   Password = rec.Password
               })
                .ToList();
        }

        public List<AdminViewModel> GetFilteredList(AdminBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            return context.Admins.Where(rec => (rec.Login == model.Login && rec.Password == model.Password))
            .ToList().Select(CreateModel).ToList();
        }

        private static AdminViewModel CreateModel(Admin admin)
        {
            return new AdminViewModel
            {
                Id = admin.Id,
                Login = admin.Login,
                Password = admin.Password
            };
        }

        //public List<AdminViewModel> GetFilteredList(AdminBindingModel model)

        //public AdminViewModel GetElement(AdminBindingModel model)

        public void Insert(AdminBindingModel model)
        {
            using var context = new LibraryDatabase();

            context.Admins.Add(CreateModel(model, new Admin()));
            context.SaveChanges();
        }

        public void Update(AdminBindingModel model)
        {
            using var context = new LibraryDatabase();

            var element = context.Admins.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Admin не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        public void Delete(AdminBindingModel model)
        {
            using var context = new LibraryDatabase();

            var element = context.Admins.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Admins.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Admin не найден");
            }

        }

        private Admin CreateModel(AdminBindingModel model, Admin _admin)
        {
            _admin.Login = model.Login;
            _admin.Password = model.Password;
            return _admin;
        }
    }
}
