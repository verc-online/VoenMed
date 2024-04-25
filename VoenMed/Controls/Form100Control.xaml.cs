using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextFormattingHelper;
using VoenMed.Utility;
using VoenMedLibrary.DataAccess;
using VoenMedLibrary.Models;
using static VoenMedLibrary.Models.EnumModel;

namespace VoenMed.Controls
{
    /// <summary>
    /// Interaction logic for Form100Control.xaml
    /// </summary>
    public partial class Form100Control : UserControl
    {
        DefaultsModel defaultsModel;
        List<RankModel> ranks = new(); // Звания
        List<DrugModel> drugs = new(); // Лекарства

        // То, что отправляется в базу данных
        List<ProvidedDrugModel> providedDrugsModel = new();
        InjuryStatusLocalisModel injuryModel = new();
        ConditionModel conditionModel = new();
        HelpProvidedModel helpProvided = new(); //
        Form100Model form100Model = new();
        public Form100Control()
        {
            InitializeComponent();
            InitializeNewForm();
            LoadDefaultsFromDatabase();

            // Звания
            InitializeRanks();
            WireUpRanksDropDown();

            // Лекарства
            InitializeDrugList();
            WireUpDrugsDropdown();
            InitializeFavDrugs();

            // Локализация
            // Состояние
            // Характер повреждений
            // Диагноз
        }

        #region NewForm
        private void LoadDefaultsFromDatabase()
        {
            string sqlStatement = "SELECT * FROM Defaults";
            defaultsModel = SqliteDataAccess.LoadData<DefaultsModel>(sqlStatement, new Dictionary<string, object>()).FirstOrDefault();

            if (defaultsModel != null)
            {
                docTextBox.Text = defaultsModel.Doc.ToString();
                evacAddressTextBox.Text = defaultsModel.EvacAddress.ToString();

                switch (defaultsModel.EvacTransport)
                {
                    case 1:
                        evacTransportRadioButton_1.IsChecked = true;
                        break;
                    case 2:
                        evacTransportRadioButton_2.IsChecked = true;
                        break;
                    case 3:
                        evacTransportRadioButton_3.IsChecked = true;
                        break;
                    case 5:
                        evacTransportRadioButton_5.IsChecked = true;
                        break;
                    default:
                    case 4:
                        evacTransportRadioButton_4.IsChecked = true;
                        break;
                }

                switch (defaultsModel.EvacWay)
                {
                    case 1:
                        evacWayRadioButton_1.IsChecked = true; break;
                    case 2:
                        evacWayRadioButton_2.IsChecked = true; break;
                    default:
                    case 3:
                        evacWayRadioButton_3.IsChecked = true; break;
                }


            }
            else
            {
                docTextBox.Text = "";
                evacAddressTextBox.Text = "";
                evacTransportRadioButton_1.IsChecked = true;
                evacWayRadioButton_3.IsChecked = true;
            }
        }
        private void InitializeNewForm()
        {
            form100Model.IssuedBy = defaultsModel?.IssuedBy ?? "";
            form100Model.IssuedWhen = DateTime.Now;
            issuedWhenTextBox.Text = form100Model.IssuedWhen.ToString();

            form100Model.EvacTime = DateTime.Now.AddHours(1);
            evacTimeTextbox.Text = form100Model.EvacTime.ToString();

            form100Model.DiseaseTime = DateTime.Now.AddHours(-1);
            diseaseTimeTextbox.Text = form100Model.DiseaseTime.ToString();

            form100Model.EvacAddress = defaultsModel?.EvacAddress ?? "";
            form100Model.Doc = defaultsModel?.Doc ?? "";

            form100Model.InjuryStatusLocalis = injuryModel;

            SummaryGlazgowScale();
            SummaryCondition();
        }

        #endregion

        #region NavigationBar

        private void damageStackPanelButton_Click(object sender, RoutedEventArgs e)
        {
            damageStackPanel.Visibility = Visibility.Visible;
            helpProvidedStackPanel.Visibility = Visibility.Collapsed;
            conditionStackPanel.Visibility = Visibility.Collapsed;
        }

        private void conditionStackPanelButton_Click(object sender, RoutedEventArgs e)
        {

            damageStackPanel.Visibility = Visibility.Collapsed;
            helpProvidedStackPanel.Visibility = Visibility.Collapsed;
            conditionStackPanel.Visibility = Visibility.Visible;
        }

        private void helpProvidedPanelButton_Click(object sender, RoutedEventArgs e)
        {

            damageStackPanel.Visibility = Visibility.Collapsed;
            helpProvidedStackPanel.Visibility = Visibility.Visible;
            conditionStackPanel.Visibility = Visibility.Collapsed;
        }


        private void printInfoPanelButton_Click(object sender, RoutedEventArgs e)
        {

            damageStackPanel.Visibility = Visibility.Collapsed;
            helpProvidedStackPanel.Visibility = Visibility.Collapsed;
            conditionStackPanel.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Summary
        public void SummaryStatusesLocalis()
        {

            injuryModel.GetAllStatusLocalises();
            DiagnosisTextBox.Text = injuryModel.Diagnosis;
        }

        public void SummaryProvidedHelp()
        {
            helpProvided.GetHelpProvidedSummary();
            helpProvidedSummaryTextBox.Text = helpProvided.HelpProvidedSummary;
        }

        public void SummaryCondition()
        {
            conditionModel.GetConditionSummary();
            try
            {

                conditionSummaryTextBox.Text = conditionModel.Condition;
            }
            catch (NullReferenceException nullRefException)
            {
                Console.WriteLine(nullRefException.Message);
            }
        }
        #endregion

        #region Condition

        #region Glazgow
        private void EyeOpeningSpontaneouslyRadioButtonClick(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.EyeResponse = EyeResponseEnum.EyeOpeningSpontaneously;
            SummaryGlazgowScale();
        }

        private void EyeOpeningToSpeechRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.EyeResponse = EyeResponseEnum.EyeOpeningToSpeech;
            SummaryGlazgowScale();

        }

        private void EyeOpeningResponsePainRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.EyeResponse = EyeResponseEnum.EyeOpeningResponsePain;
            SummaryGlazgowScale();

        }

