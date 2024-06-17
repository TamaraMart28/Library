using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryContracts.BindingModels;
using LibraryContracts.BusinessLogicsContracts;
using LibraryContracts.ViewModels;


namespace LibraryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderLogic _readerLogic;
        private readonly IBookLogic _bookLogic;
        private readonly IFavoriteBooksLogic _fbLogic;
        private readonly IBookBranchLogic _bbLogic;
        private readonly IReaderBookLogic _rbLogic;
        private readonly IReaderReportLogic _reportLogic;
        private readonly ILibrarianReportLogic _reportLibrarianLogic;

        public ReaderController(IReaderLogic readerLogic, IBookLogic bookLogic, IFavoriteBooksLogic fbLogic, IBookBranchLogic bbLogic, IReaderBookLogic rbLogic, IReaderReportLogic reportLogic, ILibrarianReportLogic reportLibrarianLogic)
        {
            _readerLogic = readerLogic;
            _bookLogic = bookLogic;
            _fbLogic = fbLogic;
            _bbLogic = bbLogic;
            _rbLogic = rbLogic;
            _reportLogic = reportLogic;
            _reportLibrarianLogic = reportLibrarianLogic;
        }

        [HttpGet]
        public ReaderViewModel LoginReader(string login, string password)
        {
            var list = _readerLogic.Read(new ReaderBindingModel
            {
                Login = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpGet]
        public ReaderViewModel GetReader(string login)
        {
            var list = _readerLogic.Read(new ReaderBindingModel
            {
                Login = login,
                Password = null
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void RegisterReader(ReaderBindingModel model) => _readerLogic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateReader(ReaderBindingModel model) => _readerLogic.CreateOrUpdate(model);

        [HttpGet]
        public List<ReaderViewModel> GetReaderList() => _readerLogic.Read(null)?.ToList();

        //книга
        [HttpGet]
        public List<BookViewModel> GetBookList() => _bookLogic.Read(null)?.ToList();

        [HttpGet]
        public BookViewModel GetBook(int bookId) => _bookLogic.Read(new BookBindingModel { Id = bookId })?[0];

        [HttpPost]
        public void CreateOrUpdateBook(BookBindingModel model) => _bookLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteBook(BookBindingModel model) => _bookLogic.Delete(model);

        //[HttpGet]
        //public List<BookBranchViewModel> GetBookBranchList(int bookId) => 

        //избранная книга
        [HttpGet]
        public List<FavoriteBooksViewModel> GetFavoriteBooksList() => _fbLogic.Read(null)?.ToList();

        [HttpGet]
        public FavoriteBooksViewModel GetFavoriteBooks(int fbId) => _fbLogic.Read(new FavoriteBooksBindingModel { Id = fbId })?[0];

        [HttpGet]
        public FavoriteBooksViewModel GetFavoriteBooksByIds(int readerId, int bookId) => _fbLogic.ReadByIds(new FavoriteBooksBindingModel { ReaderId = readerId, BookId = bookId })?[0];

        [HttpGet]
        public List<FavoriteBooksViewModel> GetReadersFavoriteBooks(int readerId) => _fbLogic.Read(new FavoriteBooksBindingModel { ReaderId = readerId });

        [HttpPost]
        public void CreateOrUpdateFavoriteBooks(FavoriteBooksBindingModel model) => _fbLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteFavoriteBooks(FavoriteBooksBindingModel model) => _fbLogic.Delete(model);

        //bookBranch
        [HttpGet]
        public List<BookBranchViewModel> GetBooksBookBranches(int bookId) => _bbLogic.Read(new BookBranchBindingModel { BookId = bookId });

        //readerBook
        [HttpGet]
        public List<ReaderBookViewModel> GetReadersBooks(int readerId) => _rbLogic.Read(new ReaderBookBindingModel { ReaderId = readerId });

        [HttpGet]
        public List<ReaderBookViewModel> GetReadersBooksByBook(int bookId) => _rbLogic.ReadByBookId(new ReaderBookBindingModel { BookId = bookId });

        //report
        [HttpPost]
        public void CreateReportToPdf(ReportBooksBindingModel model) => _reportLogic.SaveReadersBooksToPdf(model);

        [HttpPost]
        public void CreateReportToPdfLibrarian(ReportBooksWithReadersBindingModel model) => _reportLibrarianLogic.SaveReadersBooksToPdf(model);
    }
}
