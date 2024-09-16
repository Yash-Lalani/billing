using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace billing
{
    public partial class viewClient : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Lenovo\\source\\repos\\billing\\Database1.mdf;Integrated Security=True";
        private string invoiceNumber;

        public viewClient(string invoiceNumber)
        {
            InitializeComponent();
            this.invoiceNumber = invoiceNumber;
            this.Load += new System.EventHandler(this.viewClient_Load);
        }

        private void viewClient_Load(object sender, EventArgs e)
        {
            FetchAndDisplayData();
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
                    decimal sgst = 0;
                    decimal cgst = 0;

                    if (readerCustomer.Read())
                    {
                        invno.Text = readerCustomer["invoice_number"].ToString();
                        cname.Text = readerCustomer["customer_name"].ToString();
                        mobno.Text = readerCustomer["mobile_no"].ToString();
                        add.Text = readerCustomer["address"].ToString();
                        cusgst.Text = readerCustomer["customer_gst"].ToString();
                        payby.Text = readerCustomer["cuspaymentby"].ToString();
                        paydtl.Text = readerCustomer["paymentdetail"].ToString();
                        buyordno.Text = readerCustomer["buyers_ord_no"].ToString();
                        disth.Text = readerCustomer["dispetch_through"].ToString();
                        payterm.Text = readerCustomer["payment_term"].ToString();
                        lcharge.Text = readerCustomer["labour_charge"].ToString();
                        roundoff.Text = readerCustomer["roundoff"].ToString();
                        igst.Text = readerCustomer["igst"].ToString();
                        cgsttxt.Text = readerCustomer["cgst"].ToString();
                        sgsttxt.Text = readerCustomer["sgst"].ToString();

                        sgst = readerCustomer["sgst"] != DBNull.Value ? Convert.ToDecimal(readerCustomer["sgst"]) : 0;
                        cgst = readerCustomer["cgst"] != DBNull.Value ? Convert.ToDecimal(readerCustomer["cgst"]) : 0;

                        string statusValue = readerCustomer["status"].ToString();
                        if (statusValue == "1")
                        {
                            status.Text = "PAID";
                            status.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            status.Text = "UNPAID";
                            status.ForeColor = System.Drawing.Color.Red;
                        }
                        invdt.Text = Convert.ToDateTime(readerCustomer["invoice_date"]).ToString("dd MMM yyyy");
                        buyorddate.Text = Convert.ToDateTime(readerCustomer["buyers_ord_date"]).ToString("dd MMM yyyy");
                        disdt.Text = Convert.ToDateTime(readerCustomer["dispetch_date"]).ToString("dd MMM yyyy");
                        nettotalval.Text = Convert.ToDecimal(readerCustomer["net_total"]).ToString("N2");
                    }
                    else
                    {
                        MessageBox.Show("No data found for the provided invoice number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    readerCustomer.Close();

                    SqlDataAdapter adapterItems = new SqlDataAdapter(commandItems);
                    DataTable itemsTable = new DataTable();
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

                    CalculateTotal(itemsTable, sgst, cgst);
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
        private void CalculateTotal(DataTable itemsTable, decimal sgstPercentage, decimal cgstPercentage)
        {
            decimal total = 0;

            if (itemsTable.Columns.Contains("item_total"))
            {
                foreach (DataRow row in itemsTable.Rows)
                {
                    if (decimal.TryParse(row["item_total"].ToString(), out decimal itemTotal))
                    {
                        total += itemTotal;
                    }
                }

                decimal sgstAmount = total * (sgstPercentage / 100);
                decimal cgstAmount = total * (cgstPercentage / 100);

                basicval.Text = total.ToString("N2");
                sgstval.Text = sgstAmount.ToString("N2");
                cgstval.Text = cgstAmount.ToString("N2");
            }
            else
            {
                MessageBox.Show("Column 'item_total' not found in item details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
