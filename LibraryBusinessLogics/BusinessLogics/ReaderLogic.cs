using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContracts.BindingModels;
using LibraryContracts.ViewModels;
using LibraryContracts.StoragesContracts;
using LibraryContracts.BusinessLogicsContracts;
using System.Text.RegularExpressions;

namespace LibraryBusinessLogics.BusinessLogics
{
    public class ReaderLogic : IReaderLogic
    {
        private readonly IReaderStorage _readerStorage;
        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 8;

        public ReaderLogic(IReaderStorage readerStorage)
        {
            _readerStorage = readerStorage;
        }

        public List<ReaderViewModel> Read(ReaderBindingModel model)
        {
            if (model == null)
            {
                return _readerStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ReaderViewModel> { _readerStorage.GetElement(model) };
            }
            return _readerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ReaderBindingModel model)
        {
            var elementByLogin = _readerStorage.GetElement(new ReaderBindingModel
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
                _readerStorage.Update(model);
            }
            else
            {
                _readerStorage.Insert(model);
            }
        }

        public void Delete(ReaderBindingModel model)
        {
            var element = _readerStorage.GetElement(new ReaderBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _readerStorage.Delete(model);
        }
    }
}
