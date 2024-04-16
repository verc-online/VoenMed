using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Models.InjuryModels
{
    abstract public class DamageBaseModel
    {
        #region Характеристика ранения
        public DamageCharacterEnum Character { get; private set; }
        public ThermoDamageEnum Thermo { get; private set; } // Ожог и отморожение по 4 степени
        #endregion



        #region Character

        // Характер раневого канала
        public void MakeWoundCanalThrough()
        {
            Character |= DamageCharacterEnum.Through;
            if (Character.HasFlag(DamageCharacterEnum.Tangent))
            {
                Character ^= DamageCharacterEnum.Tangent;
            }
            if (Character.HasFlag(DamageCharacterEnum.Blind))
            {

                Character ^= DamageCharacterEnum.Blind;
            }
        }
        public void MakeWoundCanalBlind()
        {
            Character |= DamageCharacterEnum.Blind; 
            if(Character.HasFlag(DamageCharacterEnum.Through))
            {
                Character ^= DamageCharacterEnum.Through;
            }
            if(Character.HasFlag(DamageCharacterEnum.Tangent))
            {

                Character ^= DamageCharacterEnum.Tangent;
            }

        }
        public void MakeWoundCanalTangent()
        {
            Character |= DamageCharacterEnum.Tangent;
            if (Character.HasFlag(DamageCharacterEnum.Through))
            {
                Character ^= DamageCharacterEnum.Through;
            }
            if (Character.HasFlag(DamageCharacterEnum.Blind))
            {

                Character ^= DamageCharacterEnum.Blind;
            }

        }
        public void MakeTraumaOpen()
        {
            Character |= DamageCharacterEnum.TraumaOpened;
            if (Character.HasFlag(DamageCharacterEnum.Through))
            {
                Character ^= DamageCharacterEnum.Through;
            }
            if (Character.HasFlag(DamageCharacterEnum.Blind))
            {

                Character ^= DamageCharacterEnum.Blind;
            }
            if (Character.HasFlag(DamageCharacterEnum.Tangent))
            {

                Character ^= DamageCharacterEnum.Tangent;
            }
            if (Character.HasFlag(DamageCharacterEnum.TraumaClosed))
            {

                Character ^= DamageCharacterEnum.TraumaClosed;
            }
        }
        public void MakeTraumaClosed()
        {
            Character |= DamageCharacterEnum.TraumaClosed;
            if (Character.HasFlag(DamageCharacterEnum.Through))
            {
                Character ^= DamageCharacterEnum.Through;
            }
            if (Character.HasFlag(DamageCharacterEnum.Blind))
            {

                Character ^= DamageCharacterEnum.Blind;
            }
            if (Character.HasFlag(DamageCharacterEnum.Tangent))
            {

                Character ^= DamageCharacterEnum.Tangent;
            }
            if (Character.HasFlag(DamageCharacterEnum.TraumaOpened))
            {

                Character ^= DamageCharacterEnum.TraumaOpened;
            }
        }

        // Отношение к полости
        public void MakePenetrated()
        {
            Character |= DamageCharacterEnum.Penetration;
            if (Character.HasFlag(DamageCharacterEnum.SoftTissue))
            {
                Character ^= DamageCharacterEnum.SoftTissue;
            }
        }
        public void MakeNotPenetrated()
        {
            Character |= DamageCharacterEnum.SoftTissue;
            if (Character.HasFlag(DamageCharacterEnum.Penetration))
            {
                Character ^= DamageCharacterEnum.Penetration;
            }
        }
        #endregion


        #region Thermo
        public void MakeThermoBurn1()
        {
            Thermo = ThermoDamageEnum.Burn1;
        }
        public void MakeThermoBurn2()
        {
            Thermo = ThermoDamageEnum.Burn2;
        }
        public void MakeThermoBurn3()
        {
            Thermo = ThermoDamageEnum.Burn3;
        }
        public void MakeThermoBurn4()
        {
            Thermo = ThermoDamageEnum.Burn4;
        }
        public void MakeThermoFrostbite1()
        {
            Thermo = ThermoDamageEnum.Frostbite1;
        }
        public void MakeThermoFrostbite2()
        {
            Thermo = ThermoDamageEnum.Frostbite2;
        }
        public void MakeThermoFrostbite3()
        {
            Thermo = ThermoDamageEnum.Frostbite3;
        }
        public void MakeThermoFrostbite4()
        {
            Thermo = ThermoDamageEnum.Frostbite4;
        }
        #endregion
        
       
    }
}
