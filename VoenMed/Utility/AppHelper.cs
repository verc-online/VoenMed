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
using TextFormattingHelper;
using VoenMedLibrary.Models;

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


        public static void GenerateAndOpenForm100(string savePath, Form100Model form100Model)
        {
            WordDocumentDocx doc = new();
            Dictionary<string, string> keyValuePairs = new()
            { 
                {"@LastName", form100Model.LastName },
                {"@FirstName", form100Model.FirstName },
                {"@SecondName", form100Model.SecondName },
                {"@BirthDate", form100Model.BirthDate.ToString() },
                {"@MilitaryUnit", form100Model.MilitaryUnit },
                {"@MilitaryId", form100Model.MilitaryId },
                {"@Duty", form100Model.Duty },
                {"@RankTitle", form100Model.RankTitle },
                {"@IssuedBy", form100Model.IssuedBy },
                {"@Ethiology", form100Model.InjuryStatusLocalis.Ethiology.GetDescription() },
                {"@IssuedWhen", form100Model.IssuedWhen.ToString() },
                //{"@WithOutFirstAid", form100Model.WithoutFirstAid }, REMAKE
                {"@Reason", form100Model.Reason.GetDescription() },
                {"@DiseaseTime", form100Model.DiseaseTime.ToString() },
                {"@EvacOrder", form100Model.EvacuationOrder.GetDescription() },
                {"@EvacTransport", form100Model.EvacuationTransport.GetDescription() },
                {"@EvacPosition", form100Model.EvacuationPosition.GetDescription() },
                {"@EvacWay", form100Model.EvacuationWay.GetDescription() },
                {"@EvacAddress", form100Model.EvacAddress },
                {"@EvacTime", form100Model.EvacTime.ToString() },
                {"@SpecialMarks", form100Model.Special.GetDescriptionsAsText() },
                {"@LethalTime", form100Model.HelpProvided.LethalDateTime.ToString() ?? "" },
                {"@Diagnosis", form100Model.InjuryStatusLocalis.Diagnosis },
                {"@Doc", form100Model.Doc },
                {"@WayStopBleeding", form100Model.HelpProvided.WayStopBleeding.GetDescription() },
                {"@TimeTourniquetApplied", form100Model.HelpProvided.TimeTourniquetApplied.ToString() ?? "" },
                {"@DecompressionOfThePleuralCavity", form100Model.HelpProvided.DecompressionOfThePleuralCavity.GetDescriptionsAsText() },
                {"@DrainageOfThePleuralCavity", form100Model.HelpProvided.DrainageOfThePleuralCavity.GetDescriptionsAsText() },
                {"@Immobilization", form100Model.HelpProvided.Immobilization.GetDescription() },
                {"@NaCl", form100Model.HelpProvided.NaCl.ToString() ?? "" },
                {"@NaHC03", form100Model.HelpProvided.NaHC03.ToString() ?? "" },
                {"@Glucose5", form100Model.HelpProvided.Glucose5.ToString() ?? "" },
                {"@Er", form100Model.HelpProvided.Er.ToString() ?? "" },
                {"@Szp", form100Model.HelpProvided.Szp.ToString() ?? "" },
                {"@IntensiveCareMeasures", form100Model.HelpProvided.IntensiveCareMeasures.GetDescription() },
                {"@IntensiveCareMeasuresTimeSpent", form100Model.HelpProvided.IntensiveCareMeasuresTimeSpent.ToString() ?? "" },
                {"@LethalDateTime", form100Model.HelpProvided.LethalDateTime.ToString() ?? "" },
                {"@HelpProvidedSummary", form100Model.HelpProvided.HelpProvidedSummary },
                // DRUG PROVIDED,
                {"@Temperature", form100Model.Condition.Temperature.ToString() },
                {"@DiuresPerFirstHour", form100Model.Condition.DiuresPerFirstHour.ToString() },
                {"@AdditionalInfo", form100Model.Condition.AdditionalInfo.GetDescriptionsAsText() },
                {"@Complaints", form100Model.Condition.Complaints },
                {"@Condition", form100Model.Condition.Condition },
                {"@Consience", form100Model.Condition.GlasgowComaScale.Consience.GetDescription() },
                {"@EyeResponse", form100Model.Condition.GlasgowComaScale.EyeResponse.GetDescription() },
                {"@VerbalResponse", form100Model.Condition.GlasgowComaScale.VerbalResponse.GetDescription() },
                {"@MotorResponse", form100Model.Condition.GlasgowComaScale.MotorResponse.GetDescription() },
                {"@BreathingSupport", form100Model.Condition.Breathing.BreathingSupport.GetDescription() },
                {"@BreathingRate", form100Model.Condition.Breathing.BreathingRate.ToString() },
                {"@Saturation", form100Model.Condition.Breathing.Saturation.ToString() },
                {"@FiO2", form100Model.Condition.Breathing.FiO2.ToString() },
                {"@Summary", form100Model.Condition.Breathing.Summary },
                {"@Rate", form100Model.Condition.Heart.Rate.ToString() },
                {"@SystolicArterialPressure", form100Model.Condition.Heart.SystolicArterialPressure.ToString() },
                {"@CapillaryTime", form100Model.Condition.Heart.CapillaryTime.ToString() },
                {"@NoradrenalineDose", form100Model.Condition.Heart.NoradrenalineDose.ToString() },
                {"@DopamineDose", form100Model.Condition.Heart.DopamineDose.ToString() },
                {"@DobutamineDose", form100Model.Condition.Heart.DobutamineDose.ToString() },
                {"@AdrenalineDose", form100Model.Condition.Heart.AdrenalineDose.ToString() },
                // Injuries

            };

            string filePath = doc.CreateForm100(keyValuePairs, savePath);

            // AppHelper.Print(filePath);

            string argument = "/select," + filePath;

            MessageBox.Show("Успешно!");

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }
    }
}
