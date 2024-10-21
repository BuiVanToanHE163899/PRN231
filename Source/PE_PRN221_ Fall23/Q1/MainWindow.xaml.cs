using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
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

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Star> stars;
        public MainWindow()
        {
            stars = new List<Star>();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var star = new Star();
            star.Name = txtName.Text;
            star.Dob = dpDob.SelectedDate;
            star.Description = txtDesc.Text;
            star.Male = checkIsMale.IsChecked == true ? true : false;
            star.Nationality = txtNational.Text;


            stars.Add(star);
            listStars.ItemsSource = null;
            listStars.ItemsSource = stars;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(stars);
                TcpClient client = new TcpClient("127.0.0.1", 5000);
                NetworkStream stream = client.GetStream();
                byte[] jsonDataBytes = Encoding.UTF8.GetBytes(jsonData);
                stream.Write(jsonDataBytes, 0, jsonDataBytes.Length);
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                if (response == "accepted")
                {
                    MessageBox.Show("Dữ liệu đã được gửi thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
