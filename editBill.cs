using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billing
{
    public partial class editBill : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Lenovo\\source\\repos\\billing\\Database1.mdf;Integrated Security=True";
        private DataTable itemsTable = new DataTable();
        private string invoiceNumber;

        private decimal sgstPercentage = 0.0M;
        private decimal cgstPercentage = 0.0M;
        private bool canSelectRow = true;

        private Dictionary<string, string> snameDisplayTexts = new Dictionary<string, string>
        {
            { "GOLDRATE18", "Gold - 18K" },
            { "GOLDRATE20", "Gold - 20K" },
            { "GOLDRATE22", "Gold - 22K" },
            { "SILVERRATE45", "Silver - 45" },
            { "SILVERRATE60", "Silver - 60" },
            { "SILVERRATE70", "Silver - 70" },
            { "SILVERRATE92.5", "Silver - 92.5" },
            { "SILVERRATEFINE", "Silver - Fine" }
        };

        public editBill(string invoiceNumber)
        {
            InitializeComponent();
            PopulateTypeBox();
            this.invoiceNumber = invoiceNumber;
            this.Load += new System.EventHandler(this.editBill_Load);
            dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            dataGridView1.RowsRemoved += DataGridView1_RowsRemoved;
        }

        private void editBill_Load(object sender, EventArgs e)
        {
            FetchAndDisplayData();
            DisplayTaxSettings();
            CalculateAndDisplayTotalAmount();
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void DisplayTaxSettings()
        {
            sgstPercentage = FetchTaxPercentage("SGST");
            cgstPercentage = FetchTaxPercentage("CGST");
        }

        private decimal FetchTaxPercentage(string taxName)
        {
            decimal percentage = 0.0M;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT parameter FROM settings WHERE sname = @sname";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@sname", taxName);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out decimal parsedValue))
                        {
                            percentage = parsedValue;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error fetching {taxName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return percentage;
        }

        private void PopulateTypeBox()
        {
            typebox.Items.Clear();
            foreach (string sname in snameDisplayTexts.Keys)
            {
                typebox.Items.Add(snameDisplayTexts[sname]);
            }
        }

        private void typebox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDisplayText = typebox.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedDisplayText))
            {
                string selectedSname = snameDisplayTexts.FirstOrDefault(x => x.Value == selectedDisplayText).Key;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand query = new SqlCommand("SELECT parameter FROM settings WHERE sname = @sname", conn);
                    query.Parameters.AddWithValue("@sname", selectedSname);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = query.ExecuteReader();
                        if (reader.Read())
                        {
                            string parameter = reader["parameter"].ToString();
                            rateinp.Text = parameter;
                            CalculateTotalAmount();
                        }
                        else
                        {
                            rateinp.Text = "";
                            totalamount.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error fetching settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void netwgt_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }

        private void rateinp_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }

        private void CalculateTotalAmount()
        {
            decimal totalAmount = 0.00M;

            if (decimal.TryParse(rateinp.Text, out decimal rate) && rate != 0 &&
                decimal.TryParse(netwgt.Text, out decimal weight))
            {
                totalAmount = rate * weight;
            }

            totalamount.Text = totalAmount.ToString("0.00");
        }

        private void FetchAndDisplayData()
        {
            string queryCustomer = "SELECT * FROM customers WHERE invoice_number = @invoiceNumber";
            string queryItems = "SELECT item_name, item_type, hsn, pcs, net_wgt, fine_wgt, rate, item_total " +
                                "FROM item_details WHERE cus_id = (SELECT id FROM customers WHERE invoice_number = @invoiceNumber)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand commandCustomer = new SqlCommand(queryCustomer, connection);
                commandCustomer.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);

                SqlCommand commandItems = new SqlCommand(queryItems, connection);
                commandItems.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);

                try
                {
                    connection.Open();
                    SqlDataReader readerCustomer = commandCustomer.ExecuteReader();

                    if (readerCustomer.Read())
                    {
                        // Populate customer information
                        invnum.Text = readerCustomer["invoice_number"].ToString();
                        cusname.Text = readerCustomer["customer_name"].ToString();
                        mobileval.Text = readerCustomer["mobile_no"].ToString();
                        addressTextbox.Text = readerCustomer["address"].ToString();
                        cusgst.Text = readerCustomer["customer_gst"].ToString();
                        paymentby.Text = readerCustomer["cuspaymentby"].ToString();
                        paydtl.Text = readerCustomer["paymentdetail"].ToString();
                        ordno.Text = readerCustomer["buyers_ord_no"].ToString();
                        disthrough.Text = readerCustomer["dispetch_through"].ToString();
                        payterm.Text = readerCustomer["payment_term"].ToString();
                        lcharge.Text = readerCustomer["labour_charge"].ToString();
                        roundoff.Text = readerCustomer["roundoff"].ToString();
                        igsttxt.Text = readerCustomer["igst"].ToString();
                        cgsttxt.Text = readerCustomer["cgst"].ToString();
                        sgsttxt.Text = readerCustomer["sgst"].ToString();

                        sgstPercentage = readerCustomer["sgst"] != DBNull.Value ? Convert.ToDecimal(readerCustomer["sgst"]) : 0;
                        cgstPercentage = readerCustomer["cgst"] != DBNull.Value ? Convert.ToDecimal(readerCustomer["cgst"]) : 0;

                        string statusValue = readerCustomer["status"].ToString();
                        paid.Checked = statusValue == "1";
                        unpaid.Checked = statusValue != "1";

                        invdate.Text = Convert.ToDateTime(readerCustomer["invoice_date"]).ToString("dd MMM yyyy");
                        orddate.Text = Convert.ToDateTime(readerCustomer["buyers_ord_date"]).ToString("dd MMM yyyy");
                        disdate.Text = Convert.ToDateTime(readerCustomer["dispetch_date"]).ToString("dd MMM yyyy");
                    }
                    else
                    {
                        MessageBox.Show("No data found for the provided invoice number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    readerCustomer.Close();

                    itemsTable.Columns.Clear(); 
                    itemsTable.Columns.Add("item_name", typeof(string));
                    itemsTable.Columns.Add("item_type", typeof(string));
                    itemsTable.Columns.Add("hsn", typeof(string));
                    itemsTable.Columns.Add("pcs", typeof(int));
                    itemsTable.Columns.Add("net_wgt", typeof(decimal));
                    itemsTable.Columns.Add("fine_wgt", typeof(decimal));
                    itemsTable.Columns.Add("rate", typeof(decimal));
                    itemsTable.Columns.Add("item_total", typeof(decimal));

                    SqlDataAdapter adapterItems = new SqlDataAdapter(commandItems);
                    adapterItems.Fill(itemsTable);

                    dataGridView1.DataSource = itemsTable;

                    dataGridView1.Columns["item_name"].HeaderText = "Item Name";
                    dataGridView1.Columns["item_type"].HeaderText = "Item Type";
                    dataGridView1.Columns["hsn"].HeaderText = "HSN";
                    dataGridView1.Columns["pcs"].HeaderText = "PCS";
                    dataGridView1.Columns["net_wgt"].HeaderText = "Net Weight";
                    dataGridView1.Columns["fine_wgt"].HeaderText = "Fine Weight";
                    dataGridView1.Columns["rate"].HeaderText = "Rate";
                    dataGridView1.Columns["item_total"].HeaderText = "Total";
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"SQL Error: {sqlEx.Message}", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void additem_Click(object sender, EventArgs e)
        {
            string itemName = itemname.Text;
            string itemType = typebox.SelectedItem?.ToString();
            string hsn = FetchHNSFromDatabase();
            int pcs = 0;
            decimal netWeight = 0.0M;
            decimal fineWeight = 0.0M;
            decimal rate = 0.0M;

            if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(itemType) ||
                !int.TryParse(pcsinp.Text, out pcs) ||
                !decimal.TryParse(netwgt.Text, out netWeight) ||
                !decimal.TryParse(finwegt.Text, out fineWeight) ||
                !decimal.TryParse(rateinp.Text, out rate))
            {
                MessageBox.Show("Please enter valid item details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal itemTotal = netWeight * rate;

            DataRow newRow = itemsTable.NewRow();
            newRow["item_name"] = itemName;
            newRow["item_type"] = itemType;
            newRow["hsn"] = hsn;
            newRow["pcs"] = pcs;
            newRow["net_wgt"] = netWeight;
            newRow["fine_wgt"] = fineWeight;
            newRow["rate"] = rate;
            newRow["item_total"] = itemTotal;
            itemsTable.Rows.Add(newRow);

            dataGridView1.DataSource = itemsTable;

            CalculateAndDisplayTotalAmount();
        }

        private string FetchHNSFromDatabase()
        {
            string hnsValue = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT parameter FROM settings WHERE sname = 'HSN'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        hnsValue = cmd.ExecuteScalar()?.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error fetching HNS: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return hnsValue;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (canSelectRow && dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                itemname.Text = selectedRow.Cells["item_name"].Value?.ToString();
                netwgt.Text = selectedRow.Cells["net_wgt"].Value?.ToString();
                finwegt.Text = selectedRow.Cells["fine_wgt"].Value?.ToString();
                pcsinp.Text = selectedRow.Cells["pcs"].Value?.ToString();

                if (typebox.Items.Contains(selectedRow.Cells["item_type"].Value))
                {
                    typebox.SelectedItem = selectedRow.Cells["item_type"].Value.ToString();
                }
                else
                {
                    typebox.SelectedIndex = -1;
                }
            }
            else
            {
                itemname.Text = "";
                typebox.SelectedIndex = -1;
                netwgt.Text = "";
                finwegt.Text = "";
                pcsinp.Text = "";
            }
        }

        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateAndDisplayTotalAmount();
        }

        private void DataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateAndDisplayTotalAmount();
        }

        private void lcharge_TextChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayTotalAmount();
        }

        private decimal previousRoundOffValue = 0.00M;

        private void roundoff_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(roundoff.Text, out decimal currentRoundOffValue))
            {
                decimal netTotal = decimal.Parse(nettotalval.Text);
                decimal difference = currentRoundOffValue - previousRoundOffValue;
                decimal adjustedNetTotal = netTotal - difference;
                nettotalval.Text = adjustedNetTotal.ToString("0.00");
                previousRoundOffValue = currentRoundOffValue;
            }
        }

        private void CalculateAndDisplayTotalAmount()
        {
            decimal totalAmount = 0.00M;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow && decimal.TryParse(row.Cells["item_total"].Value.ToString(), out decimal rowAmount))
                {
                    totalAmount += rowAmount;
                }
            }

            decimal lchargeAmount = 0.00M;
            if (decimal.TryParse(lcharge.Text, out decimal additionalCharge))
            {
                lchargeAmount = additionalCharge;
            }

            decimal basicTotal = totalAmount;
            basicval.Text = basicTotal.ToString("0.00");

            decimal sgstAmount = basicTotal * (sgstPercentage / 100);
            decimal cgstAmount = basicTotal * (cgstPercentage / 100);

            sgstval.Text = sgstAmount.ToString("0.00");
            cgstval.Text = cgstAmount.ToString("0.00");

            decimal netTotal = basicTotal + lchargeAmount + sgstAmount + cgstAmount;

            decimal roundOff = netTotal % 10;
            decimal roundedNetTotal = netTotal - roundOff;

            nettotalval.Text = roundedNetTotal.ToString("0.00");
            roundoff.Text = roundOff.ToString("0.00");
        }

        private void updatedata_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDataInDatabase();
                MessageBox.Show("Data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDataInDatabase()
        {
            string invoiceNumber = invnum.Text;
            string address = addressTextbox.Text;
            string customerName = cusname.Text;
            string cusmobile = mobileval.Text;
            string customerGST = cusgst.Text;
            string paymentBy = paymentby.Text;
            string paymentTerm = payterm.Text;
            DateTime invoiceDate = invdate.Value;
            string buyersOrderNo = ordno.Text;
            DateTime buyersOrderDate = orddate.Value;
            string dispatchThrough = disthrough.Text;
            DateTime dispatchDate = disdate.Value;
            string paymentDetail = paydtl.Text;
            decimal lchargeValue = Convert.ToDecimal(lcharge.Text);
            decimal roundoffValue = Convert.ToDecimal(roundoff.Text);
            decimal nettotalvalValue = Convert.ToDecimal(nettotalval.Text);

            string igstValue = igsttxt.Text;
            string cgstValue = cgsttxt.Text;
            string sgstValue = sgsttxt.Text;

            int statusValue = paid.Checked ? 1 : 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string updateCustomerQuery = 
                    @"UPDATE customers SET 
                    address = @address, 
                    mobile_no = @cusmobile, 
                    customer_name = @customerName, 
                    customer_gst = @customerGST, 
                    cuspaymentby = @paymentBy, 
                    payment_term = @paymentTerm, 
                    invoice_date = @invoiceDate, 
                    buyers_ord_no = @buyersOrderNo, 
                    buyers_ord_date = @buyersOrderDate, 
                    dispetch_through = @dispatchThrough, 
                    dispetch_date = @dispatchDate, 
                    paymentdetail = @paymentDetail, 
                    IGST = @igstValue, 
                    CGST = @cgstValue, 
                    SGST = @sgstValue, 
                    labour_charge = @lcharge, 
                    roundoff = @roundoff, 
                    net_total = @nettotalval, 
                    status = @status 
                    WHERE invoice_number = @invoiceNumber";

                    using (SqlCommand cmd = new SqlCommand(updateCustomerQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@cusmobile", cusmobile);
                        cmd.Parameters.AddWithValue("@customerName", customerName);
                        cmd.Parameters.AddWithValue("@customerGST", customerGST);
                        cmd.Parameters.AddWithValue("@paymentBy", paymentBy);
                        cmd.Parameters.AddWithValue("@paymentTerm", paymentTerm);
                        cmd.Parameters.AddWithValue("@invoiceDate", invoiceDate);
                        cmd.Parameters.AddWithValue("@buyersOrderNo", buyersOrderNo);
                        cmd.Parameters.AddWithValue("@buyersOrderDate", buyersOrderDate);
                        cmd.Parameters.AddWithValue("@dispatchThrough", dispatchThrough);
                        cmd.Parameters.AddWithValue("@dispatchDate", dispatchDate);
                        cmd.Parameters.AddWithValue("@paymentDetail", paymentDetail);
                        cmd.Parameters.AddWithValue("@igstValue", Convert.ToDecimal(igstValue));
                        cmd.Parameters.AddWithValue("@cgstValue", Convert.ToDecimal(cgstValue));
                        cmd.Parameters.AddWithValue("@sgstValue", Convert.ToDecimal(sgstValue));
                        cmd.Parameters.AddWithValue("@lcharge", lchargeValue);
                        cmd.Parameters.AddWithValue("@roundoff", roundoffValue);
                        cmd.Parameters.AddWithValue("@nettotalval", nettotalvalValue);
                        cmd.Parameters.AddWithValue("@status", statusValue);

                        cmd.ExecuteNonQuery();
                    }

                    string deleteItemDetailsQuery = "DELETE FROM item_details WHERE cus_id = (SELECT id FROM customers WHERE invoice_number = @invoiceNumber)";

                    using (SqlCommand deleteItemDetailsCmd = new SqlCommand(deleteItemDetailsQuery, conn, transaction))
                    {
                        deleteItemDetailsCmd.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
                        deleteItemDetailsCmd.ExecuteNonQuery();
                    }

                    string deleteStockOutQuery = "DELETE FROM stock_out WHERE invoice_no = @invoiceNumber";

                    using (SqlCommand deleteStockOutCmd = new SqlCommand(deleteStockOutQuery, conn, transaction))
                    {
                        deleteStockOutCmd.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
                        deleteStockOutCmd.ExecuteNonQuery();
                    }

                    string insertItemQuery = @"INSERT INTO item_details (cus_id, item_name, item_type, hsn, pcs, rate, net_wgt, fine_wgt, item_total) 
                        VALUES (@cusId, @itemName, @itemType, @hsn, @pcs, @rate, @netWgt, @fineWgt, @itemTotal)";
                    string getStockIdQuery = "SELECT id FROM stocks WHERE type = @stockType";
                    //string getStockQuantityQuery = "SELECT stock FROM stocks WHERE id = @stockId";
                    string stockOutInsertQuery = @"INSERT INTO stock_out (stock_id, used_stock, used_date, invoice_no) 
                        VALUES (@stockId, @usedStock, @usedDate, @invoiceNo)";

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string itemName = Convert.ToString(row.Cells["item_name"].Value);
                        string itemType = Convert.ToString(row.Cells["item_type"].Value);
                        string hsn = Convert.ToString(row.Cells["hsn"].Value);
                        int pcs = Convert.ToInt32(row.Cells["pcs"].Value);
                        decimal rate = Convert.ToDecimal(row.Cells["rate"].Value);
                        decimal netWgt = Convert.ToDecimal(row.Cells["net_wgt"].Value);
                        decimal fineWgt = Convert.ToDecimal(row.Cells["fine_wgt"].Value);
                        decimal itemTotal = Convert.ToDecimal(row.Cells["item_total"].Value);

                        string stockType;
                        if (itemType.StartsWith("Gold"))
                        {
                            stockType = "GOLD";
                        }
                        else if (itemType.StartsWith("Silver"))
                        {
                            stockType = "SILVER";
                        }
                        else
                        {
                            MessageBox.Show($"Unknown item type '{itemType}'.");
                            continue;
                        }

                        int stockId;

                        using (SqlCommand getStockIdCmd = new SqlCommand(getStockIdQuery, conn, transaction))
                        {
                            getStockIdCmd.Parameters.AddWithValue("@stockType", stockType);

                            object result = getStockIdCmd.ExecuteScalar();
                            if (result != null)
                            {
                                stockId = Convert.ToInt32(result);
                            }
                            else
                            {
                                MessageBox.Show($"Stock type '{stockType}' not found in the database.");
                                transaction.Rollback();
                                return;
                            }
                        }

                        int customerId;

                        using (SqlCommand getCustomerIdCmd = new SqlCommand("SELECT id FROM customers WHERE invoice_number = @invoiceNumber", conn, transaction))
                        {
                            getCustomerIdCmd.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
                            object result = getCustomerIdCmd.ExecuteScalar();
                            if (result != null)
                            {
                                customerId = Convert.ToInt32(result);
                            }
                            else
                            {
                                MessageBox.Show($"Customer with invoice number '{invoiceNumber}' not found.");
                                transaction.Rollback();
                                return;
                            }
                        }

                        using (SqlCommand itemCmd = new SqlCommand(insertItemQuery, conn, transaction))
                        {
                            itemCmd.Parameters.AddWithValue("@cusId", customerId);
                            itemCmd.Parameters.AddWithValue("@itemName", itemName);
                            itemCmd.Parameters.AddWithValue("@itemType", itemType);
                            itemCmd.Parameters.AddWithValue("@hsn", hsn);
                            itemCmd.Parameters.AddWithValue("@pcs", pcs);
                            itemCmd.Parameters.AddWithValue("@rate", rate);
                            itemCmd.Parameters.AddWithValue("@netWgt", netWgt);
                            itemCmd.Parameters.AddWithValue("@fineWgt", fineWgt);
                            itemCmd.Parameters.AddWithValue("@itemTotal", itemTotal);

                            itemCmd.ExecuteNonQuery();
                        }

                        using (SqlCommand stockOutCmd = new SqlCommand(stockOutInsertQuery, conn, transaction))
                        {
                            stockOutCmd.Parameters.AddWithValue("@stockId", stockId);
                            stockOutCmd.Parameters.AddWithValue("@usedStock", netWgt);
                            stockOutCmd.Parameters.AddWithValue("@usedDate", DateTime.Now);
                            stockOutCmd.Parameters.AddWithValue("@invoiceNo", invoiceNumber);

                            stockOutCmd.ExecuteNonQuery();
                        }

                        DatabaseHelper helper = new DatabaseHelper();

                        helper.CalculateAndUpdateStock(stockId, conn, transaction);

                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
