using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBusinessLogics.OfficePackage.HelperEnums;
using LibraryBusinessLogics.OfficePackage.HelperModels;

namespace LibraryBusinessLogics.OfficePackage
{
    public abstract class LibrarianAbstractSaveToPdf
    {
        public void CreateDoc(LibrarianPdfInfo info)
        {
            CreatePdf(info);

            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });

            CreateParagraph(new PdfParagraph { Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }", Style = "Normal" });

            CreateTable(new List<string> { "6cm", "4cm", "7cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата выдачи", "Книга", "Читатели" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });


            foreach (var rb in info.BooksWithReaders)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { rb.DateOut.ToShortDateString(), rb.Name, rb.Readers },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);
        }

        // Создание pdf-файла
        protected abstract void CreatePdf(LibrarianPdfInfo info);

        // Создание параграфа с текстом
        protected abstract void CreateParagraph(PdfParagraph paragraph);

        // Создание таблицы
        protected abstract void CreateTable(List<string> columns);

        // Создание и заполнение строки
        protected abstract void CreateRow(PdfRowParameters rowParameters);

        // Сохранение файла
        protected abstract void SavePdf(LibrarianPdfInfo info);
    }
}
