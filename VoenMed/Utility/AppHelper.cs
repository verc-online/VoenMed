using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Windows;
using System.Diagnostics;
using Xceed.Words.NET;
using Microsoft.Office.Interop.Word;

namespace VoenMed.Utility
{
    public static class AppHelper
    {
        /// <summary>
        /// Print all pages of an XPS document.
        /// Optionally, hide the print dialog window.
        /// </summary>
        /// <param name="xpsFilePath">Path to source XPS file</param>
        /// <param name="hidePrintDialog">Whether to hide the print dialog window (shown by default)</param>
        /// <returns>Whether the document printed</returns>
        public static bool PrintWholeDocument(string xpsFilePath, bool hidePrintDialog = false)
        {
            // Create the print dialog object and set options.
            PrintDialog printDialog = new();

            if (!hidePrintDialog)
            {
                // Display the dialog. This returns true if the user presses the Print button.
                bool? isPrinted = printDialog.ShowDialog();
                if (isPrinted != true)
                    return false;
            }

            // Print the whole document.
            try
            {
                // Open the selected document.
                XpsDocument xpsDocument = new(xpsFilePath, FileAccess.Read);

                // Get a fixed document sequence for the selected document.
                FixedDocumentSequence fixedDocSeq = xpsDocument.GetFixedDocumentSequence();

                // Create a paginator for all pages in the selected document.
                DocumentPaginator docPaginator = fixedDocSeq.DocumentPaginator;

                // Print to a new file.
                printDialog.PrintDocument(docPaginator, $"Printing {Path.GetFileName(xpsFilePath)}");

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return false;
            }
        }


        public static void Print(string filePath)
        {
            ProcessStartInfo psi = new ProcessStartInfo(filePath)
            {
                UseShellExecute = true,
                Verb = "print",
                RedirectStandardOutput = false,
                CreateNoWindow = true,
                
            };

            using (Process p = new Process { StartInfo = psi })
            {
                p.Start();
                p.WaitForExit();
            }
        }


    }
}
