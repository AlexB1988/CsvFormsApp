using CsvFormsApp.Domain.Entities;
using CsvFormsApp.Services;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace CsvFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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
        private void loadFile_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            CounterListService _objectList = new CounterListService();
            string path = filePathBox.Text;
            string server = serverBox.Text;
            string login = loginBox.Text;
            string psw = pswBox.Text;
            string dataBase = dataBaseBox.Text;
            int period = int.Parse(periodBox.Text);
            string connectionString = $"Server={server};User={login};Password={psw};Database={dataBase}" +
                                        $";TrustServerCertificate=true;";
            _objectList.GetObjectList(path, period, connectionString);
            form2.Hide();
            MessageBox.Show(
                "Данные успешно загружены!",
                "Message",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }
        private void loginBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pswBox_TextChanged(object sender, EventArgs e)
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
    }
}