using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace billing
{
    public partial class billdetails : Form
    {
        public billdetails()
        {
            InitializeComponent();
        }
        

        private void billdetails_Load(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        public void BindGrid()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\source\repos\billing\Database1.mdf;Integrated Security=True");
            SqlCommand query = new SqlCommand("SELECT sname, parameter FROM settings", conn);

            try
            {
                conn.Open();
                SqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    string sname = reader["sname"].ToString();
                    string parameter = reader["parameter"].ToString();

                    switch (sname)
                    {
                        case "Companyname":
                            name.Text = parameter;
                            break;
                        case "HSN":
                            hnstext.Text = parameter;
                            break;
                        case "address":
                            address.Text = parameter;
                            break;
                        case "invoice_number":
                            invoicenumber.Text = parameter;
                            break;
                        case "IGST":
                            igst.Text = parameter;
                            break;
                        case "SGST":
                            sgst.Text = parameter;
                            break;
                        case "CGST":
                            cgst.Text = parameter;
                            break;
                        case "payment_term":
                            paymentterm.Text = parameter;
                            break;
                        case "companyPan":
                            companypan.Text = parameter;
                            break;
                        case "GOLDRATE18":
                            goldrate18.Text = parameter;
                            break;
                        case "GOLDRATE20":
                            goldrate20.Text = parameter;
                            break;
                        case "GOLDRATE22":
                            goldrate22.Text = parameter;
                            break;
                        case "SILVERRATE45":
                            silver45.Text = parameter;
                            break;
                        case "SILVERRATE60":
                            silver60.Text = parameter;
                            break;
                        case "SILVERRATE70":
                            silver70.Text = parameter;
                            break;
                        case "SILVERRATE92.5":
                            silver92.Text = parameter;
                            break;
                        case "SILVERRATEFINE":
                            silverfine.Text = parameter;
                            break;
                        case "CompanyGSTNO":
                            companygst.Text = parameter;
                            break;
                        default:

                            break;
                    }
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


        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            main m = new main();
            m.Show();
        }

        private void billToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            bill b = new bill();
            b.Show();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            companybankdetails cb = new companybankdetails();
            cb.Show();
        }

        private void SAVE_Click_1(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\source\repos\billing\Database1.mdf;Integrated Security=True");

            try
            {
                conn.Open();

                UpdateSetting(conn, "Companyname", name.Text);
                UpdateSetting(conn, "HSN", hnstext.Text);
                UpdateSetting(conn, "address", address.Text);
                UpdateSetting(conn, "invoice_number", invoicenumber.Text);
                UpdateSetting(conn, "IGST", igst.Text);
                UpdateSetting(conn, "SGST", sgst.Text);
                UpdateSetting(conn, "CGST", cgst.Text);
                UpdateSetting(conn, "payment_term", paymentterm.Text);
                UpdateSetting(conn, "companyPan", companypan.Text);
                UpdateSetting(conn, "GOLDRATE18", goldrate18.Text);
                UpdateSetting(conn, "GOLDRATE20", goldrate20.Text);
                UpdateSetting(conn, "GOLDRATE22", goldrate22.Text);
                UpdateSetting(conn, "SILVERRATE45", silver45.Text);
                UpdateSetting(conn, "SILVERRATE60", silver60.Text);
                UpdateSetting(conn, "SILVERRATE70", silver70.Text);
                UpdateSetting(conn, "SILVERRATE92.5", silver92.Text);
                UpdateSetting(conn, "SILVERRATEFINE", silverfine.Text);
                UpdateSetting(conn, "CompanyGSTNO", companygst.Text);

                MessageBox.Show("Settings updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

        }
        private void UpdateSetting(SqlConnection conn, string sname, string parameter)
        {
            SqlCommand updateCommand = new SqlCommand(@"UPDATE settings SET parameter = @parameter WHERE sname = @sname", conn);

            updateCommand.Parameters.Add("@parameter", SqlDbType.VarChar).Value = parameter;
            updateCommand.Parameters.Add("@sname", SqlDbType.VarChar).Value = sname;

            updateCommand.ExecuteNonQuery();
        }

        private void cancle_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}
