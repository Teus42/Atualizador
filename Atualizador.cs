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

        //Variaveis que ser�o usados para criar sess�o
        private string eq = "";
        private string session = "";

        //Variaveis que s�o utilizadas para verifica��o da atualiza��o
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
                txt_log.Text += "IP e Porta n�o podem ficar vazio \r\n";
                return;
            }  
            
            admin = txt_admin.Text;
            password = txt_password.Text;            

            txt_ip.Text = txt_ip.Text.Trim(); //Usando o Trim para limpar o texto do ip e porta
            txt_porta.Text = txt_porta.Text.Trim();

            ip = txt_ip.Text;            
            porta = Int32.Parse(txt_porta.Text);

            //Checagem se est� utilizando o ssl

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
                txt_log.Text += "Usu�rio e/ou senha n�o foram preenchidos, tentando sess�o com as credenciais padr�o \r\n";
                admin = "admin";
                password = "admin";
            }

            //Criando sess�o
            try
            {
                string response = WebJson.Send(eq + "login", "{\"login\":\"" + admin + "\",\"password\":\"" + password + "\"}");

                if (response.Contains("session"))
                {
                    session = response.Split('"')[3];
                }

                txt_log.Text += "Sess�o Iniciada com sucesso \r\n";

            }
            catch
            {
                txt_log.Text += "Ocorreu um erro ao tentar iniciar a sess�o \r\n ";
                return;
            } 

            /* 
             * Utilizando o Sleepy (criado mais pra baixo) para poder fazer o c�digo aguardar um tempo.
             * Nesse casso utilizei para dar uma pausa de 2 segundos entre as mensagens, por�m ele 
             * tamb�m � utilizado em outros pontos mais essenciais.
             */

            Sleepy(2000);

            txt_log.Text += "Reiniciando equipamento no modo recovery \r\n";

            Sleepy(2000);

            //reboot_recovery � o que faz reiniciar no modo recovery
            WebJson.Send(eq + "reboot_recovery", null, session);            

            /* 
             * Como nesse ponto que ap�s o reboot preciso fazer o c�digo esperar mais tempo
             * pois se eu j� configurar o attInit como true e o equipamento ainda est� pingando 
             * vai ocacionar em um erro porque para atualizar ser� preciso do cgi e n�o do fcgi
             * e o cgi s� � acessado em recovery
             */

            Sleepy(5000);
            

            attInit = true;
            configComplete = false;
        }

        /* 
         * Utilizo alguns timer para poder fazer a atualiza��o, isso porque vou precisar checar
         * se o equipamento j� reiniciou, para isso o ping abaixo.
         */
        private void tmr_att_Tick(object sender, EventArgs e)
        {

            Ping pingEq = new Ping();

            if (ip == "")
            {
                return;
            }          

            //Definido ap�s criar sess�o e solicitar o reinicio em modo recovery
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
                        // cgi/run_factory_update.sh � o que faz a atualiza��o com reset ocorrer
                        string link = eq2 + "cgi/run_factory_update.sh";                        
                        
                        WebJson.DownClient(link);                      

                        txt_log.Text += "Processo de atualiza��o come�ou... \r\nFavor verifar progresso diretamente pelo equipamento. \r\n";

                        tmr_checkAtt.Enabled = true; //Ativando o timer da checagem da atualiza��o
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
         * Metodo basico de fazer o c�digo esperar algum tempo, tamb�m � possivel fazer a thread aguardar
         * utilizando o Sleep mas acaba travando o front, com esse metodo Sleepy crio um timer e fa�o 
         * ele aguardar um tempo em milisegundos para poder seguir com o c�digo, assim o front n�o fica travado
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

        //Checando a atualiza��o
        private void tmr_checkAtt_Tick(object sender, EventArgs e)
        {
            string eq2 = "http://" + ip + ":" + 80 + "/"; //Recovery sempre vai usar a porta 80
            string link2 = eq2 + "cgi/read_status.sh"; //Link que retorna a atualiza��o, isso � visto na web quando se faz a atualiza��o pelo navegador

            string progress = WebJson.DownClient(link2);

            //Pensando em tirar ou melhor isso
            if(p1 == true && progress.Contains("Atualizando o kernel padrao...")) 
            {
                txt_log.Text += "Atualizando o kernel padrao... \r\n";
                p1 = false;
            }
            if (p2 == true && progress.Contains("Atualiza��o do kernel padr�o realizada com sucesso!"))
            {
                txt_log.Text += "Kernel padr�o atualizado \r\n";
                p2 = false;
            } 

            if (progress.Contains("FIM!")) 
            {
                string linkReboot = eq2 + "cgi/reboot_normal.sh";
                WebJson.DownClient(linkReboot);                
                txt_log.Text += "Atualiza��o finalizada, reiniciando em modo normal... \r\n";               
                tmr_checkAtt.Enabled = false;
                tmr_checkAtt.Stop();                
                Sleepy(10000);
                attComplete = true; //Boleano que ativa a reconfigura��o do equipamento                
            }
        }

        //M�todo que realiza a reconfigura��o com as informadas previamente informadas
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

                txt_log.Text += "Sess�o Iniciada com sucesso (2) \r\n";

            }
            catch (Exception ex)
            {
                txt_log.Text += "Ocorreu um erro ao tentar iniciar a sess�o \r\n ";
                return;
            }
            
            WebJson.Send(eq + "change_login", "{\"login\":\"" + newAdmin + "\",\"password\":\"" + newPassword + "\"}", session);
            WebJson.Send(eq + "set_system_network", "{\"ip\":\"" + newIp + "\",\"netmask\":\"" + "255.255.128.0" + "\",\"gateway\":\"" + "192.168.0.1" + "\",\"web_server_port\": "+newPorta+"}", session);
            txt_log.Text += "Configura��es e credenciais modificadas \r\nIP: "+newIp+"\r\nPorta: "+newPorta;
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
                        txt_log.Text += "Modificando configura��es para as informadas previamente (ip,porta,usu�rio e senha) \r\n";
                        SetConfig(ip, porta, admin, password);
                        configComplete = true;
                    } 
                }
            }
        }
    }
}