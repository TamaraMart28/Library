using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.StoragesContracts;
using LibraryContracts.ViewModels;
using LibraryContracts.BusinessLogicsContracts;
using LibraryBusinessLogics.OfficePackage;
using LibraryBusinessLogics.OfficePackage.HelperModels;
using LibraryContracts.Enums;

namespace LibraryBusinessLogics.BusinessLogics
{
    public class LibrarianReportLogic : ILibrarianReportLogic
    {
        private readonly IReaderBookLogic _rbLogic;
        private readonly IBookLogic _bLogic;
        private readonly IBookBranchLogic _bbLogic;
        private readonly IReaderLogic _rLogic;
        private readonly ILibrarianLogic _lLogic;
        private readonly LibrarianAbstractSaveToPdf _saveToPdf;

        public LibrarianReportLogic(IReaderBookLogic rbLogic, IBookLogic bLogic, IBookBranchLogic bbLogic, IReaderLogic rLogic, ILibrarianLogic lLogic, LibrarianAbstractSaveToPdf saveToPdf)
        {
            _rbLogic = rbLogic;
            _bLogic = bLogic;
            _bbLogic = bbLogic;
            _rLogic = rLogic;
            _lLogic = lLogic;
            _saveToPdf = saveToPdf;
        }

        // отчет по книгам за выбранный период
        public List<ReportBooksWithReadersViewModel> GetBooksWithReaders(ReportBooksWithReadersBindingModel model)
        {
            var list = new List<ReportBooksWithReadersViewModel>();
            //получаем библиотекаря чтобы потом удостоверится, что запись о книге относится к его филиалу
            var librarian = _lLogic.Read(new LibrarianBindingModel { Id = model.LibrarianId })?[0];
            //получаем список всех readerBooks
            var rbListAll = _rbLogic.Read(null)?.ToList();
            //берем нужные по дате выдачи в диапазоне
            var rbList = rbListAll.Where(p => p.DateOut >= model.DateFrom && p.DateOut <= model.DateTo);
            //отсекаем все записи где не совпадают филиалы
            var rbListWithRightBranchId = rbList;
            foreach (var item in rbList)
            {
                //получаем bookBranch очередной записи readerbook чтобы проверить что филиал взятой книжки = филиалу библиотекаря
                var bookBranch = _bbLogic.Read(new BookBranchBindingModel { Id = item.BookBranchId })?[0];
                if (bookBranch.BranchId != librarian.BranchId)
                {
                    rbListWithRightBranchId = rbListWithRightBranchId.Where(p => p.BookBranchId != bookBranch.Id);
                }
            }
            //получаем отсюда все книжки
            var BooksIdInList = new List<BookAndDate>();
            foreach (var item in rbListWithRightBranchId) 
            {
                var New = new BookAndDate { BookId = item.BookId, DateOut = item.DateOut };
                BooksIdInList.RemoveAll(item => item.BookId == New.BookId && item.DateOut == New.DateOut);
                BooksIdInList.Add(New);
                
            }
            //убираем повторяющиеся
            //var BooksIdInListUnic = BooksIdInList.Distinct().ToList();
            //теперь спокойно (надеюсь) собираем данные
            foreach (var item in BooksIdInList)
            {
                //получаем книжку чтоб достать название
                var book = _bLogic.Read(new BookBindingModel { Id = item.BookId })?[0];
                //получаем из нашего списка все записи readerbook с той же книжкой и датой 
                var itemsWithThisBookAndDate = rbListWithRightBranchId.Where(p => p.BookId == item.BookId && p.DateOut == item.DateOut);
                //формируем клиентиков
                //var readers = new List<string>();
                var readers = "";
                foreach (var r in itemsWithThisBookAndDate)
                {
                    var reader = _rLogic.Read(new ReaderBindingModel { Id = r.ReaderId})?[0];
                    readers = readers + reader.FullName + "; ";
                }
                //наконец добавляем запись в ГЛАВНЫЙ лист
                list.Add(new ReportBooksWithReadersViewModel { DateOut = item.DateOut, Name = book.Name, Readers = readers});
            }
            return list;
        }

        public void SaveReadersBooksToPdf(ReportBooksWithReadersBindingModel model)
        {
            _saveToPdf.CreateDoc(new LibrarianPdfInfo
            {
                FileName = model.FileName,
                Title = "Выданные книги",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                BooksWithReaders = GetBooksWithReaders(model)
            });
        }
    }
}
