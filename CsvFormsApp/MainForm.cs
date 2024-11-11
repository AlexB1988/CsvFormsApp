using CsvFormsApp.Domain.Entities;
using CsvFormsApp.Services;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace CsvFormsApp
{
    public partial class MainForm : Form
    {
        static DownLoadForm downLoadForm = new DownLoadForm();
        Task downloadGifTask = new Task(() => downLoadForm.Show());
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void broweButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePathBox.Text = openFileDialog.FileName;
            }
        }

        private void filePathBox_TextChanged(object sender, EventArgs e)
        {
            
        }
        private async void loadFile_Click(object sender, EventArgs e)
        {
            downLoadForm.Show();   
            try
            {
                int sublistID = 0;
                int unitID = 0;
                if (radioXvsButton.Checked)
                {
                    sublistID = 22;
                    unitID = 4;

                }
                if (radioGvsButton.Checked)
                {
                    sublistID = 24;
                    unitID = 4;

                }
                if (radioElButton.Checked)
                {
                    sublistID = 27;
                    unitID = 3;

                }
                if (radioEledButton.Checked)
                {
                    sublistID = 42;
                    unitID = 3;

                }
                if (radioElenButton.Checked)
                {
                    sublistID = 28;
                    unitID = 3;

                }
                if (radioOtButton.Checked)
                {
                    sublistID = 38;
                    unitID = 2;

                }
                if (rateBox.Text.Contains("."))
                {
                    rateBox.Text = rateBox.Text.Replace(".", ",");
                }
                CounterListService _objectList = new CounterListService();
                decimal rate = decimal.Parse(rateBox.Text);
                string path = filePathBox.Text;
                string server = serverBox.Text;
                string login = loginBox.Text;
                string psw = pswBox.Text;
                string dataBase = dataBaseBox.Text;
                int period = int.Parse(periodBox.Text);
                bool currentFlow = checkBoxCurrentFlow.Checked;
                string connectionString = $"Server={server};User={login};Password={psw};Database={dataBase}" +
                                            $";TrustServerCertificate=true;";

                var result = await _objectList.GetObjectList(path, period, connectionString, currentFlow, sublistID, unitID, rate);

				MessageBox.Show(result,
                    "�����������",
	                MessageBoxButtons.OK,
	                MessageBoxIcon.Information,
	                MessageBoxDefaultButton.Button1,
	                MessageBoxOptions.DefaultDesktopOnly);
			}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    ex.GetType().Name,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
            finally
            {
				downLoadForm.Hide();
			}
}
        private void loginBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pswBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void rateBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataBaseBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void serverBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void perioBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void periodBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxCurrentFlow_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rateLabel_Click(object sender, EventArgs e)
        {

        }
    }
}