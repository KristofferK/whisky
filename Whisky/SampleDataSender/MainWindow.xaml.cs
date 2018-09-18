using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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
        private static Random random = new Random();
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

            SendToServer(measurement, true, Target.SelectedValue.ToString(), GetAuthValue());
        }

        private Measurement GenerateMeasurement()
        {
            try
            {

                return new Measurement
                {
                    Temperature = float.Parse(Temperature.Text.Replace(",",".")),
                    Pressure = int.Parse(Pressure.Text),
                    SensorID = SensorID.SelectedValue.ToString()
                };
            }
            catch
            {
                return null;
            }
        }

        private async void SendToServer(Measurement measurement, bool updateUi, string baseUri, string auth)
        {
            var json = JsonConvert.SerializeObject(measurement);
            if (updateUi)
            {
                MessageToServer.Text = $"Sending to {baseUri}api/Measurement/Add:\n" + json;
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            var response = await client.PutAsync(baseUri + "api/Measurement/Add", new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();

            if (updateUi)
            {
                MessageToServer.Text = $"Sent to {baseUri}api/Measurement/Add:\n{json}\n\nReceived from {baseUri}api/Measurement/Add:\n{content}";
            }
        }

        private void FloodServer_ButtonClicked(object sender, RoutedEventArgs e)
        {
            var baseUri = Target.SelectedValue.ToString();
            var auth = GetAuthValue();
            MessageToServer.Text = $"Flooding {baseUri}api/Measurement/Add";
            new Thread(() =>
            {
                for (var i = 0; i < 7; i++)
                {
                    for (var sensorID = 1; sensorID < 4; sensorID++)
                    {
                        SendToServer(new Measurement()
                        {
                            Pressure = random.Next(1000, 1300),
                            SensorID = sensorID.ToString(),
                            Temperature = (float)(random.Next(5000, 9000) / 100.0)
                        }, false, baseUri, auth);
                        Thread.Sleep(30);
                    }
                    Thread.Sleep(random.Next(100, 350));
                }
            })
            {
                IsBackground = true
            }.Start();
        }

        private string GetAuthValue()
        {
            var plainTextBytes = Encoding.GetEncoding("iso-8859-1").GetBytes($"{APIUsername.Text}:{APIPassword.Text}");
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
