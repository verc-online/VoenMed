using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoenMedLibrary.DataAccess;
using VoenMedLibrary.Models;
using VoenMedLibrary.Models.InjuryModels;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMedLibrary.Data
{
    public class Form100Data
    {
        private readonly Form100Model form100Model;

        public Form100Data(Form100Model model)
        {
            this.form100Model = model;
        }

        public void DeleteForm100()
        {

            string sql = "DELETE FROM Form100s WHERE Id = @Id";
            Dictionary<string, object> parameters = new() { { "@Id", form100Model.Id } };
            SqliteDataAccess.SaveData(sql, parameters);

            PersonIdModel personIdModel = new PersonIdModel();
        }

        public void LoadFullModelFromDatabase()
        {
            string sql;
            Dictionary<string, object> parameters = new() { { "@Form100Id", form100Model.Id } };

            sql = "SELECT * FROM HelpProvided WHERE Form100Id = @Form100Id";
            form100Model.HelpProvided = SqliteDataAccess.LoadData<HelpProvidedModel>(sql, parameters).First();


            sql = "SELECT * FROM DrugsProvided WHERE Form100Id = @Form100Id";
            form100Model.HelpProvided.DrugsProvided = SqliteDataAccess.LoadData<ProvidedDrugModel>(sql, parameters);

            sql = "SELECT * FROM Conditions WHERE Form100Id = @Form100Id";
            form100Model.Condition = SqliteDataAccess.LoadData<ConditionModel>(sql, parameters).First();

            sql = "SELECT * FROM GlasgowComaScales WHERE Form100Id = @Form100Id";
            form100Model.Condition.GlasgowComaScale = SqliteDataAccess.LoadData<GlasgowComaScaleModel>(sql, parameters).First();

            sql = "SELECT * FROM Breathings WHERE Form100Id = @Form100Id";
            form100Model.Condition.Breathing = SqliteDataAccess.LoadData<BreathingModel>(sql, parameters).First();

            sql = "SELECT * FROM Hearts WHERE Form100Id = @Form100Id";
            form100Model.Condition.Heart = SqliteDataAccess.LoadData<HeartModel>(sql, parameters).First();

            sql = "SELECT * FROM Injuries WHERE Form100Id = @Form100Id AND Area = @Area";
            parameters.Add("@Area", AreaEnum.Head);
            form100Model.InjuryStatusLocalis.Head = SqliteDataAccess.LoadData<HeadDamageModel>(sql, parameters).FirstOrDefault(new HeadDamageModel());

            sql = "SELECT * FROM Injuries WHERE Form100Id = @Form100Id AND Area = @Area";
            parameters["@Area"] = AreaEnum.Neck;
            form100Model.InjuryStatusLocalis.Neck = SqliteDataAccess.LoadData<NeckDamageModel>(sql, parameters).FirstOrDefault(new NeckDamageModel());

            sql = "SELECT * FROM Injuries WHERE Form100Id = @Form100Id AND Area = @Area";
            parameters["@Area"] = AreaEnum.Thorax;
            form100Model.InjuryStatusLocalis.Thorax = SqliteDataAccess.LoadData<ThoraxDamageModel>(sql, parameters).FirstOrDefault(new ThoraxDamageModel());

            sql = "SELECT * FROM Injuries WHERE Form100Id = @Form100Id AND Area = @Area";
            parameters["@Area"] = AreaEnum.Abdomen;
            form100Model.InjuryStatusLocalis.Abdomen = SqliteDataAccess.LoadData<AbdomenDamageModel>(sql, parameters).FirstOrDefault(new AbdomenDamageModel());

            sql = "SELECT * FROM Injuries WHERE Form100Id = @Form100Id AND Area = @Area";
            parameters["@Area"] = AreaEnum.Pelvis;
            form100Model.InjuryStatusLocalis.Pelvis = SqliteDataAccess.LoadData<PelvisDamageModel>(sql, parameters).FirstOrDefault(new PelvisDamageModel());

            sql = "SELECT * FROM Injuries WHERE Form100Id = @Form100Id AND Area = @Area";
            parameters["@Area"] = AreaEnum.Spine;
            form100Model.InjuryStatusLocalis.Spine = SqliteDataAccess.LoadData<SpineDamageModel>(sql, parameters).FirstOrDefault(new SpineDamageModel());

            sql = "SELECT * FROM Injuries WHERE Form100Id = @Form100Id AND Area = @Area";
            parameters["@Area"] = AreaEnum.RightUpper;
            form100Model.InjuryStatusLocalis.RightUpper = SqliteDataAccess.LoadData<UpperDamageModel>(sql, parameters).FirstOrDefault(new UpperDamageModel());

            sql = "SELECT * FROM Injuries WHERE Form100Id = @Form100Id AND Area = @Area";
            parameters["@Area"] = AreaEnum.LeftUpper;
            form100Model.InjuryStatusLocalis.LeftUpper = SqliteDataAccess.LoadData<UpperDamageModel>(sql, parameters).FirstOrDefault(new UpperDamageModel());


            sql = "SELECT * FROM Injuries WHERE Form100Id = @Form100Id AND Area = @Area";
            parameters["@Area"] = AreaEnum.RightLower;
            form100Model.InjuryStatusLocalis.RightLower = SqliteDataAccess.LoadData<LowerDamageModel>(sql, parameters).FirstOrDefault(new LowerDamageModel());

            sql = "SELECT * FROM Injuries WHERE Form100Id = @Form100Id AND Area = @Area";
            parameters["@Area"] = AreaEnum.LeftLower;
            form100Model.InjuryStatusLocalis.LeftLower = SqliteDataAccess.LoadData<LowerDamageModel>(sql, parameters).FirstOrDefault(new LowerDamageModel());


        }

        public string SaveToDatabase()
        {
            string output = "";
            string sql;
            int id;
            Dictionary<string, object> parameters;

            try
            {
                // Persons

                 sql = "insert into Persons (LastName, FirstName, SecondName, BirthDate, MilitaryUnit, MilitaryId, Duty, Rank, RankTitle) " +
                                    "values (@LastName, @FirstName, @SecondName, @BirthDate, @MilitaryUnit, @MilitaryId, @Duty, @Rank, @RankTitle);";
                parameters = new Dictionary<string, object>()
            {
                {"@LastName", form100Model.LastName },
                {"@FirstName", form100Model.FirstName },
                {"@SecondName", form100Model.SecondName },
                {"@BirthDate", form100Model.BirthDate },
                {"@MilitaryUnit", form100Model.MilitaryUnit },
                {"@MilitaryId", form100Model.MilitaryId },
                {"@Duty", form100Model.Duty },
                {"@Rank", form100Model.Rank },
                {"@RankTitle", form100Model.RankTitle },
            };

                SqliteDataAccess.SaveData(sql, parameters);

                sql = "select Id from Persons where LastName = @LastName";

                id = SqliteDataAccess.LoadData<PersonIdModel>(sql, new Dictionary<string, object> { { "LastName", form100Model.LastName } }).Last().Id;

            }
            catch (Exception er)
            {
                return "Persons " + er.Message;
            }

            try
            {
                //Form100s
                sql = "insert into Form100s (IssuedBy, Ethiology, IssuedWhen, PersonId, WithOutFirstAid, Reason, DiseaseTime, EvacOrder, EvacTransport, EvacPosition, EvacWay, EvacAddress, EvacTime, SpecialMarks, LethalTime, Diagnosis, Doc) " +
                                "values (@IssuedBy, @Ethiology, @IssuedWhen, @PersonId, @WithOutFirstAid, @Reason, @DiseaseTime, @EvacOrder, @EvacTransport, @EvacPosition, @EvacWay, @EvacAddress, @EvacTime, @SpecialMarks, @LethalTime, @Diagnosis, @Doc);";

                parameters = new Dictionary<string, object>()
            {
                {"@IssuedBy", form100Model.IssuedBy },
                {"@Ethiology", form100Model.InjuryStatusLocalis.Ethiology },
                {"@IssuedWhen", form100Model.IssuedWhen },
                {"@PersonId", id },
                {"@WithOutFirstAid", form100Model.WithoutFirstAid },
                {"@Reason", form100Model.Reason },
                {"@DiseaseTime", form100Model.DiseaseTime },
                {"@EvacOrder", form100Model.EvacuationOrder },
                {"@EvacTransport", form100Model.EvacuationTransport },
                {"@EvacPosition", form100Model.EvacuationPosition },
                {"@EvacWay", form100Model.EvacuationWay },
                {"@EvacAddress", form100Model.EvacAddress },
                {"@EvacTime", form100Model.EvacTime },
                {"@SpecialMarks", form100Model.Special },
                {"@LethalTime", form100Model.HelpProvided.LethalDateTime },
                {"@Diagnosis", form100Model.InjuryStatusLocalis.Diagnosis },
                {"@Doc", form100Model.Doc },
            };
                SqliteDataAccess.SaveData(sql, parameters);

            }
            catch (Exception er)
            {
                return "Form100s " + er.Message;
            }

            try
            {
                // HelpProvided
                sql = "select Id from Form100s where PersonId = @PersonId";

                id = SqliteDataAccess.LoadData<Form100Model>(sql, new Dictionary<string, object> { { "PersonId", id } }).Last().Id;

                sql = "insert into HelpProvided (Form100Id, WayStopBleeding, TimeTourniquetApplied, DecompressionOfThePleuralCavity, DrainageOfThePleuralCavity, Immobilization, NaCl, NaHC03, Glucose5, Er, Szp, IntensiveCareMeasures, IntensiveCareMeasuresTimeSpent, LethalDateTime, HelpProvidedSummary) " +
                                        "values (@Form100Id, @WayStopBleeding, @TimeTourniquetApplied, @DecompressionOfThePleuralCavity, @DrainageOfThePleuralCavity, @Immobilization, @NaCl, @NaHC03, @Glucose5, @Er, @Szp, @IntensiveCareMeasures, @IntensiveCareMeasuresTimeSpent, @LethalDateTime, @HelpProvidedSummary);";
                parameters = new Dictionary<string, object>()
            {
                {"@Form100Id", id },
                {"@WayStopBleeding", form100Model.HelpProvided.WayStopBleeding },
                {"@TimeTourniquetApplied", form100Model.HelpProvided.TimeTourniquetApplied },
                {"@DecompressionOfThePleuralCavity", form100Model.HelpProvided.DecompressionOfThePleuralCavity },
                {"@DrainageOfThePleuralCavity", form100Model.HelpProvided.DrainageOfThePleuralCavity },
                {"@Immobilization", form100Model.HelpProvided.Immobilization },
                {"@NaCl", form100Model.HelpProvided.NaCl },
                {"@NaHC03", form100Model.HelpProvided.NaHC03 },
                {"@Glucose5", form100Model.HelpProvided.Glucose5 },
                {"@Er", form100Model.HelpProvided.Er },
                {"@Szp", form100Model.HelpProvided.Szp },
                {"@IntensiveCareMeasures", form100Model.HelpProvided.IntensiveCareMeasures },
                {"@IntensiveCareMeasuresTimeSpent", form100Model.HelpProvided.IntensiveCareMeasuresTimeSpent },
                {"@LethalDateTime", form100Model.HelpProvided.LethalDateTime },
                {"@HelpProvidedSummary", form100Model.HelpProvided.HelpProvidedSummary },
            };
                SqliteDataAccess.SaveData(sql, parameters);
            }
            catch (Exception er)
            {
                return "HelpProvided " + er.Message;
            }

            try
            {
                // DrugsProvided
                sql = "insert into DrugsProvided (Form100Id, DrugId, Title, Dose, Measurement) " +
                                    "values (@Form100Id, @DrugId, @Title, @Dose, @Measurement);";
                foreach (var drug in form100Model.HelpProvided.DrugsProvided)
                {

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@DrugId", drug.DrugId },
                    {"@DrugId", drug.Title },
                    {"@DrugId", drug.Dose },
                    {"@DrugId", drug.Measurement },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
                // Conditions
                sql = "insert into Conditions (Form100Id, Temperature, DiuresPerFirstHour, AdditionalInfo, Complaints, Condition) " +
                                    "values (@Form100Id, @Temperature, @DiuresPerFirstHour, @AdditionalInfo, @Complaints, @Condition);";

                parameters = new Dictionary<string, object>()
            {
                {"@Form100Id", id },
                {"@Temperature", form100Model.Condition.Temperature },
                {"@DiuresPerFirstHour", form100Model.Condition.DiuresPerFirstHour },
                {"@AdditionalInfo", form100Model.Condition.AdditionalInfo },
                {"@Complaints", form100Model.Condition.Complaints },
                {"@Condition", form100Model.Condition.Condition },
            };
                SqliteDataAccess.SaveData(sql, parameters);
            }
            catch (Exception er)
            {
                return "DrugsProvided and Condition " + er.Message;
            }

            try
            {
                // GlasgowComaScales
                sql = "insert into GlasgowComaScales (Form100Id, Consience, EyeResponse, VerbalResponse, MotorResponse) " +
                                    "values (@Form100Id, @Consience, @EyeResponse, @VerbalResponse, @MotorResponse);";

                parameters = new Dictionary<string, object>()
            {
                {"@Form100Id", id },
                {"@Consience", form100Model.Condition.GlasgowComaScale.Consience },
                {"@EyeResponse", form100Model.Condition.GlasgowComaScale.EyeResponse },
                {"@VerbalResponse", form100Model.Condition.GlasgowComaScale.VerbalResponse },
                {"@MotorResponse", form100Model.Condition.GlasgowComaScale.MotorResponse },
            };
                SqliteDataAccess.SaveData(sql, parameters);
            }
            catch (Exception er) { return "GlasgowComaScales " + er.Message; }

            try
            {
                // Breathings
                sql = "insert into Breathings (Form100Id, BreathingSupport, BreathingRate, Saturation, FiO2, Summary) " +
                                    "values (@Form100Id, @BreathingSupport, @BreathingRate, @Saturation, @FiO2, @Summary);";

                parameters = new Dictionary<string, object>()
            {
                {"@Form100Id", id },
                {"@BreathingSupport", form100Model.Condition.Breathing.BreathingSupport },
                {"@BreathingRate", form100Model.Condition.Breathing.BreathingRate },
                {"@Saturation", form100Model.Condition.Breathing.Saturation },
                {"@FiO2", form100Model.Condition.Breathing.FiO2 },
                {"@Summary", form100Model.Condition.Breathing.Summary },
            };
                SqliteDataAccess.SaveData(sql, parameters);
            }
            catch (Exception er) { return "Breathings " + er.Message; }

            try
            {
                // Hearts
                sql = "insert into Hearts (Form100Id, Rate, SystolicArterialPressure, CapillaryTime, NoradrenalineDose, DopamineDose, DobutamineDose, AdrenalineDose) " +
                                    "values (@Form100Id, @Rate, @SystolicArterialPressure, @CapillaryTime, @NoradrenalineDose, @DopamineDose, @DobutamineDose, @AdrenalineDose);";

                parameters = new Dictionary<string, object>()
            {
                {"@Form100Id", id },
                {"@Rate", form100Model.Condition.Heart.Rate },
                {"@SystolicArterialPressure", form100Model.Condition.Heart.SystolicArterialPressure },
                {"@CapillaryTime", form100Model.Condition.Heart.CapillaryTime },
                {"@NoradrenalineDose", form100Model.Condition.Heart.NoradrenalineDose },
                {"@DopamineDose", form100Model.Condition.Heart.DopamineDose },
                {"@DobutamineDose", form100Model.Condition.Heart.DobutamineDose },
                {"@AdrenalineDose", form100Model.Condition.Heart.AdrenalineDose },
            };
                SqliteDataAccess.SaveData(sql, parameters);
            }
            catch (Exception er) { return "Hearts " + er.Message; }

            try
            {
                // Heads
                if (form100Model.InjuryStatusLocalis.Head.Localisation > 0)
                {
                    sql = "insert into Injuries (Form100Id, Area, Localisation, Damage, Fracture, StatusLocalis) " +
                                        "values (@Form100Id, @Area, @Localisation, @Damage, @Fracture, @StatusLocalis);";

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@Area", AreaEnum.Head },
                    {"@Localisation", form100Model.InjuryStatusLocalis.Head.Localisation },
                    {"@Damage", form100Model.InjuryStatusLocalis.Head.Damage },
                    {"@Fracture", form100Model.InjuryStatusLocalis.Head.Fracture },
                    {"@StatusLocalis", form100Model.InjuryStatusLocalis.Head.StatusLocalis },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
            }
            catch (Exception er)
            {
                return "Heads " + er.Message;
            }

            try
            {
                // Neck
                if (form100Model.InjuryStatusLocalis.Neck.Localisation > 0)
                {
                    sql = "insert into Injuries (Form100Id, Area, Localisation, Damage, StatusLocalis) " +
                                        "values (@Form100Id, @Area, @Localisation, @Damage, @StatusLocalis);";

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@Area", AreaEnum.Neck },
                    {"@Localisation", form100Model.InjuryStatusLocalis.Neck.Localisation },
                    {"@Damage", form100Model.InjuryStatusLocalis.Neck.Damage },
                    {"@StatusLocalis", form100Model.InjuryStatusLocalis.Neck.StatusLocalis },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
            }
            catch (Exception er) { return "Neck " + er.Message; }


            try
            {
                // Thorax
                if (form100Model.InjuryStatusLocalis.Thorax.Localisation > 0)
                {
                    sql = "insert into Injuries (Form100Id, Area, Localisation, Damage, Fracture, StatusLocalis) " +
                                        "values (@Form100Id, @Area, @Localisation, @Damage, @Fracture, @StatusLocalis);";

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@Area", AreaEnum.Thorax },
                    {"@Localisation", form100Model.InjuryStatusLocalis.Thorax.Localisation },
                    {"@Damage", form100Model.InjuryStatusLocalis.Thorax.Damage },
                    {"@Fracture", form100Model.InjuryStatusLocalis.Thorax.Fracture },
                    {"@StatusLocalis", form100Model.InjuryStatusLocalis.Thorax.StatusLocalis },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
            }
            catch (Exception er) { return "Thorax " + er.Message; }

            try
            {
                // Abdomen
                if (form100Model.InjuryStatusLocalis.Abdomen.Localisation > 0)
                {
                    sql = "insert into Injuries (Form100Id, Area, Localisation, Damage, StatusLocalis) " +
                                        "values (@Form100Id, @Area, @Localisation, @Damage, @StatusLocalis);";

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@Area", AreaEnum.Abdomen },
                    {"@Localisation", form100Model.InjuryStatusLocalis.Abdomen.Localisation },
                    {"@Damage", form100Model.InjuryStatusLocalis.Abdomen.Damage },
                    {"@StatusLocalis", form100Model.InjuryStatusLocalis.Abdomen.StatusLocalis },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
            }
            catch (Exception er) { return "Abdomen " + er.Message; }


            try
            {
                // Pelvis
                if (form100Model.InjuryStatusLocalis.Pelvis.Localisation > 0)
                {
                    sql = "insert into Injuries (Form100Id, Area, Localisation, Damage, Fracture, StatusLocalis) " +
                                        "values (@Form100Id, @Area, @Localisation, @Damage, @Fracture, @StatusLocalis);";

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@Area", AreaEnum.Pelvis },
                    {"@Localisation", form100Model.InjuryStatusLocalis.Pelvis.Localisation },
                    {"@Damage", form100Model.InjuryStatusLocalis.Pelvis.Damage },
                    {"@Fracture", form100Model.InjuryStatusLocalis.Pelvis.Fracture },
                    {"@StatusLocalis", form100Model.InjuryStatusLocalis.Pelvis.StatusLocalis },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
            }
            catch (Exception er) { return "Pelvis " + er.Message; }


            try
            {
                // Spine
                if (form100Model.InjuryStatusLocalis.Spine.Localisation > 0)
                {
                    sql = "insert into Injuries (Form100Id, Area, Localisation, Damage, Fracture, StatusLocalis) " +
                                        "values (@Form100Id, @Area, @Localisation, @Damage, @Fracture, @StatusLocalis);";

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@Area", AreaEnum.Spine },
                    {"@Localisation", form100Model.InjuryStatusLocalis.Spine.Localisation },
                    {"@Damage", form100Model.InjuryStatusLocalis.Spine.Damage },
                    {"@Fracture", form100Model.InjuryStatusLocalis.Spine.Fracture },
                    {"@StatusLocalis", form100Model.InjuryStatusLocalis.Spine.StatusLocalis },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
            }
            catch (Exception er) { return "Spine " + er.Message; }

            try
            {
                // RightUpper
                if (form100Model.InjuryStatusLocalis.Thorax.Localisation > 0)
                {
                    sql = "insert into Injuries (Form100Id, Area, Localisation, Damage, Fracture, Desctruction, StatusLocalis) " +
                                        "values (@Form100Id, @Area, @Localisation, @Damage, @Fracture, @Desctruction, @StatusLocalis);";

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@Area", AreaEnum.RightUpper },
                    {"@Localisation", form100Model.InjuryStatusLocalis.RightUpper.Localisation },
                    {"@Damage", form100Model.InjuryStatusLocalis.RightUpper.Damage },
                    {"@Fracture", form100Model.InjuryStatusLocalis.RightUpper.Fracture },
                    {"@Desctruction", form100Model.InjuryStatusLocalis.RightUpper.Desctruction },
                    {"@StatusLocalis", form100Model.InjuryStatusLocalis.RightUpper.StatusLocalis },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
            }
            catch (Exception er)
            {
                return "RightUpper " + er.Message;
            }

            try
            {
                // LeftUpper
                if (form100Model.InjuryStatusLocalis.LeftUpper.Localisation > 0)
                {
                    sql = "insert into Injuries (Form100Id, Area, Localisation, Damage, Fracture, Desctruction, StatusLocalis) " +
                                        "values (@Form100Id, @Area, @Localisation, @Damage, @Fracture, @Desctruction, @StatusLocalis);";

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@Area", AreaEnum.LeftUpper },
                    {"@Localisation", form100Model.InjuryStatusLocalis.LeftUpper.Localisation },
                    {"@Damage", form100Model.InjuryStatusLocalis.LeftUpper.Damage },
                    {"@Fracture", form100Model.InjuryStatusLocalis.LeftUpper.Fracture },
                    {"@Desctruction", form100Model.InjuryStatusLocalis.LeftUpper.Desctruction },
                    {"@StatusLocalis", form100Model.InjuryStatusLocalis.LeftUpper.StatusLocalis },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
            }
            catch (Exception er) { return "LeftUpper " + er.Message; }

            try
            {
                // RightLower
                if (form100Model.InjuryStatusLocalis.RightLower.Localisation > 0)
                {
                    sql = "insert into Injuries (Form100Id, Area, Localisation, Damage, Fracture, Desctruction, StatusLocalis) " +
                                        "values (@Form100Id, @Area, @Localisation, @Damage, @Fracture, @Desctruction, @StatusLocalis);";

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@Area", AreaEnum.RightLower },
                    {"@Localisation", form100Model.InjuryStatusLocalis.RightLower.Localisation },
                    {"@Damage", form100Model.InjuryStatusLocalis.RightLower.Damage },
                    {"@Fracture", form100Model.InjuryStatusLocalis.RightLower.Fracture },
                    {"@Desctruction", form100Model.InjuryStatusLocalis.RightLower.Desctruction },
                    {"@StatusLocalis", form100Model.InjuryStatusLocalis.RightLower.StatusLocalis },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
            }
            catch (Exception er) { return "RightLower " + er.Message; }


            try
            {
                // LeftLower
                if (form100Model.InjuryStatusLocalis.LeftLower.Localisation > 0)
                {
                    sql = "insert into Injuries (Form100Id, Area, Localisation, Damage, Fracture, Desctruction, StatusLocalis) " +
                                        "values (@Form100Id, @Area, @Localisation, @Damage, @Fracture, @Desctruction, @StatusLocalis);";

                    parameters = new Dictionary<string, object>()
                {
                    {"@Form100Id", id },
                    {"@Area", AreaEnum.LeftLower },
                    {"@Localisation", form100Model.InjuryStatusLocalis.LeftLower.Localisation },
                    {"@Damage", form100Model.InjuryStatusLocalis.LeftLower.Damage },
                    {"@Fracture", form100Model.InjuryStatusLocalis.LeftLower.Fracture },
                    {"@Desctruction", form100Model.InjuryStatusLocalis.LeftLower.Desctruction },
                    {"@StatusLocalis", form100Model.InjuryStatusLocalis.LeftLower.StatusLocalis },
                };
                    SqliteDataAccess.SaveData(sql, parameters);
                }
            }
            catch (Exception er) { return "LeftLower " + er.Message; }

            return output;
        }

    }
}
