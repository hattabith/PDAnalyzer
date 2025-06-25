using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Partial_Discharge_Analyzer
{
    /// <summary>
    /// Interaction logic for AddIpWindow.xaml
    /// </summary>
    public partial class AddIpWindow : Window
    {
        public string FullAddress { get; private set; }

        public AddIpWindow()
        {
            InitializeComponent();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var ip = IpTextBox.Text.Trim();
            var port = PortTextBox.Text.Trim();

            // Валідація IP
            if (!IPAddress.TryParse(ip, out _))
            {
                MessageBox.Show("Неправильна IP адреса", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Валідація порту, якщо заданий
            if (!string.IsNullOrEmpty(port))
            {
                if (!int.TryParse(port, out int portNum) || portNum < 1 || portNum > 65535)
                {
                    MessageBox.Show("Неправильний порт", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                FullAddress = $"{ip}:{port}";
            }
            else
            {
                FullAddress = ip;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}

