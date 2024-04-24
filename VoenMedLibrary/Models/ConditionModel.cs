using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models
{
    public class ConditionModel
    {
        public GlasgowComaScaleModel GlasgowComaScale { get; set; } = new();
        public BreathingModel Breathing { get; set; } = new();
        public HeartModel Heart { get; set; } = new();

        public decimal Temperature { get; set; } = 36.6M;
        public int DiuresPerFirstHour { get; set; } = 0;

        public AdditionalInfoEnum AdditionalInfo { get; set; }
        public string Complaints { get; set; } = string.Empty;

        public string Condition { get; set; } = "";

        public void GetConditionSummary()
        {
            string output = "Объективно ";
            int score = 0;
            score = GlasgowComaScale.CalculateRTS() + Breathing.CalculateRTS() + Heart.CalculateRTS();

            switch(score)
            {
                // Легкое
                case > 11:
                    output += "состояние средней степени тяжести. ";
                    break;
                // Тяжелое
                case > 3:
                    output += "состояние тяжелое. ";
                    break;
                // Крайне тяжелое
                case <=3:
                    output += "состояние крайне тяжелое. ";
                    break;
            }
            if(Heart.NoradrenalineDose > 0
                || Heart.DopamineDose > 0
                || Heart.DobutamineDose > 0
                || Heart.AdrenalineDose > 0
                || (int)Breathing.BreathingSupport > 1
                )
            {
                output += " Нестабильное. ";
            }
            else
            {
                output += " Стабильное. ";
            }
            output += $"Сознание: {GlasgowComaScale.Consience.GetDescription().ToLower()}. ";

            Breathing.GetSummary();
            output += Breathing.Summary;

            Heart.GetSummary();
            output += Heart.Summary;

            output += $"Диурез за 1 первый час {DiuresPerFirstHour} мл. ";
            output += $"Температура {Temperature} °C. ";

            output += AdditionalInfo > 0 ? $"{AdditionalInfo.GetDescriptionsAsText()}. " : "";
            
            if(Complaints.Length > 0)
            {
                output += "Жалобы: " + Complaints;
            }


            Condition = output;

        }
    }
}
