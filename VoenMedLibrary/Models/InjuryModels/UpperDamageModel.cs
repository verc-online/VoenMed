﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models.InjuryModels
{
    public class UpperDamageModel : DamageBaseModel
    {
        public UpperLocalisationEnum Localisation { get; private set; }
        public LimbDamageEnum Damage { get; private set; }
        public LimbFractureEnum Fracture { get; private set; }
        public LimbDesctructionEnum Desctruction { get; private set; }
        public string StatusLocalis { get; set; } = "";

        #region Localisation
        // ADD
        public void AddLocalisationShoulderFront()
        {
            Localisation |= UpperLocalisationEnum.ShoulderFront;
        }
        public void AddLocalisationForearmFront()
        {
            Localisation |= UpperLocalisationEnum.ForearmFront;
        }
        public void AddLocalisationWristFront()
        {
            Localisation |= UpperLocalisationEnum.WristFront;
        }
        public void AddLocalisationShoulderTourniquiet()
        {
            Localisation |= UpperLocalisationEnum.ShoulderTourniquet;
        }
        public void AddLocalisationForearmTourniquet()
        {
            Localisation |= UpperLocalisationEnum.ForearmTourniquet;
        }
        public void AddLocalisationShoulderBack()
        {
            Localisation |= UpperLocalisationEnum.ShoulderBack;
        }
        public void AddLocalisationForearmBack()
        {
            Localisation |= UpperLocalisationEnum.ForearmBack;
        }
        public void AddLocalisationWristBack()
        {
            Localisation |= UpperLocalisationEnum.WristBack;
        }

        // DELETE
        public void DeleteLocalisationShoulderFront()
        {
            Localisation ^= UpperLocalisationEnum.ShoulderFront;
        }
        public void DeleteLocalisationForearmFront()
        {
            Localisation ^= UpperLocalisationEnum.ForearmFront;
        }
        public void DeleteLocalisationWristFront()
        {
            Localisation ^= UpperLocalisationEnum.WristFront;
        }
        public void DeleteLocalisationShoulderTourniquiet()
        {
            Localisation ^= UpperLocalisationEnum.ShoulderTourniquet;
        }
        public void DeleteLocalisationForearmTourniquet()
        {
            Localisation ^= UpperLocalisationEnum.ForearmTourniquet;
        }
        public void DeleteLocalisationShoulderBack()
        {
            Localisation ^= UpperLocalisationEnum.ShoulderBack;
        }
        public void DeleteLocalisationForearmBack()
        {
            Localisation ^= UpperLocalisationEnum.ForearmBack;
        }
        public void DeleteLocalisationWristBack()
        {
            Localisation ^= UpperLocalisationEnum.WristBack;
        }
        #endregion

        #region Damage
        // Повреждения
        public void AddJointDamage()
        {
            Damage |= LimbDamageEnum.Joint;
        }
        public void DeleteJointDamage()
        {
            Damage ^= LimbDamageEnum.Joint;
        }
        public void AddTrunksDamage()
        {
            Damage |= LimbDamageEnum.Trunks;
        }
        public void DeleteTrunksDamage()
        {
            Damage ^= LimbDamageEnum.Trunks;
        }

        public void AddSoftTissueDamage()
        {
            Damage |= LimbDamageEnum.SoftTissue;
        }
        public void DeleteSoftTissueDamage()
        {
            Damage ^= LimbDamageEnum.SoftTissue;
        }

        public void AddVesselsDamage()
        {
            Damage |= LimbDamageEnum.Vessels;
        }
        public void DeleteVesselsDamage()
        {
            Damage ^= LimbDamageEnum.Vessels;
        }
        
        public void AddDetachmentDamage()
        {
            Damage |= LimbDamageEnum.Detachment;
        }
        public void DeleteDetachmentDamage()
        {
            Damage ^= LimbDamageEnum.Detachment;
        }

        // Переломы
        public void AddBoneFracture()
        {
            Fracture |= LimbFractureEnum.Bone;
        }
        public void DeleteBoneFracture()
        {
            Fracture ^= LimbFractureEnum.Bone;
        }
        
        // Отрыв
        public void AddFullDesctruction()
        {
            Desctruction |= LimbDesctructionEnum.Full;
        }
        public void DeleteFullDesctruction()
        {
            Desctruction ^= LimbDesctructionEnum.Full;
        }
        public void AddPartDesctruction()
        {
            Desctruction |= LimbDesctructionEnum.Part;
        }
        public void DeletePartDesctruction()
        {
            Desctruction ^= LimbDesctructionEnum.Part;
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
            output += " таза ";

            output += Localisation.GetDescriptionsAsText().ToLower() + " ";

            if (Damage > 0)
                output += "c повреждениями " + Damage.GetDescriptionsAsText().ToLower() + " ";
            if (Fracture > 0)
                output += "с переломами " + Fracture.GetDescriptionsAsText().ToLower() + ". ";
            if(Desctruction > 0)
                output += "с " + Fracture.GetDescriptionsAsText().ToLower() + " отрывом. ";

            if (Thermo > 0)
                output += Thermo.GetDescriptionsAsText() + ". ";

            StatusLocalis = output;
            return output;
        }

        #endregion
    }
}