namespace CsvFormsApp
{
    partial class Form1
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.broweButton = new System.Windows.Forms.Button();
            this.filePathBox = new System.Windows.Forms.TextBox();
            this.loadFile = new System.Windows.Forms.Button();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.pswBox = new System.Windows.Forms.TextBox();
            this.serverLabel = new System.Windows.Forms.Label();
            this.loginLabel = new System.Windows.Forms.Label();
            this.pswLabel = new System.Windows.Forms.Label();
            this.dataBaseBox = new System.Windows.Forms.TextBox();
            this.dataBaseLabel = new System.Windows.Forms.Label();
            this.periodLabel = new System.Windows.Forms.Label();
            this.serverBox = new System.Windows.Forms.TextBox();
            this.periodBox = new System.Windows.Forms.TextBox();
            this.checkBoxCurrentFlow = new System.Windows.Forms.CheckBox();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "(*.csv)|*.csv";
            // 
            // broweButton
            // 
            this.broweButton.Location = new System.Drawing.Point(508, 274);
            this.broweButton.Name = "broweButton";
            this.broweButton.Size = new System.Drawing.Size(75, 23);
            this.broweButton.TabIndex = 0;
            this.broweButton.Text = "Browse";
            this.broweButton.UseVisualStyleBackColor = true;
            this.broweButton.Click += new System.EventHandler(this.broweButton_Click);
            // 
            // filePathBox
            // 
            this.filePathBox.Location = new System.Drawing.Point(39, 275);
            this.filePathBox.Name = "filePathBox";
            this.filePathBox.ReadOnly = true;
            this.filePathBox.Size = new System.Drawing.Size(439, 23);
            this.filePathBox.TabIndex = 1;
            this.filePathBox.TextChanged += new System.EventHandler(this.filePathBox_TextChanged);
            // 
            // loadFile
            // 
            this.loadFile.Location = new System.Drawing.Point(273, 304);
            this.loadFile.Name = "loadFile";
            this.loadFile.Size = new System.Drawing.Size(75, 23);
            this.loadFile.TabIndex = 2;
            this.loadFile.Text = "Загрузить";
            this.loadFile.UseVisualStyleBackColor = true;
            this.loadFile.Click += new System.EventHandler(this.loadFile_Click);
            // 
            // loginBox
            // 
            this.loginBox.Location = new System.Drawing.Point(115, 84);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(188, 23);
            this.loginBox.TabIndex = 3;
            this.loginBox.TextChanged += new System.EventHandler(this.loginBox_TextChanged);
            // 
            // pswBox
            // 
            this.pswBox.Location = new System.Drawing.Point(115, 132);
            this.pswBox.Name = "pswBox";
            this.pswBox.PasswordChar = '*';
            this.pswBox.Size = new System.Drawing.Size(188, 23);
            this.pswBox.TabIndex = 5;
            this.pswBox.TextChanged += new System.EventHandler(this.pswBox_TextChanged);
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(39, 38);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(50, 15);
            this.serverLabel.TabIndex = 6;
            this.serverLabel.Text = "Сервер:";
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(45, 87);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(44, 15);
            this.loginLabel.TabIndex = 7;
            this.loginLabel.Text = "Логин:";
            // 
            // pswLabel
            // 
            this.pswLabel.AutoSize = true;
            this.pswLabel.Location = new System.Drawing.Point(37, 135);
            this.pswLabel.Name = "pswLabel";
            this.pswLabel.Size = new System.Drawing.Size(52, 15);
            this.pswLabel.TabIndex = 8;
            this.pswLabel.Text = "Пароль:";
            // 
            // dataBaseBox
            // 
            this.dataBaseBox.Location = new System.Drawing.Point(115, 181);
            this.dataBaseBox.Name = "dataBaseBox";
            this.dataBaseBox.Size = new System.Drawing.Size(188, 23);
            this.dataBaseBox.TabIndex = 9;
            this.dataBaseBox.TextChanged += new System.EventHandler(this.dataBaseBox_TextChanged);
            // 
            // dataBaseLabel
            // 
            this.dataBaseLabel.AutoSize = true;
            this.dataBaseLabel.Location = new System.Drawing.Point(55, 181);
            this.dataBaseLabel.Name = "dataBaseLabel";
            this.dataBaseLabel.Size = new System.Drawing.Size(34, 15);
            this.dataBaseLabel.TabIndex = 10;
            this.dataBaseLabel.Text = "База:";
            // 
            // periodLabel
            // 
            this.periodLabel.AutoSize = true;
            this.periodLabel.Location = new System.Drawing.Point(51, 231);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(49, 15);
            this.periodLabel.TabIndex = 12;
            this.periodLabel.Text = "Период";
            // 
            // serverBox
            // 
            this.serverBox.Location = new System.Drawing.Point(115, 36);
            this.serverBox.Name = "serverBox";
            this.serverBox.Size = new System.Drawing.Size(188, 23);
            this.serverBox.TabIndex = 13;
            this.serverBox.TextChanged += new System.EventHandler(this.serverBox_TextChanged);
            // 
            // periodBox
            // 
            this.periodBox.Location = new System.Drawing.Point(115, 228);
            this.periodBox.Name = "periodBox";
            this.periodBox.Size = new System.Drawing.Size(188, 23);
            this.periodBox.TabIndex = 14;
            this.periodBox.TextChanged += new System.EventHandler(this.periodBox_TextChanged);
            // 
            // checkBoxCurrentFlow
            // 
            this.checkBoxCurrentFlow.AutoSize = true;
            this.checkBoxCurrentFlow.Location = new System.Drawing.Point(358, 36);
            this.checkBoxCurrentFlow.Name = "checkBoxCurrentFlow";
            this.checkBoxCurrentFlow.Size = new System.Drawing.Size(225, 19);
            this.checkBoxCurrentFlow.TabIndex = 15;
            this.checkBoxCurrentFlow.Text = "Загрузить с текущими показаниями";
            this.checkBoxCurrentFlow.UseVisualStyleBackColor = true;
            this.checkBoxCurrentFlow.CheckedChanged += new System.EventHandler(this.checkBoxCurrentFlow_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 333);
            this.Controls.Add(this.checkBoxCurrentFlow);
            this.Controls.Add(this.periodBox);
            this.Controls.Add(this.serverBox);
            this.Controls.Add(this.periodLabel);
            this.Controls.Add(this.dataBaseLabel);
            this.Controls.Add(this.dataBaseBox);
            this.Controls.Add(this.pswLabel);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.pswBox);
            this.Controls.Add(this.loginBox);
            this.Controls.Add(this.loadFile);
            this.Controls.Add(this.filePathBox);
            this.Controls.Add(this.broweButton);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CsvMaker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenFileDialog openFileDialog;
        private Button broweButton;
        private TextBox filePathBox;
        private Button loadFile;
        private TextBox loginBox;
        private TextBox pswBox;
        private Label serverLabel;
        private Label loginLabel;
        private Label pswLabel;
        private TextBox dataBaseBox;
        private Label dataBaseLabel;
        private Label periodLabel;
        private TextBox serverBox;
        private TextBox periodBox;
        private CheckBox checkBoxCurrentFlow;
    }
}