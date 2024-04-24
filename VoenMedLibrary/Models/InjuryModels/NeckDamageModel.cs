using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models.InjuryModels
{
    public class NeckDamageModel : DamageBaseModel
    {
        public NeckLocalisationEnum Localisation { get; private set; }
        public NeckDamageEnum Damage { get; private set; }

        public string StatusLocalis { get; set; } = "";
        
 
        #region Damage
        // Повреждения
        public void AddThroatDamage()
        {
            Damage |= NeckDamageEnum.Throat;
        }
        public void DeleteThroatDamage()
        {
            Damage ^= NeckDamageEnum.Throat;

        }
        public void AddLarynxDamage()
        {
            Damage |= NeckDamageEnum.Larynx;
        }
        public void DeleteLarynxDamage()
        {
            Damage ^= NeckDamageEnum.Larynx;

        }
        public void AddVesselsDamage()
        {
            Damage |= NeckDamageEnum.Vessels;
        }
        public void DeleteVesselsDamage()
        {
            Damage ^= NeckDamageEnum.Vessels;

        }
        public void AddTracheaDamage()
        {
            Damage |= NeckDamageEnum.Trachea;
        }
        public void DeleteTracheaDamage()
        {
            Damage ^= NeckDamageEnum.Trachea;

        }
        public void AddEsophagusDamage()
        {
            Damage |= NeckDamageEnum.Esophagus;
        }
        public void DeleteEsophagusDamage()
        {
            Damage ^= NeckDamageEnum.Esophagus;

        }

        #endregion

        // Если нет локализации повреждений, то и характер нулевой
        private void CheckLocalisation()
        {
            if (Localisation == 0) Damage = 0;
        }

        #region StatusLocalis
        public string GetStatusLocalis()
        {
            string output = "";
            switch ((int)Character)
            {
                case < 64:
                    output += Character.GetDescriptionsAsText() + " ранение ";
                    break;
                case 64:
                case 128:
                    output += Character.GetDescriptionsAsText();
                    break;
            }
            output += " шеи ";

            if (Damage > 0)
                output += "c повреждениями " + Damage.GetDescriptionsAsText().ToLower() + ". ";
            if (Thermo > 0)
                output += Thermo.GetDescriptionsAsText() + ". ";

            StatusLocalis = output;
            return output;
        }

        #endregion

        #region Localisation
        // Neck Front
        public void AddNeckFront()
        {
            Localisation |= NeckLocalisationEnum.FrontNeck;
            CheckLocalisation();
        }
        public void DeleteNeckFront()
        {
            Localisation ^= NeckLocalisationEnum.FrontNeck;
            CheckLocalisation();
        }
        // Neck Back
        public void AddNeckBack()
        {
            Localisation |= NeckLocalisationEnum.BackNeck;
            CheckLocalisation();
        }
        public void DeleteNeckBack()
        {
            Localisation ^= NeckLocalisationEnum.FrontNeck;
            CheckLocalisation();
        }

        public void ResetNeck()
        {
            Localisation = 0;
            CheckLocalisation();
        }
        #endregion
    }
}
