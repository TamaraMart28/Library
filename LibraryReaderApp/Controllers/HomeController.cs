using LibraryReaderApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;
using Newtonsoft.Json;
using LibraryContracts.Enums;
using System.Text.RegularExpressions;

namespace LibraryReaderApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public void LogoutReader()
        {
            Session.Reader = null;
            Response.Redirect("Index");
            return;
        }

        public void LogoutAdmin()
        {
            Session.Admin = null;
            Response.Redirect("Index");
            return;
        }

        public IActionResult Privacy()
        {
            var rbListAll = APIClient.GetRequest<List<ReaderBookViewModel>>($"api/reader/getreadersbooks?readerId={Session.Reader.Id}");
            IEnumerable<ReaderBookViewModel> rbListDolg = rbListAll;
            rbListDolg = rbListDolg.Where(p => p.Status == 0);

            var ReadersBooksDolg = new List<ReaderBooksDolgForTable>();
            foreach (var item in rbListDolg)
            {
                var book = APIClient.GetRequest<BookViewModel>($"api/reader/getbook?bookId={item.BookId}");
                var bookBranch = APIClient.GetRequest<BookBranchViewModel>($"api/admin/getbookbranch?bookBranchId={item.BookBranchId}");
                var branch = APIClient.GetRequest<BranchViewModel>($"api/admin/getbranch?branchId={bookBranch.BranchId}");

                ReadersBooksDolg.Add(new ReaderBooksDolgForTable { Name = book.Name, Address = branch.Address, DateOut = item.DateOut, Srok = "1 месяц" });
            }
            ViewBag.ReadersBooksDolg = ReadersBooksDolg;

            return View();
        }


        [HttpGet]
        public IActionResult RegisterReader()
        {
            return View();
        }

        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 8;
        [HttpPost]
        public void RegisterReader(string login, string password, string fio)
        {
            try
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fio))
                {
                    throw new Exception("Введите логин, пароль и ФИО");
                }
                

                var reader = APIClient.GetRequest<ReaderViewModel>($"api/reader/getreader?login={login}");

                if (login != null && reader != null && login == reader.Login)
                {
                    throw new Exception("Учетная запись по такому логину уже существует");
                }
                if (!Regex.IsMatch(login, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                {
                    throw new Exception("В качестве логина должна быть указана почта");
                }
                if (password.Length > _passwordMaxLength || password.Length < _passwordMinLength
                    || !Regex.IsMatch(password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
                {
                    throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен состоять из цифр, букв и небуквенных символов");
                }

                if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(fio))
                {
                    APIClient.PostRequest("api/reader/registerreader", new ReaderBindingModel
                    {
                        FullName = fio,
                        Login = login,
                        Password = password,
                        FavoriteBooks = new Dictionary<int, string>(),
                        ReaderBooks = new Dictionary<int, string>()
                    });
                    TempData["Message"] = "Регистрация прошла успешно!";
                    Response.Redirect("LoginReader");
                    return;
                }
                throw new Exception("Введите логин, пароль и ФИО");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                Response.Redirect("RegisterReader");
                return;
            }
        }

        [HttpGet]
        public IActionResult LoginReader()
        {
            return View();
        }

        [HttpPost]
        public void LoginReader(string login, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    throw new Exception("Введите логин и пароль");
                }

                if (!Regex.IsMatch(login, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                {
                    throw new Exception("В качестве логина должна быть указана почта");
                }
                if (password.Length > _passwordMaxLength || password.Length < _passwordMinLength
                    || !Regex.IsMatch(password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
                {
                    throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен состоять из цифр, букв и небуквенных символов");
                }
                if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
                {
                    Session.Reader = APIClient.GetRequest<ReaderViewModel>($"api/reader/loginreader?login={login}&password={password}");
                    if (Session.Reader == null)
                    {
                        throw new Exception("Неверный логин/пароль");
                    }
                    Response.Redirect("Index");
                    return;
                }
                throw new Exception("Введите логин и пароль");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                Response.Redirect("LoginReader");
                return;
            }
        }

        [HttpPost]
        public void UpdateReader(string fio)
        {
            try
            {
                if (!string.IsNullOrEmpty(fio))
                {
                    APIClient.PostRequest("api/reader/updatereader", new ReaderBindingModel
                    {
                        Id = Session.Reader.Id,
                        Login = Session.Reader.Login,
                        Password = Session.Reader.Password,
                        FullName = fio,
                        FavoriteBooks = new Dictionary<int, string>(),
                        ReaderBooks = new Dictionary<int, string>()
                    });
                    Session.Reader.FullName = fio;
                    Response.Redirect("Privacy");
                    return;
                }
                throw new Exception("Введите имя");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                Response.Redirect("Privacy");
                return;
            }
        }

        [HttpGet]//done
        public IActionResult Branches()
        {
            var branches = APIClient.GetRequest<List<BranchViewModel>>($"api/admin/getbranchlist");
            // Создание списка координат маркеров
            var coordinatesList = new List<Tuple<double, double>>();
            var infoList = new List<String>();
            foreach (var branch in branches)
            {
                coordinatesList.Add(new Tuple<double, double>(branch.Сoordinate1, branch.Сoordinate2));
                infoList.Add(branch.Address + " " + branch.Schedule);
            }
            
            // Сериализация списка координат в JSON
            var json = JsonConvert.SerializeObject(coordinatesList);

            // Передача JSON в ViewBag
            ViewBag.CoordinatesListJson = json;

            var json2 = JsonConvert.SerializeObject(infoList);
            ViewBag.InfoListJson = json2;

            //double[,] coordinates = new double[branches.Count, 2];
            //for (int i = 0; i < branches.Count; i++)
            //{
            //    coordinates[i, 0] = branches[i].Сoordinate1;
            //    coordinates[i, 1] = branches[i].Сoordinate2;
            //}
            //ViewBag.BranchesC = coordinates;
            //ViewBag.Branches = branches;
            //ViewBag.BranchesSize = branches.Count;
            //ViewBag.number1 = 48.447307;
            //ViewBag.number2 = 54.313104;
            //ViewBag.number3 = 55.289254243403306;
            //ViewBag.number4 = 25.211614202468652;
            //var info = new List<string>();
            //foreach(var item in branches)
            //{
            //    info.Add(item.Address);
            //}
            //ViewBag.info = info;
            return View();
        }

        [HttpGet]//done
        public async Task<IActionResult> Books(int page = 1, string? name = "", string? author = "", string? genre = "", string? genreinput = "")
        {
            int pageSize = 2;   // количество элементов на странице
            
            var books = APIClient.GetRequest<List<BookViewModel>>($"api/reader/getbooklist");
            
            IEnumerable<BookViewModel> selectedBooks = books;
            if (!string.IsNullOrWhiteSpace(name))
            {
                selectedBooks = selectedBooks.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(author))
            {
                selectedBooks = selectedBooks.Where(p => p.Author.ToLower().Contains(author.ToLower()));
            }
            //if (genre == "Все") genre = "";
            //if (!string.IsNullOrWhiteSpace(genre) || !string.IsNullOrWhiteSpace(genreinput))
            //{
            //    selectedBooks = selectedBooks.Where(p => p.Genres.Contains(genreinput));
            //}
            if (genre == "Все") genre = "";
            if (!string.IsNullOrWhiteSpace(genre))
            {
                selectedBooks = selectedBooks.Where(p => p.Genres.Contains(genre));
            }
            if (genre == "") genre = "Все";
            List<Genres> genresList = Enum.GetValues(typeof(Genres))
                            .Cast<Genres>()
                            .ToList();
            ViewBag.Genres = genresList;

            selectedBooks = selectedBooks.Reverse();

            var count = selectedBooks.Count();
            var items = selectedBooks.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Books = items
            };
            ViewBag.Name = name;
            ViewBag.Author = author;
            //ViewBag.Genre = genreinput;
            ViewBag.Genre = genre;
            return View(viewModel);
        }

        [HttpGet]//done
        public IActionResult Book(int bookId)
        {
            var book = APIClient.GetRequest<BookViewModel>($"api/reader/getbook?bookId={bookId}");
            var bbList = APIClient.GetRequest<List<BookBranchViewModel>>($"api/reader/getbooksbookbranches?bookId={bookId}");

            var bbDictionary = new Dictionary<string, int>();
            foreach (var bb in bbList)
            {
                var brunch = APIClient.GetRequest<BranchViewModel>($"api/admin/getbranch?branchId={bb.BranchId}");
                bbDictionary.Add(brunch.Address, bb.AmountInStock);
            }
            ViewBag.bbDictionary = bbDictionary;

            //рейтинг
            var rbListAll = APIClient.GetRequest<List<ReaderBookViewModel>>($"api/reader/getreadersbooksbybook?bookId={bookId}");
            var rating = rbListAll.Count;
            ViewBag.count = rbListAll.Count;
            if (rating > 5) rating = 5;
            ViewBag.rating = rating;

            return View(book);
        }

        [HttpGet]//done
        public IActionResult FavoriteBooks(int page = 1)
        {
            var fbList = APIClient.GetRequest<List<FavoriteBooksViewModel>>($"api/reader/getreadersfavoritebooks?readerId={Session.Reader.Id}");
            //
            var books = new List<BookViewModel>();

            foreach (var fb in fbList) 
            {
                var book = APIClient.GetRequest<BookViewModel>($"api/reader/getbook?bookId={fb.BookId}");
                books.Add(book);
            }

            int pageSize = 2;
            var count = books.Count();
            var items = books.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Books = items
            };

            return View(viewModel);
        }

        [HttpGet]//done
        public void AddFavoriteBook(int bookId, int readerId)
        {
            APIClient.PostRequest("api/reader/createorupdatefavoritebooks", new FavoriteBooksBindingModel
            {
                ReaderId = readerId,
                BookId = bookId
            });
            Response.Redirect($"Book?bookId={bookId}");
            TempData["Message"] = "Книга добавлена в Избранное";
            return;


            //var fav = new Dictionary<int, string>();
            //fav.Add(bookId, name);

            //List<BookViewModel> books = new List<BookViewModel>();
            //books.Add(new BookViewModel() { Id = bookId, Name = name, Author = "Author1", Annotation = "Ann1", Genres = "Драма, G2", Image = null, Year = 1982, BookBranches = null });
            //APIClient.PostRequest("api/reader/updatereader", new ReaderBindingModel
            //{
            //    Id = Session.Reader.Id,
            //    FullName = Session.Reader.FullName,
            //    Login = Session.Reader.Login,
            //    Password = Session.Reader.Password,
            //    FavoriteBooks = fav,
            //    ReaderBooks = new Dictionary<int, string>()
            //});
            //Response.Redirect("Index");
            //books.ToDictionary(book => book.Id, book => book.Name),
        }

        [HttpGet]//done
        public void DeleteFavoriteBook(int bookId)
        {
            var fb = APIClient.GetRequest<FavoriteBooksViewModel>($"api/reader/getfavoritebooksbyids?readerId={Session.Reader.Id}&bookId={bookId}");
            APIClient.PostRequest("api/reader/deletefavoritebooks", fb);
            Response.Redirect("FavoriteBooks");
            //всплывающее окно об успехе добавления
            return;
        }

        [HttpGet]//done
        public IActionResult ReaderBooks(int page = 1)
        {
            var rbListAll = APIClient.GetRequest<List<ReaderBookViewModel>>($"api/reader/getreadersbooks?readerId={Session.Reader.Id}");
            IEnumerable<ReaderBookViewModel> rbListDolg = rbListAll;
            rbListDolg = rbListDolg.Where(p => (int)p.Status == (int)BookBranchStatus.Принята);

            var ReadersBooksDolg = new List<ReaderBooksDolgForTable>();
            foreach(var item in rbListDolg)
            {
                var book = APIClient.GetRequest<BookViewModel>($"api/reader/getbook?bookId={item.BookId}");
                var bookBranch = APIClient.GetRequest<BookBranchViewModel>($"api/admin/getbookbranch?bookBranchId={item.BookBranchId}");
                var branch = APIClient.GetRequest<BranchViewModel>($"api/admin/getbranch?branchId={bookBranch.BranchId}");

                ReadersBooksDolg.Add(new ReaderBooksDolgForTable { Name = book.Name, Address = branch.Address, DateOut = item.DateOut, Srok = "1 месяц" });
            }
            ViewBag.ReadersBooksDolg = ReadersBooksDolg;

            return View();
        }

        //отчет
        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }

        [HttpPost]//done
        public IActionResult CreateReport(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                if (dateFrom > dateTo)
                {
                    throw new Exception("Дата начала должна быть меньше даты окончания");
                }
                APIClient.PostRequest("api/reader/createreporttopdf", new ReportBooksBindingModel
                {
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    ReaderId = Session.Reader.Id,
                    FileName = @"..\\LibraryReaderApp\\wwwroot\\report\\BooksReport.pdf"
                });
                var fileName = "BooksReport.pdf";
                var filePath = _environment.WebRootPath + @"\report\" + fileName;
                return PhysicalFile(filePath, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                //Response.Redirect("Privacy");
                var rbListAll = APIClient.GetRequest<List<ReaderBookViewModel>>($"api/reader/getreadersbooks?readerId={Session.Reader.Id}");
                IEnumerable<ReaderBookViewModel> rbListDolg = rbListAll;
                rbListDolg = rbListDolg.Where(p => p.Status == 0);

                var ReadersBooksDolg = new List<ReaderBooksDolgForTable>();
                foreach (var item in rbListDolg)
                {
                    var book = APIClient.GetRequest<BookViewModel>($"api/reader/getbook?bookId={item.BookId}");
                    var bookBranch = APIClient.GetRequest<BookBranchViewModel>($"api/admin/getbookbranch?bookBranchId={item.BookBranchId}");
                    var branch = APIClient.GetRequest<BranchViewModel>($"api/admin/getbranch?branchId={bookBranch.BranchId}");

                    ReadersBooksDolg.Add(new ReaderBooksDolgForTable { Name = book.Name, Address = branch.Address, DateOut = item.DateOut, Srok = "1 месяц" });
                }
                ViewBag.ReadersBooksDolg = ReadersBooksDolg;
                return View("Privacy");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        //админ
        [HttpGet]
        public IActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public void LoginAdmin(string login, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
                {
                    Session.Admin = APIClient.GetRequest<AdminViewModel>($"api/admin/loginadmin?login={login}&password={password}");
                    if (Session.Admin == null)
                    {
                        throw new Exception("Неверный логин/пароль");
                    }
                    Response.Redirect("IndexAdmin");
                    return;
                }
                throw new Exception("Введите логин и пароль");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                Response.Redirect("LoginAdmin");
                return;
            }
        }

        [HttpGet]
        public IActionResult IndexAdmin()
        {
            return View();
        }

        //библиотекари

        public IActionResult Librarians()
        {
            var librarians = APIClient.GetRequest<List<LibrarianViewModel>>("api/admin/getlibrarianlist");
            return View(librarians);
        }

        [HttpGet]
        public IActionResult LibrarianCreate()
        {
            var brunchList = APIClient.GetRequest<List<BranchViewModel>>("api/admin/getbranchlist");
            var brunchDictionary = new Dictionary<int, string>();
            foreach (var brunch in brunchList)
            {
                brunchDictionary.Add(brunch.Id, brunch.Address);
            }
            ViewBag.brunchDictionary = brunchDictionary;
            return View();
        }

        [HttpPost]
        public void LibrarianCreate(string librarianFullName, string librarianLogin, string librarianPassword, int librarianBranchId)
        {
            try
            {
                if (string.IsNullOrEmpty(librarianLogin) || string.IsNullOrEmpty(librarianPassword) || string.IsNullOrEmpty(librarianFullName) || librarianBranchId == null)
                {
                    throw new Exception("Введите логин, пароль, ФИО и номер филиала");
                }

                if (!Regex.IsMatch(librarianLogin, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                {
                    throw new Exception("В качестве логина должна быть указана почта");
                }
                if (librarianPassword.Length > _passwordMaxLength || librarianPassword.Length < _passwordMinLength
                    || !Regex.IsMatch(librarianPassword, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
                {
                    throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен состоять из цифр, букв и небуквенных символов");
                }

                if (!string.IsNullOrEmpty(librarianFullName) && !string.IsNullOrEmpty(librarianLogin) && !string.IsNullOrEmpty(librarianPassword) && librarianBranchId != null)
                {
                    APIClient.PostRequest("api/admin/createorupdatelibrarian", new LibrarianBindingModel
                    {
                        FullName = librarianFullName,
                        Login = librarianLogin,
                        Password = librarianPassword,
                        BranchId = librarianBranchId
                    });
                    Response.Redirect("Librarians");
                    return;
                }
                throw new Exception("Введите данные");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                Response.Redirect("LibrarianCreate");
                return;
            }
        }

        [HttpGet]
        public IActionResult LibrarianUpdate(int librarianId)
        {
            var librarian = APIClient.GetRequest<LibrarianViewModel>($"api/admin/getlibrarian?librarianId={librarianId}");

            var brunchList = APIClient.GetRequest<List<BranchViewModel>>("api/admin/getbranchlist");
            var brunchDictionary = new Dictionary<int, string>();
            foreach (var brunch in brunchList)
            {
                brunchDictionary.Add(brunch.Id, brunch.Address);
            }
            ViewBag.brunchDictionary = brunchDictionary;

            return View(librarian);
        }

        [HttpPost]
        public void LibrarianUpdate(int librarianId, string librarianFullName, string librarianLogin, string librarianPassword, int librarianBranchId)
        {
            try
            {
                String bId = librarianBranchId.ToString();
                if (string.IsNullOrEmpty(librarianLogin) || string.IsNullOrEmpty(librarianPassword) || string.IsNullOrEmpty(librarianFullName) || string.IsNullOrEmpty(bId))
                {
                    throw new Exception("Введите логин, пароль, ФИО и номер филиала");
                }

                if (!Regex.IsMatch(librarianLogin, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                {
                    throw new Exception("В качестве логина должна быть указана почта");
                }
                if (librarianPassword.Length > _passwordMaxLength || librarianPassword.Length < _passwordMinLength
                    || !Regex.IsMatch(librarianPassword, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
                {
                    throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен состоять из цифр, букв и небуквенных символов");
                }
                if (!string.IsNullOrEmpty(librarianFullName) && !string.IsNullOrEmpty(librarianLogin) && !string.IsNullOrEmpty(librarianPassword) && librarianBranchId != null)
                {
                    APIClient.PostRequest("api/admin/createorupdatelibrarian", new LibrarianBindingModel
                    {
                        Id = librarianId,
                        FullName = librarianFullName,
                        Login = librarianLogin,
                        Password = librarianPassword,
                        BranchId = librarianBranchId
                    });
                    Response.Redirect("Librarians");
                    return;
                }
                throw new Exception("Введите данные");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                Response.Redirect($"/Home/LibrarianUpdate?librarianId={librarianId}&librarianFullName={librarianFullName}&librarianLogin={librarianLogin}&librarianPassword={librarianPassword}&librarianBranchId={librarianBranchId}");
                return;
            }
        }

        [HttpGet]
        public void LibrarianDelete(int librarianId)
        {
            var librarian = APIClient.GetRequest<LibrarianViewModel>($"api/admin/getlibrarian?librarianId={librarianId}");
            APIClient.PostRequest("api/admin/deletelibrarian", librarian);
            Response.Redirect("Librarians");
        }

        //филиалы

        public IActionResult BranchesAdmin()
        {
            var branches = APIClient.GetRequest<List<BranchViewModel>>("api/admin/getbranchlist");
            return View(branches);
        }

        [HttpGet]
        public IActionResult BranchCreate()
        {
            return View();
        }

        [HttpPost]
        public void BranchCreate(string branchAddress, string branchSchedule, string branchСoordinate1, string branchСoordinate2)
        {
            try
            {
                if (string.IsNullOrEmpty(branchСoordinate1) || string.IsNullOrEmpty(branchСoordinate2))
                {
                    throw new Exception("Выберите расположение филиала на карте");
                }

                var bb1 = branchСoordinate1.Replace('.', ',');
                var bb2 = branchСoordinate2.Replace('.', ',');
                var bc1 = Convert.ToDouble(bb1);
                var bc2 = Convert.ToDouble(bb2);

                if (!string.IsNullOrEmpty(branchAddress) && !string.IsNullOrEmpty(branchSchedule))
                {
                    APIClient.PostRequest("api/admin/createorupdatebranch", new BranchBindingModel
                    {
                        Address = branchAddress,
                        Schedule = branchSchedule,
                        Сoordinate1 = bc1,
                        Сoordinate2 = bc2,
                        Librarians = new Dictionary<int, string>(),
                        BookBranches = new Dictionary<int, string>()
                    });
                    Response.Redirect("BranchesAdmin");
                    return;
                }
                throw new Exception("Введите данные");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                Response.Redirect("BranchCreate");
                return;
            }
        }

        [HttpGet]
        public IActionResult BranchUpdate(int branchId)
        {
            var branch = APIClient.GetRequest<BranchViewModel>($"api/admin/getbranch?branchId={branchId}");

            return View(branch);
        }

        [HttpPost]
        public void BranchUpdate(int branchId, string branchAddress, string branchSchedule, string branchСoordinate1, string branchСoordinate2)
        {
            try
            {
                if (string.IsNullOrEmpty(branchСoordinate1) || string.IsNullOrEmpty(branchСoordinate2))
                {
                    throw new Exception("Выберите расположение филиала на карте");
                }

                var bb1 = branchСoordinate1.Replace('.', ',');
                var bb2 = branchСoordinate2.Replace('.', ',');
                var bc1 = Convert.ToDouble(bb1);
                var bc2 = Convert.ToDouble(bb2);

                var branch = APIClient.GetRequest<BranchViewModel>($"api/admin/getbranch?branchId={branchId}");
                if (!string.IsNullOrEmpty(branchAddress) && !string.IsNullOrEmpty(branchSchedule) && branchСoordinate1 != null && branchСoordinate2 != null)
                {
                    APIClient.PostRequest("api/admin/createorupdatebranch", new BranchBindingModel
                    {
                        Id = branchId,
                        Address = branchAddress,
                        Schedule = branchSchedule,
                        Сoordinate1 = bc1,
                        Сoordinate2 = bc2,
                        Librarians = new Dictionary<int, string>(),
                        BookBranches = new Dictionary<int, string>(),
                    });
                    Response.Redirect("BranchesAdmin");
                    return;
                }
                throw new Exception("Введите данные");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                Response.Redirect($"/Home/BranchUpdate?branchId={branchId}");
                return;
            }
        }

        [HttpGet]
        public void BranchDelete(int branchId)
        {
            var branch = APIClient.GetRequest<BranchViewModel>($"api/admin/getbranch?branchId={branchId}");
            branch.Librarians = new Dictionary<int, string>();
            branch.BookBranches = new Dictionary<int, string>();
            APIClient.PostRequest("api/admin/deletebranch", branch);
            Response.Redirect("BranchesAdmin");
        }



        public IActionResult test()
        {
            //// Создание списка координат маркеров
            //var coordinatesList = new List<Tuple<double, double>>();
            //coordinatesList.Add(new Tuple<double, double>(55.751244, 37.618423)); // Москва
            //coordinatesList.Add(new Tuple<double, double>(59.9386300, 30.3141300)); // Санкт-Петербург
            //coordinatesList.Add(new Tuple<double, double>(56.8389261, 60.6057025)); // Екатеринбург

            //var infoList = new List<String> { "Москва", "Санкт-Петербург", "Екатеринбург" };

            //// Сериализация списка координат в JSON
            //var json = JsonConvert.SerializeObject(coordinatesList);

            //// Передача JSON в ViewBag
            //ViewBag.CoordinatesListJson = json;

            //var json2 = JsonConvert.SerializeObject(infoList);
            //ViewBag.InfoListJson = json2;

            //var book = APIClient.GetRequest<BookViewModel>($"api/reader/getbook?bookId=9");
            ViewBag.ImageName = "martishka3.jpg";
            var rbListAll = APIClient.GetRequest<List<ReaderBookViewModel>>($"api/reader/getreadersbooksbybook?bookId=13");
            ViewBag.rating = rbListAll.Count;

            return View();
        }

        public IActionResult GetImage(string imageName)
        {
            //var imageFilePath = Path.Combine("C:\\Users\\Tamara\\Desktop", imageName);
            var imageFilePath = Path.Combine("F:\\!Учеба\\3 КУРС\\ПиАПС\\курсач\\LibraryCW\\LibrarianApp\\wwwroot\\Files", imageName);
            var imageFileStream = System.IO.File.OpenRead(imageFilePath);
            return File(imageFileStream, "image/jpeg");
        }

        [HttpPost]
        public void test1(string branchСoordinate1)
        {
            branchСoordinate1.Replace('.', ',');
            var bc1 = Convert.ToDouble(branchСoordinate1);
            Console.WriteLine(bc1);
            Response.Redirect("test");
        }
    }
}