using LibraryContracts.ViewModels;

namespace LibraryReaderApp.Models
{
    public class IndexViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
