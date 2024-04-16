using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoenMedLibrary.Models
{
    public class EnumModel
    {
        #region Head

        [Flags]
        public enum HeadLocalisationEnum
        {
            None = 0b_0000_0000,  // 0
            [Description("Спереди")]
            FrontHead = 0b_0000_0001,  // 1
            [Description("Сзади")]
            BackHead = 0b_0000_0010,  // 2
        }
        [Flags]
        public enum HeadDamageEnum
        {
            // None = 0b_0000_0000,  // 0
            [Description("Мозга")]
            Brain = 0b_0000_0001,  // 1
            [Description("Глаз")]
            Eye = 0b_0000_0010,  // 2
            [Description("Ушей")]
            Ear = 0b_0000_0100,  // 4
            [Description("Челюстно-лицевой области")]
            MaxilloFacial = 0b_0000_1000,  // 8
        }
        [Flags]
        public enum HeadFractureEnum
        {
            // None = 0b_0000_0000,  // 0
            [Description("Костей черепа")]
            Skull = 0b_0000_0001,  // 1
            [Description("Челюстно-лицевой области")]
            MaxilloFacial = 0b_0000_0010,  // 2
        }
        #endregion

        #region Neck

        [Flags]
        public enum NeckLocalisationEnum
        {
            None = 0b_0000_0000,  // 0
            FrontNeck = 0b_0000_0001,  // 1
            BackNeck = 0b_0000_0010,  // 2
        }

        [Flags]
        public enum NeckDamageEnum
        {
            [Description("Глотки")]
            Throat = 0b_0000_0000_0001,  // 1 Глотка
            [Description("Гортани")]
            Larynx = 0b_0000_0000_0010,  // 2 Гортани
            [Description("Трахеи")]
            Trachea = 0b_0000_0000_0100,  // 4 Трахея
            [Description("Пищевода")]
            Esophagus = 0b_0000_0000_1000,  // 8 Пищевод
            [Description("Кровеносных сосудов")]
            Vessels = 0b_0000_0001_0000,  // 16 Кровеносные сосуды
        }
        #endregion

        #region Localisation
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
        #endregion

        #region Thorax

        [Flags]
        public enum ThoraxLocalisationEnum
        {
            [Description("Спереди справа")]
            FrontRight = 0b_0000_0001,  // 1
            [Description("Спереди слева")]
            FrontLeft = 0b_0000_0010,  // 2
            [Description("Сзади справа")]
            BackRight = 0b_0000_0100,  // 4
            [Description("Сзади слева")]
            BackLeft = 0b_0000_1000,  // 8
        }
        [Flags]
        public enum ThoraxFractureEnum
        {
            // None = 0b_0000_0000,  // 0
            [Description("Ребер")]
            Rib = 0b_0000_0001,  // 1
            [Description("Грудины")]
            Sternum = 0b_0000_0010,  // 2
            [Description("Ключицы")]
            Collarbone = 0b_0000_0100,  // 4
            [Description("Лопатки")]
            Blade = 0b_0000_1000,  // 8
        }
        [Flags]
        public enum ThoraxDamageEnum
        {
            [Description("Легких")]
            Lung = 0b_0000_0001,  // 1
            [Description("Сердца")]
            Heart = 0b_0000_0010,  // 2
            [Description("Пищевода")]
            Esophagus = 0b_0000_0100,  // 4
            [Description("Кровеносных сосудов")]
            Vessels = 0b_0000_1000,  // 8
        }
        #endregion

        #region Abdomen
        [Flags]
        public enum AbdomenLocalisationEnum
        {
            [Description("Спереди справа")]
            FrontRight = 0b_0000_0001,  // 1
            [Description("Спереди слева")]
            FrontLeft = 0b_0000_0010,  // 2
            [Description("Сзади справа")]
            BackRight = 0b_0000_0100,  // 4
            [Description("Сзади слева")]
            BackLeft = 0b_0000_1000,  // 8
        }
        [Flags]
        public enum AbdomenDamageEnum
        {
            [Description("Паренхиматозных органов")]
            Parenchymal = 0b_0000_0001,  // 1
            [Description("Полых органов")]
            Hollow = 0b_0000_0010,  // 2
            [Description("Сзади справа")]
            Vessels = 0b_0000_0100,  // 4
            [Description("Сзади слева")]
            NonOrgan = 0b_0000_1000,  // 8
        }

        #endregion

        #region Pelvis

        [Flags]
        public enum PelvisLocalisationEnum
        {
            None = 0b_0000_0000,  // 0
            [Description("Спереди")]
            FrontPelvis = 0b_0000_0001,  // 1
            [Description("Сзади")]
            BackPelvis = 0b_0000_0010,  // 2
        }
        [Flags]
        public enum PelvisDamageEnum
        {
            [Description("Мозга")]
            Brain = 0b_0000_0001,  // 1
            [Description("Глаз")]
            Eye = 0b_0000_0010,  // 2
            [Description("Ушей")]
            Ear = 0b_0000_0100,  // 4
            [Description("Челюстно-лицевой области")]
            MaxilloFacial = 0b_0000_1000,  // 8
        }
        [Flags]
        public enum PelvisFractureEnum
        {
            [Description("Костей черепа")]
            Skull = 0b_0000_0001,  // 1
            [Description("Челюстно-лицевой области")]
            MaxilloFacial = 0b_0000_0010,  // 2
        }
        #endregion

        #region Ethiology

        // Ранение и травма
        [Flags]
        public enum DamageEthiologyEnum
        {
            // Ранение
            [Description("Минно-взрывное")]
            Explosion = 0b_0000_0000_0001,  // 1 Минно-взрывное
            [Description("Пулевое")]
            Gunshot = 0b_0000_0000_0010,  // 2 Пулевое
            [Description("Осколочное")]
            Fragile = 0b_0000_0000_0100,  // 4 Осколочное

            // Не огнестрельное
            [Description("Колото-резаное")]
            StabbedCut = 0b_0000_0000_1000,  // 8 Колото-резаное
            [Description("Колотое")]
            Stabbed = 0b_0000_0001_0000,  // 16 Колотое
            [Description("Рубленое")]
            Chopped = 0b_0000_0010_0000,  // 32 Рубленое

            // Травма
            [Description("Травма")]
            Trauma = 0b_0100_0000,  // 64 Травма
        }

        [Flags]
        public enum DamageCharacterEnum
        {
            [Description("Сквозное")]
            Through = 0b_0000_0001,  // 1 Сквозное
            [Description("Слепое")]
            Blind = 0b_0000_0010,  // 2 Слепое
            [Description("Касательное")]
            Tangent = 0b_0000_0100,  // 4 Касательное
            // Отношение к полости
            [Description("проникающее")]
            Penetration = 0b_0000_0000_1000,  // 8 Проникающее
            [Description("не проникающее")]
            SoftTissue = 0b_0000_0001_0000,  // 16 Не Проникающее
            // Травма
            [Description("Открытая травма")]
            TraumaOpened = 0b_0100_0000,  // 64 Травма открытая
            [Description("Закрытая травма")]
            TraumaClosed = 0b_1000_0000,  // 128 Травма закрытая

        }

        [Flags]
        public enum ThermoDamageEnum
        {
            // Ожоги
            [Description("Ожог I степени")]
            Burn1 = 0b_0000_0001,  // 1
            [Description("Ожог II степени")]
            Burn2 = 0b_0000_0010,  // 2
            [Description("Ожог III степени")]
            Burn3 = 0b_0000_0100,  // 4
            [Description("Ожог IV степени")]
            Burn4 = 0b_0000_1000,  // 8

            // Отморожения
            [Description("Отморожение I степени")]
            Frostbite1 = 0b_0001_0000,  // 16
            [Description("Отморожение II степени")]
            Frostbite2 = 0b_0010_0000,  // 32
            [Description("Отморожение III степени")]
            Frostbite3 = 0b_0100_0000,  // 64
            [Description("Отморожение IV степени")]
            Frostbite4 = 0b_1000_0000,  // 128
        }

        #endregion

    }
}