        private void NoEyeResponseRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.EyeResponse = EyeResponseEnum.NoEyeResponse;
            SummaryGlazgowScale();

        }

        private void OrientedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.VerbalResponse = VerbalResponseEnum.Oriented;
            SummaryGlazgowScale();

        }

        private void ConfusedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.VerbalResponse = VerbalResponseEnum.Confused;
            SummaryGlazgowScale();

        }


        private void InappropriateWordsRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.VerbalResponse = VerbalResponseEnum.InappropriateWords;
            SummaryGlazgowScale();

        }

        private void IncomprehensibleSoundsRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.VerbalResponse = VerbalResponseEnum.IncomprehensibleSounds;
            SummaryGlazgowScale();

        }

        private void NoVerbalResponseRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.VerbalResponse = VerbalResponseEnum.NoVerbalResponse;
            SummaryGlazgowScale();

        }

        private void ObeysMotorCommandsRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.MotorResponse = MotorResponseEnum.ObeysMotorCommands;
            SummaryGlazgowScale();
        }

        private void LocalizesToPainRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.MotorResponse = MotorResponseEnum.LocalizesToPain;
            SummaryGlazgowScale();

        }

        private void WithdrawalToPainRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.MotorResponse = MotorResponseEnum.WithdrawalToPain;
            SummaryGlazgowScale();

        }

        private void AbnormalFlexionToPainRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.MotorResponse = MotorResponseEnum.AbnormalFlexionToPain;

            SummaryGlazgowScale();
        }

        private void ExtensionToPainRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.MotorResponse = MotorResponseEnum.ExtensionToPain;
            SummaryGlazgowScale();

        }

        private void NoMotorResponseRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.GlasgowComaScale.MotorResponse = MotorResponseEnum.NoMotorResponse;
            SummaryGlazgowScale();

        }

        private void SummaryGlazgowScale()
        {
            int sum = conditionModel.GlasgowComaScale.CalculateConsience();
            conscienceIntTextBlock.Text = sum.ToString();

            conscienceTextBlock.Text = conditionModel.GlasgowComaScale.Consience.GetDescription();
            SummaryCondition();

        }
        #endregion

        private void BodyMassTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcNoradrenalineDose();
            CalcDopamineDose();
            CalcAdrenalineDose();
            CalcDobutamineDose();

        }
        private void CalcNoradrenalineDose()
        {
            int bodyMass = int.TryParse(BodyMassTextBox.Text, out int parsedBodyMass) ? parsedBodyMass : 0;
            decimal mg = decimal.TryParse(mgNoradrenalineDoseTextBox.Text, out decimal parsedMg) ? parsedMg : 0;
            decimal volume = decimal.TryParse(volumeNoradrenalineDoseTextBox.Text, out decimal parsedVolume) ? parsedVolume : 0;
            decimal volumePerHour = decimal.TryParse(volumePerHourNoradrenalineDoseTextBox.Text, out decimal parsedVolumePerHour) ? parsedVolumePerHour : 0;
            try
            {
                conditionModel.Heart.CalcNoradrenalineDose(bodyMass, mg, volume, volumePerHour);
                NoradrenalineDoseTextBox.Text = conditionModel.Heart.NoradrenalineDose.ToString();
            }
            catch
            {
                MessageBox.Show("Для рассчета дозировки пишем числа, разрешается писать числа с точкой! Например 1.0");
            }
            SummaryCondition();

        }

        // NorAdrenaline
        private void mgNoradrenalineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcNoradrenalineDose();
        }

        private void volumeNoradrenalineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcNoradrenalineDose();

        }

        private void volumePerHourNoradrenalineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcNoradrenalineDose();
        }

        // Dopamine
        private void CalcDopamineDose()
        {
            int bodyMass = int.TryParse(BodyMassTextBox.Text, out int parsedBodyMass) ? parsedBodyMass : 0;
            decimal mg = decimal.TryParse(mgDopamineDoseTextBox.Text, out decimal parsedMg) ? parsedMg : 0;
            decimal volume = decimal.TryParse(volumeDopamineDoseTextBox.Text, out decimal parsedVolume) ? parsedVolume : 0;
            decimal volumePerHour = decimal.TryParse(volumePerHourDopamineDoseTextBox.Text, out decimal parsedVolumePerHour) ? parsedVolumePerHour : 0;
            try
            {
                conditionModel.Heart.CalcDopamineDose(bodyMass, mg, volume, volumePerHour);
                DopamineDoseTextBox.Text = conditionModel.Heart.DopamineDose.ToString();
            }
            catch
            {
                MessageBox.Show("Для рассчета дозировки пишем числа, разрешается писать числа с точкой! Например 1.0");
            }
            SummaryCondition();
        }
        private void mgDopamineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcDopamineDose();
        }

        private void volumeDopamineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcDopamineDose();

        }

        private void volumePerHourDopamineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcDopamineDose();

        }


        // Dobutamine
        private void CalcDobutamineDose()
        {
            int bodyMass = int.TryParse(BodyMassTextBox.Text, out int parsedBodyMass) ? parsedBodyMass : 0;
            decimal mg = decimal.TryParse(mgDobutamineDoseTextBox.Text, out decimal parsedMg) ? parsedMg : 0;
            decimal volume = decimal.TryParse(volumeDobutamineDoseTextBox.Text, out decimal parsedVolume) ? parsedVolume : 0;
            decimal volumePerHour = decimal.TryParse(volumePerHourDobutamineDoseTextBox.Text, out decimal parsedVolumePerHour) ? parsedVolumePerHour : 0;
            try
            {
                conditionModel.Heart.CalcDobutamineDose(bodyMass, mg, volume, volumePerHour);
                DobutamineDoseTextBox.Text = conditionModel.Heart.DobutamineDose.ToString();
            }
            catch
            {
                MessageBox.Show("Для рассчета дозировки пишем числа, разрешается писать числа с точкой! Например 1.0");
            }
            SummaryCondition();
        }
        private void mgDobutamineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcDobutamineDose();
        }

        private void volumeDobutamineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcDobutamineDose();

        }

        private void volumePerHourDobutamineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcDobutamineDose();

        }
        // Adrenaline
        private void CalcAdrenalineDose()
        {
            int bodyMass = int.TryParse(BodyMassTextBox.Text, out int parsedBodyMass) ? parsedBodyMass : 0;
            decimal mg = decimal.TryParse(mgAdrenalineDoseTextBox.Text, out decimal parsedMg) ? parsedMg : 0;
            decimal volume = decimal.TryParse(volumeAdrenalineDoseTextBox.Text, out decimal parsedVolume) ? parsedVolume : 0;
            decimal volumePerHour = decimal.TryParse(volumePerHourAdrenalineDoseTextBox.Text, out decimal parsedVolumePerHour) ? parsedVolumePerHour : 0;
            try
            {
                conditionModel.Heart.CalcAdrenalineDose(bodyMass, mg, volume, volumePerHour);
                AdrenalineDoseTextBox.Text = conditionModel.Heart.AdrenalineDose.ToString();
            }
            catch
            {
                MessageBox.Show("Для рассчета дозировки пишем числа, разрешается писать числа с точкой! Например 1.0");
            }
            SummaryCondition();
        }
        private void mgAdrenalineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcAdrenalineDose();
        }

        private void volumeAdrenalineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcAdrenalineDose();

        }

        private void volumePerHourAdrenalineDoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcAdrenalineDose();

        }

        private void heartSupportNeedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            HeartSupportStackPanel.Visibility = Visibility.Visible;
            conditionModel.Heart.ResetHeartSupport();
            SummaryCondition();
        }

        private void heartSupportNoNeedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            HeartSupportStackPanel.Visibility = Visibility.Collapsed;
            CalcNoradrenalineDose();
            CalcDopamineDose();
            CalcAdrenalineDose();
            CalcDobutamineDose();

        }

        private void EffectiveBreathingSupportRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.Breathing.BreathingSupport = BreathingSupportEnum.Effective;
            SummaryCondition();
        }

        private void InsuflationBreathingSupportRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.Breathing.BreathingSupport = BreathingSupportEnum.Insuflation;

            SummaryCondition();
        }

        private void NVUBreathingSupportRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.Breathing.BreathingSupport = BreathingSupportEnum.NVU;

            SummaryCondition();
        }

        private void EndotrachealBreathingSupportRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.Breathing.BreathingSupport = BreathingSupportEnum.Endotracheal;

            SummaryCondition();
        }

        private void ConicotomyBreathingSupportRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.Breathing.BreathingSupport = BreathingSupportEnum.Conicotomy;

            SummaryCondition();
        }

        private void BreathingRateTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int rate = int.TryParse(BreathingRateTextBox.Text, out int parsedRate) ? parsedRate : 0;
            if (rate == 0)
            {
                MessageBox.Show("Сатурация выставлена неправильно или равна нулю!");
                BreathingRateTextBox.Text = "0";
            }
            conditionModel.Breathing.BreathingRate = rate;
            SummaryCondition();
        }

        private void SaturationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int saturation = int.TryParse(SaturationTextBox.Text, out int parsedSaturation) ? parsedSaturation : 0;
            if (saturation == 0)
            {
                MessageBox.Show("Сатурация выставлена неправильно или равна нулю!");
                SaturationTextBox.Text = "0";
            }
            conditionModel.Breathing.Saturation = saturation;
            SummaryCondition();
        }

        private void FiO2TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int fiO2 = int.TryParse(FiO2TextBox.Text, out int parsedFiO2) ? parsedFiO2 : 0;
            if (fiO2 == 0)
            {
                MessageBox.Show("FiO2 выставлен неправильно!");
                FiO2TextBox.Text = "0";
            }
            conditionModel.Breathing.FiO2 = fiO2;

            SummaryCondition();

        }

        private void HeartRateTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int rate = int.TryParse(HeartRateTextBox.Text, out int parsedRate) ? parsedRate : 0;
            if (rate == 0)
            {
                MessageBox.Show("ЧСС выставлено неправильно или равно нулю!");
                HeartRateTextBox.Text = "0";
            }
            conditionModel.Heart.Rate = rate;
            SummaryCondition();
        }

        private void SystolicArterialPressureTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int pressure = int.TryParse(SystolicArterialPressureTextBox.Text, out int parsedPressure) ? parsedPressure : 0;
            if (pressure == 0)
            {
                MessageBox.Show("САД выставлено неправильно или равно нулю!");
                SystolicArterialPressureTextBox.Text = "0";
            }
            conditionModel.Heart.SystolicArterialPressure = pressure;
            conditionModel.GetConditionSummary();
            SummaryCondition();
        }

        private void CapillaryTimeFastRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.Heart.CapillaryTime = CapillaryTimeEnum.Fast;
            SummaryCondition();
        }

        private void CapillaryTimeSlowRadioButton_Click(object sender, RoutedEventArgs e)
        {
            conditionModel.Heart.CapillaryTime = CapillaryTimeEnum.Slow;

            SummaryCondition();
        }

        private void TemperatureTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                conditionModel.Temperature = decimal.Parse(TemperatureTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Неправильно заполнена температура! Пример: 36,6 - с запятой!");
                TemperatureTextBox.Text = "36,6";
            }
            SummaryCondition();
        }

        private void DiuresPerFirstHourTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                conditionModel.DiuresPerFirstHour = int.Parse(DiuresPerFirstHourTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Неправильно введен диурез! Только целые числа.");
                DiuresPerFirstHourTextBox.Text = "0";
            }
            SummaryCondition();
        }
        #endregion

        #region Head
        private void frontHeadInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.AddHeadFront();
            CheckIfHeadIsDamaged();
        }
        private void frontHeadInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.DeleteHeadFront();
            CheckIfHeadIsDamaged();
        }
        private void backHeadInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.AddHeadBack();
            CheckIfHeadIsDamaged();
        }
        private void backHeadInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.DeleteHeadBack();
            CheckIfHeadIsDamaged();

        }
        private void CheckIfHeadIsDamaged()
        {
            headInjuryDetailButton.Visibility = injuryModel.Head.Localisation > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (headInjuryDetailButton.Visibility == Visibility.Visible)
            {
                showHeadInjuryDetailPanel();
            }
            else
            {
                collapseAllBodyPartsInjuryCharacteristic();
            }
        }
        public void showHeadInjuryDetailPanel()
        {
            collapseAllBodyPartsInjuryCharacteristic();
            headInjuryDetailPanel.Visibility = Visibility.Visible;
        }
        private void UpdateHeadStatusLocalis()
        {
            headStatusLocalisTextBox.Text = injuryModel.Head.GetStatusLocalis();

            SummaryStatusesLocalis();
        }
        private void headInjuryBlindRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeWoundCanalBlind();
            UpdateHeadStatusLocalis();
        }
        private void headInjuryThroughRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeWoundCanalThrough();
            UpdateHeadStatusLocalis();
        }
        private void headInjuryTangentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeWoundCanalTangent();
            UpdateHeadStatusLocalis();
        }
        private void headInjuryPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakePenetrated();
            UpdateHeadStatusLocalis();
        }
        private void headInjuryNotPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeNotPenetrated();
            UpdateHeadStatusLocalis();
        }
        private void headTraumaOpenRadioButton_Click(object sender, RoutedEventArgs e)
        {

            injuryModel.Head.MakeTraumaOpen();
            UpdateHeadStatusLocalis();
        }
        private void headTraumaClosedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeTraumaClosed();
            UpdateHeadStatusLocalis();
        }
        private void headBrainDamageCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.AddBrainDamage();
            UpdateHeadStatusLocalis();

        }
        private void headBrainDamageCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.DeleteBrainDamage();
            UpdateHeadStatusLocalis();
        }
        private void headEyeDamageCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.AddEyeDamage();
            UpdateHeadStatusLocalis();
        }
        private void headEyeDamageCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.DeleteEyeDamage();
            UpdateHeadStatusLocalis();
        }
        private void headEarDamageCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.AddEarDamage();
            UpdateHeadStatusLocalis();
        }
        private void headEarDamageCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.DeleteEarDamage();
            UpdateHeadStatusLocalis();
        }
        private void headFacialDamageCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.AddMaxilloFacialDamage();
            UpdateHeadStatusLocalis();
        }
        private void headFacialDamageCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.DeleteMaxilloFacialDamage();
            UpdateHeadStatusLocalis();
        }
        private void headSkullBonesFractureCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.AddSkullFracture();
            UpdateHeadStatusLocalis();
        }
        private void headSkullBonesFractureCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.DeleteSkullFracture();
            UpdateHeadStatusLocalis();
        }
        private void headFaceFractureCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.AddFaceFracture();
            UpdateHeadStatusLocalis();
        }
        private void headFaceFractureCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.DeleteFaceFracture();
            UpdateHeadStatusLocalis();

        }
        private void headThermoBurn1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeThermoBurn1();
            UpdateHeadStatusLocalis();
        }
        private void headThermoBurn2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeThermoBurn2();

            UpdateHeadStatusLocalis();
        }
        private void headThermoBurn3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeThermoBurn3();
            UpdateHeadStatusLocalis();

        }
        private void headThermoBurn4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeThermoBurn4();
            UpdateHeadStatusLocalis();

        }
        private void headFrostbite1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeThermoFrostbite1();
            UpdateHeadStatusLocalis();

        }
        private void headFrostbite2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeThermoFrostbite2();
            UpdateHeadStatusLocalis();

        }
        private void headFrostbite3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeThermoFrostbite3();
            UpdateHeadStatusLocalis();

        }
        private void headFrostbite4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Head.MakeThermoFrostbite4();
            UpdateHeadStatusLocalis();

        }
        private void headInjuryDetailButton_Click(object sender, RoutedEventArgs e)
        {
            showHeadInjuryDetailPanel();
        }
        #endregion

        #region InjuryCharacter
        public void collapseAllBodyPartsInjuryCharacteristic()
        {
            headInjuryDetailPanel.Visibility = Visibility.Collapsed;
            neckInjuryDetailPanel.Visibility = Visibility.Collapsed;
            thoraxInjuryDetailPanel.Visibility = Visibility.Collapsed;
            abdomenInjuryDetailPanel.Visibility = Visibility.Collapsed;
            pelvisInjuryDetailPanel.Visibility = Visibility.Collapsed;
            spineInjuryDetailPanel.Visibility = Visibility.Collapsed;
            rightUpperInjuryDetailPanel.Visibility = Visibility.Collapsed;
            leftUpperInjuryDetailPanel.Visibility = Visibility.Collapsed;
            rightLowerInjuryDetailPanel.Visibility = Visibility.Collapsed;
            leftLowerInjuryDetailPanel.Visibility = Visibility.Collapsed;
            // FFDBF1FF Chosen
        }

        public void showThoraxInjuryDetailPanel()
        {
            collapseAllBodyPartsInjuryCharacteristic();
            thoraxInjuryDetailPanel.Visibility = Visibility.Visible;
        }
        public void showAbdomenInjuryDetailPanel()
        {
            collapseAllBodyPartsInjuryCharacteristic();
            abdomenInjuryDetailPanel.Visibility = Visibility.Visible;
        }
        public void showPelvisInjuryDetailPanel()
        {
            collapseAllBodyPartsInjuryCharacteristic();
            pelvisInjuryDetailPanel.Visibility = Visibility.Visible;
        }
        public void showRightUpperInjuryDetailPanel()
        {
            collapseAllBodyPartsInjuryCharacteristic();
            rightUpperInjuryDetailPanel.Visibility = Visibility.Visible;
        }
        public void showLeftUpperInjuryDetailPanel()
        {
            collapseAllBodyPartsInjuryCharacteristic();
            leftUpperInjuryDetailPanel.Visibility = Visibility.Visible;
        }
        public void showRightLowerInjuryDetailPanel()
        {
            collapseAllBodyPartsInjuryCharacteristic();
            rightLowerInjuryDetailPanel.Visibility = Visibility.Visible;
        }
        public void showLeftLowerInjuryDetailPanel()
        {
            collapseAllBodyPartsInjuryCharacteristic();
            leftLowerInjuryDetailPanel.Visibility = Visibility.Visible;
        }
        public void showSpineInjuryDetailPanel()
        {
            collapseAllBodyPartsInjuryCharacteristic();
            spineInjuryDetailPanel.Visibility = Visibility.Visible;
        }

        private void thoraxInjuryDetailButton_Click(object sender, RoutedEventArgs e)
        {
            showThoraxInjuryDetailPanel();
        }
        private void neckInjuryDetailButton_Click(object sender, RoutedEventArgs e)
        {
            showNeckInjuryDetailPanel();
        }
        private void abdomenInjuryDetailButton_Click(object sender, RoutedEventArgs e)
        {
            showAbdomenInjuryDetailPanel();
        }
        private void pelvisInjuryDetailButton_Click(object sender, RoutedEventArgs e)
        {
            showPelvisInjuryDetailPanel();
        }
        private void spineInjuryDetailButton_Click(object sender, RoutedEventArgs e)
        {
            showSpineInjuryDetailPanel();
        }
        private void rightUpperInjuryDetailButton_Click(object sender, RoutedEventArgs e)
        {
            showRightUpperInjuryDetailPanel();
        }
        private void leftUpperInjuryDetailButton_Click(object sender, RoutedEventArgs e)
        {
            showLeftUpperInjuryDetailPanel();
        }
        private void rightLowerInjuryDetailButton_Click(object sender, RoutedEventArgs e)
        {
            showRightLowerInjuryDetailPanel();
        }
        private void leftLowerInjuryDetailButton_Click(object sender, RoutedEventArgs e)
        {
            showLeftLowerInjuryDetailPanel();
        }
        #endregion

        #region Drugs
        private void InitializeFavDrugs()
        {
            favDrug1Button.DataContext = drugs.Where(x => x.Id == defaultsModel.FavDrug1Id).First();
            favDrug1Button.Content = drugs.Where(x => x.Id == defaultsModel.FavDrug1Id).First().Title;

            favDrug2Button.DataContext = drugs.Where(x => x.Id == defaultsModel.FavDrug2Id).First();
            favDrug2Button.Content = drugs.Where(x => x.Id == defaultsModel.FavDrug2Id).First().Title;

            favDrug3Button.DataContext = drugs.Where(x => x.Id == defaultsModel.FavDrug3Id).First();
            favDrug3Button.Content = drugs.Where(x => x.Id == defaultsModel.FavDrug3Id).First().Title;

            favDrug4Button.DataContext = drugs.Where(x => x.Id == defaultsModel.FavDrug4Id).First();
            favDrug4Button.Content = drugs.Where(x => x.Id == defaultsModel.FavDrug4Id).First().Title;

        }
        private void InitializeDrugList()
        {
            string sql = "select * from Drugs order by Title;";
            drugs = SqliteDataAccess.LoadData<DrugModel>(sql, new Dictionary<string, object> { });

        }
        private void WireUpDrugsDropdown()
        {
            drugsComboBox.ItemsSource = drugs;
            drugsComboBox.DisplayMemberPath = "Title";
            drugsComboBox.SelectedValuePath = "Id";

        }
        private void drugsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            drugMeasurement.Text = ((DrugModel)drugsComboBox.SelectedItem).Measurement ?? "ед. изм.";
            drugDose.Text = ((DrugModel)drugsComboBox.SelectedItem).StandardDose.ToString();
        }
        private void provideDrugButton_Click(object sender, RoutedEventArgs e)
        {
            ProvidedDrugModel model = new ProvidedDrugModel();
            if (drugsComboBox.SelectedValue is null)
            {
                MessageBox.Show("Выберите препарат!");
                return;
            }
            try
            {
                model.Id = (int)drugsComboBox.SelectedValue;
                model.Title = ((DrugModel)drugsComboBox.SelectedItem).Title;
                model.Dose = double.Parse(drugDose.Text);
            }
            catch { MessageBox.Show("Ошибка!"); return; };
            model.Measurement = drugMeasurement.Text;
            AddDrugToProvidedDrugList(model);
        }
        private void deleteDrugProvidedButton_Click(object sender, RoutedEventArgs e)
        {

            var drug = ((FrameworkElement)sender).DataContext as ProvidedDrugModel;
            providedDrugsModel.Remove(drug);
            drugsProvidedList.ItemsSource = new ObservableCollection<ProvidedDrugModel>(providedDrugsModel);
            helpProvided.DrugsProvided = providedDrugsModel;
            SummaryProvidedHelp();
        }
        private void favDrug1Button_Click(object sender, RoutedEventArgs e)
        {
            var favDrug = ((FrameworkElement)sender).DataContext as DrugModel;
            ProvidedDrugModel drug = new()
            {
                Id = favDrug.Id,
                Dose = favDrug.StandardDose,
                Title = favDrug.Title,
                Measurement = favDrug.Measurement
            };
            AddDrugToProvidedDrugList(drug);
        }
        private void AddDrugToProvidedDrugList(ProvidedDrugModel drug)
        {
            if (providedDrugsModel.Where(d => d.Id == drug.Id).Count() > 0)
            {
                providedDrugsModel.Where(d => d.Id == drug.Id).ToList().ForEach(d => d.Dose = Math.Round(d.Dose += drug.Dose, 3));
            }
            else
            {
                providedDrugsModel.Add(drug);

            }
            drugsProvidedList.ItemsSource = new ObservableCollection<ProvidedDrugModel>(providedDrugsModel);
            helpProvided.DrugsProvided = providedDrugsModel;
            SummaryProvidedHelp();
        }
        private void favDrug2Button_Click(object sender, RoutedEventArgs e)
        {
            var favDrug = ((FrameworkElement)sender).DataContext as DrugModel;
            ProvidedDrugModel drug = new()
            {
                Id = favDrug.Id,
                Dose = favDrug.StandardDose,
                Title = favDrug.Title,
                Measurement = favDrug.Measurement
            };
            AddDrugToProvidedDrugList(drug);
        }
        private void favDrug3Button_Click(object sender, RoutedEventArgs e)
        {
            var favDrug = ((FrameworkElement)sender).DataContext as DrugModel;
            ProvidedDrugModel drug = new()
            {
                Id = favDrug.Id,
                Dose = favDrug.StandardDose,
                Title = favDrug.Title,
                Measurement = favDrug.Measurement
            };
            AddDrugToProvidedDrugList(drug);
        }
        private void favDrug4Button_Click(object sender, RoutedEventArgs e)
        {
            var favDrug = ((FrameworkElement)sender).DataContext as DrugModel;
            ProvidedDrugModel drug = new()
            {
                Id = favDrug.Id,
                Dose = favDrug.StandardDose,
                Title = favDrug.Title,
                Measurement = favDrug.Measurement
            };
            AddDrugToProvidedDrugList(drug);
        }
        private void resetDrugsProvidedButton_Click(object sender, RoutedEventArgs e)
        {
            providedDrugsModel.Clear();
            drugsProvidedList.ItemsSource = new ObservableCollection<ProvidedDrugModel>(providedDrugsModel);
            helpProvided.DrugsProvided = providedDrugsModel;
            SummaryProvidedHelp();
        }
        #endregion

        #region Ranks
        private void WireUpRanksDropDown()
        {
            ranksDropdown.ItemsSource = ranks;
            ranksDropdown.DisplayMemberPath = "RankTitle";
            ranksDropdown.SelectedValuePath = "Id";
        }
        private void InitializeRanks()
        {
            string sql = "select * from Ranks";
            ranks = SqliteDataAccess.LoadData<RankModel>(sql, new Dictionary<string, object>());
        }
        #endregion

        #region InjuryEthiology
        // Травма/Ранение
        private void injuryEthiologyRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            injuryEthiologyPanel.Visibility = Visibility.Visible;

            // TODO: Скрыть все травмы во вкладках

            // Head
            headTraumaStackPanel.Visibility = Visibility.Collapsed;
            headInjuryStackPanel.Visibility = Visibility.Visible;

            // Neck
            neckTraumaStackPanel.Visibility = Visibility.Collapsed;
            neckInjuryStackPanel.Visibility = Visibility.Visible;

            // Thorax
            thoraxTraumaStackPanel.Visibility = Visibility.Collapsed;
            thoraxInjuryStackPanel.Visibility = Visibility.Visible;

            // Abdomen
            abdomenTraumaStackPanel.Visibility = Visibility.Collapsed;
            abdomenInjuryStackPanel.Visibility = Visibility.Visible;

            // Pelvis
            pelvisTraumaStackPanel.Visibility = Visibility.Collapsed;
            pelvisInjuryStackPanel.Visibility = Visibility.Visible;


            // Spine
            spineTraumaStackPanel.Visibility = Visibility.Collapsed;
            spineInjuryStackPanel.Visibility = Visibility.Visible;

            // RightUpper
            rightUpperTraumaStackPanel.Visibility = Visibility.Collapsed;
            rightUpperInjuryStackPanel.Visibility = Visibility.Visible;

            // Left Upper
            leftUpperTraumaStackPanel.Visibility = Visibility.Collapsed;
            leftUpperInjuryStackPanel.Visibility = Visibility.Visible;


            // Right Lower
            rightLowerTraumaStackPanel.Visibility = Visibility.Collapsed;
            rightLowerInjuryStackPanel.Visibility = Visibility.Visible;


            // Left Lower
            leftLowerTraumaStackPanel.Visibility = Visibility.Collapsed;
            leftLowerInjuryStackPanel.Visibility = Visibility.Visible;
        }
        private void traumaEthiologyRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            injuryEthiologyPanel.Visibility = Visibility.Collapsed;
            injuryModel.MakeEthiologyTrauma();
            CheckIfEthiologyIsChecked();


            //TODO: Скрыть все описания ранений
            // Head
            headInjuryStackPanel.Visibility = Visibility.Collapsed;
            headTraumaStackPanel.Visibility = Visibility.Visible;


            // Neck
            neckInjuryStackPanel.Visibility = Visibility.Collapsed;
            neckTraumaStackPanel.Visibility = Visibility.Visible;

            // Thorax
            thoraxInjuryStackPanel.Visibility = Visibility.Collapsed;
            thoraxTraumaStackPanel.Visibility = Visibility.Visible;

            // Abdomen
            abdomenInjuryStackPanel.Visibility = Visibility.Collapsed;
            abdomenTraumaStackPanel.Visibility = Visibility.Visible;

            // Pelvis
            pelvisInjuryStackPanel.Visibility = Visibility.Collapsed;
            pelvisTraumaStackPanel.Visibility = Visibility.Visible;

            // Pelvis
            spineInjuryStackPanel.Visibility = Visibility.Collapsed;
            spineTraumaStackPanel.Visibility = Visibility.Visible;

            // Right Upper
            rightUpperInjuryStackPanel.Visibility = Visibility.Collapsed;
            rightUpperTraumaStackPanel.Visibility = Visibility.Visible;


            // Left Upper
            leftUpperInjuryStackPanel.Visibility = Visibility.Collapsed;
            leftUpperTraumaStackPanel.Visibility = Visibility.Visible;


            // Right Lower
            rightLowerInjuryStackPanel.Visibility = Visibility.Collapsed;
            rightLowerTraumaStackPanel.Visibility = Visibility.Visible;


            // Left Lower
            leftLowerInjuryStackPanel.Visibility = Visibility.Collapsed;
            leftLowerTraumaStackPanel.Visibility = Visibility.Visible;

        }
        private void injuryGunshotTypeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryGunshotTypesStackPanel.Visibility = Visibility.Visible;
            injuryNonGunshotTypesStackPanel.Visibility = Visibility.Collapsed;
        }
        private void injuryNonGunshotTypeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryGunshotTypesStackPanel.Visibility = Visibility.Collapsed;
            injuryNonGunshotTypesStackPanel.Visibility = Visibility.Visible;
        }
        private void FragileRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.MakeEthiologyFragile();
            CheckIfEthiologyIsChecked();
        }
        private void GunshotRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.MakeEthiologyGunshot();
            CheckIfEthiologyIsChecked();

        }
        private void ExplosionRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.MakeEthiologyExplosion();
            CheckIfEthiologyIsChecked();

        }
        private void StabbedCutRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.MakeEthiologyStabbedCut();
            CheckIfEthiologyIsChecked();

        }
        private void StabbedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.MakeEthiologyStabbed();
            CheckIfEthiologyIsChecked();

        }
        private void ChoppedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.MakeEthiologyChopped();
            CheckIfEthiologyIsChecked();
        }
        private void CheckIfEthiologyIsChecked()
        {
            if (injuryModel.Ethiology > 0)
            {
                messageStackPanel.Visibility = Visibility.Collapsed;
                damagedAreaDetailStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                messageStackPanel.Visibility = Visibility.Visible;
                damagedAreaDetailStackPanel.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Neck
        private void CheckIfNeckIsDamaged()
        {
            neckInjuryDetailButton.Visibility = injuryModel.Neck.Localisation > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (neckInjuryDetailButton.Visibility == Visibility.Visible)
            {
                showNeckInjuryDetailPanel();
            }
            else
            {
                collapseAllBodyPartsInjuryCharacteristic();
            }
        }
        public void showNeckInjuryDetailPanel()
        {
            collapseAllBodyPartsInjuryCharacteristic();
            neckInjuryDetailPanel.Visibility = Visibility.Visible;
        }
        private void UpdateNeckStatusLocalis()
        {
            neckStatusLocalisTextBox.Text = injuryModel.Neck.GetStatusLocalis();
            SummaryStatusesLocalis();

        }

        private void ThroatCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.AddThroatDamage();
            UpdateNeckStatusLocalis();
        }

        private void ThroatCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.DeleteThroatDamage();
            UpdateNeckStatusLocalis();

        }
        private void LarynxCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.AddLarynxDamage();
            UpdateNeckStatusLocalis();

        }

        private void LarynxCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.DeleteLarynxDamage();
            UpdateNeckStatusLocalis();

        }

        private void TracheaCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.AddTracheaDamage();
            UpdateNeckStatusLocalis();

        }

        private void TracheaCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.DeleteTracheaDamage();
            UpdateNeckStatusLocalis();

        }

        private void EsophagusCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.AddEsophagusDamage();
            UpdateNeckStatusLocalis();

        }

        private void EsophagusCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.DeleteEsophagusDamage();

            UpdateNeckStatusLocalis();

        }

        private void neckVesselsCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.AddVesselsDamage();

            UpdateNeckStatusLocalis();

        }

        private void neckVesselsCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.DeleteVesselsDamage();

            UpdateNeckStatusLocalis();

        }

        private void neckThermoBurn1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeThermoBurn1();
            UpdateNeckStatusLocalis();

        }

        private void neckThermoBurn2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeThermoBurn2();
            UpdateNeckStatusLocalis();

        }

        private void neckThermoBurn3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeThermoBurn3();
            UpdateNeckStatusLocalis();

        }

        private void neckThermoBurn4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeThermoBurn4();

            UpdateNeckStatusLocalis();

        }

        private void neckFrostbite1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeThermoFrostbite1();
            UpdateNeckStatusLocalis();

        }

        private void neckFrostbite2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeThermoFrostbite2();

            UpdateNeckStatusLocalis();
        }

        private void neckFrostbite3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeThermoFrostbite3();

            UpdateNeckStatusLocalis();
        }

        private void neckFrostbite4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeThermoFrostbite4();
            UpdateNeckStatusLocalis();
        }


        private void neckInjuryBlindRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeWoundCanalBlind();
            UpdateNeckStatusLocalis();

        }

        private void neckInjuryThroughRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeWoundCanalThrough();
            UpdateNeckStatusLocalis();
        }

        private void neckInjuryTangentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeWoundCanalTangent();
            UpdateNeckStatusLocalis();
        }

        private void neckInjuryPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakePenetrated();
            UpdateNeckStatusLocalis();
        }

        private void neckInjuryNotPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeNotPenetrated();
            UpdateNeckStatusLocalis();
        }

        private void neckTraumaOpenRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeTraumaOpen();
            UpdateNeckStatusLocalis();
        }

        private void neckTraumaClosedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.MakeTraumaClosed();
            UpdateNeckStatusLocalis();
        }
        private void frontNeckInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.AddNeckFront();
            CheckIfNeckIsDamaged();
            UpdateNeckStatusLocalis();
        }

        private void frontNeckInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.DeleteNeckFront();
            CheckIfNeckIsDamaged();
            UpdateNeckStatusLocalis();
        }


        private void backNeckInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.AddNeckBack();
            CheckIfNeckIsDamaged();
            UpdateNeckStatusLocalis();

        }

        private void backNeckInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Neck.DeleteNeckBack();
            CheckIfNeckIsDamaged();
            UpdateNeckStatusLocalis();
        }
        #endregion

        #region Thorax
        private void frontThoracInjuryRight_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddThoraxFrontRight();
            CheckIfThoraxIsDamaged();
            UpdateThoraxStatusLocalis();
        }

        private void frontThoracInjuryRight_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteThoraxFrontRight();
            CheckIfThoraxIsDamaged();
            UpdateThoraxStatusLocalis();
        }

        private void frontThoracInjuryLeft_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddThoraxFrontLeft();
            CheckIfThoraxIsDamaged();
            UpdateThoraxStatusLocalis();
        }

        private void frontThoracInjuryLeft_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteThoraxFrontLeft(); ;
            CheckIfThoraxIsDamaged();
            UpdateThoraxStatusLocalis();
        }

        private void backThoracInjuryLeft_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddThoraxBackLeft();
            CheckIfThoraxIsDamaged();
            UpdateThoraxStatusLocalis();
        }

        private void backThoracInjuryLeft_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteThoraxBackLeft();
            CheckIfThoraxIsDamaged();
            UpdateThoraxStatusLocalis();
        }

        private void backThoracInjuryRight_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddThoraxBackRight();
            CheckIfThoraxIsDamaged();
            UpdateThoraxStatusLocalis();
        }

        private void backThoracInjuryRight_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteThoraxBackRight();
            CheckIfThoraxIsDamaged();
            UpdateThoraxStatusLocalis();
        }

        // Check If damaged
        private void CheckIfThoraxIsDamaged()
        {
            thoraxInjuryDetailButton.Visibility = injuryModel.Thorax.Localisation > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (thoraxInjuryDetailButton.Visibility == Visibility.Visible)
            {
                showThoraxInjuryDetailPanel();
            }
            else
            {
                collapseAllBodyPartsInjuryCharacteristic();
            }
        }
        private void UpdateThoraxStatusLocalis()
        {
            thoraxStatusLocalisTextBox.Text = injuryModel.Thorax.GetStatusLocalis();

            SummaryStatusesLocalis();

        }

        // Thermo
        private void thoraxThermoBurn1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeThermoBurn1();
            UpdateThoraxStatusLocalis();

        }

        private void thoraxThermoBurn2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeThermoBurn2();
            UpdateThoraxStatusLocalis();

        }

        private void thoraxThermoBurn3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeThermoBurn3();
            UpdateThoraxStatusLocalis();

        }

        private void thoraxThermoBurn4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeThermoBurn4();

            UpdateThoraxStatusLocalis();

        }

        private void thoraxFrostbite1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeThermoFrostbite1();
            UpdateThoraxStatusLocalis();

        }

        private void thoraxFrostbite2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeThermoFrostbite2();

            UpdateThoraxStatusLocalis();
        }

        private void thoraxFrostbite3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeThermoFrostbite3();

            UpdateThoraxStatusLocalis();
        }

        private void thoraxFrostbite4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeThermoFrostbite4();
            UpdateThoraxStatusLocalis();
        }

        // Injury
        private void thoraxInjuryBlindRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeWoundCanalBlind();
            UpdateThoraxStatusLocalis();

        }

        private void thoraxInjuryThroughRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeWoundCanalThrough();
            UpdateThoraxStatusLocalis();
        }

        private void thoraxInjuryTangentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeWoundCanalTangent();
            UpdateThoraxStatusLocalis();
        }

        private void thoraxInjuryPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakePenetrated();
            UpdateThoraxStatusLocalis();
        }

        private void thoraxInjuryNotPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeNotPenetrated();
            UpdateThoraxStatusLocalis();
        }
        // Trauma
        private void thoraxTraumaOpenRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeTraumaOpen();
            UpdateThoraxStatusLocalis();
        }

        private void thoraxTraumaClosedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.MakeTraumaClosed();
            UpdateThoraxStatusLocalis();
        }


        // Common
        private void thoraxVesselsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddVesselsDamage();
            UpdateThoraxStatusLocalis();
        }


        private void thoraxVesselsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteVesselsDamage();
            UpdateThoraxStatusLocalis();
        }

        // Special
        private void LungCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddLungDamage();
            UpdateThoraxStatusLocalis();
        }

        private void LungCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteLungDamage();
            UpdateThoraxStatusLocalis();
        }

        private void HeartCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddHeartDamage();
            UpdateThoraxStatusLocalis();
        }

        private void HeartCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteHeartDamage();
            UpdateThoraxStatusLocalis();
        }

        private void thoraxEsophagusCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddEsophagusDamage();
            UpdateThoraxStatusLocalis();
        }

        private void thoraxEsophagusCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteEsophagusDamage();

            UpdateThoraxStatusLocalis();
        }

        private void RibCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddRibFracture();
            UpdateThoraxStatusLocalis();
        }

        private void RibCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteRibFracture();
            UpdateThoraxStatusLocalis();
        }

        private void SternumCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddSternumFracture();
            UpdateThoraxStatusLocalis();
        }

        private void SternumCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteSternumFracture();
            UpdateThoraxStatusLocalis();
        }

        private void CollarboneCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddCollarboneFracture();
            UpdateThoraxStatusLocalis();
        }

        private void CollarboneCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteCollarboneFracture();
            UpdateThoraxStatusLocalis();
        }

        private void BladeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.AddBladeFracture();
            UpdateThoraxStatusLocalis();
        }

        private void BladeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Thorax.DeleteBladeFracture();
            UpdateThoraxStatusLocalis();
        }
        #endregion

        #region Abdomen

        private void frontBellyInjuryRight_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.AddAbdomenFrontRight();
            CheckIfAbdomenIsDamaged();
            UpdateAbdomenStatusLocalis();
        }

        private void frontBellyInjuryRight_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.DeleteAbdomenFrontRight();
            CheckIfAbdomenIsDamaged();
            UpdateAbdomenStatusLocalis();
        }

        private void frontBellyInjuryLeft_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.AddAbdomenFrontLeft();
            CheckIfAbdomenIsDamaged();
            UpdateAbdomenStatusLocalis();
        }

        private void frontBellyInjuryLeft_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.DeleteAbdomenFrontLeft();
            CheckIfAbdomenIsDamaged();
            UpdateAbdomenStatusLocalis();
        }

        private void backBellyInjuryLeft_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.AddAbdomenBackLeft();
            CheckIfAbdomenIsDamaged();
            UpdateAbdomenStatusLocalis();
        }

        private void backBellyInjuryLeft_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.DeleteAbdomenBackLeft();
            CheckIfAbdomenIsDamaged();
            UpdateAbdomenStatusLocalis();
        }

        private void backBellyInjuryRight_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.AddAbdomenBackRight();
            CheckIfAbdomenIsDamaged();
            UpdateAbdomenStatusLocalis();
        }

        private void backBellyInjuryRight_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.DeleteAbdomenBackRight();
            CheckIfAbdomenIsDamaged();
            UpdateAbdomenStatusLocalis();
        }


        // Check If damaged
        private void CheckIfAbdomenIsDamaged()
        {
            abdomenInjuryDetailButton.Visibility = injuryModel.Abdomen.Localisation > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (abdomenInjuryDetailButton.Visibility == Visibility.Visible)
            {
                showAbdomenInjuryDetailPanel();
            }
            else
            {
                collapseAllBodyPartsInjuryCharacteristic();
            }
        }

        // Update Status Localis
        private void UpdateAbdomenStatusLocalis()
        {
            abdomenStatusLocalisTextBox.Text = injuryModel.Abdomen.GetStatusLocalis();
            SummaryStatusesLocalis();

        }

        // Thermo
        private void abdomenThermoBurn1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeThermoBurn1();
            UpdateAbdomenStatusLocalis();

        }

        private void abdomenThermoBurn2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeThermoBurn2();
            UpdateAbdomenStatusLocalis();

        }

        private void abdomenThermoBurn3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeThermoBurn3();
            UpdateAbdomenStatusLocalis();

        }

        private void abdomenThermoBurn4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeThermoBurn4();

            UpdateAbdomenStatusLocalis();

        }

        private void abdomenFrostbite1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeThermoFrostbite1();
            UpdateAbdomenStatusLocalis();

        }

        private void abdomenFrostbite2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeThermoFrostbite2();

            UpdateAbdomenStatusLocalis();
        }

        private void abdomenFrostbite3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeThermoFrostbite3();

            UpdateAbdomenStatusLocalis();
        }

        private void abdomenFrostbite4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeThermoFrostbite4();
            UpdateAbdomenStatusLocalis();
        }

        // Injury
        private void abdomenInjuryBlindRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeWoundCanalBlind();
            UpdateAbdomenStatusLocalis();

        }

        private void abdomenInjuryThroughRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeWoundCanalThrough();
            UpdateAbdomenStatusLocalis();
        }

        private void abdomenInjuryTangentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeWoundCanalTangent();
            UpdateAbdomenStatusLocalis();
        }

        private void abdomenInjuryPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakePenetrated();
            UpdateAbdomenStatusLocalis();
        }

        private void abdomenInjuryNotPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeNotPenetrated();
            UpdateAbdomenStatusLocalis();
        }
        // Trauma
        private void abdomenTraumaOpenRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeTraumaOpen();
            UpdateAbdomenStatusLocalis();
        }

        private void abdomenTraumaClosedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.MakeTraumaClosed();
            UpdateAbdomenStatusLocalis();
        }


        // Common
        private void abdomenVesselsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.AddVesselsDamage();
            UpdateAbdomenStatusLocalis();
        }


        private void abdomenVesselsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.DeleteVesselsDamage();
            UpdateAbdomenStatusLocalis();
        }

        private void ParenchymalCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.AddParenchymalDamage();
            UpdateAbdomenStatusLocalis();
        }

        private void ParenchymalCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.DeleteParenchymalDamage();
            UpdateAbdomenStatusLocalis();
        }

        private void HollowCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.AddHollowDamage();
            UpdateAbdomenStatusLocalis();
        }

        private void HollowCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.AddHollowDamage();
            UpdateAbdomenStatusLocalis();
        }


        private void nonOrganCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.AddNonOrganDamage();
            UpdateAbdomenStatusLocalis();
        }

        private void nonOrganCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Abdomen.DeleteNonOrganDamage();
            UpdateAbdomenStatusLocalis();
        }
        #endregion

        #region Pelvis

        private void pelvisFrontCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.Pelvis.AddPelvisFront();
            CheckIfPelvisIsDamaged();
            UpdatePelvisStatusLocalis();

        }

        private void pelvisFrontCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.Pelvis.DeletePelvisFront();
            CheckIfPelvisIsDamaged();
            UpdatePelvisStatusLocalis();

        }
        private void pelvisBackCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddPelvisBack();
            CheckIfPelvisIsDamaged();
            UpdatePelvisStatusLocalis();
        }

        private void pelvisBackCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.Pelvis.DeletePelvisBack();
            CheckIfPelvisIsDamaged();
            UpdatePelvisStatusLocalis();
        }

        // Check If damaged
        private void CheckIfPelvisIsDamaged()
        {
            pelvisInjuryDetailButton.Visibility = injuryModel.Pelvis.Localisation > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (pelvisInjuryDetailButton.Visibility == Visibility.Visible)
            {
                showPelvisInjuryDetailPanel();
            }
            else
            {
                collapseAllBodyPartsInjuryCharacteristic();
            }
        }

        // Update Status Localis
        private void UpdatePelvisStatusLocalis()
        {
            pelvisStatusLocalisTextBox.Text = injuryModel.Pelvis.GetStatusLocalis();
            SummaryStatusesLocalis();

        }

        // Thermo
        private void pelvisThermoBurn1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeThermoBurn1();
            UpdatePelvisStatusLocalis();

        }

        private void pelvisThermoBurn2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeThermoBurn2();
            UpdatePelvisStatusLocalis();

        }

        private void pelvisThermoBurn3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeThermoBurn3();
            UpdatePelvisStatusLocalis();

        }

        private void pelvisThermoBurn4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeThermoBurn4();

            UpdatePelvisStatusLocalis();

        }

        private void pelvisFrostbite1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeThermoFrostbite1();
            UpdatePelvisStatusLocalis();

        }

        private void pelvisFrostbite2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeThermoFrostbite2();

            UpdatePelvisStatusLocalis();
        }

        private void pelvisFrostbite3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeThermoFrostbite3();

            UpdatePelvisStatusLocalis();
        }

        private void pelvisFrostbite4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeThermoFrostbite4();
            UpdatePelvisStatusLocalis();
        }

        // Injury
        private void pelvisInjuryBlindRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeWoundCanalBlind();
            UpdatePelvisStatusLocalis();

        }

        private void pelvisInjuryThroughRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeWoundCanalThrough();
            UpdatePelvisStatusLocalis();
        }

        private void pelvisInjuryTangentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeWoundCanalTangent();
            UpdatePelvisStatusLocalis();
        }

        private void pelvisInjuryPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakePenetrated();
            UpdatePelvisStatusLocalis();
        }

        private void pelvisInjuryNotPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeNotPenetrated();
            UpdatePelvisStatusLocalis();
        }
        // Trauma
        private void pelvisTraumaOpenRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeTraumaOpen();
            UpdatePelvisStatusLocalis();
        }

        private void pelvisTraumaClosedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.MakeTraumaClosed();
            UpdatePelvisStatusLocalis();
        }


        // Common
        private void pelvisVesselsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddVesselsDamage();
            UpdatePelvisStatusLocalis();
        }


        private void pelvisVesselsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.DeleteVesselsDamage();
            UpdatePelvisStatusLocalis();
        }

        private void bladderCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddBladderDamage();
            UpdatePelvisStatusLocalis();
        }
        // UNCHECKED
        private void urethraCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddUrethraDamage();
            UpdatePelvisStatusLocalis();
        }

        private void urethraCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.DeleteUrethraDamage();
            UpdatePelvisStatusLocalis();
        }

        private void rectumCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddRectumDamage();
            UpdatePelvisStatusLocalis();
        }

        private void rectumCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.DeleteRectumDamage();
            UpdatePelvisStatusLocalis();
        }

        private void pevlisVesselsCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddVesselsDamage();
            UpdatePelvisStatusLocalis();
        }

        private void pevlisVesselsCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.DeleteVesselsDamage();
            UpdatePelvisStatusLocalis();
        }


        private void hollowPevlisCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddHollowDamage();
            UpdatePelvisStatusLocalis();
        }

        private void hollowPevlisCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.DeleteHollowDamage();
            UpdatePelvisStatusLocalis();
        }

        private void pelvicBonesCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddPelvicBonesFracture();
            UpdatePelvisStatusLocalis();
        }

        private void pelvicBonesCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.DeletePelvicBonesFracture();
            UpdatePelvisStatusLocalis();
        }

        private void sacrumCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddSacrumFracture();
            UpdatePelvisStatusLocalis();
        }

        private void sacrumCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.DeleteSacrumFracture();
            UpdatePelvisStatusLocalis();
        }

        private void coccyxCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddCoccyxFracture();
            UpdatePelvisStatusLocalis();
        }

        private void bladderCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.DeleteBladderDamage();
            UpdatePelvisStatusLocalis();
        }

        private void coccyxCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.DeleteCoccyxFracture();
            UpdatePelvisStatusLocalis();
        }
        #endregion

        #region Spine
        private void spineCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.Spine.AddSpine();
            CheckIfSpineIsDamaged();
        }

        private void spineCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.Spine.DeleteSpine();
            CheckIfSpineIsDamaged();
        }

        // Check If damaged
        private void CheckIfSpineIsDamaged()
        {
            spineInjuryDetailButton.Visibility = injuryModel.Spine.Localisation > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (spineInjuryDetailButton.Visibility == Visibility.Visible)
            {
                showSpineInjuryDetailPanel();
            }
            else
            {
                collapseAllBodyPartsInjuryCharacteristic();
            }
        }

        // Update Status Localis
        private void UpdateSpineStatusLocalis()
        {
            spineStatusLocalisTextBox.Text = injuryModel.Spine.GetStatusLocalis();
            SummaryStatusesLocalis();
        }

        // Thermo
        private void spineThermoBurn1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeThermoBurn1();
            UpdateSpineStatusLocalis();

        }

        private void spineThermoBurn2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeThermoBurn2();
            UpdateSpineStatusLocalis();

        }

        private void spineThermoBurn3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeThermoBurn3();
            UpdateSpineStatusLocalis();

        }

        private void spineThermoBurn4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeThermoBurn4();

            UpdateSpineStatusLocalis();

        }

        private void spineFrostbite1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeThermoFrostbite1();
            UpdateSpineStatusLocalis();

        }

        private void spineFrostbite2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeThermoFrostbite2();

            UpdateSpineStatusLocalis();
        }

        private void spineFrostbite3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeThermoFrostbite3();

            UpdateSpineStatusLocalis();
        }

        private void spineFrostbite4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeThermoFrostbite4();
            UpdateSpineStatusLocalis();
        }

        // Injury
        private void spineInjuryBlindRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeWoundCanalBlind();
            UpdateSpineStatusLocalis();

        }

        private void spineInjuryThroughRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeWoundCanalThrough();
            UpdateSpineStatusLocalis();
        }

        private void spineInjuryTangentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeWoundCanalTangent();
            UpdateSpineStatusLocalis();
        }

        private void spineInjuryPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakePenetrated();
            UpdateSpineStatusLocalis();
        }

        private void spineInjuryNotPenetratedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeNotPenetrated();
            UpdateSpineStatusLocalis();
        }
        // Trauma
        private void spineTraumaOpenRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeTraumaOpen();
            UpdateSpineStatusLocalis();
        }

        private void spineTraumaClosedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.MakeTraumaClosed();
            UpdateSpineStatusLocalis();
        }


        // Common
        private void spineVesselsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.AddVesselsDamage();
            UpdateSpineStatusLocalis();
        }


        private void spineVesselsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.DeleteVesselsDamage();
            UpdateSpineStatusLocalis();
        }


        // Special

        private void cordCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.AddCordDamage();
            UpdateSpineStatusLocalis();
        }

        private void cordCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.DeleteCordDamage();
            UpdateSpineStatusLocalis();

        }

        private void rootCordCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Spine.AddRootDamage();
            UpdateSpineStatusLocalis();

        }

        private void rootCordCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.Spine.DeleteRootDamage();
            UpdateSpineStatusLocalis();
        }

        private void bodyCheckbox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.Spine.AddBodyFracture();
            UpdateSpineStatusLocalis();
        }

        private void bodyCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.Spine.DeleteBodyFracture();
            UpdateSpineStatusLocalis();
        }

        private void archesCordCheckbox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.Spine.AddArchesFracture();
            UpdateSpineStatusLocalis();
        }

        private void archesCordCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.Spine.DeleteArchesFracture();
            UpdateSpineStatusLocalis();
        }

        private void processCordCheckbox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.Spine.AddProcessFracture();
            UpdateSpineStatusLocalis();
        }

        private void processCordCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.Spine.DeleteProcessFracture();
            UpdateSpineStatusLocalis();
        }
        #endregion    

        #region RightUpper

        private void frontRightShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.AddLocalisationShoulderFront();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        private void frontRightShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.DeleteLocalisationShoulderFront();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }
        private void frontRightForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.AddLocalisationForearmFront();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        private void frontRightForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.DeleteLocalisationForearmFront();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }


        private void frontRightWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.AddLocalisationWristFront();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        private void frontRightWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.DeleteLocalisationWristFront();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }
        private void frontRightShoulderTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.AddLocalisationShoulderTourniquiet();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        private void frontRightShoulderTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.DeleteLocalisationShoulderTourniquiet();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        private void frontRightForearmTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.AddLocalisationForearmTourniquet();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        private void frontRightForearmTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.DeleteLocalisationForearmTourniquet();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }
        private void backRightShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.AddLocalisationShoulderBack();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        private void backRightShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.DeleteLocalisationShoulderBack();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        private void backRightForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.AddLocalisationForearmBack();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        private void backRightForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.DeleteLocalisationForearmBack();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }




        private void backRightWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.AddLocalisationWristBack();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        private void backRightWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.DeleteLocalisationWristBack();
            CheckIfRightUpperIsDamaged();
            UpdateRightUpperStatusLocalis();
        }

        // Check If damaged
        private void CheckIfRightUpperIsDamaged()
        {
            rightUpperInjuryDetailButton.Visibility = injuryModel.RightUpper.Localisation > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (rightUpperInjuryDetailButton.Visibility == Visibility.Visible)
            {
                showRightUpperInjuryDetailPanel();
            }
            else
            {
                collapseAllBodyPartsInjuryCharacteristic();
            }
        }

        // Update Status Localis
        private void UpdateRightUpperStatusLocalis()
        {
            rightUpperStatusLocalisTextBox.Text = injuryModel.RightUpper.GetStatusLocalis();
            SummaryStatusesLocalis();
        }

        // Thermo
        private void rightUpperThermoBurn1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeThermoBurn1();
            UpdateRightUpperStatusLocalis();

        }

        private void rightUpperThermoBurn2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeThermoBurn2();
            UpdateRightUpperStatusLocalis();

        }

        private void rightUpperThermoBurn3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeThermoBurn3();
            UpdateRightUpperStatusLocalis();

        }

        private void rightUpperThermoBurn4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeThermoBurn4();

            UpdateRightUpperStatusLocalis();

        }

        private void rightUpperFrostbite1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeThermoFrostbite1();
            UpdateRightUpperStatusLocalis();

        }

        private void rightUpperFrostbite2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeThermoFrostbite2();

            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperFrostbite3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeThermoFrostbite3();

            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperFrostbite4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeThermoFrostbite4();
            UpdateRightUpperStatusLocalis();
        }

        // Injury
        private void rightUpperInjuryBlindRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeWoundCanalBlind();
            UpdateRightUpperStatusLocalis();

        }

        private void rightUpperInjuryThroughRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeWoundCanalThrough();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperInjuryTangentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeWoundCanalTangent();
            UpdateRightUpperStatusLocalis();
        }

        // Trauma
        private void rightUpperTraumaOpenRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeTraumaOpen();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperTraumaClosedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.MakeTraumaClosed();
            UpdateRightUpperStatusLocalis();
        }


        // Common
        private void rightUpperVesselsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.AddVesselsDamage();
            UpdateRightUpperStatusLocalis();
        }


        private void rightUpperVesselsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.DeleteVesselsDamage();
            UpdateRightUpperStatusLocalis();
        }


        // Special


        private void rightUpperjointCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.AddJointDamage();
            UpdateRightUpperStatusLocalis();

        }

        private void rightUpperjointCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightUpper.DeleteJointDamage();
            UpdateRightUpperStatusLocalis();
        }
        private void rightUppertrunksCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.AddTrunksDamage();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUppertrunksCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.DeleteTrunksDamage();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUppersoftTissueCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.AddSoftTissueDamage();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUppersoftTissueCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.DeleteSoftTissueDamage();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperdetachmentCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.AddDetachmentDamage();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperdetachmentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.DeleteDetachmentDamage();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperboneFractureCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.AddBoneFracture();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperboneFractureCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.DeleteBoneFracture();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperfullDestructionCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.AddFullDesctruction();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperfullDestructionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.DeleteFullDesctruction();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperpartDestructionCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.AddPartDesctruction();
            UpdateRightUpperStatusLocalis();
        }

        private void rightUpperpartDestructionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightUpper.DeletePartDesctruction();
            UpdateRightUpperStatusLocalis();
        }
        #endregion

        #region LeftUpper

        private void frontLeftShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.AddLocalisationShoulderFront();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }

        private void frontLeftForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.AddLocalisationForearmFront();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }

        private void frontLeftWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.AddLocalisationWristFront();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }

        private void frontLeftShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.DeleteLocalisationShoulderFront();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }

        private void fronLeftForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.DeleteLocalisationForearmFront();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }

        private void frontLeftWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.DeleteLocalisationWristFront();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }
        private void frontLeftShoulderTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.AddLocalisationShoulderTourniquiet();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }

        private void frontLeftShoulderTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.DeleteLocalisationShoulderTourniquiet();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }

        private void frontLeftForearmTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.AddLocalisationForearmTourniquet();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }

        private void frontLeftForearmTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.DeleteLocalisationForearmTourniquet();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }
        private void backLeftShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.AddLocalisationShoulderBack();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }

        private void backLeftShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.DeleteLocalisationShoulderBack();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }

        private void backLeftForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.DeleteLocalisationForearmBack();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }
        private void backLeftForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.AddLocalisationForearmBack();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }


        private void backLeftWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.DeleteLocalisationWristBack();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }


        private void backLeftWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.AddLocalisationWristBack();
            CheckIfLeftUpperIsDamaged();
            UpdateLeftUpperStatusLocalis();
        }


        // Check If damaged
        private void CheckIfLeftUpperIsDamaged()
        {
            leftUpperInjuryDetailButton.Visibility = injuryModel.LeftUpper.Localisation > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (leftUpperInjuryDetailButton.Visibility == Visibility.Visible)
            {
                showLeftUpperInjuryDetailPanel();
            }
            else
            {
                collapseAllBodyPartsInjuryCharacteristic();
            }
        }

        // Update Status Localis
        private void UpdateLeftUpperStatusLocalis()
        {
            leftUpperStatusLocalisTextBox.Text = injuryModel.LeftUpper.GetStatusLocalis();
            SummaryStatusesLocalis();
        }

        // Thermo
        private void leftUpperThermoBurn1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeThermoBurn1();
            UpdateLeftUpperStatusLocalis();

        }

        private void leftUpperThermoBurn2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeThermoBurn2();
            UpdateLeftUpperStatusLocalis();

        }

        private void leftUpperThermoBurn3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeThermoBurn3();
            UpdateLeftUpperStatusLocalis();

        }

        private void leftUpperThermoBurn4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeThermoBurn4();

            UpdateLeftUpperStatusLocalis();

        }

        private void leftUpperFrostbite1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeThermoFrostbite1();
            UpdateLeftUpperStatusLocalis();

        }

        private void leftUpperFrostbite2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeThermoFrostbite2();

            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperFrostbite3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeThermoFrostbite3();

            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperFrostbite4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeThermoFrostbite4();
            UpdateLeftUpperStatusLocalis();
        }

        // Injury
        private void leftUpperInjuryBlindRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeWoundCanalBlind();
            UpdateLeftUpperStatusLocalis();

        }

        private void leftUpperInjuryThroughRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeWoundCanalThrough();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperInjuryTangentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeWoundCanalTangent();
            UpdateLeftUpperStatusLocalis();
        }

        // Trauma
        private void leftUpperTraumaOpenRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeTraumaOpen();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperTraumaClosedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.MakeTraumaClosed();
            UpdateLeftUpperStatusLocalis();
        }


        // Common
        private void leftUpperVesselsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.AddVesselsDamage();
            UpdateLeftUpperStatusLocalis();
        }


        private void leftUpperVesselsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.DeleteVesselsDamage();
            UpdateLeftUpperStatusLocalis();
        }


        // Special


        private void leftUpperjointCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.AddJointDamage();
            UpdateLeftUpperStatusLocalis();

        }

        private void leftUpperjointCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftUpper.DeleteJointDamage();
            UpdateLeftUpperStatusLocalis();
        }
        private void leftUppertrunksCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.AddTrunksDamage();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUppertrunksCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.DeleteTrunksDamage();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUppersoftTissueCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.AddSoftTissueDamage();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUppersoftTissueCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.DeleteSoftTissueDamage();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperdetachmentCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.AddDetachmentDamage();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperdetachmentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.DeleteDetachmentDamage();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperboneFractureCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.AddBoneFracture();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperboneFractureCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.DeleteBoneFracture();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperfullDestructionCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.AddFullDesctruction();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperfullDestructionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.DeleteFullDesctruction();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperpartDestructionCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.AddPartDesctruction();
            UpdateLeftUpperStatusLocalis();
        }

        private void leftUpperpartDestructionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftUpper.DeletePartDesctruction();
            UpdateLeftUpperStatusLocalis();
        }
        #endregion

        #region Right Lower
        private void frontRightHipTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.AddFrontHipTourniquiet();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void frontRightHipTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.DeleteFrontHipTourniquiet();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void frontRightHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.AddFrontHip();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void frontRightHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.DeleteFrontHip();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void frontRightShinTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.AddFrontShinTourniquiet();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void frontRightShinTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.DeleteFrontShinTourniquiet();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void frontRightShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.AddFrontShin();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void frontRightShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.DeleteFrontShin();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void frontRightFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.AddFrontFoot();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void frontRightFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.DeleteFrontFoot();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }


        private void backRightHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.AddBackHip();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void backRightHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.DeleteBackHip();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }


        private void backRightShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.AddBackShin();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void backRightShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.DeleteBackShin();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }


        private void backRightFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.AddBackFoot();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }

        private void backRightFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.DeleteBackFoot();
            CheckIfRightLowerIsDamaged();
            UpdateRightLowerStatusLocalis();
        }


        // Check If damaged
        private void CheckIfRightLowerIsDamaged()
        {
            rightLowerInjuryDetailButton.Visibility = injuryModel.RightLower.Localisation > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (rightLowerInjuryDetailButton.Visibility == Visibility.Visible)
            {
                showRightLowerInjuryDetailPanel();
            }
            else
            {
                collapseAllBodyPartsInjuryCharacteristic();
            }
        }

        // Update Status Localis
        private void UpdateRightLowerStatusLocalis()
        {
            rightLowerStatusLocalisTextBox.Text = injuryModel.RightLower.GetStatusLocalis();
            SummaryStatusesLocalis();
        }

        // Thermo
        private void rightLowerThermoBurn1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeThermoBurn1();
            UpdateRightLowerStatusLocalis();

        }

        private void rightLowerThermoBurn2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeThermoBurn2();
            UpdateRightLowerStatusLocalis();

        }

        private void rightLowerThermoBurn3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeThermoBurn3();
            UpdateRightLowerStatusLocalis();

        }

        private void rightLowerThermoBurn4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeThermoBurn4();

            UpdateRightLowerStatusLocalis();

        }

        private void rightLowerFrostbite1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeThermoFrostbite1();
            UpdateRightLowerStatusLocalis();

        }

        private void rightLowerFrostbite2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeThermoFrostbite2();

            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerFrostbite3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeThermoFrostbite3();

            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerFrostbite4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeThermoFrostbite4();
            UpdateRightLowerStatusLocalis();
        }

        // Injury
        private void rightLowerInjuryBlindRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeWoundCanalBlind();
            UpdateRightLowerStatusLocalis();

        }

        private void rightLowerInjuryThroughRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeWoundCanalThrough();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerInjuryTangentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeWoundCanalTangent();
            UpdateRightLowerStatusLocalis();
        }

        // Trauma
        private void rightLowerTraumaOpenRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeTraumaOpen();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerTraumaClosedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.MakeTraumaClosed();
            UpdateRightLowerStatusLocalis();
        }


        // Common
        private void rightLowerVesselsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.AddVesselsDamage();
            UpdateRightLowerStatusLocalis();
        }


        private void rightLowerVesselsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.DeleteVesselsDamage();
            UpdateRightLowerStatusLocalis();
        }


        // Special


        private void rightLowerjointCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.AddJointDamage();
            UpdateRightLowerStatusLocalis();

        }

        private void rightLowerjointCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.RightLower.DeleteJointDamage();
            UpdateRightLowerStatusLocalis();
        }
        private void rightLowertrunksCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.AddTrunksDamage();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowertrunksCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.DeleteTrunksDamage();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowersoftTissueCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.AddSoftTissueDamage();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowersoftTissueCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.DeleteSoftTissueDamage();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerdetachmentCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.AddDetachmentDamage();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerdetachmentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.DeleteDetachmentDamage();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerboneFractureCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.AddBoneFracture();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerboneFractureCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.DeleteBoneFracture();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerfullDestructionCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.AddFullDesctruction();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerfullDestructionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.DeleteFullDesctruction();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerpartDestructionCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.AddPartDesctruction();
            UpdateRightLowerStatusLocalis();
        }

        private void rightLowerpartDestructionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.RightLower.DeletePartDesctruction();
            UpdateRightLowerStatusLocalis();
        }

        #endregion

        #region Left Lower
        private void frontLeftHipTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.AddFrontHipTourniquiet();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void frontLeftHipTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.DeleteFrontHipTourniquiet();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void frontLeftHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.AddFrontHip();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void frontLeftHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.DeleteFrontHip();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void frontLeftShinTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.AddFrontShinTourniquiet();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void frontLeftShinTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.DeleteFrontShinTourniquiet();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void frontLeftShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.AddFrontShin();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void frontLeftShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.DeleteFrontShin();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void frontLeftFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.AddFrontFoot();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void frontLeftFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.DeleteFrontFoot();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }


        private void backLeftHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.AddBackHip();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void backLeftHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.DeleteBackHip();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }


        private void backLeftShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.AddBackShin();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void backLeftShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.DeleteBackShin();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }


        private void backLeftFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.AddBackFoot();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }

        private void backLeftFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.DeleteBackFoot();
            CheckIfLeftLowerIsDamaged();
            UpdateLeftLowerStatusLocalis();
        }


        // Check If damaged
        private void CheckIfLeftLowerIsDamaged()
        {
            leftLowerInjuryDetailButton.Visibility = injuryModel.LeftLower.Localisation > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (leftLowerInjuryDetailButton.Visibility == Visibility.Visible)
            {
                showLeftLowerInjuryDetailPanel();
            }
            else
            {
                collapseAllBodyPartsInjuryCharacteristic();
            }
        }

        // Update Status Localis
        private void UpdateLeftLowerStatusLocalis()
        {
            leftLowerStatusLocalisTextBox.Text = injuryModel.LeftLower.GetStatusLocalis();
            SummaryStatusesLocalis();
        }

        // Thermo
        private void leftLowerThermoBurn1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeThermoBurn1();
            UpdateLeftLowerStatusLocalis();

        }

        private void leftLowerThermoBurn2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeThermoBurn2();
            UpdateLeftLowerStatusLocalis();

        }

        private void leftLowerThermoBurn3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeThermoBurn3();
            UpdateLeftLowerStatusLocalis();

        }

        private void leftLowerThermoBurn4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeThermoBurn4();

            UpdateLeftLowerStatusLocalis();

        }

        private void leftLowerFrostbite1RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeThermoFrostbite1();
            UpdateLeftLowerStatusLocalis();

        }

        private void leftLowerFrostbite2RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeThermoFrostbite2();

            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerFrostbite3RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeThermoFrostbite3();

            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerFrostbite4RadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeThermoFrostbite4();
            UpdateLeftLowerStatusLocalis();
        }

        // Injury
        private void leftLowerInjuryBlindRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeWoundCanalBlind();
            UpdateLeftLowerStatusLocalis();

        }

        private void leftLowerInjuryThroughRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeWoundCanalThrough();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerInjuryTangentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeWoundCanalTangent();
            UpdateLeftLowerStatusLocalis();
        }

        // Trauma
        private void leftLowerTraumaOpenRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeTraumaOpen();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerTraumaClosedRadioButton_Click(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.MakeTraumaClosed();
            UpdateLeftLowerStatusLocalis();
        }


        // Common
        private void leftLowerVesselsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.AddVesselsDamage();
            UpdateLeftLowerStatusLocalis();
        }


        private void leftLowerVesselsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.DeleteVesselsDamage();
            UpdateLeftLowerStatusLocalis();
        }


        // Special


        private void leftLowerjointCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.AddJointDamage();
            UpdateLeftLowerStatusLocalis();

        }

        private void leftLowerjointCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.LeftLower.DeleteJointDamage();
            UpdateLeftLowerStatusLocalis();
        }
        private void leftLowertrunksCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.AddTrunksDamage();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowertrunksCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.DeleteTrunksDamage();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowersoftTissueCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.AddSoftTissueDamage();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowersoftTissueCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.DeleteSoftTissueDamage();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerdetachmentCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.AddDetachmentDamage();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerdetachmentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.DeleteDetachmentDamage();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerboneFractureCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.AddBoneFracture();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerboneFractureCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.DeleteBoneFracture();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerfullDestructionCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.AddFullDesctruction();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerfullDestructionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.DeleteFullDesctruction();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerpartDestructionCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.AddPartDesctruction();
            UpdateLeftLowerStatusLocalis();
        }

        private void leftLowerpartDestructionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.LeftLower.DeletePartDesctruction();
            UpdateLeftLowerStatusLocalis();
        }


        #endregion

        #region Additional Info
        private void SeizuresCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            conditionModel.AdditionalInfo |= AdditionalInfoEnum.Seizures;
        }

        private void SeizuresCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            conditionModel.AdditionalInfo ^= AdditionalInfoEnum.Seizures;

        }

        private void ExcitementCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            conditionModel.AdditionalInfo |= AdditionalInfoEnum.Excitement;

        }

        private void ExcitementCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            conditionModel.AdditionalInfo ^= AdditionalInfoEnum.Excitement;

        }

        private void HallucinationsCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            conditionModel.AdditionalInfo |= AdditionalInfoEnum.Hallucinations;

        }

        private void HallucinationsCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            conditionModel.AdditionalInfo ^= AdditionalInfoEnum.Hallucinations;

        }

        private void VomitingCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            conditionModel.AdditionalInfo |= AdditionalInfoEnum.Vomiting;

        }

        private void VomitingCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            conditionModel.AdditionalInfo ^= AdditionalInfoEnum.Vomiting;

        }

        private void DiarrheaCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            conditionModel.AdditionalInfo |= AdditionalInfoEnum.Diarrhea;

        }

        private void DiarrheaCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            conditionModel.AdditionalInfo ^= AdditionalInfoEnum.Diarrhea;

        }
        #endregion

        #region DrainageDecompression
        private void decompressionOfThePleuralCavityRightCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            helpProvided.DecompressionOfThePleuralCavity |= DecompressionOfThePleuralCavityEnum.Right;
            SummaryProvidedHelp();
        }

        private void decompressionOfThePleuralCavityRightCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            helpProvided.DecompressionOfThePleuralCavity ^= DecompressionOfThePleuralCavityEnum.Right;
            SummaryProvidedHelp();
        }

        private void decompressionOfThePleuralCavityLeftCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            helpProvided.DecompressionOfThePleuralCavity |= DecompressionOfThePleuralCavityEnum.Left;
            SummaryProvidedHelp();
        }

        private void decompressionOfThePleuralCavityLeftCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            helpProvided.DecompressionOfThePleuralCavity ^= DecompressionOfThePleuralCavityEnum.Left;
            SummaryProvidedHelp();
        }

        private void drainageOfThePleuralCavityRightCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            helpProvided.DrainageOfThePleuralCavity |= DrainageOfThePleuralCavityEnum.Right;
            SummaryProvidedHelp();
        }

        private void drainageOfThePleuralCavityRightCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            helpProvided.DrainageOfThePleuralCavity ^= DrainageOfThePleuralCavityEnum.Right;
            SummaryProvidedHelp();

        }

        private void drainageOfThePleuralCavityLeftCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            helpProvided.DrainageOfThePleuralCavity |= DrainageOfThePleuralCavityEnum.Left;
            SummaryProvidedHelp();

        }

        private void drainageOfThePleuralCavityLeftCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            helpProvided.DrainageOfThePleuralCavity ^= DrainageOfThePleuralCavityEnum.Left;
            SummaryProvidedHelp();

        }
        #endregion

        #region StopBleeding
        // TODO: При написании времени вылетает ошибка!
        private void TimeTourniquetAppliedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                helpProvided.TimeTourniquetApplied = DateTime.Parse(TimeTourniquetAppliedTextBox.Text);
                SummaryProvidedHelp();

            }
            catch
            {
                MessageBox.Show("Неправильно задано время!");
            }

        }

        private void HideTimeTourniquetAppliedStackPanel()
        {
            TimeTourniqAppliedStackPanel.Visibility = Visibility.Hidden;

        }

        private void ShowTimeTourniquetAppliedStackPanel()
        {
            TimeTourniqAppliedStackPanel.Visibility = Visibility.Visible;
            TimeTourniquetAppliedTextBox.Text = DateTime.Now.ToString();
            SummaryProvidedHelp();

        }

        private void NoNeedTourniquetRadioButton_Click(object sender, RoutedEventArgs e)
        {
            HideTimeTourniquetAppliedStackPanel();
            helpProvided.WayStopBleeding = 0;
            SummaryProvidedHelp();

        }

        private void TourniquetRadioButton_Click(object sender, RoutedEventArgs e)
        {
            ShowTimeTourniquetAppliedStackPanel();
            helpProvided.WayStopBleeding = WayStopBleedingEnum.Tourniquet;
            SummaryProvidedHelp();

        }

        private void PressureBandageRadioButton_Click(object sender, RoutedEventArgs e)
        {
            ShowTimeTourniquetAppliedStackPanel();
            helpProvided.WayStopBleeding = WayStopBleedingEnum.PressureBandage;
            SummaryProvidedHelp();

        }

        private void OtherWayStopBleedingRadioButton_Click(object sender, RoutedEventArgs e)
        {
            ShowTimeTourniquetAppliedStackPanel();
            helpProvided.WayStopBleeding = WayStopBleedingEnum.Other;
            SummaryProvidedHelp();

        }
        #endregion

        #region Immobilization
        private void NoImmobilizationRadioButton_Click(object sender, RoutedEventArgs e)
        {
            helpProvided.Immobilization = 0;
            SummaryProvidedHelp();
        }

        private void StandartImmobilizationRadioButton_Click(object sender, RoutedEventArgs e)
        {
            helpProvided.Immobilization = ImmobilizationEnum.Standart;

            SummaryProvidedHelp();
        }

        private void OtherImmobilizationRadioButton_Click(object sender, RoutedEventArgs e)
        {
            helpProvided.Immobilization = ImmobilizationEnum.Other;
            SummaryProvidedHelp();

        }
        #endregion

        #region Transfusions
        private void NaClTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                helpProvided.NaCl = int.Parse(NaClTextBox.Text);
                SummaryProvidedHelp();
            }
            catch
            {
                MessageBox.Show("Неправильно введено число! Только целые числа.");
                NaClTextBox.Text = "0";
            }

        }

        private void NaHCO3TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                helpProvided.NaHC03 = int.Parse(NaHCO3TextBox.Text);
                SummaryProvidedHelp();
            }
            catch
            {
                MessageBox.Show("Неправильно введено число! Только целые числа.");
                NaHCO3TextBox.Text = "0";
            }
        }

        private void GlucoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                helpProvided.Glucose5 = int.Parse(GlucoseTextBox.Text);
                SummaryProvidedHelp();
            }
            catch
            {
                MessageBox.Show("Неправильно введено число! Только целые числа.");
                GlucoseTextBox.Text = "0";
            }
        }

        private void ErTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                helpProvided.Er = int.Parse(ErTextBox.Text);
                SummaryProvidedHelp();
            }
            catch
            {
                MessageBox.Show("Неправильно введено число! Только целые числа.");
                ErTextBox.Text = "0";
            }
        }

        private void SzpTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                helpProvided.Szp = int.Parse(SzpTextBox.Text);
                SummaryProvidedHelp();
            }
            catch
            {
                MessageBox.Show("Неправильно введено число! Только целые числа.");
                SzpTextBox.Text = "0";
            }
        }
        #endregion

        #region TODO: ToSort
        private void ComplaintsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            conditionModel.Complaints = ComplaintsTextBox.Text;
            SummaryCondition();
        }

        private void IntensiveCareTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                helpProvided.IntensiveCareMeasuresTimeSpent = int.Parse(IntensiveCareTimeTextBox.Text);
                SummaryProvidedHelp();
            }

            catch
            {
                MessageBox.Show("Не заадана продолжительность реанимационных мероприятий!");

            }
        }

        private void IntensiveCareMeasuressSuccess_Checked(object sender, RoutedEventArgs e)
        {
            helpProvided.IntensiveCareMeasures = IntensiveCareMeasuresEnum.Success;
            try
            {
                helpProvided.IntensiveCareMeasuresTimeSpent = int.Parse(IntensiveCareTimeTextBox.Text);
                LethalTimeStackPanel.Visibility = Visibility.Hidden;
                helpProvided.LethalDateTime = null;
                SummaryProvidedHelp();
            }
            catch
            {
                MessageBox.Show("Не заадана продолжительность реанимационных мероприятий!");

            }
        }

        private void IntensiveCareMeasuressLethal_Checked(object sender, RoutedEventArgs e)
        {
            LethalTimeStackPanel.Visibility = Visibility.Visible;
            LethalTimeTextBox.Text = DateTime.Now.AddMinutes(30).ToString();
            helpProvided.IntensiveCareMeasures = IntensiveCareMeasuresEnum.Lethal;
            helpProvided.LethalDateTime = DateTime.Now.AddMinutes(30);

            try
            {
                helpProvided.IntensiveCareMeasuresTimeSpent = int.Parse(IntensiveCareTimeTextBox.Text);
                SummaryProvidedHelp();
            }
            catch
            {
                MessageBox.Show("Не заадана продолжительность реанимационных мероприятий!");
            }
        }

        private void IntensiveCareWereRadioButton_Click(object sender, RoutedEventArgs e)
        {
            IntensiveCareStackPanel.Visibility = Visibility.Visible;
        }

        private void IntensiveCareWerentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            IntensiveCareStackPanel.Visibility = Visibility.Hidden;
            helpProvided.IntensiveCareMeasures = 0;
            SummaryProvidedHelp();
        }

        private void headStatusLocalisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryModel.Head.StatusLocalis = headStatusLocalisTextBox.Text;
            SummaryStatusesLocalis();
        }

        private void neckStatusLocalisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryModel.Neck.StatusLocalis = neckStatusLocalisTextBox.Text;
            SummaryStatusesLocalis();
        }

        private void thoraxStatusLocalisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryModel.Thorax.StatusLocalis = thoraxStatusLocalisTextBox.Text;
            SummaryStatusesLocalis();
        }

        private void abdomenStatusLocalisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryModel.Abdomen.StatusLocalis = abdomenStatusLocalisTextBox.Text;
            SummaryStatusesLocalis();
        }

        private void pelvisStatusLocalisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryModel.Pelvis.StatusLocalis = pelvisStatusLocalisTextBox.Text;
            SummaryStatusesLocalis();
        }

        private void spineStatusLocalisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryModel.Spine.StatusLocalis = spineStatusLocalisTextBox.Text;
            SummaryStatusesLocalis();
        }

        private void rightUpperStatusLocalisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryModel.RightUpper.StatusLocalis = rightUpperStatusLocalisTextBox.Text;
            SummaryStatusesLocalis();
        }

        private void leftUpperStatusLocalisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryModel.LeftUpper.StatusLocalis = leftUpperStatusLocalisTextBox.Text;
            SummaryStatusesLocalis();
        }

        private void rightLowerStatusLocalisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryModel.RightLower.StatusLocalis = rightLowerStatusLocalisTextBox.Text;
            SummaryStatusesLocalis();
        }

        private void leftLowerStatusLocalisTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            injuryModel.LeftLower.StatusLocalis = leftLowerStatusLocalisTextBox.Text;
            SummaryStatusesLocalis();
        }

        private void helpProvidedSummaryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            helpProvided.HelpProvidedSummary = helpProvidedSummaryTextBox.Text;
        }

        private void conditionSummaryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            conditionModel.Condition = conditionSummaryTextBox.Text;
        }
        #endregion

        #region Form100PesonalInfo

        private void issuedWhenTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                form100Model.IssuedWhen = DateTime.Parse(issuedWhenTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Неправильно указана дата выдачи Формы 100!");
            }
        }

        private void personLastNameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            form100Model.LastName = personFirstNameTextbox.Text;
        }

        private void personFirstNameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            form100Model.FirstName = personLastNameTextbox.Text;

        }

        private void personSecondNameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            form100Model.SecondName = personSecondNameTextbox.Text;

        }

        private void birthDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            form100Model.BirthDate = DateOnly.Parse(birthDateDatePicker.Text);
        }

        private void militaryIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            form100Model.MilitaryId = militaryIdTextBox.Text;
        }

        private void militaryUnitTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            form100Model.MilitaryUnit = militaryUnitTextBox.Text;
        }

        private void ranksDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            form100Model.Rank = ((RankModel)ranksDropdown.SelectedItem).Id;
            form100Model.RankTitle = ((RankModel)ranksDropdown.SelectedItem).RankTitle;

        }

        private void dutyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            form100Model.Duty = dutyTextBox.Text;
        }

        private void BattleGunshotRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.Reason = ReasonEnum.Gunshot;
        }


        private void OtherRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.Reason = ReasonEnum.Other;

        }

        private void FrostbiteRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.Reason = ReasonEnum.Frostbite;

        }

        private void NucleoRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.Reason = ReasonEnum.Nucleo;

        }

        private void ChemistryRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.Reason = ReasonEnum.Chemistry;

        }

        private void BacterialRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.Reason = ReasonEnum.Bacterial;

        }

        private void InfectionRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.Reason = ReasonEnum.Infection;

        }

        private void BodyRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.Reason = ReasonEnum.Body;

        }

        private void WithoutFirstAidCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            form100Model.WithoutFirstAid = 1;
        }

        private void WithoutFirstAidCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            form100Model.WithoutFirstAid = 0;

        }

        private void diseaseTimeTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                form100Model.DiseaseTime = DateTime.Parse(diseaseTimeTextbox.Text);
            }
            catch
            {
                MessageBox.Show("Неправильно указана дата ранения(заболевания)!");
            }
        }

        private void evacWayRadioButton_1_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationWay = EvacuationWayEnum.Company;
        }

        private void evacWayRadioButton_2_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationWay = EvacuationWayEnum.Battalion;

        }

        private void evacWayRadioButton_3_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationWay = EvacuationWayEnum.Hospital;

        }

        private void FirstOrderRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationOrder = EvacuationOrderEnum.First;
        }

        private void SecondOrderRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationOrder = EvacuationOrderEnum.Second;

        }

        private void ThirdOrderRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationOrder = EvacuationOrderEnum.Third;

        }

        private void evacTransportRadioButton_1_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationTransport = EvacuationTransportEnum.Am;
        }

        private void evacTransportRadioButton_2_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationTransport = EvacuationTransportEnum.Am;

        }

        private void evacTransportRadioButton_3_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationTransport = EvacuationTransportEnum.MedAm;

        }

        private void evacTransportRadioButton_4_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationTransport = EvacuationTransportEnum.Helicopter;

        }

        private void evacTransportRadioButton_5_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationTransport = EvacuationTransportEnum.Airplane;

        }

        private void layingPositionRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationPosition = EvacuationPositionEnum.LyingDown;
        }

        private void sittingPositionRadioButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.EvacuationPosition = EvacuationPositionEnum.Sitting;

        }

        private void LifeThreateningConditionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            form100Model.Special |= SpecialEnum.LifeThreateningCondition;
        }

        private void LifeThreateningConditionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            form100Model.Special ^= SpecialEnum.LifeThreateningCondition;

        }

        private void IsolationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            form100Model.Special |= SpecialEnum.Isolation;

        }

        private void IsolationCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            form100Model.Special ^= SpecialEnum.Isolation;

        }

        private void RadiationDamageCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            form100Model.Special |= SpecialEnum.RadiationDamage;

        }

        private void RadiationDamageCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            form100Model.Special ^= SpecialEnum.RadiationDamage;

        }

        private void SanitaryTreatmentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            form100Model.Special |= SpecialEnum.SanitaryTreatment;

        }

        private void SanitaryTreatmentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            form100Model.Special ^= SpecialEnum.RadiationDamage;

        }

        private void evacAddressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            form100Model.EvacAddress = evacAddressTextBox.Text;
        }

        private void evacTimeTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                form100Model.EvacTime = DateTime.Parse(evacTimeTextbox.Text);
            }
            catch
            {
                MessageBox.Show("Неправильно указана дата эвакуации!");
            }
        }

        private void docTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            form100Model.Doc = docTextBox.Text;

        }
        #endregion

        private void saveFromButton_Click(object sender, RoutedEventArgs e)
        {
            form100Model.InjuryStatusLocalis = injuryModel;
            form100Model.Condition = conditionModel;
            form100Model.HelpProvided = helpProvided;
            string savePath;
            Dictionary<string, string> keyValuePairs = new();

            WordDocumentDocx doc = new();
            if (string.IsNullOrWhiteSpace(defaultsModel.SavePath))
            {
                MessageBox.Show("Не указан путь сохранения форм 100! Сохраняю в папку с приложением");
                savePath = defaultsModel.GetDefaultSavePath();
            }
            else
            {
                savePath = defaultsModel.SavePath;
            }
            string filePath = doc.CreateForm100(keyValuePairs, savePath);
            // AppHelper.Print(filePath);
            string argument = "/select," + filePath;

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }
    }
}
