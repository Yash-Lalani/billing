using Org.BouncyCastle.Asn1.X500;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static iText.Kernel.Pdf.Colorspace.PdfSpecialCs;

namespace billing
{
    public partial class bill : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Lenovo\\source\\repos\\billing\\Database1.mdf;Integrated Security=True";

        public bill()
        {
            InitializeComponent();

            dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            dataGridView1.RowsRemoved += DataGridView1_RowsRemoved;

            PopulateTypeBox();
            netwgt.Text = "1";
            CalculateAndDisplayTotalAmount();
            DisplayTaxSettings();
            disdate.Value = DateTime.Now;
            orddate.Value = DateTime.Now;
            invdate.Value = DateTime.Now;

        }
        private void DisplayTaxSettings()
        {
            sgstPercentage = FetchTaxPercentage("SGST");
            cgstPercentage = FetchTaxPercentage("CGST");

            FetchAndDisplayTax("IGST", igst);
            FetchAndDisplayTax("CGST", cgst);
            FetchAndDisplayTax("SGST", sgst);

            FetchAndDisplayTax("payment_term", panel);
            FetchAndDisplayTax("invoice_number", invnum);
        }

        private void FetchAndDisplayTax(string taxName, Control control)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "";
                if (taxName == "invoice_number")
                {
                    query = "SELECT TOP 1 invoice_number FROM customers ORDER BY invoice_number DESC";
                }
                else
                {
                    query = "SELECT parameter FROM settings WHERE sname = @sname";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (taxName == "invoice_number")
                    {
                        try
                        {
                            conn.Open();
                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                int currentInvoiceNumber = Convert.ToInt32(result);
                                int incrementedInvoiceNumber = currentInvoiceNumber + 1;
                                control.Text = incrementedInvoiceNumber.ToString();
                            }
                            else
                            {
                                control.Text = "1";
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error fetching {taxName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@sname", taxName);
                        try
                        {
                            conn.Open();
                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                control.Text = result.ToString();
                            }
                            else
                            {
                                control.Text = " (Not Found)";
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error fetching {taxName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }


        private int currentSrNo = 1;

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
                        if (result != null)
                        {
                            if (decimal.TryParse(result.ToString(), out decimal parsedValue))
                            {
                                percentage = parsedValue;
                            }
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
        private decimal sgstPercentage = 0.0M;
        private decimal cgstPercentage = 0.0M;


        private void bill_Load(object sender, EventArgs e)
        {
            PopulateTypeBox();
            netwgt.Text = "1";
        }

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

                SqlConnection conn = new SqlConnection(connectionString);
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

                        if (!string.IsNullOrEmpty(rateinp.Text))
                        {
                            CalculateTotalAmount();
                        }
                        else
                        {
                            totalamount.Text = "";
                        }
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
                finally
                {
                    conn.Close();
                }
            }
        }

        private void netwgt_TextChanged_1(object sender, EventArgs e)
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

            if (decimal.TryParse(rateinp.Text, out decimal rate))
            {
                if (rate != 0)
                {
                    if (decimal.TryParse(netwgt.Text, out decimal weight))
                    {
                        totalAmount = rate * weight;
                    }
                }
            }

            totalamount.Text = totalAmount.ToString("0.00");
        }
        private void additem_Click(object sender, EventArgs e)
        {
            try
            {
                string items = itemname.Text;
                string type = typebox.SelectedItem?.ToString();
                string netWeight = netwgt.Text;
                string pcs = pcsinp.Text;
                string rate = rateinp.Text;
                string amount = totalamount.Text;
                string fineWeightValue = finwegt.Text;

                string hsn = FetchHNSFromDatabase();

                if (string.IsNullOrEmpty(items) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(netWeight) || string.IsNullOrEmpty(rate) || string.IsNullOrEmpty(amount))
                {
                    MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dataGridView1.Rows.Add(currentSrNo.ToString(), items, hsn, type, pcs, netWeight, fineWeightValue, rate, amount);

                currentSrNo++;

                CalculateAndDisplayTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void lcharge_TextChanged_1(object sender, EventArgs e)
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
                if (!row.IsNewRow)
                {
                    if (decimal.TryParse(row.Cells["amount"].Value.ToString(), out decimal rowAmount))
                    {
                        totalAmount += rowAmount;
                    }
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

            cgstval.Text = sgstAmount.ToString("0.00");
            sgstval.Text = cgstAmount.ToString("0.00");

            decimal netTotal = basicTotal + lchargeAmount + sgstAmount + cgstAmount;

            decimal roundOff = netTotal % 10;
            decimal roundedNetTotal = netTotal - roundOff;

            nettotalval.Text = roundedNetTotal.ToString("0.00");

            roundoff.Text = roundOff.ToString("0.00");
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

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Remove"].Index && e.RowIndex >= 0)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void savedata_Click_1(object sender, EventArgs e)
        {
            try
            {
                SaveDataToDatabase();
                // MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveDataToDatabase()
        {
            string invoiceNumber = invnum.Text;
            string address = addressTextbox.Text;
            string customerName = cusname.Text;
            string cusmobile = mobileval.Text;
            string customerGST = cusgst.Text;
            string paymentBy = paymentby.Text;
            string paymentTerm = panel.Text;
            DateTime invoiceDate = invdate.Value;
            string buyersOrderNo = ordno.Text;
            DateTime buyersOrderDate = orddate.Value;
            string dispatchThrough = disthrough.Text;
            DateTime dispatchDate = disdate.Value;
            string paymentDetail = paydtl.Text;
            decimal lchargeValue = Convert.ToDecimal(lcharge.Text);
            decimal roundoffValue = Convert.ToDecimal(roundoff.Text);
            decimal nettotalvalValue = Convert.ToDecimal(nettotalval.Text);

            string igstValue = igst.Text;
            string cgstValue = cgst.Text;
            string sgstValue = sgst.Text;

            int statusValue = radioButtonPaid.Checked ? 1 : 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insert into customers table
                    string insertCustomerQuery = @"INSERT INTO customers (invoice_number, address, mobile_no, customer_name, customer_gst, cuspaymentby, payment_term, invoice_date, buyers_ord_no, buyers_ord_date, dispetch_through, dispetch_date, paymentdetail, IGST, CGST, SGST, labour_charge, roundoff, net_total, status) 
                                        VALUES (@invoiceNumber, @address, @cusmobile, @customerName, @customerGST, @paymentBy, @paymentTerm, @invoiceDate, @buyersOrderNo, @buyersOrderDate, @dispatchThrough, @dispatchDate, @paymentDetail, @igstValue, @cgstValue, @sgstValue, @lcharge, @roundoff, @nettotalval, @status);
                                        SELECT SCOPE_IDENTITY();";

                    int customerId;

                    using (SqlCommand cmd = new SqlCommand(insertCustomerQuery, conn, transaction))
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

                        customerId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Define queries
                    string insertItemQuery = @"INSERT INTO item_details (cus_id, item_name, item_type, hsn, pcs, rate, net_wgt, fine_wgt, item_total) 
                                       VALUES (@cusId, @itemName, @itemType, @hsn, @pcs, @rate, @netWgt, @fineWgt, @itemTotal)";
                    string getStockIdQuery = "SELECT id FROM stocks WHERE type = @stockType";
                    string getStockQuantityQuery = "SELECT stock FROM stocks WHERE id = @stockId";
                    string stockUpdateQuery = @"UPDATE stocks SET stock = stock - @netWgt WHERE id = @stockId";
                    string stockOutInsertQuery = @"INSERT INTO stock_out (stock_id, used_stock, used_date, invoice_no) 
                   VALUES (@stockId, @usedStock, @usedDate, @invoiceNo)";

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // Skip empty rows
                        if (row.IsNewRow) continue;

                        string itemName = Convert.ToString(row.Cells["item"].Value);
                        string itemType = Convert.ToString(row.Cells["type"].Value);
                        string hsn = Convert.ToString(row.Cells["HSN"].Value);
                        int pcs = Convert.ToInt32(row.Cells["pcs"].Value);
                        decimal rate = Convert.ToDecimal(row.Cells["rate"].Value);
                        decimal netWgt = Convert.ToDecimal(row.Cells["nwgt"].Value);
                        decimal fineWgt = Convert.ToDecimal(row.Cells["fwgt"].Value);
                        decimal itemTotal = Convert.ToDecimal(row.Cells["amount"].Value);

                        // Determine stock type based on item type description
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

                        // Get stock ID
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

                        // Check available stock
                        decimal availableStock;

                        using (SqlCommand getStockQuantityCmd = new SqlCommand(getStockQuantityQuery, conn, transaction))
                        {
                            getStockQuantityCmd.Parameters.AddWithValue("@stockId", stockId);

                            object result = getStockQuantityCmd.ExecuteScalar();
                            if (result != null)
                            {
                                availableStock = Convert.ToDecimal(result);
                            }
                            else
                            {
                                MessageBox.Show($"Stock ID '{stockId}' not found.");
                                transaction.Rollback();
                                return;
                            }
                        }

                        if (availableStock < netWgt)
                        {
                            MessageBox.Show($"Insufficient stock for item '{itemName}'. Available stock: {availableStock}.", "Stock Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }

                        // Update stock
                        using (SqlCommand stockUpdateCmd = new SqlCommand(stockUpdateQuery, conn, transaction))
                        {
                            stockUpdateCmd.Parameters.AddWithValue("@netWgt", netWgt);
                            stockUpdateCmd.Parameters.AddWithValue("@stockId", stockId);

                            int rowsAffected = stockUpdateCmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                MessageBox.Show($"Failed to update stock for ID: {stockId}");
                                transaction.Rollback();
                                return;
                            }
                        }

                        // Insert into stock_out
                        using (SqlCommand stockOutCmd = new SqlCommand(stockOutInsertQuery, conn, transaction))
                        {
                            stockOutCmd.Parameters.AddWithValue("@stockId", stockId);
                            stockOutCmd.Parameters.AddWithValue("@usedStock", netWgt);
                            stockOutCmd.Parameters.AddWithValue("@usedDate", DateTime.Now);
                            stockOutCmd.Parameters.AddWithValue("@invoiceNo", invoiceNumber);

                            stockOutCmd.ExecuteNonQuery();
                        }

                        // Insert item details
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
                        DatabaseHelper helper = new DatabaseHelper();

                        helper.CalculateAndUpdateStock(stockId, conn, transaction);
                    }

                    transaction.Commit();
                    MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void invnum_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}



