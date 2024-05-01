using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoenMedLibrary.Models.InjuryModels;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models
{
    public class InjuryStatusLocalisModel
    {
        public int Id { get; set; }
        public int Form100Id { get; set; }

        // Этиололгия: ранения или травма
        public DamageEthiologyEnum Ethiology { get; set; }

        #region Локализация повреждения
        public HeadDamageModel Head { get; set; } = new();
        public NeckDamageModel Neck { get;  set; } = new();
        public ThoraxDamageModel Thorax { get;  set; } = new();
        public AbdomenDamageModel Abdomen { get;  set; } = new();
        public PelvisDamageModel Pelvis { get;  set; } = new();
        public SpineDamageModel Spine { get;  set; } = new();

        public UpperDamageModel RightUpper { get;  set; } = new();
        public UpperDamageModel LeftUpper { get;  set; } = new();
        public LowerDamageModel RightLower { get;  set; } = new();
        public LowerDamageModel LeftLower { get;  set; } = new();

        public string Diagnosis { get; set; }
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

        public void GetAllStatusLocalises()
        {
            string output = "Этиология ранения - ";
            output += Ethiology > 0 ? Ethiology.GetDescriptionsAsText(): "";
            output += Head.Localisation > 0 ? Head.StatusLocalis : "";
            output += Neck.Localisation > 0 ? Neck.StatusLocalis : "";
            output += Thorax.Localisation > 0 ? Thorax.StatusLocalis : "";
            output += Abdomen.Localisation > 0 ? Abdomen.StatusLocalis : "";
            output += Pelvis.Localisation > 0 ? Pelvis.StatusLocalis : "";
            output += Spine.Localisation > 0 ? Spine.StatusLocalis : "";
            output += RightUpper.Localisation > 0 ? RightUpper.StatusLocalis : "";
            output += LeftUpper.Localisation > 0 ? LeftUpper.StatusLocalis : "";
            output += RightLower.Localisation > 0 ? RightLower.StatusLocalis : "";
            output += LeftLower.Localisation > 0 ? LeftLower.StatusLocalis : "";

            Diagnosis = output;
        }

    }
}
