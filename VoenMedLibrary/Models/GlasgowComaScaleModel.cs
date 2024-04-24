using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models
{
    public class GlasgowComaScaleModel
    {
        public ConsienceEnum Consience { get; private set; }
        public EyeResponseEnum EyeResponse { get; set; } = EyeResponseEnum.EyeOpeningSpontaneously;
        public VerbalResponseEnum VerbalResponse { get; set; } = VerbalResponseEnum.Oriented;
        public MotorResponseEnum MotorResponse { get; set; } = MotorResponseEnum.ObeysMotorCommands;

        public int CalculateConsience()
        {
            int sum = SumResponses();

            switch (sum)
            {
                case >= 15:
                    Consience = ConsienceEnum.Clear;
                    break;
                case >= 14:
                    Consience = ConsienceEnum.ModerateStun;
                    break;
                case >= 11:
                    Consience = ConsienceEnum.DeepStun;
                    break;
                case >= 8:
                    Consience = ConsienceEnum.Sopor;
                    break;
                case >= 6:
                    Consience = ConsienceEnum.ModerateComa;
                    break;
                case >= 4:
                    Consience = ConsienceEnum.DeepComa;
                    break;
                default:
                    Consience = ConsienceEnum.TerminalComa;
                    break;
            }

            return sum;
        }

        private int SumResponses()
        {
            return (int)EyeResponse + (int)VerbalResponse + (int)MotorResponse;
        }

        internal int CalculateRTS()
        {
            switch(SumResponses())
            {
                case > 13:
                    return 4;
                case > 9:
                    return 3;
                case > 6:
                    return 2;
                case > 4:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
