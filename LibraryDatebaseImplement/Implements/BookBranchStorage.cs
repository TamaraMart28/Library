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
    public class BookBranchStorage : IBookBranchStorage
    {
        public List<BookBranchViewModel> GetFullList()
        {
            using var context = new LibraryDatabase();

            return context.BookBranches
               .Select(rec => new BookBranchViewModel
               {
                   Id = rec.Id,
                   BranchId = rec.BranchId,
                   BookId = rec.BookId,
                   Amount = rec.Amount,
                   AmountInStock = rec.AmountInStock
                })
                .ToList();
        }

        public List<BookBranchViewModel> GetFilteredList(BookBranchBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            return context.BookBranches.Where(rec => rec.BookId == model.BookId)
            .Select(rec => new BookBranchViewModel
            {
                Id = rec.Id,
                BranchId = rec.BranchId,
                BookId = rec.BookId,
                Amount = rec.Amount,
                AmountInStock = rec.AmountInStock
            })
            .ToList();
        }

        public BookBranchViewModel GetElement(BookBranchBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            var _bb = context.BookBranches.FirstOrDefault(rec => rec.Id == model.Id || rec.BookId == model.BookId);
            return _bb != null ? new BookBranchViewModel
            {
                Id = _bb.Id,
                BranchId = _bb.BranchId,
                BookId = _bb.BookId,
                Amount = _bb.Amount,
                AmountInStock = _bb.AmountInStock
            } :
            null;
        }

        public void Insert(BookBranchBindingModel model)//to do
        {
            //using var context = new LibraryDatabase();

            //context.Branches.Add(CreateModel(model, new Branch()));
            //context.SaveChanges();
        }

        public void Update(BookBranchBindingModel model)//to do
        {
            //using var context = new LibraryDatabase();

            //var element = context.Branches.FirstOrDefault(rec => rec.Id == model.Id);

            //if (element == null)
            //{
            //    throw new Exception("Филиал не найден");
            //}
            //CreateModel(model, element);
            //context.SaveChanges();
        }

        public void Delete(BookBranchBindingModel model)//to do
        {
            //using var context = new LibraryDatabase();

            //var element = context.Branches.FirstOrDefault(rec => rec.Id == model.Id);
            //if (element != null)
            //{
            //    context.Branches.Remove(element);
            //    context.SaveChanges();
            //}
            //else
            //{
            //    throw new Exception("Филиал не найден");
            //}
        }

        private BookBranch CreateModel(BookBranchBindingModel model, BookBranch _bb)
        {
            _bb.BranchId = model.BranchId;
            _bb.BookId = model.BookId;
            _bb.Amount = model.Amount;
            _bb.AmountInStock = model.AmountInStock;
            return _bb;
        }
    }
}
