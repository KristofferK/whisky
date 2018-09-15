using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace SampleDataSender
{
    public partial class MainWindow : Window
    {
        private HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendDataToServer_ButtonClicked(object sender, RoutedEventArgs e)
        {
            var measurement = GenerateMeasurement();
            if (measurement == null)
            {
                MessageToServer.Text = "Could not generate measurement from specified values";
                return;
            }

            var json = JsonConvert.SerializeObject(measurement);
            MessageToServer.Text = "Sending:\n" + json;
            SendToServer(json);
        }

        private Measurement GenerateMeasurement()
        {
            try
            {

                return new Measurement
                {
                    Temperature = int.Parse(Temperature.Text),
                    Pressure = int.Parse(Pressure.Text),
                    SensorID = SensorID.SelectedValue.ToString()
                };
            }
            catch
            {
                return null;
            }
        }

        private async void SendToServer(string json)
        {
            var response = await client.PutAsync("https://localhost:44330/api/Measurement/Add", new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            MessageToServer.Text = $"Sent:\n{json}\n\nReceived:\n{content}";
        }
    }
}
