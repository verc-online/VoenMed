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
            Localisation ^= PelvisLocalisationEnum.FrontPelvis;
        }
        #endregion


        #region Damage
        // Повреждения
        public void AddEsophagusDamage()
        {
            Damage |= PelvisDamageEnum.Esophagus;
        }
        public void DeleteEsophagusDamage()
        {
            Damage ^= PelvisDamageEnum.Esophagus;
        }
        public void AddHeartDamage()
        {
            Damage |= PelvisDamageEnum.Heart;
        }
        public void DeleteHeartDamage()
        {
            Damage ^= PelvisDamageEnum.Heart;
        }

        public void AddLungDamage()
        {
            Damage |= PelvisDamageEnum.Lung;
        }
        public void DeleteLungDamage()
        {
            Damage ^= PelvisDamageEnum.Lung;
        }

        public void AddVesselsDamage()
        {
            Damage |= PelvisDamageEnum.Vessels;
        }
        public void DeleteVesselsDamage()
        {
            Damage ^= PelvisDamageEnum.Vessels;
        }

        // Переломы
        public void AddRibFracture()
        {
            Fracture |= PelvisFractureEnum.Rib;
        }
        public void DeleteRibFracture()
        {
            Fracture ^= PelvisFractureEnum.Rib;
        }
        public void AddSternumFracture()
        {
            Fracture |= PelvisFractureEnum.Sternum;
        }
        public void DeleteSternumFracture()
        {
            Fracture ^= PelvisFractureEnum.Sternum;
        }
        public void AddCollarboneFracture()
        {
            Fracture |= PelvisFractureEnum.Collarbone;
        }
        public void DeleteCollarboneFracture()
        {
            Fracture ^= PelvisFractureEnum.Collarbone;
        }
        public void AddBladeFracture()
        {
            Fracture |= PelvisFractureEnum.Blade;
        }
        public void DeleteBladeFracture()
        {
            Fracture ^= PelvisFractureEnum.Blade;
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
            output += " грудной клетки ";

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
