using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace TextFormattingHelper
{
     public class WordDocumentDocx
    {


        public string CreateForm100(Dictionary<string, string>keyValuePairs, string savePath)
        {
            // We will need a file name for our output file (change to suit your machine):
            string fileNameTemplate = @"{0}-Форма100-{1}.docx";

            // Let's save the file with a meaningful name, including the 
            // applicant name and the letter date:
            string outputFileName = Path.Combine(
                savePath, string.Format(fileNameTemplate, keyValuePairs.FirstOrDefault().Value, DateTime.Now.ToString("dd-MM-yy")));


            //снимем защиту с файла для редактирования защищённых областей
            //letter.RemoveProtection();
            //letter.AddPasswordProtection(EditRestrictions.readOnly, password);
            using (DocX letter = DocX.Load("V:\\Git\\VoenMed\\VoenMed\\bin\\Debug\\net8.0-windows\\Forms100\\_Образец.docx"))
            {
                // Perform the replace:
                foreach (KeyValuePair<string, string> entry in keyValuePairs)
                {
                    letter.ReplaceText(entry.Key, entry.Value);
                }

                // Save as New filename:
                letter.SaveAs(Path.Combine(Environment.CurrentDirectory, outputFileName));

                return outputFileName;
            }
        }

    }
}
