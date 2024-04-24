using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models.InjuryModels
{
    public class LowerDamageModel : DamageBaseModel
    {
        public LowerLocalisationEnum Localisation { get; private set; }
        public LimbDamageEnum Damage { get; private set; }
        public LimbFractureEnum Fracture { get; private set; }
        public LimbDesctructionEnum Desctruction { get; private set; }
        public string StatusLocalis { get; set; } = "";

        #region Localisation
        // ADD
        public void AddFrontHip()
        {
            Localisation |= LowerLocalisationEnum.HipFront;
        }
        public void AddFrontShin()
        {
            Localisation |= LowerLocalisationEnum.ShinFront;
        }
        public void AddFrontFoot()
        {
            Localisation |= LowerLocalisationEnum.FootFront;
        }
        public void AddFrontHipTourniquiet()
        {
            Localisation |= LowerLocalisationEnum.HipTourniquiet;
        }
        public void AddFrontShinTourniquiet()
        {
            Localisation |= LowerLocalisationEnum.ShinTourniquiet;
        }
        public void AddBackHip()
        {
            Localisation |= LowerLocalisationEnum.HipBack;
        }
        public void AddBackShin()
        {
            Localisation |= LowerLocalisationEnum.ShinBack;
        }
        public void AddBackFoot()
        {
            Localisation |= LowerLocalisationEnum.FootBack;
        }
        // DELETE
        public void DeleteFrontHip()
        {
            Localisation ^= LowerLocalisationEnum.HipFront;
        }
        public void DeleteFrontShin()
        {
            Localisation ^= LowerLocalisationEnum.ShinFront;
        }
        public void DeleteFrontFoot()
        {
            Localisation ^= LowerLocalisationEnum.FootFront;
        }
        public void DeleteFrontHipTourniquiet()
        {
            Localisation ^= LowerLocalisationEnum.HipTourniquiet;
        }
        public void DeleteFrontShinTourniquiet()
        {
            Localisation ^= LowerLocalisationEnum.ShinTourniquiet;
        }
        public void DeleteBackHip()
        {
            Localisation ^= LowerLocalisationEnum.HipBack;
        }
        public void DeleteBackShin()
        {
            Localisation ^= LowerLocalisationEnum.ShinBack;
        }
        public void DeleteBackFoot()
        {
            Localisation ^= LowerLocalisationEnum.FootBack;
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

            output += Localisation.GetDescriptionsAsText().ToLower() + ". ";

            if (Damage > 0)
                output += "Повреждения " + Damage.GetDescriptionsAsText().ToLower() + ". ";
            if (Fracture > 0)
                output += "Переломы " + Fracture.GetDescriptionsAsText().ToLower() + ". ";
            if(Desctruction > 0)
                output += "С " + Desctruction.GetDescriptionsAsText() + " отрывом. ";

            if (Thermo > 0)
                output += Thermo.GetDescriptionsAsText() + ". ";

            StatusLocalis = output;
            return output;
        }

        #endregion
    }
}
