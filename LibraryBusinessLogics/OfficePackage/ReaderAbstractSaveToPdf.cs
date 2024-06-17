using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBusinessLogics.OfficePackage.HelperEnums;
using LibraryBusinessLogics.OfficePackage.HelperModels;


namespace LibraryBusinessLogics.OfficePackage
{
    public abstract class ReaderAbstractSaveToPdf
    {
        public void CreateDoc(ReaderPdfInfo info)
        {
            CreatePdf(info);

            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });

            CreateParagraph(new PdfParagraph { Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }", Style = "Normal" });

            CreateTable(new List<string> { "6cm", "3cm", "4cm", "4cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Название", "Автор", "Дата вязтия", "Дата возвращения" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });


            foreach (var rb in info.ReadersBooks)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { rb.Name, rb.Author, rb.DateOut.ToShortDateString(), rb.DateIn.ToShortDateString()},
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);
        }

        // Создание pdf-файла
        protected abstract void CreatePdf(ReaderPdfInfo info);

        // Создание параграфа с текстом
        protected abstract void CreateParagraph(PdfParagraph paragraph);

        // Создание таблицы
        protected abstract void CreateTable(List<string> columns);

        // Создание и заполнение строки
        protected abstract void CreateRow(PdfRowParameters rowParameters);

        // Сохранение файла
        protected abstract void SavePdf(ReaderPdfInfo info);
    }
}

