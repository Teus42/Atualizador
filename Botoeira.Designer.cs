namespace Atualizador
{
    partial class Botoeira
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btn_atualizar
            // 
            this.btn_atualizar.Location = new System.Drawing.Point(17, 104);
            this.btn_atualizar.Name = "btn_atualizar";
            this.btn_atualizar.Size = new System.Drawing.Size(258, 37);
            this.btn_atualizar.TabIndex = 0;
            this.btn_atualizar.Text = "Abrir Rele";
            this.btn_atualizar.UseVisualStyleBackColor = true;
            this.btn_atualizar.Click += new System.EventHandler(this.btn_atualizar_Click);
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(17, 49);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(143, 23);
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
            // Botoeira
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 156);
            this.Controls.Add(this.lbl_porta);
            this.Controls.Add(this.lbl_ip);
            this.Controls.Add(this.txt_porta);
            this.Controls.Add(this.txt_ip);
            this.Controls.Add(this.btn_atualizar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Botoeira";
            this.Text = "Botoeira";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_atualizar;
        private TextBox txt_ip;
        private TextBox txt_porta;
        private Label lbl_ip;
        private Label lbl_porta;
        private System.Windows.Forms.Timer timer1;
    }
}