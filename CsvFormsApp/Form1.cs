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

        }
    }
}