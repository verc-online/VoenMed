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
        public SpineDamageModel Spine { get; private set; } = new();
        
        public UpperDamageModel RightUpper { get; private set; } = new();
        public UpperDamageModel LeftUpper { get; private set; } = new();
        public LowerDamageModel RightLeg { get; private set; } = new();
        public LowerDamageModel LeftLeg { get; private set; } = new();
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


    }
}
