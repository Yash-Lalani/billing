using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace billing
{
    public partial class stockrecord : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Lenovo\\source\\repos\\billing\\Database1.mdf;Integrated Security=True";

        public stockrecord()
        {
            InitializeComponent();
            LoadTypeComboBox();
            StocklistGridView.CellEndEdit += StocklistGridView_CellEndEdit;
        }

        private void StocklistGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the CellValueChanged event is fired
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                StocklistGridView.EndEdit();
            }
        }

        private void LoadTypeComboBox()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT type FROM stocks";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader;

                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();

                    typecheck.Items.Clear();
                    while (reader.Read())
                    {
                        typecheck.Items.Add(reader["type"].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading types: " + ex.Message);
                }
            }
        }

        private void typecheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = typecheck.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedType))
            {
                MessageBox.Show("Please select a type.");
                return;
            }

            FetchStockRecords(selectedType);
        }

        private void FetchStockRecords(string stockType)
        {
            DataTable stockInTable = new DataTable();
            DataTable stockOutTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string stockIdQuery = "SELECT id FROM stocks WHERE type = @type";
                SqlCommand stockIdCommand = new SqlCommand(stockIdQuery, connection);
                stockIdCommand.Parameters.AddWithValue("@type", stockType);

                int stockId;
                try
                {
                    connection.Open();
                    object stockIdResult = stockIdCommand.ExecuteScalar();

                    if (stockIdResult == null)
                    {
                        MessageBox.Show("No stock ID found for the selected type.");
                        return;
                    }

                    stockId = Convert.ToInt32(stockIdResult);

                    // Fetch stock_in records
                    string stockInQuery = "SELECT added_stock, added_date FROM stock_in WHERE stock_id = @stockId";
                    SqlCommand stockInCommand = new SqlCommand(stockInQuery, connection);
                    stockInCommand.Parameters.AddWithValue("@stockId", stockId);

                    SqlDataAdapter stockInAdapter = new SqlDataAdapter(stockInCommand);
                    stockInAdapter.Fill(stockInTable);

                    // Fetch stock_out records
                    string stockOutQuery = "SELECT used_stock, used_date FROM stock_out WHERE stock_id = @stockId";
                    SqlCommand stockOutCommand = new SqlCommand(stockOutQuery, connection);
                    stockOutCommand.Parameters.AddWithValue("@stockId", stockId);

                    SqlDataAdapter stockOutAdapter = new SqlDataAdapter(stockOutCommand);
                    stockOutAdapter.Fill(stockOutTable);

                    // Combine stock_in and stock_out data
                    DataTable combinedTable = new DataTable();
                    combinedTable.Columns.Add("srno", typeof(int));
                    combinedTable.Columns.Add("Type", typeof(string));
                    combinedTable.Columns.Add("Stock IN (./Gram)", typeof(decimal));
                    combinedTable.Columns.Add("Stock OUT (./Gram)", typeof(decimal));
                    combinedTable.Columns.Add("Date", typeof(DateTime));

                    int srno = 1;
                    decimal totalStockIn = 0;
                    decimal totalStockOut = 0;

                    foreach (DataRow row in stockInTable.Rows)
                    {
                        decimal addedStock = Convert.ToDecimal(row["added_stock"]);
                        totalStockIn += addedStock;
                        combinedTable.Rows.Add(
                            srno++,
                            stockType,
                            addedStock,
                            0,
                            row["added_date"]
                        );
                    }

                    foreach (DataRow row in stockOutTable.Rows)
                    {
                        decimal usedStock = Convert.ToDecimal(row["used_stock"]);
                        totalStockOut += usedStock;
                        combinedTable.Rows.Add(
                            srno++,
                            stockType,
                            0,
                            usedStock,
                            row["used_date"]
                        );
                    }

                    decimal totalStock = totalStockIn - totalStockOut;
                    totalstock.Text = $"Total Stock: {totalStock} (./Gram)";

                    StocklistGridView.DataSource = null;
                    StocklistGridView.DataSource = combinedTable;

                    StocklistGridView.Columns["srno"].ReadOnly = true;
                    StocklistGridView.Columns["Type"].ReadOnly = true;
                    StocklistGridView.Columns["Stock OUT (./Gram)"].ReadOnly = true;
                    StocklistGridView.Columns["Date"].ReadOnly = true;

                    StocklistGridView.Columns["Stock IN (./Gram)"].ReadOnly = false;

                    StocklistGridView.Columns["Date"].DefaultCellStyle.Format = "dd-MM-yyyy";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching stock records: " + ex.Message);
                }
            }
        }
    }
}

