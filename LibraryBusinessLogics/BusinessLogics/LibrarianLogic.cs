using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LibraryContracts.StoragesContracts;
using LibraryContracts.BusinessLogicsContracts;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;

namespace LibraryBusinessLogics.BusinessLogics
{
    public class LibrarianLogic : ILibrarianLogic
    {
        private readonly ILibrarianStorage _librarianStorage;
        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 8;

        public LibrarianLogic(ILibrarianStorage librarianStorage)
        {
            _librarianStorage = librarianStorage;
        }

        public List<LibrarianViewModel> Read(LibrarianBindingModel model)
        {
            if (model == null)
            {
                return _librarianStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<LibrarianViewModel> { _librarianStorage.GetElement(model) };
            }
            return _librarianStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(LibrarianBindingModel model)
        {
            var elementByLogin = _librarianStorage.GetElement(new LibrarianBindingModel
            {
                Login = model.Login
            });
            if (elementByLogin != null && elementByLogin.Id != model.Id)
            {
                throw new Exception("Учетная запись по такому логину уже существует");
            }
            if (!Regex.IsMatch(model.Login, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength
                || !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен состоять из цифр, букв и небуквенных символов");
            }
            if (model.Id.HasValue)
            {
                _librarianStorage.Update(model);
            }
            else
            {
                _librarianStorage.Insert(model);
            }
        }

        public void Delete(LibrarianBindingModel model)
        {
            var element = _librarianStorage.GetElement(new LibrarianBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _librarianStorage.Delete(model);
        }
    }
}
