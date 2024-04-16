using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models.InjuryModels
{
    public class InjuryStatusLocalisModel
    {
        public int Id { get; set; }
        public int Form100Id { get; set; }

        // Этиололгия: ранения или травма
        public DamageEthiologyEnum Ethiology { get; set; }

        #region Локализация повреждения
        public HeadDamageModel Head { get; set; } = new();
        public NeckDamageModel Neck { get; private set; } = new();
        public ThoraxDamageModel Thorax { get; private set; } = new();
        public AbdomenDamageModel Abdomen { get; private set; } = new();
        public PelvisDamageModel Pelvis { get; private set; } = new();

        // TODO Добавить
        // Позвоночник
        public UpperLocalisationEnum RightUpper { get; private set; }
        public UpperLocalisationEnum LeftUpper { get; private set; }
        public LowerLocalisationEnum RightLeg { get; private set; }
        public LowerLocalisationEnum LeftLeg { get; private set; }
        #endregion

        #region Ethiology change
        public void MakeEthiologyGunshot()
        {
            Ethiology = DamageEthiologyEnum.Gunshot;
        }
        public void MakeEthiologyExplosion()
        {
            Ethiology = DamageEthiologyEnum.Explosion;
        }
        public void MakeEthiologyFragile()
        {
            Ethiology = DamageEthiologyEnum.Fragile;
        }
        public void MakeEthiologyStabbedCut()
        {
            Ethiology = DamageEthiologyEnum.StabbedCut;
        }
        public void MakeEthiologyStabbed()
        {
            Ethiology = DamageEthiologyEnum.Stabbed;
        }
        public void MakeEthiologyChopped()
        {
            Ethiology = DamageEthiologyEnum.Chopped;
        }
        public void MakeEthiologyTrauma()
        {
            Ethiology = DamageEthiologyEnum.Trauma;
        }
        #endregion

        #region RightUpper
        // ADD
        public void AddRightUpperShoulderFront()
        {
            RightUpper |= UpperLocalisationEnum.ShoulderFront;
        }
        public void AddRightUpperForearmFront()
        {
            RightUpper |= UpperLocalisationEnum.ForearmFront;
        }
        public void AddRightUpperWristFront()
        {
            RightUpper |= UpperLocalisationEnum.WristFront;
        }
        public void AddRightUpperShoulderTourniquiet()
        {
            RightUpper |= UpperLocalisationEnum.ShoulderTourniquet;
        }
        public void AddRightUpperForearmTourniquet()
        {
            RightUpper |= UpperLocalisationEnum.ForearmTourniquet;
        }
        public void AddRightUpperShoulderBack()
        {
            RightUpper |= UpperLocalisationEnum.ShoulderBack;
        }
        public void AddRightUpperForearmBack()
        {
            RightUpper |= UpperLocalisationEnum.ForearmBack;
        }
        public void AddRightUpperWristBack()
        {
            RightUpper |= UpperLocalisationEnum.WristBack;
        }

        // DELETE
        public void DeleteRightUpperShoulderFront()
        {
            RightUpper ^= UpperLocalisationEnum.ShoulderFront;
        }
        public void DeleteRightUpperForearmFront()
        {
            RightUpper ^= UpperLocalisationEnum.ForearmFront;
        }
        public void DeleteRightUpperWristFront()
        {
            RightUpper ^= UpperLocalisationEnum.WristFront;
        }
        public void DeleteRightUpperShoulderTourniquiet()
        {
            RightUpper ^= UpperLocalisationEnum.ShoulderTourniquet;
        }
        public void DeleteRightUpperForearmTourniquet()
        {
            RightUpper ^= UpperLocalisationEnum.ForearmTourniquet;
        }
        public void DeleteRightUpperShoulderBack()
        {
            RightUpper ^= UpperLocalisationEnum.ShoulderBack;
        }
        public void DeleteRightUpperForearmBack()
        {
            RightUpper ^= UpperLocalisationEnum.ForearmBack;
        }
        public void DeleteRightUpperWristBack()
        {
            RightUpper ^= UpperLocalisationEnum.WristBack;
        }
        #endregion

        #region LeftUpper
        public void AddLeftUpperShoulderFront()
        {
            LeftUpper |= UpperLocalisationEnum.ShoulderFront;
        }
        public void AddLeftUpperForearmFront()
        {
            LeftUpper |= UpperLocalisationEnum.ForearmFront;
        }
        public void AddLeftUpperWristFront()
        {
            LeftUpper |= UpperLocalisationEnum.WristFront;
        }
        public void AddLeftUpperShoulderTourniquiet()
        {
            LeftUpper |= UpperLocalisationEnum.ShoulderTourniquet;
        }
        public void AddLeftUpperForearmTourniquet()
        {
            LeftUpper |= UpperLocalisationEnum.ForearmTourniquet;
        }
        public void AddLeftUpperShoulderBack()
        {
            LeftUpper |= UpperLocalisationEnum.ShoulderBack;
        }
        public void AddLeftUpperForearmBack()
        {
            LeftUpper |= UpperLocalisationEnum.ForearmBack;
        }
        public void AddLeftUpperWristBack()
        {
            LeftUpper |= UpperLocalisationEnum.WristBack;
        }

        public void DeleteLeftUpperShoulderFront()
        {
            LeftUpper ^= UpperLocalisationEnum.ShoulderFront;
        }
        public void DeleteLeftUpperForearmFront()
        {
            LeftUpper ^= UpperLocalisationEnum.ForearmFront;
        }
        public void DeleteLeftUpperWristFront()
        {
            LeftUpper ^= UpperLocalisationEnum.WristFront;
        }
        public void DeleteLeftUpperShoulderTourniquiet()
        {
            LeftUpper ^= UpperLocalisationEnum.ShoulderTourniquet;
        }
        public void DeleteLeftUpperForearmTourniquet()
        {
            LeftUpper ^= UpperLocalisationEnum.ForearmTourniquet;
        }
        public void DeleteLeftUpperShoulderBack()
        {
            LeftUpper ^= UpperLocalisationEnum.ShoulderBack;
        }
        public void DeleteLeftUpperForearmBack()
        {
            LeftUpper ^= UpperLocalisationEnum.ForearmBack;
        }
        public void DeleteLeftUpperWristBack()
        {
            LeftUpper ^= UpperLocalisationEnum.WristBack;
        }
        #endregion

        #region RightLeg

        // ADD
        public void AddRightFrontHip()
        {
            RightLeg |= LowerLocalisationEnum.HipFront;
        }
        public void AddRightFrontShin()
        {
            RightLeg |= LowerLocalisationEnum.ShinFront;
        }
        public void AddRightFrontFoot()
        {
            RightLeg |= LowerLocalisationEnum.FootFront;
        }
        public void AddRightFrontHipTourniquiet()
        {
            RightLeg |= LowerLocalisationEnum.HupTourniquiet;
        }
        public void AddRightFrontShinTourniquiet()
        {
            RightLeg |= LowerLocalisationEnum.ShinTourniquiet;
        }
        public void AddRightBackHip()
        {
            RightLeg |= LowerLocalisationEnum.HipBack;
        }
        public void AddRightBackShin()
        {
            RightLeg |= LowerLocalisationEnum.ShinBack;
        }
        public void AddRightBackFoot()
        {
            RightLeg |= LowerLocalisationEnum.FootBack;
        }
        // DELETE
        public void DeleteRightFrontHip()
        {
            RightLeg ^= LowerLocalisationEnum.HipFront;
        }
        public void DeleteRightFrontShin()
        {
            RightLeg ^= LowerLocalisationEnum.ShinFront;
        }
        public void DeleteRightFrontFoot()
        {
            RightLeg ^= LowerLocalisationEnum.FootFront;
        }
        public void DeleteRightFrontHipTourniquiet()
        {
            RightLeg ^= LowerLocalisationEnum.HupTourniquiet;
        }
        public void DeleteRightFrontShinTourniquiet()
        {
            RightLeg ^= LowerLocalisationEnum.ShinTourniquiet;
        }
        public void DeleteRightBackHip()
        {
            RightLeg ^= LowerLocalisationEnum.HipBack;
        }
        public void DeleteRightBackShin()
        {
            RightLeg ^= LowerLocalisationEnum.ShinBack;
        }
        public void DeleteRightBackFoot()
        {
            RightLeg ^= LowerLocalisationEnum.FootBack;
        }
        #endregion

        #region LeftLeg

        // ADD
        public void AddLeftFrontHip()
        {
            LeftLeg |= LowerLocalisationEnum.HipFront;
        }
        public void AddLeftFrontShin()
        {
            LeftLeg |= LowerLocalisationEnum.ShinFront;
        }
        public void AddLeftFrontFoot()
        {
            LeftLeg |= LowerLocalisationEnum.FootFront;
        }
        public void AddLeftFrontHipTourniquiet()
        {
            LeftLeg |= LowerLocalisationEnum.HupTourniquiet;
        }
        public void AddLeftFrontShinTourniquiet()
        {
            LeftLeg |= LowerLocalisationEnum.ShinTourniquiet;
        }
        public void AddLeftBackHip()
        {
            LeftLeg |= LowerLocalisationEnum.HipBack;
        }
        public void AddLeftBackShin()
        {
            LeftLeg |= LowerLocalisationEnum.ShinBack;
        }
        public void AddLeftBackFoot()
        {
            LeftLeg |= LowerLocalisationEnum.FootBack;
        }
        // DELETE
        public void DeleteLeftFrontHip()
        {
            LeftLeg ^= LowerLocalisationEnum.HipFront;
        }
        public void DeleteLeftFrontShin()
        {
            LeftLeg ^= LowerLocalisationEnum.ShinFront;
        }
        public void DeleteLeftFrontFoot()
        {
            LeftLeg ^= LowerLocalisationEnum.FootFront;
        }
        public void DeleteLeftFrontHipTourniquiet()
        {
            LeftLeg ^= LowerLocalisationEnum.HupTourniquiet;
        }
        public void DeleteLeftFrontShinTourniquiet()
        {
            LeftLeg ^= LowerLocalisationEnum.ShinTourniquiet;
        }
        public void DeleteLeftBackHip()
        {
            LeftLeg ^= LowerLocalisationEnum.HipBack;
        }
        public void DeleteLeftBackShin()
        {
            LeftLeg ^= LowerLocalisationEnum.ShinBack;
        }
        public void DeleteLeftBackFoot()
        {
            LeftLeg ^= LowerLocalisationEnum.FootBack;
        }
        #endregion



    }
}
