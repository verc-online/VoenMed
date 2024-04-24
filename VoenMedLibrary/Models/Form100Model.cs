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
        public string IssuedBy { get; set; } // Кем выдана
        public DateTime IssuedWhen { get; set; } // Когда выдана

        //
        public string LastName { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }
        public string MilitaryId { get; set; }
        public string MilitaryUnit { get; set; }
        public string Duty { get; set; }

        public int Rank { get; set; } // TODO: Enum

        public ReasonEnum Reason { get; set; } = ReasonEnum.Gunshot;
        public int WithoutFirstAid { get; set; } = 0;
        public DateTime DiseaseTime { get; set; }

        public EvacuationWayEnum EvacuationWay { get; set; } = EvacuationWayEnum.Hospital;
        public EvacuationOrderEnum EvacuationOrder { get; set; } = EvacuationOrderEnum.First;
        public EvacuationTransportEnum EvacuationTransport { get; set; } = EvacuationTransportEnum.MedAm;
        public EvacuationPositionEnum EvacuationPosition { get; set; } = EvacuationPositionEnum.LyingDown;
        public SpecialEnum Special { get; set; } = SpecialEnum.LifeThreateningCondition;
        public string EvacAddress { get; set; }
        public DateTime EvacTime { get; set; }
        public string Doc { get; set; }

        public InjuryStatusLocalisModel InjuryStatusLocalis { get; set; }
        public ConditionModel Condition { get; set; }
        public HelpProvidedModel HelpProvided { get; set; }

    }
}
