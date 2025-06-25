using System.Collections.ObjectModel;
using System.Net;
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


namespace Partial_Discharge_Analyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> ipAddresses = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();


            ipAddresses.Add("TCPIP0::192.168.2.127:5025");
            // TODO Add RegExp to manage the connection string format
            //ConnComboBox.ItemsSource = new List<string> { "TCPIP0::192.168.2.127::5025::SOCKET", "192.168.88.1", "192.168.88.1:3000", "192.168.88.1:5000", "192.168.2.127" };
            SCPIOutputTextBox.Text = "SCPI Output will be displayed here. \rExample:\r*IDN?\rMANUFACTURE INSTR0.01.1";
            ConnComboBox.ItemsSource = ipAddresses;
        }

        public void ApplyTheme(string themeName)
        {
            var dict = new ResourceDictionary();
            dict.Source = new Uri($"Themes/{themeName}Theme.xaml", UriKind.Relative);

            // Видаляємо попередню тему
            var existing = Application.Current.Resources.MergedDictionaries
                            .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme"));
            if (existing != null)
                Application.Current.Resources.MergedDictionaries.Remove(existing);

            // Додаємо нову
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        private void ThemeSelector_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)((ComboBox)sender).SelectedItem).Content is string theme)
            {
                ApplyTheme(theme);
            }
        }

        private void SCPIOutputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AddIpWindow();
            if (dialog.ShowDialog() == true)
            {
                ipAddresses.Add(dialog.FullAddress); // IP або IP:PORT
            }
        }
    }
}