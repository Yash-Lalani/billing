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

namespace billing
{
    public partial class companybankdetails : Form
    {
        public companybankdetails()
        {
            InitializeComponent();
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
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
                        case "BankName":
                            bankname.Text = parameter;
                            break;
                        case "BankACCNO":
                            bankaccno.Text = parameter;
                            break;
                        case "BankBranch":
                            bankbranch.Text = parameter;
                            break;
                        case "BankIFSCCode":
                            bankifsc.Text = parameter;
                            break;
                        case "BankMICRCode":
                            bankmicr.Text = parameter;
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

        private void save_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\source\repos\billing\Database1.mdf;Integrated Security=True");

            try
            {
                conn.Open();

                UpdateSetting(conn, "BankName", bankname.Text);
                UpdateSetting(conn, "BankACCNO", bankaccno.Text);
                UpdateSetting(conn, "BankBranch", bankbranch.Text);
                UpdateSetting(conn, "BankIFSCCode", bankifsc.Text);
                UpdateSetting(conn, "BankMICRCode", bankmicr.Text);

                MessageBox.Show("Company Bank Details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
