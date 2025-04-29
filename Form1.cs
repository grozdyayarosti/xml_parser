using System.Data;

namespace xml_parser
{
    public partial class Form1 : Form
    {
        private string currentXmlPath = "data.xml";

        public Form1()
        {
            InitializeComponent();
            LoadXmlData();
        }

        private void LoadXmlData()
        {
            try
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(currentXmlPath);

                if (dataSet.Tables.Count > 0)
                {
                    dataGridView1.DataSource = dataSet.Tables[0];
                    ConfigureDataGridView();
                    lblStatus.Text = $"Data is succesfully loaded from {currentXmlPath}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Uploading error from XML: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = (DataTable)dataGridView1.DataSource;
                if (dataTable != null)
                {
                    dataTable.WriteXml(currentXmlPath, XmlWriteMode.WriteSchema);
                    lblStatus.Text = $"Data is succesfully saved to {currentXmlPath}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Saving error from XML: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)dataGridView1.DataSource;
            if (dataTable != null)
            {
                DataRow newRow = dataTable.NewRow();
                dataTable.Rows.Add(newRow);
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentXmlPath = openFileDialog.FileName;
                txtFilePath.Text = currentXmlPath;
                LoadXmlData();
            }
        }
    }
}