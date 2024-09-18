namespace Hotel_Chain_Distributed
{
    partial class AutorizationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutorizationForm));
            this.labelComboBox = new System.Windows.Forms.Label();
            this.comboBoxAut = new System.Windows.Forms.ComboBox();
            this.buttonAutOK = new System.Windows.Forms.Button();
            this.labelAutPass = new System.Windows.Forms.Label();
            this.labelAutLogin = new System.Windows.Forms.Label();
            this.textBoxAutPass = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBoxAutLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelComboBox
            // 
            this.labelComboBox.AutoSize = true;
            this.labelComboBox.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelComboBox.Location = new System.Drawing.Point(205, 28);
            this.labelComboBox.Name = "labelComboBox";
            this.labelComboBox.Size = new System.Drawing.Size(55, 26);
            this.labelComboBox.TabIndex = 31;
            this.labelComboBox.Text = "Тип:";
            // 
            // comboBoxAut
            // 
            this.comboBoxAut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxAut.FormattingEnabled = true;
            this.comboBoxAut.Items.AddRange(new object[] {
            "Центральный офис",
            "Филиал"});
            this.comboBoxAut.Location = new System.Drawing.Point(266, 25);
            this.comboBoxAut.Name = "comboBoxAut";
            this.comboBoxAut.Size = new System.Drawing.Size(304, 33);
            this.comboBoxAut.TabIndex = 30;
            this.comboBoxAut.Text = "Выберите тип";
            this.comboBoxAut.SelectedIndexChanged += new System.EventHandler(this.comboBoxAut_SelectedIndexChanged);
            // 
            // buttonAutOK
            // 
            this.buttonAutOK.BackColor = System.Drawing.Color.DarkOrange;
            this.buttonAutOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAutOK.Location = new System.Drawing.Point(587, 64);
            this.buttonAutOK.Name = "buttonAutOK";
            this.buttonAutOK.Size = new System.Drawing.Size(144, 69);
            this.buttonAutOK.TabIndex = 29;
            this.buttonAutOK.Text = "Войти";
            this.buttonAutOK.UseVisualStyleBackColor = false;
            this.buttonAutOK.Click += new System.EventHandler(this.buttonAutOK_Click);
            // 
            // labelAutPass
            // 
            this.labelAutPass.AutoSize = true;
            this.labelAutPass.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAutPass.Location = new System.Drawing.Point(214, 106);
            this.labelAutPass.Name = "labelAutPass";
            this.labelAutPass.Size = new System.Drawing.Size(88, 26);
            this.labelAutPass.TabIndex = 28;
            this.labelAutPass.Text = "Пароль:";
            // 
            // labelAutLogin
            // 
            this.labelAutLogin.AutoSize = true;
            this.labelAutLogin.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAutLogin.Location = new System.Drawing.Point(214, 154);
            this.labelAutLogin.Name = "labelAutLogin";
            this.labelAutLogin.Size = new System.Drawing.Size(76, 26);
            this.labelAutLogin.TabIndex = 27;
            this.labelAutLogin.Text = "Отель:";
            // 
            // textBoxAutPass
            // 
            this.textBoxAutPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAutPass.Location = new System.Drawing.Point(321, 103);
            this.textBoxAutPass.Name = "textBoxAutPass";
            this.textBoxAutPass.Size = new System.Drawing.Size(249, 30);
            this.textBoxAutPass.TabIndex = 26;
            this.textBoxAutPass.Text = "test";
            this.textBoxAutPass.UseSystemPasswordChar = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(187, 172);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(311, 151);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(259, 33);
            this.comboBox1.TabIndex = 32;
            this.comboBox1.Text = "Выберите отель";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBoxAutLogin
            // 
            this.textBoxAutLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAutLogin.Location = new System.Drawing.Point(321, 64);
            this.textBoxAutLogin.Name = "textBoxAutLogin";
            this.textBoxAutLogin.Size = new System.Drawing.Size(249, 30);
            this.textBoxAutLogin.TabIndex = 33;
            this.textBoxAutLogin.Text = "test";
            this.textBoxAutLogin.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(214, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 26);
            this.label1.TabIndex = 34;
            this.label1.Text = "Логин:";
            // 
            // AutorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(753, 209);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxAutLogin);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.labelComboBox);
            this.Controls.Add(this.comboBoxAut);
            this.Controls.Add(this.buttonAutOK);
            this.Controls.Add(this.labelAutPass);
            this.Controls.Add(this.labelAutLogin);
            this.Controls.Add(this.textBoxAutPass);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "AutorizationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.AutorizationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelComboBox;
        private System.Windows.Forms.ComboBox comboBoxAut;
        private System.Windows.Forms.Button buttonAutOK;
        private System.Windows.Forms.Label labelAutPass;
        private System.Windows.Forms.Label labelAutLogin;
        private System.Windows.Forms.TextBox textBoxAutPass;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBoxAutLogin;
        private System.Windows.Forms.Label label1;
    }
}