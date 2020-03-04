namespace Clientless_login
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.captcha_text = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.TextBox();
            this.char_list = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.Ping = new System.Windows.Forms.Timer(this.components);
            this.pw = new System.Windows.Forms.TextBox();
            this.id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ip = new System.Windows.Forms.TextBox();
            this.version = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(106, 77);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Send login";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 118);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // captcha_text
            // 
            this.captcha_text.Location = new System.Drawing.Point(60, 189);
            this.captcha_text.Name = "captcha_text";
            this.captcha_text.Size = new System.Drawing.Size(100, 20);
            this.captcha_text.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(72, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Send";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(200, 80);
            this.logs.Multiline = true;
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(428, 299);
            this.logs.TabIndex = 5;
            // 
            // char_list
            // 
            this.char_list.FormattingEnabled = true;
            this.char_list.Location = new System.Drawing.Point(39, 288);
            this.char_list.Name = "char_list";
            this.char_list.Size = new System.Drawing.Size(121, 21);
            this.char_list.TabIndex = 6;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(51, 333);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Select character";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Ping
            // 
            this.Ping.Enabled = true;
            this.Ping.Interval = 5000;
            this.Ping.Tick += new System.EventHandler(this.Ping_Tick);
            // 
            // pw
            // 
            this.pw.Location = new System.Drawing.Point(46, 34);
            this.pw.Name = "pw";
            this.pw.PasswordChar = '*';
            this.pw.Size = new System.Drawing.Size(135, 20);
            this.pw.TabIndex = 8;
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(46, 8);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(135, 20);
            this.id.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "PW";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Server ip";
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(380, 12);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(176, 20);
            this.ip.TabIndex = 12;
            this.ip.Text = "127.0.0.1";
            // 
            // version
            // 
            this.version.Location = new System.Drawing.Point(380, 38);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(176, 20);
            this.version.TabIndex = 14;
            this.version.Text = "439";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Server version";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 391);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.version);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.id);
            this.Controls.Add(this.pw);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.char_list);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.captcha_text);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox captcha_text;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.TextBox logs;
        public System.Windows.Forms.Button button4;
        public System.Windows.Forms.ComboBox char_list;
        public System.Windows.Forms.Timer Ping;
        public System.Windows.Forms.TextBox pw;
        public System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox ip;
        public System.Windows.Forms.TextBox version;
        private System.Windows.Forms.Label label4;
    }
}

