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
        #region Form100
        public enum ReasonEnum
        {
            [Description("Огнестрельное")] Gunshot, // 
            [Description("Другое поражение")] Other, // 
            [Description("Обморожение")] Nucleo, // 
            [Description("Ядерное ")] Frostbite, // 
            [Description("Химическое")] Chemistry, // 
            [Description("Бактериальное")] Bacterial, // 
            [Description("Инфенкионное ")] Infection, // 
            [Description("Небоевая патология ")] Body, // 
        }

        public enum AreaEnum
        {
            [Description("Голова")] Head = 1, // 
            [Description("Шея")] Neck, // 
            [Description("Грудь")] Thorax, // 
            [Description("Таз")] Pelvis, // 
            [Description("Органы брюшной полости")] Abdomen, // 
            [Description("Позвоночник")] Spine, // 
            [Description("Правая рука")] RightUpper, // 
            [Description("Левая рука")] LeftUpper, // 
            [Description("Правая нога")] RightLower, // 
            [Description("Левая нога")] LeftLower // 
        }
        public enum EvacuationWayEnum
        {
            [Description("ОмедБ")] Company, // 
            [Description("ОмедО")] Battalion, // 
            [Description("Госпиталь")] Hospital, // 
        }
        public enum EvacuationOrderEnum
        {
            [Description("Первая очередь")] First, // 
            [Description("Вторая очередь")] Second, // 
            [Description("Третья очередь")] Third, // 
        }

        public enum EvacuationTransportEnum
        {
            [Description("Автомобиль")] Am, // 
            [Description("Санитарный транспорт")] MedAm, // 
            [Description("Вертолет")] Helicopter, // 
            [Description("Самолет")] Airplane, // 
            [Description("Корабль")] Ship, // 
        }
        public enum EvacuationPositionEnum
        {
            [Description("Санитарный транспорт")] Sitting, // 
            [Description("Вертолет")] LyingDown, // 
        }


        [Flags]
        public enum SpecialEnum
        {
            // None = 0b_0000_0000,  // 0
            [Description("Угрожающее жизни состояние")] LifeThreateningCondition = 0b_0000_0001, // 1
            [Description("Глаз")] Isolation = 0b_0000_0010, // 2
            [Description("Радиационное поражение")] RadiationDamage = 0b_0000_0100, // 4
            [Description("Санитарная обработка")]
            SanitaryTreatment = 0b_0000_1000, // 8
        }

        #endregion
        #region Head

        [Flags]
        public enum HeadLocalisationEnum
        {
            None = 0b_0000_0000, // 0
            [Description("Спереди")] FrontHead = 0b_0000_0001, // 1
            [Description("Сзади")] BackHead = 0b_0000_0010, // 2
        }

        [Flags]
        public enum HeadDamageEnum
        {
            // None = 0b_0000_0000,  // 0
            [Description("Мозга")] Brain = 0b_0000_0001, // 1
            [Description("Глаз")] Eye = 0b_0000_0010, // 2
            [Description("Ушей")] Ear = 0b_0000_0100, // 4

            [Description("Челюстно-лицевой области")]
            MaxilloFacial = 0b_0000_1000, // 8
        }

        [Flags]
        public enum HeadFractureEnum
        {
            // None = 0b_0000_0000,  // 0
            [Description("Костей черепа")] Skull = 0b_0000_0001, // 1

            [Description("Челюстно-лицевой области")]
            MaxilloFacial = 0b_0000_0010, // 2
        }

        #endregion

        #region Neck

        [Flags]
        public enum NeckLocalisationEnum
        {
            None = 0b_0000_0000, // 0
            FrontNeck = 0b_0000_0001, // 1
            BackNeck = 0b_0000_0010, // 2
        }

        [Flags]
        public enum NeckDamageEnum
        {
            [Description("Глотки")] Throat = 0b_0000_0000_0001, // 1 Глотка
            [Description("Гортани")] Larynx = 0b_0000_0000_0010, // 2 Гортани
            [Description("Трахеи")] Trachea = 0b_0000_0000_0100, // 4 Трахея
            [Description("Пищевода")] Esophagus = 0b_0000_0000_1000, // 8 Пищевод
            [Description("Кровеносных сосудов")] Vessels = 0b_0000_0001_0000, // 16 Кровеносные сосуды
        }

        #endregion

        #region Thorax

        [Flags]
        public enum ThoraxLocalisationEnum
        {
            [Description("Спереди справа")] FrontRight = 0b_0000_0001, // 1
            [Description("Спереди слева")] FrontLeft = 0b_0000_0010, // 2
            [Description("Сзади справа")] BackRight = 0b_0000_0100, // 4
            [Description("Сзади слева")] BackLeft = 0b_0000_1000, // 8
        }

        [Flags]
        public enum ThoraxFractureEnum
        {
            // None = 0b_0000_0000,  // 0
            [Description("Ребер")] Rib = 0b_0000_0001, // 1
            [Description("Грудины")] Sternum = 0b_0000_0010, // 2
            [Description("Ключицы")] Collarbone = 0b_0000_0100, // 4
            [Description("Лопатки")] Blade = 0b_0000_1000, // 8
        }

        [Flags]
        public enum ThoraxDamageEnum
        {
            [Description("Легких")] Lung = 0b_0000_0001, // 1
            [Description("Сердца")] Heart = 0b_0000_0010, // 2
            [Description("Пищевода")] Esophagus = 0b_0000_0100, // 4
            [Description("Кровеносных сосудов")] Vessels = 0b_0000_1000, // 8
        }

        #endregion

        #region Abdomen

        [Flags]
        public enum AbdomenLocalisationEnum
        {
            [Description("Спереди справа")] FrontRight = 0b_0000_0001, // 1
            [Description("Спереди слева")] FrontLeft = 0b_0000_0010, // 2
            [Description("Сзади справа")] BackRight = 0b_0000_0100, // 4
            [Description("Сзади слева")] BackLeft = 0b_0000_1000, // 8
        }

        [Flags]
        public enum AbdomenDamageEnum
        {
            [Description("Паренхиматозных органов")]
            Parenchymal = 0b_0000_0001, // 1
            [Description("Полых органов")] Hollow = 0b_0000_0010, // 2
            [Description("Сзади справа")] Vessels = 0b_0000_0100, // 4
            [Description("Сзади слева")] NonOrgan = 0b_0000_1000, // 8
        }

        #endregion

        #region Pelvis

        [Flags]
        public enum PelvisLocalisationEnum
        {
            [Description("Спереди")] FrontPelvis = 0b_0000_0001, // 1
            [Description("Сзади")] BackPelvis = 0b_0000_0010, // 2
        }

        [Flags]
        public enum PelvisDamageEnum
        {
            [Description("Мочевого пузыря")] Bladder = 0b_0000_0001, // 1
            [Description("Уретры")] Urethra = 0b_0000_0010, // 2
            [Description("Прямой кишки")] Rectum = 0b_0000_0100, // 4
            [Description("Кровеносных сосудов")] Vessels = 0b_0000_1000, // 8
            [Description("Полых органов")] Hollow = 0b_0001_0000, // 16
        }

        [Flags]
        public enum PelvisFractureEnum
        {
            [Description("Тазовых костей")] PelvicBones = 0b_0000_0001, // 1
            [Description("Крестца")] Sacrum = 0b_0000_0010, // 2
            [Description("Копчика")] Coccyx = 0b_0000_0100, // 4
        }

        #endregion

        #region Spine

        [Flags]
        public enum SpineLocalisationEnum
        {
            [Description("")] FrontSpine = 0b_0000_0001, // 1
            [Description("Сзади")] BackSpine = 0b_0000_0010, // 2
        }

        [Flags]
        public enum SpineDamageEnum
        {
            [Description("Спинного мозга")] Cord = 0b_0000_0001, // 1
            [Description("Его корешков")] Root = 0b_0000_0010, // 2
            [Description("Кровеносных сосудов")] Vessels = 0b_0000_0100, // 4

        }

        [Flags]
        public enum SpineFractureEnum
        {
            [Description("Тел")] Body = 0b_0000_0001, // 1
            [Description("Дужек")] Arches = 0b_0000_0010, // 2
            [Description("Отростков")] Process = 0b_0000_0100, // 4
        }

        #endregion

        #region Limbs

        [Flags]
        public enum LowerLocalisationEnum
        {
            [Description("Бедра спереди")] HipFront = 0b_0000_0001, // 1
            [Description("Бедра сзади")] HipBack = 0b_0000_0010, // 2
            [Description("Голени спереди")] ShinFront = 0b_0000_0100, // 4
            [Description("Голени сзади")] ShinBack = 0b_0000_1000, // 8
            [Description("Стопы спереди")] FootFront = 0b_0001_0000, // 16
            [Description("Стопы сзади")] FootBack = 0b_0010_0000, // 32
            [Description("Наложен жгут на бедро")] ShinTourniquiet = 0b_0100_0000, // 64

            [Description("Наложен жгут на голень")]
            HipTourniquiet = 0b_1000_0000, // 128
        }

        [Flags]
        public enum UpperLocalisationEnum
        {
            [Description("Плеча спереди")] ShoulderFront = 0b_0000_0001, // 1
            [Description("Плеча сзади")] ShoulderBack = 0b_0000_0010, // 2
            [Description("Предплечья спереди")] ForearmFront = 0b_0000_0100, // 4
            [Description("Предплечья сзади")] ForearmBack = 0b_0000_1000, // 8
            [Description("Кисти спереди")] WristFront = 0b_0001_0000, // 16
            [Description("Кисти сзади")] WristBack = 0b_0010_0000, // 32

            [Description("Наложение жгута на плечо")]
            ShoulderTourniquet = 0b_0100_0000, // 128

            [Description("Наложение жгута на предплечье")]
            ForearmTourniquet = 0b_1000_0000, // 64
        }


        [Flags]
        public enum LimbDamageEnum
        {
            [Description("Суставов")] Joint = 0b_0000_0001, // 1
            [Description("Кровеносных сосудов")] Vessels = 0b_0000_0010, // 2
            [Description("Нервных стволов")] Trunks = 0b_0000_0100, // 4
            [Description("Мягких тканей")] SoftTissue = 0b_0000_1000, // 8

            [Description("Отслойкой мягких тканей")]
            Detachment = 0b_0001_0000, // 16
        }

        [Flags]
        public enum LimbFractureEnum
        {
            [Description("Костей")] Bone = 0b_0000_0001, // 1
        }

        [Flags]
        public enum LimbDesctructionEnum
        {
            [Description("Полным")] Full = 0b_0000_0001, // 1
            [Description("Неполным")] Part = 0b_0000_0010, // 2
        }

        #endregion

        #region Ethiology

        // Ранение и травма
        [Flags]
        public enum DamageEthiologyEnum
        {
            // Ранение
            [Description("Минно-взрывное")] Explosion = 0b_0000_0000_0001, // 1 Минно-взрывное
            [Description("Пулевое")] Gunshot = 0b_0000_0000_0010, // 2 Пулевое
            [Description("Осколочное")] Fragile = 0b_0000_0000_0100, // 4 Осколочное

            // Не огнестрельное
            [Description("Колото-резаное")] StabbedCut = 0b_0000_0000_1000, // 8 Колото-резаное
            [Description("Колотое")] Stabbed = 0b_0000_0001_0000, // 16 Колотое
            [Description("Рубленое")] Chopped = 0b_0000_0010_0000, // 32 Рубленое

            // Травма
            [Description("Травма")] Trauma = 0b_0100_0000, // 64 Травма
        }

        [Flags]
        public enum DamageCharacterEnum
        {
            [Description("Сквозное")] Through = 0b_0000_0001, // 1 Сквозное
            [Description("Слепое")] Blind = 0b_0000_0010, // 2 Слепое
            [Description("Касательное")] Tangent = 0b_0000_0100, // 4 Касательное

            // Отношение к полости
            [Description("проникающее")] Penetration = 0b_0000_0000_1000, // 8 Проникающее
            [Description("не проникающее")] SoftTissue = 0b_0000_0001_0000, // 16 Не Проникающее

            // Травма
            [Description("Открытая травма")] TraumaOpened = 0b_0100_0000, // 64 Травма открытая
            [Description("Закрытая травма")] TraumaClosed = 0b_1000_0000, // 128 Травма закрытая
        }

        [Flags]
        public enum ThermoDamageEnum
        {
            // Ожоги
            [Description("Ожог I степени")] Burn1 = 0b_0000_0001, // 1
            [Description("Ожог II степени")] Burn2 = 0b_0000_0010, // 2
            [Description("Ожог III степени")] Burn3 = 0b_0000_0100, // 4
            [Description("Ожог IV степени")] Burn4 = 0b_0000_1000, // 8

            // Отморожения
            [Description("Отморожение I степени")] Frostbite1 = 0b_0001_0000, // 16

            [Description("Отморожение II степени")]
            Frostbite2 = 0b_0010_0000, // 32

            [Description("Отморожение III степени")]
            Frostbite3 = 0b_0100_0000, // 64

            [Description("Отморожение IV степени")]
            Frostbite4 = 0b_1000_0000, // 128
        }

        #endregion

        #region Condition
        // Состояние
        public enum ConditionEnum
        {
            [Description("Стабильное")] Stable = 0b_0000_0001, // 1
            [Description("Нестабильное")] Unstable = 0b_0000_0010, // 2
            [Description("Клиническая смерть")] ClinicalDeath = 0b_0000_0100, // 4
        }


        // Сознание
        public enum ConsienceEnum
        {
            [Description("Ясное")] Clear, // 
            [Description("Умеренное оглушение")] ModerateStun, // 
            [Description("Глубокое оглушение")] DeepStun, // 
            [Description("Сопор")] Sopor, // 
            [Description("Умеренная кома")] ModerateComa, // 
            [Description("Глубокая кома")] DeepComa, // 
            [Description("Терминальная кома")] TerminalComa, // 
        }        
        
        // Сознание
        public enum EyeResponseEnum
        {
            [Description("Нет контакта")] NoEyeResponse = 1, // 
            [Description("Открывает на боль")] EyeOpeningResponsePain = 2, // 
            [Description("Открывает на речь")] EyeOpeningToSpeech = 3, // 
            [Description("Спонтанное движение глаз")] EyeOpeningSpontaneously = 4, // 
        }
        // Сознание
        public enum VerbalResponseEnum
        {
            [Description("Нет ответа")] NoVerbalResponse = 1, // 
            [Description("Бесссвязные звуки")] IncomprehensibleSounds = 2, // 
            [Description("Нечеткие ответы")] InappropriateWords = 3, // 
            [Description("Дезориентированная")] Confused = 4, // 
            [Description("Ориентированная")] Oriented = 5, // 
        }
        // Сознание
        public enum MotorResponseEnum
        {
            [Description("Нет ответа")] NoMotorResponse = 1, // 
            [Description("Разгибание от боли")] ExtensionToPain = 2, // 
            [Description("Сгибание от боли")] AbnormalFlexionToPain = 3, // 
            [Description("Отдергивание от боли")] WithdrawalToPain = 4, // 
            [Description("Локализует боль")] LocalizesToPain = 5, // 
            [Description("Выполняет команды")] ObeysMotorCommands = 6, // 
        }

        #endregion

        #region Breathing
        public enum indexOxygenationEnum
        {
            [Description("Легкая")] Stable, // 200-300
            [Description("Средняя")] Unstable, // 100-200
            [Description("Тяжелая")] ClinicalDeath, // <100
            [Description("Норма")] Normal // <100
        }
        public enum BreathingSupportEnum
        {
            [Description("Дыхание эффективное самостоятельное")] Effective, // 200-300
            [Description("Дыхание самостоятельное, инсуфляция кислорода")] Insuflation, // 100-200
            [Description("Надгортанный воздуховод")] NVU, // <100
            [Description("Эндотрахеальная трубка")] Endotracheal, // <100
            [Description("Коникотомия")] Conicotomy // <100
        }
        #endregion

        #region Heart
        public enum CapillaryTimeEnum
        {
            [Description("Меньше 2 с.")] Fast, // 
            [Description("Больше 2 с.")] Slow, // 
        }
        #endregion

        #region AdditionalInfo
        // Ранение и травма
        [Flags]
        public enum AdditionalInfoEnum
        {
            [Description("Судороги")] Seizures = 0b_0000_0000_0001, // 1 
            [Description("Психомоторное возбуждение")] Excitement = 0b_0000_0000_0010, // 2 
            [Description("Галлюцинации")] Hallucinations = 0b_0000_0000_0100, // 4 
            [Description("Рвота")] Vomiting = 0b_0000_0000_1000, // 8 
            [Description("Диарея")] Diarrhea = 0b_0000_0001_0000, // 16 
        }
        #endregion

        #region HelpProvided
        public enum WayStopBleedingEnum
        {
            [Description("Жгутом")] Tourniquet = 1, // 
            [Description("Давящей повязкой")] PressureBandage, // 
            [Description("Прошиванием сосудов")] Other, // 
        }


        [Flags]
        public enum DecompressionOfThePleuralCavityEnum
        {
            [Description("Справа")] Right = 0b_0000_0001, // 1
            [Description("Слева")] Left = 0b_0000_0010, // 2
        }

        [Flags]
        public enum DrainageOfThePleuralCavityEnum
        {
            [Description("Справа")] Right = 0b_0000_0001, // 1
            [Description("Слева")] Left = 0b_0000_0010, // 2
        }


        [Flags]
        public enum ImmobilizationEnum
        {
            [Description("Штатными средствами")] Standart = 0b_0000_0001, // 1
            [Description("Подручными средствами")] Other = 0b_0000_0010, // 2
        }

        [Flags]
        public enum IntensiveCareMeasuresEnum
        {
            [Description("Успешно")] Success = 0b_0000_0001, // 1
            [Description("Летальный исход")] Lethal = 0b_0000_0010, // 2
        }
        #endregion

    }
}