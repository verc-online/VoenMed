using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VoenMedLibrary.DataAccess;
using VoenMedLibrary.Models;
using VoenMedLibrary.Models.InjuryModels;

namespace VoenMed.Controls
{
    /// <summary>
    /// Interaction logic for Form100Control.xaml
    /// </summary>
    public partial class Form100Control : UserControl
    {
        DefaultsModel model;
        List<RankModel> ranks = new(); // Звания
        List<DrugModel> drugs = new(); // Лекарства

        // То, что отправляется в базу данных
        List<ProvidedDrugModel> providedDrugsModel = new(); // Использованные лекарства
        InjuryStatusLocalisModel injuryModel = new(); // Локализация и описание повреждений

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

        #region InjuryLocalis
        // Когда нажимаешь на чекбокс должен выполнять битовое сложение, потом записывать число в бд


        private void frontRightShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightUpperShoulderFront();
        }

        private void fronRightForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightUpperForearmFront();
        }

        private void fronRightWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightUpperWristFront();
        }

        private void frontRightShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightUpperShoulderFront();
        }

        private void fronRightForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightUpperForearmFront();
        }

        private void fronRightWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightUpperWristFront();
        }

        private void frontLeftShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftUpperShoulderFront();
        }

        private void fronLeftForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftUpperForearmFront();
        }

        private void fronLeftWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftUpperWristFront();
        }

        private void frontLeftShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperShoulderFront();
        }

        private void fronLeftForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperForearmFront();
        }

        private void fronLeftWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperWristFront();
        }

        private void backLeftShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperShoulderBack();
        }

        private void backLeftForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperForearmBack();
        }

        private void backLeftWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperWristBack();
        }

        private void frontRightShoulderTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightUpperShoulderTourniquiet();
        }

        private void frontRightShoulderTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightUpperShoulderTourniquiet();
        }

        private void fronRightForearmTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightUpperForearmTourniquet();
        }

        private void fronRightForearmTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightUpperForearmTourniquet();
        }

        private void frontLeftShoulderTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftUpperShoulderTourniquiet();
        }

        private void frontLeftShoulderTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperShoulderTourniquiet();
        }

        private void fronLeftForearmTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftUpperForearmTourniquet();
        }

        private void fronLeftForearmTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperForearmTourniquet();
        }

        private void frontRightHipTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightFrontHipTourniquiet();
        }

        private void frontRightHipTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightFrontHipTourniquiet();
        }

        private void frontRightHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightFrontHip();
        }

        private void frontRightHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightFrontHip();
        }

        private void frontRightShinTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightFrontShinTourniquiet();
        }

        private void frontRightShinTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightFrontShinTourniquiet();
        }

        private void frontRightShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightFrontShin();
        }

        private void frontRightShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightFrontShin();
        }

        private void frontRightFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightFrontFoot();
        }

        private void frontRightFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightFrontFoot();
        }

        private void frontLeftHipTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftFrontHipTourniquiet();
        }

        private void frontLeftHipTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftFrontHipTourniquiet();
        }

        private void frontLeftShinTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftFrontShinTourniquiet();
        }

        private void frontLeftShinTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftFrontShinTourniquiet();
        }

        private void frontLeftShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftFrontShin();
        }

        private void frontLeftShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftFrontShin();
        }

        private void frontLeftFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftFrontFoot();
        }

        private void frontLeftFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftFrontFoot(); ;
        }


        private void backLeftShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperShoulderBack();
        }

        private void backRightShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightUpperShoulderBack();
        }

        private void backRightShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightUpperShoulderBack();
        }

        private void backLeftForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperForearmBack();
        }
        private void backRightForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightUpperForearmBack();
        }

        private void backRightForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightUpperForearmBack();
        }

        private void backLeftWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftUpperWristBack();
        }

        private void backRightHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightBackHip();
        }

        private void backRightHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightBackHip();
        }

        private void backLeftHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftBackHip();
        }

        private void backLeftHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftBackHip();
        }

        private void backRightWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightUpperWristBack();
        }

        private void backRightWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightUpperWristBack();
        }

        private void backLeftShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftBackShin();
        }

        private void backLeftShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftBackShin();
        }

        private void backRightShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightBackShin();
        }

        private void backRightShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightBackShin();
        }

        private void backLeftFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftBackFoot();
        }

        private void backLeftFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftBackFoot();
        }

        private void backRightFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddRightBackFoot();
        }

        private void backRightFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteRightBackFoot();
        }

        private void frontLeftHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.AddLeftFrontHip();
        }

        private void frontLeftHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryModel.DeleteLeftFrontHip();
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

        #region NewForm
        private void InitializeNewForm()
        {
            issuedWhenTextBox.Text = DateTime.Now.ToString();
            evacTimeTextbox.Text = DateTime.Now.AddHours(1).ToString();
            diseaseTimeTextbox.Text = DateTime.Now.AddHours(-1).ToString();
        }
        private void LoadDefaultsFromDatabase()
        {
            string sqlStatement = "SELECT * FROM Defaults";
            model = SqliteDataAccess.LoadData<DefaultsModel>(sqlStatement, new Dictionary<string, object>()).FirstOrDefault();

            if (model != null)
            {
                docTextBox.Text = model.Doc.ToString();
                evacAddressTextBox.Text = model.EvacAddress.ToString();

                switch (model.EvacTransport)
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

                switch (model.EvacWay)
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

        #endregion

        #region Drugs
        private void InitializeFavDrugs()
        {
            favDrug1Button.DataContext = drugs.Where(x => x.Id == model.FavDrug1Id).First();
            favDrug1Button.Content = drugs.Where(x => x.Id == model.FavDrug1Id).First().Title;

            favDrug2Button.DataContext = drugs.Where(x => x.Id == model.FavDrug2Id).First();
            favDrug2Button.Content = drugs.Where(x => x.Id == model.FavDrug2Id).First().Title;

            favDrug3Button.DataContext = drugs.Where(x => x.Id == model.FavDrug3Id).First();
            favDrug3Button.Content = drugs.Where(x => x.Id == model.FavDrug3Id).First().Title;

            favDrug4Button.DataContext = drugs.Where(x => x.Id == model.FavDrug4Id).First();
            favDrug4Button.Content = drugs.Where(x => x.Id == model.FavDrug4Id).First().Title;

        }
        private void InitializeDrugList()
        {
            string sql = "select * from Drugs;";
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
                providedDrugsModel.Where(d => d.Id == drug.Id).ToList().ForEach(d => d.Dose += drug.Dose);
            }
            else
            {
                providedDrugsModel.Add(drug);

            }
            drugsProvidedList.ItemsSource = new ObservableCollection<ProvidedDrugModel>(providedDrugsModel);
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
            abdomenTraumaStackPanel.Visibility = Visibility.Visible;
            abdomenInjuryStackPanel.Visibility = Visibility.Collapsed;
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
            neckTraumaStackPanel.Visibility = Visibility.Visible;
            neckInjuryStackPanel.Visibility = Visibility.Collapsed;

            // Thorax
            thoraxTraumaStackPanel.Visibility = Visibility.Visible;
            thoraxInjuryStackPanel.Visibility = Visibility.Collapsed;

            // Abdomen
            abdomenTraumaStackPanel.Visibility = Visibility.Visible;
            abdomenInjuryStackPanel.Visibility = Visibility.Collapsed;

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
                damagedAreaDetailStackPanel.Visibility= Visibility.Visible;
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
        }

        private void pelvisFrontCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.Pelvis.DeletePelvisFront();
            CheckIfPelvisIsDamaged();
        }
        private void pelvisBackCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            injuryModel.Pelvis.AddPelvisBack();
            CheckIfPelvisIsDamaged();
        }

        private void pelvisBackCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            injuryModel.Pelvis.DeletePelvisBack();
            CheckIfPelvisIsDamaged();
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
        #endregion

        private void spineCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void spineCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void bladderCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void urethraCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void urethraCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void rectumCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rectumCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void pevlisVesselsCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void pevlisVesselsCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void hollowCheckbox_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void hollowCheckbox_Unchecked_1(object sender, RoutedEventArgs e)
        {

        }

        private void hollowPevlisCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void hollowPevlisCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void pelvicBonesCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void pelvicBonesCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void sacrumCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void sacrumCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void coccyxCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
