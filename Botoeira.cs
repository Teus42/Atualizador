using System;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Drawing;

namespace Atualizador
{
    public partial class Botoeira : Form
    {
        private string ip = "";
        private string porta = "";
        private string eq = "";
        private string session = "";
        private bool attInit = false;
        private PingReply pingReply;

        public Botoeira()
        {
            InitializeComponent();
        }

        private void btn_atualizar_Click(object sender, EventArgs e)
        {

            if (txt_ip.Text == "" || txt_porta.Text == "")
            {
                
                return;
            }


            string admin = "admin", password = "admin";

            ip = txt_ip.Text;            
            porta = txt_porta.Text;

            eq = "http://" + ip + ":" + porta + "/";

            try
            {
                string response = WebJson.Send(eq + "login", "{\"login\":\"" + admin + "\",\"password\":\"" + password + "\"}");

                if (response.Contains("session"))
                {
                    session = response.Split('"')[3];
                }              
                
            }
            catch (Exception ex)
            {               
                return;
            }
            
            string id = "65793, reason=3";
            string cmd = "{\"actions\":[{\"action\": \"sec_box\", \"parameters\":\"id=" + id + "\"}]}";
           
            WebJson.Send(eq + "execute_actions", cmd, session);
            MessageBox.Show("Relé aberto");

        }
    }
}