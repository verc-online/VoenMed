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
        List<ProvidedDrugModel> providedDrugs = new(); // Использованные лекарства, которые сохраняются в БД
        InjuryStatusLocalisModel injuryStatusLocalis = new(); // Локализация повреждений
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

        #region HeadLocalis
        // Когда нажимаешь на чекбокс должен выполнять битовое сложение, потом записывать число в бд

        private void frontHeadInjury_Checked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.AddHeadFront();
        }

        private void frontHeadInjury_Unchecked(object sender, RoutedEventArgs e)
        {
            injuryStatusLocalis.DeleteHeadFront();
        }
        #endregion

        #region RightHand
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

        private void frontLeftShoulderInjury_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void fronLeftForearmInjury_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void fronLeftWristInjury_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
