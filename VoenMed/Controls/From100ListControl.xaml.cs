using System;
using System.Collections.Generic;
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
using VoenMed.Utility;
using VoenMedLibrary.Data;
using VoenMedLibrary.DataAccess;
using VoenMedLibrary.Models;

namespace VoenMed.Controls
{
    /// <summary>
    /// Interaction logic for From100ListControl.xaml
    /// </summary>
    public partial class From100ListControl : UserControl
    {
        List<Form100Model> form100Models = new();
        InjuryStatusLocalisModel InjuryStatusLocalisModel;
        Form100Data form100Data;
        public From100ListControl()
        {
            InitializeComponent();
            WireUpForm100Models();
        }

        private void WireUpForm100Models()
        {
            string sql = "SELECT Form100s.*, " +
                "Persons.LastName, Persons.FirstName, Persons.SecondName, Persons.BirthDate, Persons.MilitaryId, Persons.MilitaryUnit, Persons.Duty, Persons.Rank,Persons.RankTitle  " +
                "FROM Form100s " +
                "INNER JOIN Persons ON Persons.Id = Form100s.PersonId;";
            form100Models = SqliteDataAccess.LoadData<Form100Model>(sql, new Dictionary<string, object>());
            form100ListBox.ItemsSource = form100Models;
            form100ListBox.SelectedValuePath = "Id";

        }

        private void form100ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedModel = (Form100Model)form100ListBox.SelectedItem;
            if (selectedModel is not null)
            {
                LastNameTextBox.Text = selectedModel.LastName;
                FirstNameTextBox.Text = selectedModel.FirstName;
                SecondNameTextBox.Text = selectedModel.SecondName;
                DiagnosisTextBox.Text = selectedModel.Diagnosis;
                RankTitleTextBox.Text = selectedModel.RankTitle;
                DutyTextBox.Text = selectedModel.Duty;
            }
            else
            {
                LastNameTextBox.Text = "";
                FirstNameTextBox.Text = "";
                SecondNameTextBox.Text = "";
                DiagnosisTextBox.Text = "";
                RankTitleTextBox.Text = "";
                DutyTextBox.Text = "";
            }
        }

        private void form100PrintButton_Click(object sender, RoutedEventArgs e)
        {
            form100Data = new Form100Data((Form100Model)form100ListBox.SelectedItem);
            form100Data.LoadFullModelFromDatabase();
            string savePath;

            string sqlStatement = "SELECT * FROM Defaults";
            DefaultsModel defaultsModel = SqliteDataAccess.LoadData<DefaultsModel>(sqlStatement, new Dictionary<string, object>()).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(defaultsModel?.SavePath))
            {
                MessageBox.Show("Не указан путь сохранения форм 100! Сохраняю в папку с приложением");
                savePath = defaultsModel.GetDefaultSavePath();
            }
            else
            {
                savePath = defaultsModel.SavePath;
            }

            AppHelper.GenerateAndOpenForm100(savePath, (Form100Model)form100ListBox.SelectedItem);
        }

        private void form100DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            form100Data = new Form100Data((Form100Model)form100ListBox.SelectedItem);
            form100Data.DeleteForm100();
            WireUpForm100Models();
        }

    }
}
