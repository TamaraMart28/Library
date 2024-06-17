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
    public class ReaderReportLogic : IReaderReportLogic
    {
        private readonly IReaderBookStorage _rbStorage;
        private readonly IBookStorage _bStorage;
        private readonly IReaderBookLogic _rbLogic;
        private readonly IBookLogic _bLogic;
        private readonly IBookBranchLogic _bbLogic;
        private readonly ReaderAbstractSaveToPdf _saveToPdf;

        public ReaderReportLogic(IReaderBookStorage rbStorage, IBookStorage bStorage, IReaderBookLogic rbLogic, IBookLogic bLogic, IBookBranchLogic bbLogic, ReaderAbstractSaveToPdf saveToPdf)
        {
            _rbStorage = rbStorage;
            _bStorage = bStorage;
            _rbLogic = rbLogic;
            _bLogic = bLogic;
            _bbLogic = bbLogic;
            _saveToPdf = saveToPdf;
        }

        // отчет по книгам за выбранный период
        public List<ReportBooksViewModel> GetReadersBooks(ReportBooksBindingModel model)
        {
            var list = new List<ReportBooksViewModel>();

            var rbListAll = _rbLogic.Read(new ReaderBookBindingModel { ReaderId = model.ReaderId });
            var rbList = rbListAll.Where(p => p.DateIn >= model.DateFrom && p.DateIn <= model.DateTo);
            rbList = rbList.Where(p => (int)p.Status == (int)BookBranchStatus.Принята);

            foreach (var item in rbList)
            {
                var book = _bLogic.Read(new BookBindingModel { Id = item.BookId })?[0];

                list.Add(new ReportBooksViewModel { Name = book.Name, Author = book.Author, DateOut = item.DateOut, DateIn = (DateTime)item.DateIn });
            }

            return list;
        }

        public void SaveReadersBooksToPdf(ReportBooksBindingModel model)
        {
            _saveToPdf.CreateDoc(new ReaderPdfInfo
            {
                FileName = model.FileName,
                Title = "Прочитанные книги",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                ReadersBooks = GetReadersBooks(model)
            });
        }
    }
}
