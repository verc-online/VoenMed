using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models
{
    public class HeartModel
    {
        public int Rate { get; set; } = 60;
        public int SystolicArterialPressure { get; set; } = 120;
        public CapillaryTimeEnum CapillaryTime { get; set; } = CapillaryTimeEnum.Fast;
        public decimal NoradrenalineDose { get; set; }
        public decimal DopamineDose { get; set; }

        public decimal DobutamineDose { get; set; }
        public decimal AdrenalineDose { get; set; }

        public void CalcNoradrenalineDose(int personWeight = 80, decimal mg = 1, decimal volume = 1, decimal volumePerHour = 1)
        {
            try
            {
                NoradrenalineDose = Math.Round(mg * 1000 * volumePerHour / (personWeight * 60 * volume),2);
            }
            catch { NoradrenalineDose = 0 ; }
        }

        public void CalcDopamineDose(int personWeight = 80, decimal mg = 1, decimal volume = 1, decimal volumePerHour = 1)
        {
            try
            {
                DopamineDose = Math.Round(mg * 1000 * volumePerHour / (personWeight * 60 * volume), 2);
            }
            catch { DopamineDose = 0; }
        }

        public void CalcDobutamineDose(int personWeight = 80, decimal mg = 1, decimal volume = 1, decimal volumePerHour = 1)
        {
            try
            {
                DobutamineDose = Math.Round(mg * 1000 * volumePerHour / (personWeight * 60 * volume), 2);
            }
            catch { DobutamineDose = 0; }
        }

        public void CalcAdrenalineDose(int personWeight = 80, decimal mg = 1, decimal volume = 1, decimal volumePerHour = 1)
        {
            try
            {
                AdrenalineDose = Math.Round(mg * 1000 * volumePerHour / (personWeight * 60 * volume), 2);
            }
            catch { AdrenalineDose = 0; }
        }

        public void ResetHeartSupport()
        {
            AdrenalineDose = 0;
            DopamineDose = 0;
            DobutamineDose = 0;
            NoradrenalineDose = 0;
        }

        internal int CalculateRTS()
        {
            switch(SystolicArterialPressure)
            {
                case > 89:
                    return 4;
                case > 76:
                    return 3;
                case > 50:
                    return 2;
                case > 0:
                    return 0;
                default:
                    return 0;
            }
        }

        public string Summary { get; set; }

        public void GetSummary()
        {
            string output = string.Empty;

            if( NoradrenalineDose > 0
                || DobutamineDose > 0
                || DopamineDose > 0
                || AdrenalineDose > 0)
            {
                output += "Гемодинамические показатели нестабильные. На фоне применения";

                output += NoradrenalineDose > 0 ? $" Норадреналина {NoradrenalineDose} мкг/кг/мин " : "";
                output += DobutamineDose > 0 ? $" Добутамина {DobutamineDose} мкг/кг/мин " : "";
                output += DopamineDose > 0 ? $" Допамина {DopamineDose} мкг/кг/мин " : "";
                output += AdrenalineDose > 0 ? $" Адреналина {AdrenalineDose} мкг/кг/мин " : "";

                output += $"систолическое артериальное давление {SystolicArterialPressure} мм. рт. ст. ";
                output += $"ЧСС {Rate} уд./мин. ";
                output += $"Время капиллярного наполнения {CapillaryTime.GetDescription().ToLower()}.";
            }
            else
            {
                output += "Гемодинамические показатели стабильные. ";
                output += $"Cистолическое артериальное давление {SystolicArterialPressure} мм. рт. ст. ";
                output += $"ЧСС {Rate} уд./мин. ";
                output += $"Время капиллярного наполнения {CapillaryTime.GetDescription().ToLower()} ";
            }

            Summary = output ;
        }
    }
}
