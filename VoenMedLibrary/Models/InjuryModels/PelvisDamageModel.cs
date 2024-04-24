using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models.InjuryModels
{
    public class PelvisDamageModel : DamageBaseModel
    {
        public PelvisLocalisationEnum Localisation { get; private set; }
        public PelvisDamageEnum Damage { get; private set; }
        public PelvisFractureEnum Fracture { get; private set; }
        public string StatusLocalis { get; set; } = "";

        #region Localisation
        // ADD
        public void AddPelvisFront()
        {
            Localisation |= PelvisLocalisationEnum.FrontPelvis;
        }
        public void AddPelvisBack()
        {
            Localisation |= PelvisLocalisationEnum.BackPelvis;
        }

        // DELETE
        public void DeletePelvisFront()
        {
            Localisation ^= PelvisLocalisationEnum.FrontPelvis;
        }
        public void DeletePelvisBack()
        {
            Localisation ^= PelvisLocalisationEnum.BackPelvis;
        }
        #endregion

        #region Damage
        // Повреждения
        public void AddBladderDamage()
        {
            Damage |= PelvisDamageEnum.Bladder;
        }
        public void DeleteBladderDamage()
        {
            Damage ^= PelvisDamageEnum.Bladder;
        }
        public void AddUrethraDamage()
        {
            Damage |= PelvisDamageEnum.Urethra;
        }
        public void DeleteUrethraDamage()
        {
            Damage ^= PelvisDamageEnum.Urethra;
        }

        public void AddRectumDamage()
        {
            Damage |= PelvisDamageEnum.Rectum;
        }
        public void DeleteRectumDamage()
        {
            Damage ^= PelvisDamageEnum.Rectum;
        }

        public void AddVesselsDamage()
        {
            Damage |= PelvisDamageEnum.Vessels;
        }
        public void DeleteVesselsDamage()
        {
            Damage ^= PelvisDamageEnum.Vessels;
        }
        
        public void AddHollowDamage()
        {
            Damage |= PelvisDamageEnum.Hollow;
        }
        public void DeleteHollowDamage()
        {
            Damage ^= PelvisDamageEnum.Hollow;
        }

        // Переломы
        public void AddPelvicBonesFracture()
        {
            Fracture |= PelvisFractureEnum.PelvicBones;
        }
        public void DeletePelvicBonesFracture()
        {
            Fracture ^= PelvisFractureEnum.PelvicBones;
        }
        public void AddSacrumFracture()
        {
            Fracture |= PelvisFractureEnum.Sacrum;
        }
        public void DeleteSacrumFracture()
        {
            Fracture ^= PelvisFractureEnum.Sacrum;
        }
        public void AddCoccyxFracture()
        {
            Fracture |= PelvisFractureEnum.Coccyx;
        }
        public void DeleteCoccyxFracture()
        {
            Fracture ^= PelvisFractureEnum.Coccyx;
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
            if (((int)Character) < 64)
                output += Character.GetDescriptionsAsText() + " ранение ";
            else if (((int)Character) == 64 || ((int)Character) == 128)
                output += Character.GetDescriptionsAsText();
            output += " таза ";

            output += Localisation.GetDescriptionsAsText().ToLower() + " ";

            if (Damage > 0)
                output += "c повреждениями " + Damage.GetDescriptionsAsText().ToLower() + " ";
            if (Fracture > 0)
                output += "с переломами " + Fracture.GetDescriptionsAsText().ToLower() + ". ";
            if (Thermo > 0)
                output += Thermo.GetDescriptionsAsText() + ". ";

            StatusLocalis = output;
            return output;
        }

        #endregion
    }
}
