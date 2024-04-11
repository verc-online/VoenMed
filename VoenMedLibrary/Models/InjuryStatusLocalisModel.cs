using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models
{
    public class InjuryStatusLocalisModel
    {
        public int Id { get; set; }
        public int Form100Id { get; set; }

        #region Локализация повреждения
        public HeadLocalisationEnum Head { get; private set; }
        public NeckLocalisationEnum Neck { get; private set; }
        public UpperLocalisationEnum RightUpper { get; private set; }
        public UpperLocalisationEnum LeftUpper { get; private set; }
        public ThoraxLocalisationEnum Thorax { get; private set; }
        public AbdomenLocalisationEnum Abdomen { get; private set; }
        public LowerLocalisationEnum RightLeg { get; private set; }
        public LowerLocalisationEnum LeftLeg { get; private set; }
        #endregion

        #region Характеристика ранения
        public InjuryMechanismEnum Mechanism { get; private set; } // Механическая, мвр, сочетанная 
        public ThermoEnum Thermo { get; private set; } // Ожог и отморожение по 4 степени
        public InjuryTypeEnum InjuryType { get; private set; } // Взрывное, огнестрельное, колотое
        public ThroughEnum Through { get; private set; } // Сквозное, слепое
        public DepthEnum Depth { get; private set; } // Проникающее, мягких тканей
        
        // Особенности
        public int Dislocation { get; private set; }
        public int Amputation { get; private set; }
        public int LargeVessels { get; private set; }
        public int Fracture { get; private set; }
        public int PneumoThorax { get; private set; }
        #endregion

        #region Head
        // Head Front

        public void AddHeadFront()
        {
            Head |= HeadLocalisationEnum.FrontHead;
        }
        public void DeleteHeadFront()
        {
            Head ^= HeadLocalisationEnum.FrontHead;
        }
        // Head Back
        public void AddHeadBack()
        {
            Head |= HeadLocalisationEnum.FrontHead;
        }
        public void DeleteHeadBack()
        {
            Head ^= HeadLocalisationEnum.FrontHead;
        }
        public void ResetHead()
        {
            Head = 0;
        }

        #endregion

        #region Neck
        // Neck Front
        public void AddNeckFront()
        {
            Neck |= NeckLocalisationEnum.FrontNeck;
        }
        public void DeleteNeckFront()
        {
            Neck ^= NeckLocalisationEnum.FrontNeck;
        }
        // Neck Back
        public void AddNeckBack()
        {
            Neck |= NeckLocalisationEnum.BackNeck;
        }
        public void DeleteNeckBack()
        {
            Neck ^= NeckLocalisationEnum.FrontNeck;
        }

        public void ResetNeck()
        {
            Neck = 0;
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

        #region Thorax
        // ADD
        public void AddThoraxFrontRight()
        {
            Thorax |= ThoraxLocalisationEnum.FrontRight;
        }
        public void AddThoraxFrontLeft()
        {
            Thorax |= ThoraxLocalisationEnum.FrontLeft;
        }
        public void AddThoraxBackRight()
        {
            Thorax |= ThoraxLocalisationEnum.BackRight;
        }
        public void AddThoraxBackLeft()
        {
            Thorax |= ThoraxLocalisationEnum.BackLeft;
        }

        // DELETE
        public void DeleteThoraxFrontRight()
        {
            Thorax ^= ThoraxLocalisationEnum.FrontRight;
        }
        public void DeleteThoraxFrontLeft()
        {
            Thorax ^= ThoraxLocalisationEnum.FrontLeft;
        }
        public void DeleteThoraxBackRight()
        {
            Thorax ^= ThoraxLocalisationEnum.BackRight;
        }
        public void DeleteThoraxBackLeft()
        {
            Thorax ^= ThoraxLocalisationEnum.BackLeft;
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

        #region Abdomen 
        public void AddAbdomenFrontRight()
        {
            Abdomen |= AbdomenLocalisationEnum.FrontRight;
        }
        public void AddAbdomenFrontLeft()
        {
            Abdomen |= AbdomenLocalisationEnum.FrontLeft;
        }
        public void AddAbdomenBackRight()
        {
            Abdomen |= AbdomenLocalisationEnum.BackRight;
        }
        public void AddAbdomenBackLeft()
        {
            Abdomen |= AbdomenLocalisationEnum.BackLeft;
        }

        public void DeleteAbdomenFrontRight()
        {
            Abdomen ^= AbdomenLocalisationEnum.FrontRight;
        }
        public void DeleteAbdomenFrontLeft()
        {
            Abdomen ^= AbdomenLocalisationEnum.FrontLeft;
        }
        public void DeleteAbdomenBackRight()
        {
            Abdomen ^= AbdomenLocalisationEnum.BackRight;
        }
        public void DeleteAbdomenBackLeft()
        {
            Abdomen ^= AbdomenLocalisationEnum.BackLeft;
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

        #region Mechanism
        public void MakeMechanismMechanic()
        {
            Mechanism = InjuryMechanismEnum.Mechanic;
        }
        public void MakeMechanismExplosion()
        {
            Mechanism = InjuryMechanismEnum.Explosion;
        }
        public void MakeMechanismCombined()
        {
            Mechanism = InjuryMechanismEnum.Combined;
        }
        #endregion

        #region Thermo
        public void MakeThermoBurn1()
        {
            Thermo ^= (ThermoEnum.Burn2 | ThermoEnum.Burn3 | ThermoEnum.Burn4);
            Thermo |= ThermoEnum.Burn1;
        }
        public void MakeThermoBurn2()
        {
            Thermo ^= (ThermoEnum.Burn1 | ThermoEnum.Burn3 | ThermoEnum.Burn4);
            Thermo |= ThermoEnum.Burn2;
        }
        public void MakeThermoBurn3()
        {
            Thermo ^= (ThermoEnum.Burn2 | ThermoEnum.Burn1 | ThermoEnum.Burn4);
            Thermo |= ThermoEnum.Burn3;
        }
        public void MakeThermoBurn4()
        {
            Thermo ^= (ThermoEnum.Burn2 | ThermoEnum.Burn3 | ThermoEnum.Burn1);
            Thermo |= ThermoEnum.Burn4;
        }
        public void MakeThermoFrostbite1()
        {
            Thermo ^= (ThermoEnum.Frostbite2 | ThermoEnum.Frostbite3 | ThermoEnum.Frostbite4);
            Thermo |= ThermoEnum.Frostbite1;
        }
        public void MakeThermoFrostbite2()
        {
            Thermo ^= (ThermoEnum.Frostbite1 | ThermoEnum.Frostbite3 | ThermoEnum.Frostbite4);
            Thermo |= ThermoEnum.Frostbite2;
        }
        public void MakeThermoFrostbite3()
        {
            Thermo ^= (ThermoEnum.Frostbite2 | ThermoEnum.Frostbite1 | ThermoEnum.Frostbite4);
            Thermo |= ThermoEnum.Frostbite3;
        }
        public void MakeThermoFrostbite4()
        {
            Thermo ^= (ThermoEnum.Frostbite2 | ThermoEnum.Frostbite3 | ThermoEnum.Frostbite1);
            Thermo |= ThermoEnum.Frostbite4;
        }
        #endregion

        #region Type
        public void MakeInjuryTypeExplosion()
        {
            InjuryType = InjuryTypeEnum.Explosion;
        }
        public void MakeInjuryTypeGunshotBullet()
        {
            InjuryType = InjuryTypeEnum.GunshotBullet;

        }
        public void MakeInjuryTypeGunshotFragile()
        {
            InjuryType = InjuryTypeEnum.GunshotFragile;

        }
        public void MakeInjuryTypeStabbed()
        {
            InjuryType = InjuryTypeEnum.Stabbed;

        }
        #endregion

        #region Through
        // Сквозное, слепое
        public void MakeThroughThrough()
        {
            Through = ThroughEnum.Through;
        }
        public void MakeThroughBlind()
        {
            Through = ThroughEnum.Blind;
        }
        #endregion

        #region Depth
        public void MakeDepthPenetration()
        {
            Depth = DepthEnum.Penetration;
        }
        public void MakeDepthSoftTissue()
        {
            Depth = DepthEnum.Penetration;
        }
        #endregion
    }
}
