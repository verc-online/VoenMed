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
        public enum HeadLocalisation
        {
            None = 0b_0000_0000,  // 0
            FrontHead = 0b_0000_0001,  // 1
            BackHead = 0b_0000_0010,  // 2
        }

        [Flags]
        public enum NeckLocalisation
        {
            None = 0b_0000_0000,  // 0
            FrontNeck = 0b_0000_0001,  // 1
            BackNeck = 0b_0000_0010,  // 2
        }

        [Flags]
        public enum UpperLocalisation
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
        public enum ThoraxLocalisation
        {
            None = 0b_0000_0000,  // 0
            FrontRight = 0b_0000_0001,  // 1
            FrontLeft = 0b_0000_0010,  // 2
            BackRight = 0b_0000_0100,  // 4
            BackLeft = 0b_0000_1000,  // 8
        }

        [Flags]
        public enum AbdomenLocalisation
        {
            None = 0b_0000_0000,  // 0
            FrontRight = 0b_0000_0001,  // 1
            FrontLeft = 0b_0000_0010,  // 2
            BackRight = 0b_0000_0100,  // 4
            BackLeft = 0b_0000_1000,  // 8
        }


        [Flags]
        public enum LowerLocalisation
        {
            None = 0b_0000_0000,  // 0
            HipFront = 0b_0000_0001,  // 1
            ShinFront = 0b_0000_0010,  // 2
            FootFront = 0b_0000_0100,  // 4
            HupTourniquiet = 0b_0000_1000,  // 8
            ShinTourniquiet = 0b_0001_0000,  // 16
            HipBack = 0b_0010_0000,  // 32
            ShinBack = 0b_0100_0000,  // 64
            FootBack = 0b_0100_0000,  // 128

        }
        // Localisation
        // Abdomen RightLeg
        [Flags]
        public enum Days
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
