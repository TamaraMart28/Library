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
    public class BranchStorage : IBranchStorage
    {
        public List<BranchViewModel> GetFullList()
        {
            using var context = new LibraryDatabase();

            return context.Branches
               .Select(rec => new BranchViewModel
               {
                   Id = rec.Id,
                   Address = rec.Address,
                   Schedule = rec.Schedule,
                   Сoordinate1 = rec.Сoordinate1,
                   Сoordinate2 = rec.Сoordinate2
               })
                .ToList();
        }

        public List<BranchViewModel> GetFilteredList(BranchBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            return context.Branches.Where(rec => rec.Address == model.Address)
            .Select(rec => new BranchViewModel
            {
                Id = rec.Id,
                Address = rec.Address,
                Schedule = rec.Schedule,
                Сoordinate1 = rec.Сoordinate1,
                Сoordinate2 = rec.Сoordinate2
            })
            .ToList();
        }


        public BranchViewModel GetElement(BranchBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            var _branch = context.Branches.FirstOrDefault(rec => rec.Id == model.Id || rec.Address == model.Address);
            return _branch != null ? new BranchViewModel
            {
                Id = _branch.Id,
                Address = _branch.Address,
                Schedule = _branch.Schedule,
                Сoordinate1 = _branch.Сoordinate1,
                Сoordinate2 = _branch.Сoordinate2
            } :
            null;
        }

        public void Insert(BranchBindingModel model)
        {
            using var context = new LibraryDatabase();

            context.Branches.Add(CreateModel(model, new Branch()));
            context.SaveChanges();
        }

        public void Update(BranchBindingModel model)
        {
            using var context = new LibraryDatabase();

            var element = context.Branches.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Филиал не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        public void Delete(BranchBindingModel model)
        {
            using var context = new LibraryDatabase();

            var element = context.Branches.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Branches.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Филиал не найден");
            }
        }

        private Branch CreateModel(BranchBindingModel model, Branch _branch)
        {
            _branch.Address = model.Address;
            _branch.Schedule = model.Schedule;
            _branch.Сoordinate1 = model.Сoordinate1;
            _branch.Сoordinate2 = model.Сoordinate2;
            return _branch;
        }
    }
}
