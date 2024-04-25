using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace TextFormattingHelper
{
    public class WordDocument
    {
        Xceed.Words.NET.DocX doc;
        string ProtectPassword;
        private string FileName;
        //конструктор для инициализации экземпляра класса
        public WordDocument(string TemplatePath, string password)
        {
            //создадим временный файл с именем tmpfile, куда положим итоговый документ
            var tmpfile = Path.GetTempFileName();
            FileName = tmpfile;
            //получим шаблон файла, хранящийся на сервере
            doc = DocX.Load(TemplatePath);
            //получим пароль файла
            ProtectPassword = password;
        }
        //Найти и заменить текст в документе
        public void FindAndReplaceInDoc(string find, string replace)
        {
            if (doc == null)
                return;
            //снимем защиту с файла для редактирования защищённых областей
            // doc.RemoveProtection();
            //подставим необходимый текст
            doc.ReplaceText(find, replace);
            //восстановим пароль на файл
            // doc.AddPasswordProtection(EditRestrictions.readOnly, ProtectPassword);
        }
        //Сохранение документа во временный файл для последующей сериализации
        public bool SaveFile(string FileName)
        {
            if (doc != null)
            {
                try
                {
                    doc.SaveAs(FileName);
                    this.FileName = FileName;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        //Сериализация документа
        public byte[] GetDocument()
        {
            if (SaveFile(FileName))
            {
                byte[] FileBytes = File.ReadAllBytes(FileName);
                return FileBytes;
            }
            return null;
        }

        public void SaveAsDocx(string filePath, string password)
        {
            doc.SaveAs(filePath, password);
        }
    }
}
