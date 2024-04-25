using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using VoenMedLibrary.DataAccess;
using VoenMedLibrary.Models;

namespace VoenMed.Controls
{
    /// <summary>
    /// Interaction logic for DefaultsControl.xaml
    /// </summary>
    public partial class DefaultsControl : UserControl
    {
        List<DrugModel> drugs = new();
        public DefaultsControl()
        {
            InitializeComponent();

            InitializeDrugList();
            WireUpDrugsDropdowns();
            LoadDefaultsFromDatabase();
        }

        private void InitializeDrugList()
        {
            string sql = "select * from Drugs;";
            drugs = SqliteDataAccess.LoadData<DrugModel>(sql, new Dictionary<string, object> { });
            
        }

        private void WireUpDrugsDropdowns()
        {
            favDrug1ComboBox.ItemsSource= drugs;
            favDrug1ComboBox.DisplayMemberPath = "Title";
            favDrug1ComboBox.SelectedValuePath = "Id";

            favDrug2ComboBox.ItemsSource= drugs;
            favDrug2ComboBox.DisplayMemberPath = "Title";
            favDrug2ComboBox.SelectedValuePath = "Id";

            favDrug3ComboBox.ItemsSource= drugs;
            favDrug3ComboBox.DisplayMemberPath = "Title";
            favDrug3ComboBox.SelectedValuePath = "Id";

            favDrug4ComboBox.ItemsSource= drugs;
            favDrug4ComboBox.DisplayMemberPath = "Title";
            favDrug4ComboBox.SelectedValuePath = "Id";
        }

        private void LoadDefaultsFromDatabase()
        {
            string sqlStatement = "SELECT * FROM Defaults";
            DefaultsModel model = SqliteDataAccess.LoadData<DefaultsModel>(sqlStatement, new Dictionary<string, object>()).FirstOrDefault();
        
            if (model != null)
            {
                issuedByTextBox.Text = model.IssuedBy.ToString();
                docTextBox.Text = model.Doc.ToString();
                evacAddressTextBox.Text = model.EvacAddress.ToString();

                switch(model.EvacTransport)
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

                switch(model.EvacWay)
                {
                    case 1:
                        evacWayRadioButton_1.IsChecked = true; break;
                    case 2:
                        evacWayRadioButton_2.IsChecked = true; break;
                    default:
                    case 3:
                        evacWayRadioButton_3.IsChecked = true; break;
                }

                
                favDrug1ComboBox.SelectedValue = model.FavDrug1Id;
                favDrug2ComboBox.SelectedValue = model.FavDrug2Id;
                favDrug3ComboBox.SelectedValue = model.FavDrug3Id;
                favDrug4ComboBox.SelectedValue = model.FavDrug4Id;

                folderSavePathTextBox.Text = model.SavePath;

            }
            else
            {
                issuedByTextBox.Text = "";
                docTextBox.Text = "";
                evacAddressTextBox.Text = "";
                evacTransportRadioButton_1.IsChecked = true;
                evacWayRadioButton_3.IsChecked = true;
                folderSavePathTextBox.Text = "";

            }
        }
        private (bool isValid, DefaultsModel model) ValidateForm()
        {
            bool output = true;
            DefaultsModel model = new DefaultsModel();
            try
            {
                model.IssuedBy = issuedByTextBox.Text;
                model.Doc = docTextBox.Text;
                model.EvacAddress = evacAddressTextBox.Text;

                if ((bool)evacTransportRadioButton_1.IsChecked) model.EvacTransport = 1;
                else if ((bool)evacTransportRadioButton_2.IsChecked) model.EvacTransport = 2;
                else if ((bool)evacTransportRadioButton_3.IsChecked) model.EvacTransport = 3;
                else if ((bool)evacTransportRadioButton_4.IsChecked) model.EvacTransport = 4;
                else if ((bool)evacTransportRadioButton_5.IsChecked) model.EvacTransport = 5;

                if ((bool)evacWayRadioButton_1.IsChecked) model.EvacWay = 1;
                else if ((bool)evacWayRadioButton_2.IsChecked) model.EvacWay = 2;
                else if ((bool)evacWayRadioButton_3.IsChecked) model.EvacWay = 3;

                model.FavDrug1Id = (int)favDrug1ComboBox.SelectedValue;
                model.FavDrug2Id = (int)favDrug2ComboBox.SelectedValue;
                model.FavDrug3Id = (int)favDrug3ComboBox.SelectedValue;
                model.FavDrug4Id = (int)favDrug4ComboBox.SelectedValue;
                model.SavePath = folderSavePathTextBox.Text;
            }
            catch
            {
                output = false;
            }
            return (output, model);
        }
        private void SaveToDatabase(DefaultsModel model)
        {
            string sql = "delete from Defaults";
            SqliteDataAccess.SaveData(sql, new Dictionary<string, object>());

            sql = "insert into Defaults (IssuedBy,Doc,EvacAddress,EvacTransport,EvacWay, FavDrug1Id,FavDrug2Id,FavDrug3Id,FavDrug4Id, SavePath) " +
                                "values (@IssuedBy,@Doc,@EvacAddress,@EvacTransport,@EvacWay, @FavDrug1Id,@FavDrug2Id,@FavDrug3Id,@FavDrug4Id, @SavePath);";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IssuedBy", model.IssuedBy },
                {"@Doc", model.Doc },
                {"@EvacAddress", model.EvacAddress },
                {"@EvacTransport", model.EvacTransport },
                {"@EvacWay", model.EvacWay },
                {"@FavDrug1Id", model.FavDrug1Id },
                {"@FavDrug2Id", model.FavDrug2Id },
                {"@FavDrug3Id", model.FavDrug3Id },
                {"@FavDrug4Id", model.FavDrug4Id },
                {"@SavePath", model.SavePath },
            };
            SqliteDataAccess.SaveData(sql, parameters);
        }
        private void saveDefaultsButton_Click(object sender, RoutedEventArgs e)
        {
            var form = ValidateForm();

            if(form.isValid == true )
            {
                SaveToDatabase(form.model);
                MessageBox.Show("Сохранено!");
            }
            else
            {
                MessageBox.Show("Некорректные данные!");
                return;
            }
            
        }

        private void folderSavePathButton_Click(object sender, RoutedEventArgs e)
        {

            var folderDialog = new OpenFolderDialog
            {
                Title = "Выберите папку",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
            };

            if (folderDialog.ShowDialog() == true)
            {
                var folderName = folderDialog.FolderName;
                folderSavePathTextBox.Text = folderName;
            }
        }
    }
}
