using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models
{
    public class BreathingModel
    {
        public BreathingSupportEnum BreathingSupport { get; set; } = BreathingSupportEnum.Effective;
        public int BreathingRate { get; set; } = 18;
        public int Saturation { get; set; } = 97;
        public int FiO2 { get; set; } = 21;

        public string Summary { get; set; }

        internal int CalculateRTS()
        {
            int rateScore;
            if (BreathingRate > 10 && BreathingRate < 29) rateScore = 4;
            else if (BreathingRate > 29) rateScore = 3;
            else if (BreathingRate > 6 && BreathingRate < 9) rateScore = 2;
            else if (BreathingRate > 1 && BreathingRate < 2) rateScore = 1;
            else rateScore = 0;

            int typeScore;
            switch(BreathingSupport)
            {
                case BreathingSupportEnum.Effective:
                    typeScore = 1; break;
                default: typeScore = 0; break;

            }
            return rateScore + typeScore;
        }
    
        public void GetSummary()
        {
            string output = "";

            switch(BreathingSupport)
            {
                case BreathingSupportEnum.Effective:
                    output += BreathingSupport.GetDescription() + ". ";
                    break;
                case BreathingSupportEnum.Insuflation:
                    output += BreathingSupport.GetDescription() + ". ";
                    break;
                case BreathingSupportEnum.NVU:
                    output += "Проводится респираторная поддержка: Установлен " + BreathingSupport.GetDescription().ToLower() + ", ИВЛ. ";
                    break;
                case BreathingSupportEnum.Endotracheal:
                    output += "Проводится респираторная поддержка: Установлена " + BreathingSupport.GetDescription().ToLower() + ", ИВЛ. ";
                    break;
                case BreathingSupportEnum.Conicotomy:
                    output += "Проводится респираторная поддержка: Выполнена " + BreathingSupport.GetDescription().ToLower() + ", установлена ЭТ, ИВЛ. ";
                    break;
            }

            output += $"Частота дыхания {BreathingRate}, ";
            output += $"Сатурация {Saturation}%, ";
            output += $"FiO2 {FiO2}%. ";

            Summary = output;
        }
    }
}
