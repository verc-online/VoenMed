using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models.InjuryModels
{
    public class ThoraxDamageModel : DamageBaseModel
    {
        public ThoraxLocalisationEnum Localisation { get; private set; }
        public ThoraxDamageEnum Damage { get; private set; }
        public ThoraxFractureEnum Fracture { get; private set; }

        public string StatusLocalis { get; set; } = "";

        #region Localisation
        // ADD
        public void AddThoraxFrontRight()
        {
            Localisation |= ThoraxLocalisationEnum.FrontRight;
        }
        public void AddThoraxFrontLeft()
        {
            Localisation |= ThoraxLocalisationEnum.FrontLeft;
        }
        public void AddThoraxBackRight()
        {
            Localisation |= ThoraxLocalisationEnum.BackRight;
        }
        public void AddThoraxBackLeft()
        {
            Localisation |= ThoraxLocalisationEnum.BackLeft;
        }

        // DELETE
        public void DeleteThoraxFrontRight()
        {
            Localisation ^= ThoraxLocalisationEnum.FrontRight;
        }
        public void DeleteThoraxFrontLeft()
        {
            Localisation ^= ThoraxLocalisationEnum.FrontLeft;
        }
        public void DeleteThoraxBackRight()
        {
            Localisation ^= ThoraxLocalisationEnum.BackRight;
        }
        public void DeleteThoraxBackLeft()
        {
            Localisation ^= ThoraxLocalisationEnum.BackLeft;
        }

        #endregion


        #region Damage
        // Повреждения
        public void AddEsophagusDamage()
        {
            Damage |= ThoraxDamageEnum.Esophagus;
        }
        public void DeleteEsophagusDamage()
        {
            Damage ^= ThoraxDamageEnum.Esophagus;
        }
        public void AddHeartDamage()
        {
            Damage |= ThoraxDamageEnum.Heart;
        }
        public void DeleteHeartDamage()
        {
            Damage ^= ThoraxDamageEnum.Heart;
        }

        public void AddLungDamage()
        {
            Damage |= ThoraxDamageEnum.Lung;
        }
        public void DeleteLungDamage()
        {
            Damage ^= ThoraxDamageEnum.Lung;
        }

        public void AddVesselsDamage()
        {
            Damage |= ThoraxDamageEnum.Vessels;
        }
        public void DeleteVesselsDamage()
        {
            Damage ^= ThoraxDamageEnum.Vessels;
        }

        // Переломы
        public void AddRibFracture()
        {
            Fracture |= ThoraxFractureEnum.Rib;
        }
        public void DeleteRibFracture()
        {
            Fracture ^= ThoraxFractureEnum.Rib;
        }
        public void AddSternumFracture()
        {
            Fracture |= ThoraxFractureEnum.Sternum;
        }
        public void DeleteSternumFracture()
        {
            Fracture ^= ThoraxFractureEnum.Sternum;
        }
        public void AddCollarboneFracture()
        {
            Fracture |= ThoraxFractureEnum.Collarbone;
        }
        public void DeleteCollarboneFracture()
        {
            Fracture ^= ThoraxFractureEnum.Collarbone;
        }
        public void AddBladeFracture()
        {
            Fracture |= ThoraxFractureEnum.Blade;
        }
        public void DeleteBladeFracture()
        {
            Fracture ^= ThoraxFractureEnum.Blade;
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
