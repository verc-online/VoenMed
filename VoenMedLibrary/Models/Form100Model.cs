using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models
{
    public class Form100Model
    {
        public int Id { get; set; }
        public string IssuedBy { get; set; } // Кем выдана
        public DateTime IssuedWhen { get; set; } // Когда выдана

        public string LastName { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;

        public DateOnly? BirthDate { get; set; } = null;
        public string MilitaryId { get; set; } = "Неизвестен";
        public string MilitaryUnit { get; set; } = "Неизвестна";
        public string Duty { get; set; } = "Неизвестна";

        public int Rank { get; set; }
        public string RankTitle { get; set; } = "Неизвестно";

        public ReasonEnum Reason { get; set; } = ReasonEnum.Gunshot;
        public int WithoutFirstAid { get; set; } = 0;
        public DateTime DiseaseTime { get; set; }

        public EvacuationWayEnum EvacuationWay { get; set; } = EvacuationWayEnum.Hospital;
        public EvacuationOrderEnum EvacuationOrder { get; set; } = EvacuationOrderEnum.First;
        public EvacuationTransportEnum EvacuationTransport { get; set; } = EvacuationTransportEnum.MedAm;
        public EvacuationPositionEnum EvacuationPosition { get; set; } = EvacuationPositionEnum.LyingDown;
        public string EvacAddress { get; set; }
        public DateTime EvacTime { get; set; }
        public SpecialEnum Special { get; set; } = SpecialEnum.LifeThreateningCondition;

        public string Diagnosis { get; set; }
        public string Doc { get; set; }

        public InjuryStatusLocalisModel InjuryStatusLocalis { get; set; } = new();
        public ConditionModel Condition { get; set; } = new();
        public HelpProvidedModel HelpProvided { get; set; } = new();

        public string GetFullName()
        {
            string output = LastName ?? "Неизвестный " + FirstName ?? "" + " " + SecondName ?? "";
            return output;
        }

        public string GetFullPersonalInfo()
        {
            string output = "";

            output += "Звание: " + RankTitle + " "; 

            output += "ФИО: " + GetFullName() + ". ";

            output += "Год рождения: " + BirthDate ?? "Неизвестен. ";

            output += "Личный номер: " + MilitaryId + ". ";

            output += "Войсковая часть: " + MilitaryUnit + ". ";

            output += "Должность: " + Duty + ". ";


            return output;
        }

    }
}
