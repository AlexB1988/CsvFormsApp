using CsvFormsApp.Services;

namespace CsvFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            broweButton.Click += broweButton_Click;
            loadFile.Click+=loadFile_Click;
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
        private void severBox_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void loadFile_Click(object sender, EventArgs e)
        {
            CounterListService _objectList = new CounterListService();
            string path=filePathBox.Text;
            string server = severBox.Text;
            string login = loginBox.Text;
            string psw=pswBox.Text;
            string dataBase=dataBaseBox.Text;
            int period = int.Parse(periodBox.Text);
            string connectionString= $"Server={server};User={login};Password={psw};Database={dataBase};TrustServerCertificate=true;";
            _objectList.GetObjectList(path,period,connectionString);
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

        private void perioBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}