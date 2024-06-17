using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryContracts.BindingModels;
using LibraryContracts.BusinessLogicsContracts;
using LibraryContracts.ViewModels;


namespace LibraryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminLogic _adminLogic;
        private readonly ILibrarianLogic _librarianLogic;
        private readonly IBranchLogic _branchLogic;
        private readonly IBookBranchLogic _bookBranchLogic;

        public AdminController(IAdminLogic adminLogic, ILibrarianLogic librarianLogic, IBranchLogic branchLogic, IBookBranchLogic bookBranchLogic)
        {
            _adminLogic = adminLogic;
            _librarianLogic = librarianLogic;
            _branchLogic = branchLogic;
            _bookBranchLogic = bookBranchLogic;
        }

        [HttpGet]
        public AdminViewModel LoginAdmin(string login, string password)
        {
            var list = _adminLogic.Read(new AdminBindingModel
            {
                Login = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void CreateAdmin(AdminBindingModel model) => _adminLogic.Create(model);

        //филиал

        [HttpGet]
        public List<BranchViewModel> GetBranchList() => _branchLogic.Read(null)?.ToList();

        [HttpGet]
        public BranchViewModel GetBranch(int branchId) => _branchLogic.Read(new BranchBindingModel { Id = branchId })?[0];

        [HttpPost]
        public void CreateOrUpdateBranch(BranchBindingModel model) => _branchLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteBranch(BranchBindingModel model) => _branchLogic.Delete(model);

        [HttpGet]
        public List<BookBranchViewModel> GetBookBranchList() => _bookBranchLogic.Read(null)?.ToList(); // надо ли

        [HttpGet]
        public BookBranchViewModel GetBookBranch(int bookBranchId) => _bookBranchLogic.Read(new BookBranchBindingModel { Id = bookBranchId })?[0];

        //библиотекарь
        [HttpGet]
        public List<LibrarianViewModel> GetLibrarianList() => _librarianLogic.Read(null)?.ToList();

        [HttpGet]
        public LibrarianViewModel GetLibrarian(int librarianId) => _librarianLogic.Read(new LibrarianBindingModel { Id = librarianId })?[0];

        [HttpPost]
        public void CreateOrUpdateLibrarian(LibrarianBindingModel model) => _librarianLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteLibrarian(LibrarianBindingModel model) => _librarianLogic.Delete(model);

    }
}
