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
        public HeadLocalisation Head { get; set; }
        public NeckLocalisation Neck { get; set; }
        public UpperLocalisation RightUpper { get; set; }
        public UpperLocalisation LeftUpper { get; set; }
        public ThoraxLocalisation Thorax { get; set; }
        public AbdomenLocalisation Abdomen { get; set; }
        public LowerLocalisation RightLeg { get; set; }
        public LowerLocalisation LeftLeg { get; set; }
        public int Mechanic { get; set; }
        public int MVR { get; set; }
        public int Thermo { get; set; }
        public int Combined { get; set; }
        public int Explosion { get; set; }
        public int GunShot { get; set; }
        public int Stabbed { get; set; }
        public int Thru { get; set; }
        public int Blind { get; set; }
        public int Penetration { get; set; }
        public int SoftTissue { get; set; }
        public int Dislocation { get; set; }
        public int Amputation { get; set; }
        public int LargeVessels { get; set; }
        public int Fracture { get; set; }
        public int PneumoThorax { get; set; }

        #region Head
        // Head Front

        public void AddHeadFront()
        {
            Head |= HeadLocalisation.FrontHead;
        }
        public void DeleteHeadFront()
        {
            Head ^= HeadLocalisation.FrontHead;
        }
        // Head Back
        public void AddHeadBack()
        {
            Head |= HeadLocalisation.FrontHead;
        }
        public void DeleteHeadBack()
        {
            Head ^= HeadLocalisation.FrontHead;
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
            Neck |= NeckLocalisation.FrontNeck;
        }
        public void DeleteNeckFront()
        {
            Neck ^= NeckLocalisation.FrontNeck;
        }
        // Neck Back
        public void AddNeckBack()
        {
            Neck |= NeckLocalisation.FrontNeck;
        }
        public void DeleteNeckBack()
        {
            Neck ^= NeckLocalisation.FrontNeck;
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
            RightUpper |= UpperLocalisation.ShoulderFront;
        }
        public void AddRightUpperForearmFront()
        {
            RightUpper |= UpperLocalisation.ForearmFront;
        }
        public void AddRightUpperWristFront()
        {
            RightUpper |= UpperLocalisation.WristFront;
        }
        public void AddRightUpperShoulderTourniquiet()
        {
            RightUpper |= UpperLocalisation.ShoulderTourniquet;
        }
        public void AddRightUpperForearmTourniquet()
        {
            RightUpper |= UpperLocalisation.ForearmTourniquet;
        }
        public void AddRightUpperShoulderBack()
        {
            RightUpper |= UpperLocalisation.ShoulderBack;
        }
        public void AddRightUpperForearmBack()
        {
            RightUpper |= UpperLocalisation.ForearmBack;
        }
        public void AddRightUpperWristBack()
        {
            RightUpper |= UpperLocalisation.WristBack;
        }
        
        // DELETE
        public void DeleteRightUpperShoulderFront()
        {
            RightUpper ^= UpperLocalisation.ShoulderFront;
        }
        public void DeleteRightUpperForearmFront()
        {
            RightUpper ^= UpperLocalisation.ForearmFront;
        }
        public void DeleteRightUpperWristFront()
        {
            RightUpper ^= UpperLocalisation.WristFront;
        }
        public void DeleteRightUpperShoulderTourniquiet()
        {
            RightUpper ^= UpperLocalisation.ShoulderTourniquet;
        }
        public void DeleteRightUpperForearmTourniquet()
        {
            RightUpper ^= UpperLocalisation.ForearmTourniquet;
        }
        public void DeleteRightUpperShoulderBack()
        {
            RightUpper ^= UpperLocalisation.ShoulderBack;
        }
        public void DeleteRightUpperForearmBack()
        {
            RightUpper ^= UpperLocalisation.ForearmBack;
        }
        public void DeleteRightUpperWristBack()
        {
            RightUpper ^= UpperLocalisation.WristBack;
        }
        #endregion

        #region Thorax
        // ADD
        public void AddThoraxFrontRight()
        {
            Thorax |= ThoraxLocalisation.FrontRight;
        }
        public void AddThoraxFrontLeft()
        {
            Thorax |= ThoraxLocalisation.FrontLeft;
        }
        public void AddThoraxBackRight()
        {
            Thorax |= ThoraxLocalisation.BackRight;
        }
        public void AddThoraxBackLeft()
        {
            Thorax |= ThoraxLocalisation.BackLeft;
        }

        // DELETE
        public void DeleteThoraxFrontRight()
        {
            Thorax ^= ThoraxLocalisation.FrontRight;
        }
        public void DeleteThoraxFrontLeft()
        {
            Thorax ^= ThoraxLocalisation.FrontLeft;
        }
        public void DeleteThoraxBackRight()
        {
            Thorax ^= ThoraxLocalisation.BackRight;
        }
        public void DeleteThoraxBackLeft()
        {
            Thorax ^= ThoraxLocalisation.BackLeft;
        }

        #endregion

        #region LeftUpper
        public void AddLeftUpperShoulderFront()
        {
            LeftUpper |= UpperLocalisation.ShoulderFront;
        }
        public void AddLeftUpperForearmFront()
        {
            LeftUpper |= UpperLocalisation.ForearmFront;
        }
        public void AddLeftUpperWristFront()
        {
            LeftUpper |= UpperLocalisation.WristFront;
        }
        public void AddLeftUpperShoulderTourniquiet()
        {
            LeftUpper |= UpperLocalisation.ShoulderTourniquet;
        }
        public void AddLeftUpperForearmTourniquet()
        {
            LeftUpper |= UpperLocalisation.ForearmTourniquet;
        }
        public void AddLeftUpperShoulderBack()
        {
            LeftUpper |= UpperLocalisation.ShoulderBack;
        }
        public void AddLeftUpperForearmBack()
        {
            LeftUpper |= UpperLocalisation.ForearmBack;
        }
        public void AddLeftUpperWristBack()
        {
            LeftUpper |= UpperLocalisation.WristBack;
        }

        public void DeleteLeftUpperShoulderFront()
        {
            LeftUpper ^= UpperLocalisation.ShoulderFront;
        }
        public void DeleteLeftUpperForearmFront()
        {
            LeftUpper ^= UpperLocalisation.ForearmFront;
        }
        public void DeleteLeftUpperWristFront()
        {
            LeftUpper ^= UpperLocalisation.WristFront;
        }
        public void DeleteLeftUpperShoulderTourniquiet()
        {
            LeftUpper ^= UpperLocalisation.ShoulderTourniquet;
        }
        public void DeleteLeftUpperForearmTourniquet()
        {
            LeftUpper ^= UpperLocalisation.ForearmTourniquet;
        }
        public void DeleteLeftUpperShoulderBack()
        {
            LeftUpper ^= UpperLocalisation.ShoulderBack;
        }
        public void DeleteLeftUpperForearmBack()
        {
            LeftUpper ^= UpperLocalisation.ForearmBack;
        }
        public void DeleteLeftUpperWristBack()
        {
            LeftUpper ^= UpperLocalisation.WristBack;
        }
        #endregion

        #region Abdomen 
        public void AddAbdomenFrontRight()
        {
            Abdomen |= AbdomenLocalisation.FrontRight;
        }
        public void AddAbdomenFrontLeft()
        {
            Abdomen |= AbdomenLocalisation.FrontLeft;
        }
        public void AddAbdomenBackRight()
        {
            Abdomen |= AbdomenLocalisation.BackRight;
        }
        public void AddAbdomenBackLeft()
        {
            Abdomen |= AbdomenLocalisation.BackLeft;
        }

        public void DeleteAbdomenFrontRight()
        {
            Abdomen ^= AbdomenLocalisation.FrontRight;
        }
        public void DeleteAbdomenFrontLeft()
        {
            Abdomen ^= AbdomenLocalisation.FrontLeft;
        }
        public void DeleteAbdomenBackRight()
        {
            Abdomen ^= AbdomenLocalisation.BackRight;
        }
        public void DeleteAbdomenBackLeft()
        {
            Abdomen ^= AbdomenLocalisation.BackLeft;
        }
        #endregion

        #region RightLeg

        // ADD
        public void AddRightFrontHip()
        {
            RightLeg |= LowerLocalisation.HipFront;
        }
        public void AddRightFrontShin()
        {
            RightLeg |= LowerLocalisation.ShinFront;
        }
        public void AddRightFrontFoot()
        {
            RightLeg |= LowerLocalisation.FootFront;
        }
        public void AddRightFrontHipTourniquiet()
        {
            RightLeg |= LowerLocalisation.HupTourniquiet;
        }
        public void AddRightFrontShinTourniquiet()
        {
            RightLeg |= LowerLocalisation.ShinTourniquiet;
        }
        public void AddRightBackHip()
        {
            RightLeg |= LowerLocalisation.HipBack;
        }
        public void AddRightBackShin()
        {
            RightLeg |= LowerLocalisation.ShinBack;
        }
        public void AddRightBackFoot()
        {
            RightLeg |= LowerLocalisation.FootBack;
        }
        // DELETE
        public void DeleteRightFrontHip()
        {
            RightLeg ^= LowerLocalisation.HipFront;
        }
        public void DeleteRightFrontShin()
        {
            RightLeg ^= LowerLocalisation.ShinFront;
        }
        public void DeleteRightFrontFoot()
        {
            RightLeg ^= LowerLocalisation.FootFront;
        }
        public void DeleteRightFrontHipTourniquiet()
        {
            RightLeg ^= LowerLocalisation.HupTourniquiet;
        }
        public void DeleteRightFrontShinTourniquiet()
        {
            RightLeg ^= LowerLocalisation.ShinTourniquiet;
        }
        public void DeleteRightBackHip()
        {
            RightLeg ^= LowerLocalisation.HipBack;
        }
        public void DeleteRightBackShin()
        {
            RightLeg ^= LowerLocalisation.ShinBack;
        }
        public void DeleteRightBackFoot()
        {
            RightLeg ^= LowerLocalisation.FootBack;
        }
        #endregion

        #region LeftLeg

        // ADD
        public void AddLeftFrontHip()
        {
            LeftLeg |= LowerLocalisation.HipFront;
        }
        public void AddLeftFrontShin()
        {
            LeftLeg |= LowerLocalisation.ShinFront;
        }
        public void AddLeftFrontFoot()
        {
            LeftLeg |= LowerLocalisation.FootFront;
        }
        public void AddLeftFrontHipTourniquiet()
        {
            LeftLeg |= LowerLocalisation.HupTourniquiet;
        }
        public void AddLeftFrontShinTourniquiet()
        {
            LeftLeg |= LowerLocalisation.ShinTourniquiet;
        }
        public void AddLeftBackHip()
        {
            LeftLeg |= LowerLocalisation.HipBack;
        }
        public void AddLeftBackShin()
        {
            LeftLeg |= LowerLocalisation.ShinBack;
        }
        public void AddLeftBackFoot()
        {
            LeftLeg |= LowerLocalisation.FootBack;
        }
        // DELETE
        public void DeleteLeftFrontHip()
        {
            LeftLeg ^= LowerLocalisation.HipFront;
        }
        public void DeleteLeftFrontShin()
        {
            LeftLeg ^= LowerLocalisation.ShinFront;
        }
        public void DeleteLeftFrontFoot()
        {
            LeftLeg ^= LowerLocalisation.FootFront;
        }
        public void DeleteLeftFrontHipTourniquiet()
        {
            LeftLeg ^= LowerLocalisation.HupTourniquiet;
        }
        public void DeleteLeftFrontShinTourniquiet()
        {
            LeftLeg ^= LowerLocalisation.ShinTourniquiet;
        }
        public void DeleteLeftBackHip()
        {
            LeftLeg ^= LowerLocalisation.HipBack;
        }
        public void DeleteLeftBackShin()
        {
            LeftLeg ^= LowerLocalisation.ShinBack;
        }
        public void DeleteLeftBackFoot()
        {
            LeftLeg ^= LowerLocalisation.FootBack;
        }
        #endregion
    }
}
