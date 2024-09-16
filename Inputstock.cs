using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace billing
{
    public partial class Inputstock : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Lenovo\\source\\repos\\billing\\Database1.mdf;Integrated Security=True";

        public Inputstock()
        {
            InitializeComponent();
            LoadTypes();

            // Subscribe to the SelectedIndexChanged event
            typeComboBox.SelectedIndexChanged += TypeComboBox_SelectedIndexChanged;
        }

        private void LoadTypes()
        {
            typeComboBox.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT DISTINCT type FROM stocks";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            typeComboBox.Items.Add(reader["type"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = typeComboBox.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedType))
            {
                UpdateStockAndAlertLimit(selectedType);
            }
        }

        private void UpdateStockAndAlertLimit(string type)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT stock, alert_lim FROM stocks WHERE type = @type";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@type", type);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            currentst.Text = reader["stock"].ToString();
                            altliminp.Text = reader["alert_lim"].ToString();
                        }
                        else
                        {
                            currentst.Text = "Stock not found";
                            altliminp.Text = string.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void updatestock_Click(object sender, EventArgs e)
        {
            string selectedType = typeComboBox.SelectedItem?.ToString();
            string stockInput = stockinp.Text;
            string alertLimInput = altliminp.Text;

            if (string.IsNullOrEmpty(selectedType))
            {
                MessageBox.Show("Please select a type from the ComboBox.");
                return;
            }

            if (string.IsNullOrEmpty(stockInput) || !decimal.TryParse(stockInput, out decimal stock))
            {
                MessageBox.Show("Please enter a valid stock value.");
                return;
            }

            if (string.IsNullOrEmpty(alertLimInput) || !int.TryParse(alertLimInput, out int alertLim))
            {
                MessageBox.Show("Please enter a valid alert limit value.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        // Update the stock
                        string updateQuery = @"UPDATE stocks 
                SET stock = stock + @stock, alert_lim = @alertLim 
                WHERE type = @type";

                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@type", selectedType);
                            updateCommand.Parameters.AddWithValue("@stock", stock);
                            updateCommand.Parameters.AddWithValue("@alertLim", alertLim);

                            int rowsAffected = updateCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                MessageBox.Show("No record found with the selected type.");
                                transaction.Rollback();
                                return;
                            }
                        }

                        string stockIdQuery = "SELECT id FROM stocks WHERE type = @type";
                        int stockId;

                        using (SqlCommand stockIdCommand = new SqlCommand(stockIdQuery, connection, transaction))
                        {
                            stockIdCommand.Parameters.AddWithValue("@type", selectedType);

                            object result = stockIdCommand.ExecuteScalar();
                            if (result != null)
                            {
                                stockId = Convert.ToInt32(result);
                            }
                            else
                            {
                                MessageBox.Show("Could not retrieve stock ID.");
                                transaction.Rollback();
                                return;
                            }
                        }

                        string insertQuery = @"INSERT INTO stock_in (stock_id, added_stock, added_date) 
                VALUES (@stockId, @addedStock, @addedDate)";

                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                        {
                            insertCommand.Parameters.AddWithValue("@stockId", stockId);
                            insertCommand.Parameters.AddWithValue("@addedStock", stock);
                            insertCommand.Parameters.AddWithValue("@addedDate", DateTime.Now);

                            insertCommand.ExecuteNonQuery();
                        }

                        // Create an instance of DatabaseHelper
                        DatabaseHelper helper = new DatabaseHelper();

                        // Call the instance method
                        helper.CalculateAndUpdateStock(stockId, connection, transaction);

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
