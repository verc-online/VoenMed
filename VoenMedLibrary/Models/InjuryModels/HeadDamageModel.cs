using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models.InjuryModels
{
    public class HeadDamageModel : DamageBaseModel
    {
        public HeadLocalisationEnum Localisation { get; private set; }
        public HeadDamageEnum Damage { get; private set; }
        public HeadFractureEnum Fracture { get; private set; }

        public string StatusLocalis { get; set; } = "";
        
        #region Localisation
        // Head Front

        public void AddHeadFront()
        {
            Localisation |= HeadLocalisationEnum.FrontHead;
            CheckLocalisation();
        }
        public void DeleteHeadFront()
        {
            Localisation ^= HeadLocalisationEnum.FrontHead;
            CheckLocalisation();

        }
        // Head Back
        public void AddHeadBack()
        {
            Localisation |= HeadLocalisationEnum.BackHead;
            CheckLocalisation();

        }
        public void DeleteHeadBack()
        {
            Localisation ^= HeadLocalisationEnum.BackHead;
            CheckLocalisation();

        }
        public void ResetHead()
        {
            Localisation = 0;
            CheckLocalisation();

        }

        #endregion

 
        #region Damage
        // Повреждения
        public void AddBrainDamage()
        {
            Damage |= HeadDamageEnum.Brain;
        }
        public void DeleteBrainDamage()
        {
            Damage ^= HeadDamageEnum.Brain;

        }
        public void AddEyeDamage()
        {
            Damage |= HeadDamageEnum.Eye;
        }
        public void DeleteEyeDamage()
        {
            Damage ^= HeadDamageEnum.Eye;

        }
        public void AddEarDamage()
        {
            Damage |= HeadDamageEnum.Ear;
        }
        public void DeleteEarDamage()
        {
            Damage ^= HeadDamageEnum.Ear;

        }
        public void AddMaxilloFacialDamage()
        {
            Damage |= HeadDamageEnum.MaxilloFacial;
        }
        public void DeleteMaxilloFacialDamage()
        {
            Damage ^= HeadDamageEnum.MaxilloFacial;

        }

        // Переломы
        public void AddSkullFracture()
        {
            Fracture |= HeadFractureEnum.Skull;
        }
        public void DeleteSkullFracture()
        {
            Fracture ^= HeadFractureEnum.Skull;

        }
        public void AddFaceFracture()
        {
            Fracture |= HeadFractureEnum.MaxilloFacial;
        }
        public void DeleteFaceFracture()
        {
            Fracture ^= HeadFractureEnum.MaxilloFacial;

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
            output += " головы ";

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
