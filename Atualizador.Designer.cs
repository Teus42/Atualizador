namespace Atualizador
{
    partial class Atualizador
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_atualizar = new System.Windows.Forms.Button();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.txt_porta = new System.Windows.Forms.TextBox();
            this.lbl_ip = new System.Windows.Forms.Label();
            this.lbl_porta = new System.Windows.Forms.Label();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.tmr_att = new System.Windows.Forms.Timer(this.components);
            this.txt_admin = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_ssl = new System.Windows.Forms.CheckBox();
            this.btn_limpar = new System.Windows.Forms.Button();
            this.tmr_checkAtt = new System.Windows.Forms.Timer(this.components);
            this.tmr_setConfig = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btn_atualizar
            // 
            this.btn_atualizar.Location = new System.Drawing.Point(18, 138);
            this.btn_atualizar.Name = "btn_atualizar";
            this.btn_atualizar.Size = new System.Drawing.Size(258, 37);
            this.btn_atualizar.TabIndex = 6;
            this.btn_atualizar.Text = "Atualizar";
            this.btn_atualizar.UseVisualStyleBackColor = true;
            this.btn_atualizar.Click += new System.EventHandler(this.btn_atualizar_Click);
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(17, 49);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(158, 23);
            this.txt_ip.TabIndex = 1;
            // 
            // txt_porta
            // 
            this.txt_porta.Location = new System.Drawing.Point(181, 49);
            this.txt_porta.Name = "txt_porta";
            this.txt_porta.Size = new System.Drawing.Size(94, 23);
            this.txt_porta.TabIndex = 2;
            // 
            // lbl_ip
            // 
            this.lbl_ip.AutoSize = true;
            this.lbl_ip.Location = new System.Drawing.Point(17, 31);
            this.lbl_ip.Name = "lbl_ip";
            this.lbl_ip.Size = new System.Drawing.Size(17, 15);
            this.lbl_ip.TabIndex = 3;
            this.lbl_ip.Text = "IP";
            // 
            // lbl_porta
            // 
            this.lbl_porta.AutoSize = true;
            this.lbl_porta.Location = new System.Drawing.Point(181, 31);
            this.lbl_porta.Name = "lbl_porta";
            this.lbl_porta.Size = new System.Drawing.Size(35, 15);
            this.lbl_porta.TabIndex = 4;
            this.lbl_porta.Text = "Porta";
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(18, 200);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ReadOnly = true;
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_log.Size = new System.Drawing.Size(258, 219);
            this.txt_log.TabIndex = 20;
            // 
            // tmr_att
            // 
            this.tmr_att.Enabled = true;
            this.tmr_att.Interval = 1000;
            this.tmr_att.Tick += new System.EventHandler(this.tmr_att_Tick);
            // 
            // txt_admin
            // 
            this.txt_admin.Location = new System.Drawing.Point(17, 99);
            this.txt_admin.Name = "txt_admin";
            this.txt_admin.Size = new System.Drawing.Size(71, 23);
            this.txt_admin.TabIndex = 3;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(100, 99);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(71, 23);
            this.txt_password.TabIndex = 4;
            this.txt_password.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Admin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "SSL";
            // 
            // chk_ssl
            // 
            this.chk_ssl.AutoSize = true;
            this.chk_ssl.Location = new System.Drawing.Point(181, 103);
            this.chk_ssl.Name = "chk_ssl";
            this.chk_ssl.Size = new System.Drawing.Size(67, 19);
            this.chk_ssl.TabIndex = 5;
            this.chk_ssl.Text = "Ativado";
            this.chk_ssl.UseVisualStyleBackColor = true;
            // 
            // btn_limpar
            // 
            this.btn_limpar.Location = new System.Drawing.Point(82, 440);
            this.btn_limpar.Name = "btn_limpar";
            this.btn_limpar.Size = new System.Drawing.Size(130, 23);
            this.btn_limpar.TabIndex = 21;
            this.btn_limpar.Text = "Limpar";
            this.btn_limpar.UseVisualStyleBackColor = true;
            this.btn_limpar.Click += new System.EventHandler(this.btn_limpar_Click);
            // 
            // tmr_checkAtt
            // 
            this.tmr_checkAtt.Interval = 5000;
            this.tmr_checkAtt.Tick += new System.EventHandler(this.tmr_checkAtt_Tick);
            // 
            // tmr_setConfig
            // 
            this.tmr_setConfig.Enabled = true;
            this.tmr_setConfig.Interval = 10000;
            this.tmr_setConfig.Tick += new System.EventHandler(this.tmr_setConfig_Tick);
            // 
            // Atualizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 475);
            this.Controls.Add(this.btn_limpar);
            this.Controls.Add(this.chk_ssl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_admin);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.lbl_porta);
            this.Controls.Add(this.lbl_ip);
            this.Controls.Add(this.txt_porta);
            this.Controls.Add(this.txt_ip);
            this.Controls.Add(this.btn_atualizar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Atualizador";
            this.Text = "Atualizador";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_atualizar;
        private TextBox txt_ip;
        private TextBox txt_porta;
        private Label lbl_ip;
        private Label lbl_porta;
        private TextBox txt_log;
        private System.Windows.Forms.Timer tmr_att;
        private TextBox txt_admin;
        private TextBox txt_password;
        private Label label1;
        private Label label2;
        private Label label3;
        private CheckBox chk_ssl;
        private Button btn_limpar;
        private System.Windows.Forms.Timer tmr_checkAtt;
        private System.Windows.Forms.Timer tmr_setConfig;
    }
}