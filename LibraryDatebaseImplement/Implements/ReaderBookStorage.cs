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
    public class ReaderBookStorage : IReaderBookStorage
    {
        public List<ReaderBookViewModel> GetFullList()
        {
            using var context = new LibraryDatabase();

            return context.ReaderBooks
               .Select(rec => new ReaderBookViewModel
               {
                   Id = rec.Id,
                   BookBranchId = rec.BookBranchId,
                   ReaderId = rec.ReaderId,
                   BookId = rec.BookId,
                   DateOut = rec.DateOut,
                   DateIn = rec.DateIn,
                   Status = rec.Status
               })
                .ToList();
        }

        public List<ReaderBookViewModel> GetFilteredList(ReaderBookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            return context.ReaderBooks.Where(rec => rec.ReaderId == model.ReaderId)
            .Select(rec => new ReaderBookViewModel
            {
                Id = rec.Id,
                BookBranchId = rec.BookBranchId,
                ReaderId = rec.ReaderId,
                BookId = rec.BookId,
                DateOut = rec.DateOut,
                DateIn = rec.DateIn,
                Status = rec.Status
            })
            .ToList();
        }

        public List<ReaderBookViewModel> GetFilteredListByBookId(ReaderBookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new LibraryDatabase();

            return context.ReaderBooks.Where(rec => rec.BookId == model.BookId)
            .Select(rec => new ReaderBookViewModel
            {
                Id = rec.Id,
                BookBranchId = rec.BookBranchId,
                ReaderId = rec.ReaderId,
                BookId = rec.BookId,
                DateOut = rec.DateOut,
                DateIn = rec.DateIn,
                Status = rec.Status
            })
            .ToList();
        }

        public ReaderBookViewModel GetElement(ReaderBookBindingModel model)//to do
        {
            //if (model == null)
            //{
            //    return null;
            //}
            //using var context = new LibraryDatabase();

            //var _bb = context.BookBranches.FirstOrDefault(rec => rec.Id == model.Id || rec.BookId == model.BookId);
            //return _bb != null ? new BookBranchViewModel
            //{
            //    Id = _bb.Id,
            //    BranchId = _bb.BranchId,
            //    BookId = _bb.BookId,
            //    Amount = _bb.Amount,
            //    AmountInStock = _bb.AmountInStock
            //} :
            //null;
            return null;
        }

        public void Insert(ReaderBookBindingModel model)//to do
        {
            //using var context = new LibraryDatabase();

            //context.Branches.Add(CreateModel(model, new Branch()));
            //context.SaveChanges();
        }

        public void Update(ReaderBookBindingModel model)//to do
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

        public void Delete(ReaderBookBindingModel model)//to do
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

        private ReaderBook CreateModel(ReaderBookBindingModel model, ReaderBook _rb)// to do
        {
            return null;
        }
    }
}
