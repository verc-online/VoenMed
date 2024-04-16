using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models.InjuryModels
{
    public class SpineDamageModel : DamageBaseModel
    {
        public SpineLocalisationEnum Localisation { get; private set; }
        public SpineDamageEnum Damage { get; private set; }
        public SpineFractureEnum Fracture { get; private set; }
        public string StatusLocalis { get; set; } = "";

        #region Localisation
        // ADD
        public void AddSpineFront()
        {
            Localisation |= SpineLocalisationEnum.FrontSpine;
        }
        public void AddSpineBack()
        {
            Localisation |= SpineLocalisationEnum.BackSpine;
        }

        // DELETE
        public void DeleteSpineFront()
        {
            Localisation ^= SpineLocalisationEnum.FrontSpine;
        }
        public void DeleteSpineBack()
        {
            Localisation ^= SpineLocalisationEnum.FrontSpine;
        }
        #endregion

        #region Damage
        // Повреждения
        public void AddCordDamage()
        {
            Damage |= SpineDamageEnum.Cord;
        }
        public void DeleteCordDamage()
        {
            Damage ^= SpineDamageEnum.Cord;
        }
        public void AddRootDamage()
        {
            Damage |= SpineDamageEnum.Root;
        }
        public void DeleteRootDamage()
        {
            Damage ^= SpineDamageEnum.Root;
        }

        // Переломы
        public void AddBodyFracture()
        {
            Fracture |= SpineFractureEnum.Body;
        }
        public void DeleteBodyFracture()
        {
            Fracture ^= SpineFractureEnum.Body;
        }
        public void AddArchesFracture()
        {
            Fracture |= SpineFractureEnum.Arches;
        }
        public void DeleteArchesFracture()
        {
            Fracture ^= SpineFractureEnum.Arches;
        }
        public void AddProcessFracture()
        {
            Fracture |= SpineFractureEnum.Process;
        }
        public void DeleteProcessFracture()
        {
            Fracture ^= SpineFractureEnum.Process;
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
            output += " спинного мозга ";

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
