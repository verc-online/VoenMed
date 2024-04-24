using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models
{
    public class HelpProvidedModel
    {
        public WayStopBleedingEnum WayStopBleeding { get; set; } = 0;
        public DateTime? TimeTourniquetApplied { get; set; } = null;

        public DecompressionOfThePleuralCavityEnum DecompressionOfThePleuralCavity { get; set; } = 0;
        public DrainageOfThePleuralCavityEnum DrainageOfThePleuralCavity { get; set; } = 0;

        public ImmobilizationEnum Immobilization { get; set; } = 0;

        // Все инфузии трансфузии
        public int NaCl { get; set; } = 0;
        public int NaHC03 { get; set; } = 0;
        public int Glucose5 { get; set; } = 0;
        public int Er { get; set; } = 0;
        public int Szp { get; set; } = 0;

        // Реанимационные мероприятия
        public IntensiveCareMeasuresEnum IntensiveCareMeasures { get; set; } = 0;
        public int IntensiveCareMeasuresTimeSpent { get; set; } = 0;
        public DateTime? LethalDateTime { get; set; } = null;


        // Выполнено - текстовое поле
        public string HelpProvidedSummary { get; set; } = string.Empty;

        //Drugs
        public List<ProvidedDrugModel> DrugsProvided { get; set; } = new();

        public void GetHelpProvidedSummary()
        {
            string output = "";

            output += WayStopBleeding > 0 ?$"Остановка кровотечения {WayStopBleeding.GetDescription().ToLower()}. " : "";
            output += WayStopBleeding > 0 ? $"Время наложения жгута {TimeTourniquetApplied?.ToShortTimeString()}. " : "";

            output += DecompressionOfThePleuralCavity > 0 ? $"Выполнена декомпрессия плевральной полости {DecompressionOfThePleuralCavity.GetDescriptionsAsText().ToLower()}. " : "";

            output += DrainageOfThePleuralCavity > 0 ? $"Дренирование плевральной полости {DrainageOfThePleuralCavity.GetDescriptionsAsText().ToLower()}. " : "";

            output += Immobilization > 0 ? $"Иммобилизация {Immobilization.GetDescription().ToLower()} средствами. " : "";

            output += NaCl > 0 ? $"NaCl 0.9% {NaCl} мл. " : "";
            output += NaHC03 > 0 ? $"Гидрокарбоната Na {NaHC03} мл. " : "";
            output += Glucose5 > 0 ? $"Глюкозы 5% {Glucose5} мл. " : "";
            output += Er > 0 ? $"Эр. взвеси {Er} мл. " : "";
            output += Szp > 0 ? $"СЗП {Szp} мл. " : "";

            foreach(var drug in DrugsProvided)
            {
                output += $"{drug.Title} {drug.Dose} {drug.Measurement}. ";
            }

            output += IntensiveCareMeasures > 0 ? $"Проводились реанимационные мероприятия длительностью {IntensiveCareMeasuresTimeSpent} мин." +
                $" {IntensiveCareMeasures.GetDescription()}. " : "";

            output += IntensiveCareMeasures == IntensiveCareMeasuresEnum.Lethal ? $"Время: {LethalDateTime?.ToShortTimeString()}" : "";

            HelpProvidedSummary = output;

        }
    }
}
