using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace billing
{
    public partial class clientlist : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Lenovo\\source\\repos\\billing\\Database1.mdf;Integrated Security=True";
        private string placeholderText = "Search by customer name or mobile number";
        public clientlist()
        {
            InitializeComponent();
            InitializePlaceholder();
        }
        private void InitializePlaceholder()
        {
            search.Text = placeholderText;
            search.ForeColor = Color.Gray;
            search.Enter += new EventHandler(RemovePlaceholder);
            search.Leave += new EventHandler(AddPlaceholder);
        }
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (search.Text == placeholderText)
            {
                search.Text = "";
                search.ForeColor = Color.Black;
            }
        }
        private void AddPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(search.Text))
            {
                search.Text = placeholderText;
                search.ForeColor = Color.Gray;
            }
        }
        private void clientlist_Load(object sender, EventArgs e)
        {
            FetchAndPopulateData();
        }

        private void FetchAndPopulateData()
        {
            CustomerdataGridView.Rows.Clear();

            string query = "SELECT ROW_NUMBER() OVER (ORDER BY customer_name) AS srnum, " +
                           "id, customer_name AS cusname, mobile_no AS cusmobile, " +
                           "invoice_number AS invno, invoice_date AS invdate, " +
                           "buyers_ord_date AS orderdate, net_total AS nettotal, " +
                           "status " +
                           "FROM customers";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    CreateActionColumns();

                    while (reader.Read())
                    {
                        int rowIndex = CustomerdataGridView.Rows.Add();
                        DataGridViewRow row = CustomerdataGridView.Rows[rowIndex];

                        row.Cells["srnum"].Value = reader["srnum"].ToString();
                        row.Cells["cusname"].Value = reader["cusname"].ToString();
                        row.Cells["cusmobile"].Value = reader["cusmobile"].ToString();
                        row.Cells["invno"].Value = reader["invno"].ToString();
                        row.Cells["invdate"].Value = Convert.ToDateTime(reader["invdate"]).ToString("dd MMM yyyy");
                        row.Cells["orderdate"].Value = Convert.ToDateTime(reader["orderdate"]).ToString("dd MMM yyyy");
                        row.Cells["nettotal"].Value = Convert.ToDecimal(reader["nettotal"]).ToString("N2");

                        string statusValue = reader["status"].ToString();
                        row.Cells["status"].Value = statusValue == "1" ? "PAID" : "UNPAID";
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CreateActionColumns()
        {
            if (!CustomerdataGridView.Columns.Contains("actions_view"))
            {
                DataGridViewButtonColumn viewColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "View",
                    Name = "actions_view",
                    Text = "View",
                    UseColumnTextForButtonValue = true
                };
                CustomerdataGridView.Columns.Add(viewColumn);
            }

            if (!CustomerdataGridView.Columns.Contains("actions_print"))
            {
                DataGridViewButtonColumn printColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Print",
                    Name = "actions_print",
                    Text = "Print",
                    UseColumnTextForButtonValue = true
                };
                CustomerdataGridView.Columns.Add(printColumn);
            }

            if (!CustomerdataGridView.Columns.Contains("actions_edit"))
            {
                DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Edit",
                    Name = "actions_edit",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true
                };
                CustomerdataGridView.Columns.Add(editColumn);
            }

            if (!CustomerdataGridView.Columns.Contains("actions_delete"))
            {
                DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Delete",
                    Name = "actions_delete",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true
                };
                CustomerdataGridView.Columns.Add(deleteColumn);
            }
        }

        private void CustomerdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = CustomerdataGridView.Columns[e.ColumnIndex].Name;
                string invoiceNumber = CustomerdataGridView.Rows[e.RowIndex].Cells["invno"].Value.ToString();

                if (columnName == "actions_delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DeleteRecord(invoiceNumber);
                        FetchAndPopulateData();
                    }
                }
                else if (columnName == "actions_view")
                {
                    viewClient viewClientForm = new viewClient(invoiceNumber);
                    viewClientForm.Show();
                }
                else if (columnName == "actions_print")
                {
                    DatabaseHelper.GeneratePdf(invoiceNumber);
                }
                else if (columnName == "actions_edit")
                {
                    using (editBill eb= new editBill(invoiceNumber))
                    {
                        if (eb.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show("InfoForm closed with OK.");
                        }
                    }
                }
            }
        }

        private void DeleteRecord(string invoiceNumber)
        {
            string query = "DELETE FROM customers WHERE invoice_number = @invoiceNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = search.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm) || searchTerm == placeholderText)
            {
                if (searchTerm == placeholderText)
                {
                    return;
                }

                FetchAndPopulateData();
                return;
            }

            // Proceed with searching
            string query = "SELECT ROW_NUMBER() OVER (ORDER BY customer_name) AS srnum, " +
                           "id, customer_name AS cusname, mobile_no AS cusmobile, " +
                           "invoice_number AS invno, invoice_date AS invdate, " +
                           "buyers_ord_date AS orderdate, net_total AS nettotal, " +
                           "status " +
                           "FROM customers " +
                           "WHERE customer_name LIKE @searchTerm OR mobile_no LIKE @searchTerm";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    CustomerdataGridView.Rows.Clear();
                    CreateActionColumns();

                    while (reader.Read())
                    {
                        int rowIndex = CustomerdataGridView.Rows.Add();
                        DataGridViewRow row = CustomerdataGridView.Rows[rowIndex];

                        row.Cells["srnum"].Value = reader["srnum"].ToString();
                        row.Cells["cusname"].Value = reader["cusname"].ToString();
                        row.Cells["cusmobile"].Value = reader["cusmobile"].ToString();
                        row.Cells["invno"].Value = reader["invno"].ToString();
                        row.Cells["invdate"].Value = Convert.ToDateTime(reader["invdate"]).ToString("dd MMM yyyy");
                        row.Cells["orderdate"].Value = Convert.ToDateTime(reader["orderdate"]).ToString("dd MMM yyyy");
                        row.Cells["nettotal"].Value = Convert.ToDecimal(reader["nettotal"]).ToString("N2");

                        string statusValue = reader["status"].ToString();
                        row.Cells["status"].Value = statusValue == "1" ? "PAID" : "UNPAID";
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
