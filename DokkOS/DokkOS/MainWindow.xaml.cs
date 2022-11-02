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
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Net.NetworkInformation;

namespace DokkOS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
                InitializeComponent();
        }

    public void StoppingDOkk()
        {
            Environment.Exit(0);
            OutPutText.Content = "Goodbye";
        }
        private void ShowConnectSSID()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "netsh.exe";
            p.StartInfo.Arguments = "wlan show interfaces";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            string s = p.StandardOutput.ReadToEnd();
            string s1 = s.Substring(s.IndexOf("SSID"));
            s1 = s1.Substring(s1.IndexOf(":"));
            s1 = s1.Substring(2, s1.IndexOf("\n")).Trim();

            string s2 = s.Substring(s.IndexOf("Signal"));
            s2 = s2.Substring(s2.IndexOf(":"));
            s2 = s2.Substring(2, s2.IndexOf("\n")).Trim();
            OutPutText.Content = "SSID: " + s1;
            OutPutText.Content += "\n";
            OutPutText.Content += "Strenght: " + s2;

            string s3 = s.Substring(s.IndexOf("Physical address"));
            s3 = s3.Substring(s3.IndexOf(":"));
            s3 = s3.Substring(2, s3.IndexOf("\n")).Trim();
            OutPutText.Content += "\n";
            OutPutText.Content += "MAC-ADDRESS: " + s3;

            string s4 = s.Substring(s.IndexOf("Authentication"));
            s4 = s4.Substring(s4.IndexOf(":"));
            s4 = s4.Substring(2, s4.IndexOf("\n")).Trim();
            OutPutText.Content += "\n";
            OutPutText.Content += "Authentication: " + s4;

        }

        public async void Pinger()
        {
            Ping pingSender = new Ping();
            string host = "81.169.145.149";
            await Task.Run(() =>
            {
                PingReply reply = pingSender.Send(host);
                if (reply.Status == IPStatus.Success)
                {
                    string mstext = reply.RoundtripTime.ToString();
                    LatencyBox.Text = "{mstext} ms";
                }
            });
        }
        private void btnCam_Click(object sender, RoutedEventArgs e)
        {
            ShowConnectSSID();
            Pinger();

        }
    }

}
