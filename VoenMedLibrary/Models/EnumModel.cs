using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoenMedLibrary.Models
{
    public class EnumModel
    {
        [Flags]
        public enum HeadLocalisationEnum
        {
            None = 0b_0000_0000,  // 0
            FrontHead = 0b_0000_0001,  // 1
            BackHead = 0b_0000_0010,  // 2
        }

        [Flags]
        public enum NeckLocalisationEnum
        {
            None = 0b_0000_0000,  // 0
            FrontNeck = 0b_0000_0001,  // 1
            BackNeck = 0b_0000_0010,  // 2
        }

        [Flags]
        public enum UpperLocalisationEnum
        {
            None = 0b_0000_0000,  // 0
            ShoulderFront = 0b_0000_0001,  // 1
            ForearmFront = 0b_0000_0010,  // 2
            WristFront = 0b_0000_0100,  // 4
            ShoulderTourniquet = 0b_0000_1000,  // 8
            ForearmTourniquet = 0b_0001_0000,  // 16
            ShoulderBack = 0b_0010_0000,  // 32
            ForearmBack = 0b_0100_0000,  // 64
            WristBack = 0b_1000_0000,  // 128
        }

        [Flags]
        public enum ThoraxLocalisationEnum
        {
            None = 0b_0000_0000,  // 0
            FrontRight = 0b_0000_0001,  // 1
            FrontLeft = 0b_0000_0010,  // 2
            BackRight = 0b_0000_0100,  // 4
            BackLeft = 0b_0000_1000,  // 8
        }

        [Flags]
        public enum AbdomenLocalisationEnum
        {
            None = 0b_0000_0000,  // 0
            FrontRight = 0b_0000_0001,  // 1
            FrontLeft = 0b_0000_0010,  // 2
            BackRight = 0b_0000_0100,  // 4
            BackLeft = 0b_0000_1000,  // 8
        }


        [Flags]
        public enum LowerLocalisationEnum
        {
            None = 0b_0000_0000,  // 0
            HipFront = 0b_0000_0001,  // 1
            ShinFront = 0b_0000_0010,  // 2
            FootFront = 0b_0000_0100,  // 4
            HupTourniquiet = 0b_0000_1000,  // 8
            ShinTourniquiet = 0b_0001_0000,  // 16
            HipBack = 0b_0010_0000,  // 32
            ShinBack = 0b_0100_0000,  // 64
            FootBack = 0b_1000_0000,  // 128

        }

        [Flags]
        public enum InjuryMechanismEnum
        {
            None = 0b_0000_0000,  // 0
            Mechanic = 0b_0000_0001,  // 1 Механическая
            Explosion = 0b_0000_0010,  // 2 Минно-взрывная
            Combined = 0b_0000_0100,  // 4 Сочетанная
        }

        [Flags]
        public enum ThermoEnum
        {
            None = 0b_0000_0000,  // 0
            // Ожоги
            Burn1 = 0b_0000_0001,  // 1
            Burn2 = 0b_0000_0010,  // 2
            Burn3 = 0b_0000_0100,  // 4
            Burn4 = 0b_0000_1000,  // 8
            // Отморожения
            Frostbite1 = 0b_0001_0000,  // 16
            Frostbite2 = 0b_0010_0000,  // 32
            Frostbite3 = 0b_0100_0000,  // 64
            Frostbite4 = 0b_1000_0000,  // 128
        }

        [Flags]
        public enum InjuryTypeEnum
        {
            None = 0b_0000_0000,  // 0 
            Explosion = 0b_0000_0001,  // 1взрывное
            GunshotBullet = 0b_0000_0010,  // 2 огнестрельное пулевое 
            GunshotFragile = 0b_0000_0100,  // 4 осколочное
            Stabbed = 0b_0000_1000,  // 8 колотое
        }

        [Flags]
        public enum ThroughEnum
        {
            None = 0b_0000_0000,  // 0
            Through = 0b_0000_0001,  // 1 Сквозное
            Blind = 0b_0000_0010,  // 2 Слепое
        }

        [Flags]
        public enum DepthEnum
        {
            None = 0b_0000_0000,  // 0
            Penetration = 0b_0000_0001,  // 1 Проникающее
            SoftTissue = 0b_0000_0010,  // 2 Мягких тканей
        }

        [Flags]
        public enum InjuryType
        {
            None = 0b_0000_0000,  // 0
            Monday = 0b_0000_0001,  // 1
            Tuesday = 0b_0000_0010,  // 2
            Wednesday = 0b_0000_0100,  // 4
            Thursday = 0b_0000_1000,  // 8
            Friday = 0b_0001_0000,  // 16
            Saturday = 0b_0010_0000,  // 32
            Sunday = 0b_0100_0000,  // 64
            Weekend = Saturday | Sunday
        }

    }
}
