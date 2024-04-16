using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models.InjuryModels
{
    public class AbdomenDamageModel : DamageBaseModel
    {
        public AbdomenLocalisationEnum Localisation { get; private set; }
        public AbdomenDamageEnum Damage { get; private set; }

        public string StatusLocalis { get; set; } = "";

        #region Localisation
        // ADD
        public void AddAbdomenFrontRight()
        {
            Localisation |= AbdomenLocalisationEnum.FrontRight;
        }
        public void AddAbdomenFrontLeft()
        {
            Localisation |= AbdomenLocalisationEnum.FrontLeft;
        }
        public void AddAbdomenBackRight()
        {
            Localisation |= AbdomenLocalisationEnum.BackRight;
        }
        public void AddAbdomenBackLeft()
        {
            Localisation |= AbdomenLocalisationEnum.BackLeft;
        }

        // DELETE
        public void DeleteAbdomenFrontRight()
        {
            Localisation ^= AbdomenLocalisationEnum.FrontRight;
        }
        public void DeleteAbdomenFrontLeft()
        {
            Localisation ^= AbdomenLocalisationEnum.FrontLeft;
        }
        public void DeleteAbdomenBackRight()
        {
            Localisation ^= AbdomenLocalisationEnum.BackRight;
        }
        public void DeleteAbdomenBackLeft()
        {
            Localisation ^= AbdomenLocalisationEnum.BackLeft;
        }

        #endregion


        #region Damage
        // Повреждения
        public void AddParenchymalDamage()
        {
            Damage |= AbdomenDamageEnum.Parenchymal;
        }
        public void DeleteParenchymalDamage()
        {
            Damage ^= AbdomenDamageEnum.Parenchymal;
        }
        public void AddHollowDamage()
        {
            Damage |= AbdomenDamageEnum.Hollow;
        }
        public void DeleteHollowDamage()
        {
            Damage ^= AbdomenDamageEnum.Hollow;
        }
        public void AddNonOrganDamage()
        {
            Damage |= AbdomenDamageEnum.NonOrgan;
        }
        public void DeleteNonOrganDamage()
        {
            Damage ^= AbdomenDamageEnum.NonOrgan;
        }
        public void AddVesselsDamage()
        {
            Damage |= AbdomenDamageEnum.Vessels;
        }
        public void DeleteVesselsDamage()
        {
            Damage ^= AbdomenDamageEnum.Vessels;
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
            output += " живота ";

            output += Localisation.GetDescriptionsAsText().ToLower() + " ";

            if (Damage > 0)
                output += "c повреждениями " + Damage.GetDescriptionsAsText().ToLower() + ". ";
            if (Thermo > 0)
                output += Thermo.GetDescriptionsAsText() + ". ";

            StatusLocalis = output;
            return output;
        }

        #endregion
    }
}
