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
        List<ProvidedDrugModel> providedDrugs = new(); // Использованные лекарства
        InjuryStatusLocalisModel injuryStatusLocalis = new(); // Локализация и описание повреждений
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

        #region Localis
        // Когда нажимаешь на чекбокс должен выполнять битовое сложение, потом записывать число в бд

        private void frontHeadInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddHeadFront();
        }

        private void frontHeadInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteHeadFront();
        }

        private void frontRightShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightUpperShoulderFront();
        }

        private void fronRightForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightUpperForearmFront();
        }

        private void fronRightWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightUpperWristFront();
        }

        private void frontRightShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightUpperShoulderFront();
        }

        private void fronRightForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightUpperForearmFront();
        }

        private void fronRightWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightUpperWristFront();
        }

        private void frontLeftShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftUpperShoulderFront();
        }

        private void fronLeftForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftUpperForearmFront();
        }

        private void fronLeftWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftUpperWristFront();
        }

        private void frontLeftShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperShoulderFront();
        }

        private void fronLeftForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperForearmFront();
        }

        private void fronLeftWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperWristFront();
        }

        private void backLeftShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperShoulderBack();
        }

        private void backLeftForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperForearmBack();
        }

        private void backLeftWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperWristBack();
        }

        private void frontRightShoulderTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightUpperShoulderTourniquiet();
        }

        private void frontRightShoulderTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightUpperShoulderTourniquiet();
        }

        private void fronRightForearmTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightUpperForearmTourniquet();
        }

        private void fronRightForearmTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightUpperForearmTourniquet();
        }

        private void frontLeftShoulderTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftUpperShoulderTourniquiet();
        }

        private void frontLeftShoulderTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperShoulderTourniquiet();
        }

        private void fronLeftForearmTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftUpperForearmTourniquet();
        }

        private void fronLeftForearmTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperForearmTourniquet();
        }

        private void frontRightHipTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightFrontHipTourniquiet();
        }

        private void frontRightHipTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightFrontHipTourniquiet();
        }

        private void frontRightHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightFrontHip();
        }

        private void frontRightHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightFrontHip();
        }

        private void frontRightShinTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightFrontShinTourniquiet();
        }

        private void frontRightShinTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightFrontShinTourniquiet();
        }

        private void frontRightShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightFrontShin();
        }

        private void frontRightShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightFrontShin();
        }

        private void frontRightFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightFrontFoot();
        }

        private void frontRightFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightFrontFoot();
        }

        private void frontLeftHipTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftFrontHipTourniquiet();
        }

        private void frontLeftHipTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftFrontHipTourniquiet();
        }

        private void frontLeftShinTourniquiet_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftFrontShinTourniquiet();
        }

        private void frontLeftShinTourniquiet_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftFrontShinTourniquiet();
        }

        private void frontLeftShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftFrontShin();
        }

        private void frontLeftShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftFrontShin();
        }

        private void frontLeftFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftFrontFoot();
        }

        private void frontLeftFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftFrontFoot(); ;
        }

        private void frontThoracInjuryRight_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddThoraxFrontRight();
        }

        private void frontThoracInjuryRight_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteThoraxFrontRight();
        }

        private void frontThoracInjuryLeft_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddThoraxFrontLeft();
        }

        private void frontThoracInjuryLeft_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteThoraxFrontLeft(); ;
        }

        private void frontBellyInjuryRight_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddAbdomenFrontRight();
        }

        private void frontBellyInjuryRight_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteAbdomenFrontRight();
        }

        private void frontBellyInjuryLeft_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddAbdomenFrontLeft();
        }

        private void frontBellyInjuryLeft_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteAbdomenFrontLeft();
        }

        private void backHeadInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddHeadBack();
        }

        private void backHeadInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteHeadBack();
        }

        private void backNeckInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddNeckBack();
        }

        private void backNeckInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteNeckBack();
        }

        private void backLeftShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperShoulderBack();
        }

        private void backThoracInjuryLeft_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddThoraxBackLeft();
        }

        private void backThoracInjuryLeft_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteThoraxBackLeft();
        }

        private void backThoracInjuryRight_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddThoraxBackRight();
        }

        private void backThoracInjuryRight_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteThoraxBackRight();
        }

        private void backRightShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightUpperShoulderBack();
        }

        private void backRightShoulderInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightUpperShoulderBack();
        }

        private void backLeftForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperForearmBack();
        }

        private void backBellyInjuryLeft_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddAbdomenBackLeft();
        }

        private void backBellyInjuryLeft_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteAbdomenBackLeft();
        }

        private void backBellyInjuryRight_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddAbdomenBackRight();
        }

        private void backBellyInjuryRight_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteAbdomenBackRight();
        }

        private void backRightForearmInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightUpperForearmBack();
        }

        private void backRightForearmInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightUpperForearmBack();
        }

        private void backLeftWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftUpperWristBack();
        }

        private void backRightHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightBackHip();
        }

        private void backRightHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightBackHip();
        }

        private void backLeftHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftBackHip();
        }

        private void backLeftHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftBackHip();
        }

        private void backRightWristInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightUpperWristBack();
        }

        private void backRightWristInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightUpperWristBack();
        }

        private void backLeftShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftBackShin();
        }

        private void backLeftShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftBackShin();
        }

        private void backRightShinInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightBackShin();
        }

        private void backRightShinInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightBackShin();
        }

        private void backLeftFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftBackFoot();
        }

        private void backLeftFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftBackFoot();
        }

        private void backRightFootInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddRightBackFoot();
        }

        private void backRightFootInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteRightBackFoot();
        }

        private void frontLeftHipInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddLeftFrontHip();
        }

        private void frontLeftHipInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteLeftFrontHip();
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
            providedDrugs.Remove(drug);
            drugsProvidedList.ItemsSource = new ObservableCollection<ProvidedDrugModel>(providedDrugs);

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
            if (providedDrugs.Where(d => d.Id == drug.Id).Count() > 0)
            {
                providedDrugs.Where(d => d.Id == drug.Id).ToList().ForEach(d => d.Dose += drug.Dose);
            }
            else
            {
                providedDrugs.Add(drug);

            }
            drugsProvidedList.ItemsSource = new ObservableCollection<ProvidedDrugModel>(providedDrugs);
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
            providedDrugs.Clear();
            drugsProvidedList.ItemsSource = new ObservableCollection<ProvidedDrugModel>(providedDrugs);
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

    }
}
