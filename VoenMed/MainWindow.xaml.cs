using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VoenMed.Controls;

namespace VoenMed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            content.Content = new From100ListControl();
        }

        private void defaultsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new DefaultsControl();
        }

        private void formMenuItem_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new Form100Control();
        }

        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void formListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new From100ListControl();
        }
    }
}