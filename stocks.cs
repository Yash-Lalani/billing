using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace billing
{
    public partial class stocks : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Lenovo\\source\\repos\\billing\\Database1.mdf;Integrated Security=True";

        public stocks()
        {
            InitializeComponent();
        }

        private void stocks_Load(object sender, EventArgs e)
        { 
            ConfigureDataGridView();
            PopulateDataGridView();
        }

        private void ConfigureDataGridView()
        {
            CustomerdataGridView.Columns.Clear();
            CustomerdataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "srno",
                HeaderText = "Sr No",
                Width = 50
            });

            CustomerdataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Type",
                HeaderText = "Type",
                Width = 100
            });

            CustomerdataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "alert_lim",
                HeaderText = "Alert Lim.",
                Width = 100
            });

            CustomerdataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock",
                Width = 100
            });
        }

        private void PopulateDataGridView()
        {
            CustomerdataGridView.Rows.Clear();

            DataTable stocksDataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT type, alert_lim, stock FROM stocks";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                try
                {
                    adapter.Fill(stocksDataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                    return;
                }
            }

            int srno = 1;

            foreach (DataRow row in stocksDataTable.Rows)
            {
                CustomerdataGridView.Rows.Add(
                    srno++,
                    row["type"],
                    row["alert_lim"],
                    row["stock"]
                );
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
