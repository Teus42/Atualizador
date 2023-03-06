using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Atualizador
{
    public partial class Atualizador : Form
    {
        //Dados de entrada referente ao equipamento
        private string ip = "";
        private int porta = 0;
        private string admin = "";
        private string password = "";
        private bool ssl = false;

        //Variaveis que serão usados para criar sessão
        private string eq = "";
        private string session = "";

        //Variaveis que são utilizadas para verificação da atualização
        private bool p1 = true;
        private bool p2 = true;
        private bool attInit = false;
        private bool attComplete = false;
        private bool configComplete = false;

        //Utilizando para pingar o equipamento
        private PingReply pingReply;
        

        public Atualizador()
        {
            InitializeComponent();
        }

        private void btn_atualizar_Click(object sender, EventArgs e)
        {
            
            if (txt_ip.Text == "" || txt_porta.Text == "")
            {
                txt_log.Text += "IP e Porta não podem ficar vazio \r\n";
                return;
            }  
            
            admin = txt_admin.Text;
            password = txt_password.Text;            

            txt_ip.Text = txt_ip.Text.Trim(); //Usando o Trim para limpar o texto do ip e porta
            txt_porta.Text = txt_porta.Text.Trim();

            ip = txt_ip.Text;            
            porta = Int32.Parse(txt_porta.Text);

            //Checagem se está utilizando o ssl

            if (chk_ssl.Checked == true)
            {
                eq = "https://" + ip + ":" + porta + "/";
                ssl = true;
            }
            else
            {
                eq = "http://" + ip + ":" + porta + "/";
                ssl = false;
            }

            txt_log.Text += eq + "\r\n";

            if (txt_admin.Text == "" || txt_password.Text == "")
            {
                txt_log.Text += "Usuário e/ou senha não foram preenchidos, tentando sessão com as credenciais padrão \r\n";
                admin = "admin";
                password = "admin";
            }

            //Criando sessão
            try
            {
                string response = WebJson.Send(eq + "login", "{\"login\":\"" + admin + "\",\"password\":\"" + password + "\"}");

                if (response.Contains("session"))
                {
                    session = response.Split('"')[3];
                }

                txt_log.Text += "Sessão Iniciada com sucesso \r\n";

            }
            catch
            {
                txt_log.Text += "Ocorreu um erro ao tentar iniciar a sessão \r\n ";
                return;
            } 

            /* 
             * Utilizando o Sleepy (criado mais pra baixo) para poder fazer o código aguardar um tempo.
             * Nesse casso utilizei para dar uma pausa de 2 segundos entre as mensagens, porém ele 
             * também é utilizado em outros pontos mais essenciais.
             */

            Sleepy(2000);

            txt_log.Text += "Reiniciando equipamento no modo recovery \r\n";

            Sleepy(2000);

            //reboot_recovery é o que faz reiniciar no modo recovery
            WebJson.Send(eq + "reboot_recovery", null, session);            

            /* 
             * Como nesse ponto que após o reboot preciso fazer o código esperar mais tempo
             * pois se eu já configurar o attInit como true e o equipamento ainda está pingando 
             * vai ocacionar em um erro porque para atualizar será preciso do cgi e não do fcgi
             * e o cgi só é acessado em recovery
             */

            Sleepy(5000);
            

            attInit = true;
            configComplete = false;
        }

        /* 
         * Utilizo alguns timer para poder fazer a atualização, isso porque vou precisar checar
         * se o equipamento já reiniciou, para isso o ping abaixo.
         */
        private void tmr_att_Tick(object sender, EventArgs e)
        {

            Ping pingEq = new Ping();

            if (ip == "")
            {
                return;
            }          

            //Definido após criar sessão e solicitar o reinicio em modo recovery
            if (attInit == true)
            {
                try
                {
                    pingReply = pingEq.Send(ip, 2000);
                }
                catch (Exception ex)
                {
                    txt_log.Text += ex.ToString() + "\r\n";
                }

                string reply = pingReply.Status.ToString().ToLower(); //Resposta do ping

                if (reply == "success")
                {
                    try
                    {
                        string eq2 = "http://" + ip + ":" + 80 + "/";
                        // cgi/run_factory_update.sh é o que faz a atualização com reset ocorrer
                        string link = eq2 + "cgi/run_factory_update.sh";                        
                        
                        WebJson.DownClient(link);                      

                        txt_log.Text += "Processo de atualização começou... \r\nFavor verifar progresso diretamente pelo equipamento. \r\n";

                        tmr_checkAtt.Enabled = true; //Ativando o timer da checagem da atualização
                        tmr_checkAtt.Start();

                        attInit = false;
                    }
                    catch (Exception ex)
                    {
                        txt_log.Text += "Ocorreu um erro: " + ex.ToString() + " \r\n ";
                    }
                }
            }         
        }

        private void btn_limpar_Click(object sender, EventArgs e)
        {
            txt_log.Text = "";
        }

        /*
         * Metodo basico de fazer o código esperar algum tempo, também é possivel fazer a thread aguardar
         * utilizando o Sleep mas acaba travando o front, com esse metodo Sleepy crio um timer e faço 
         * ele aguardar um tempo em milisegundos para poder seguir com o código, assim o front não fica travado
         */

        public static void Sleepy(int milliseconds)
        {
            var tmr_sleepy = new System.Windows.Forms.Timer();

            if (milliseconds == 0 || milliseconds < 0)
            {
                return;
            }

            tmr_sleepy.Interval = milliseconds;
            tmr_sleepy.Enabled = true;
            tmr_sleepy.Start();

            tmr_sleepy.Tick += (s, e) =>
            {
                tmr_sleepy.Enabled = false;
                tmr_sleepy.Stop();
            };

            while (tmr_sleepy.Enabled == true)
            {
                Application.DoEvents();
            }
        }

        //Checando a atualização
        private void tmr_checkAtt_Tick(object sender, EventArgs e)
        {
            string eq2 = "http://" + ip + ":" + 80 + "/"; //Recovery sempre vai usar a porta 80
            string link2 = eq2 + "cgi/read_status.sh"; //Link que retorna a atualização, isso é visto na web quando se faz a atualização pelo navegador

            string progress = WebJson.DownClient(link2);

            //Pensando em tirar ou melhor isso
            if(p1 == true && progress.Contains("Atualizando o kernel padrao...")) 
            {
                txt_log.Text += "Atualizando o kernel padrao... \r\n";
                p1 = false;
            }
            if (p2 == true && progress.Contains("Atualização do kernel padrão realizada com sucesso!"))
            {
                txt_log.Text += "Kernel padrão atualizado \r\n";
                p2 = false;
            } 

            if (progress.Contains("FIM!")) 
            {
                string linkReboot = eq2 + "cgi/reboot_normal.sh";
                WebJson.DownClient(linkReboot);                
                txt_log.Text += "Atualização finalizada, reiniciando em modo normal... \r\n";               
                tmr_checkAtt.Enabled = false;
                tmr_checkAtt.Stop();                
                Sleepy(10000);
                attComplete = true; //Boleano que ativa a reconfiguração do equipamento                
            }
        }

        //Método que realiza a reconfiguração com as informadas previamente informadas
        private void SetConfig(string newIp,int newPorta, string newAdmin,string newPassword) 
        {
            newIp = ip;
            newPorta = porta;
            newAdmin = admin;
            newPassword = password;

            admin = "admin";
            password = "admin";
            ip = "192.168.0.129";
            porta = 80;

            eq = "http://" + ip + ":" + porta + "/";

            try
            {
                string response = WebJson.Send(eq + "login", "{\"login\":\"" + admin + "\",\"password\":\"" + password + "\"}");

                if (response.Contains("session"))
                {
                    session = response.Split('"')[3];
                }

                txt_log.Text += "Sessão Iniciada com sucesso (2) \r\n";

            }
            catch (Exception ex)
            {
                txt_log.Text += "Ocorreu um erro ao tentar iniciar a sessão \r\n ";
                return;
            }
            
            WebJson.Send(eq + "change_login", "{\"login\":\"" + newAdmin + "\",\"password\":\"" + newPassword + "\"}", session);
            WebJson.Send(eq + "set_system_network", "{\"ip\":\"" + newIp + "\",\"netmask\":\"" + "255.255.128.0" + "\",\"gateway\":\"" + "192.168.0.1" + "\",\"web_server_port\": "+newPorta+"}", session);
            txt_log.Text += "Configurações e credenciais modificadas \r\nIP: "+newIp+"\r\nPorta: "+newPorta;
        }

        //Timer para configurar
        private void tmr_setConfig_Tick(object sender, EventArgs e)
        {
            Ping pingEq = new Ping();           

            try
            {
                pingReply = pingEq.Send("192.168.0.129", 2000);
            }
            catch (Exception ex)
            {
                txt_log.Text += ex.ToString() + "\r\n";
            }

            string reply2 = pingReply.Status.ToString().ToLower();

            if (attComplete == true) 
            {             
                if (reply2 == "success")
                {
                    if (configComplete == false)
                    {
                        txt_log.Text += "Modificando configurações para as informadas previamente (ip,porta,usuário e senha) \r\n";
                        SetConfig(ip, porta, admin, password);
                        configComplete = true;
                    } 
                }
            }
        }
    }
}