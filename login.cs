using billing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Data.SqlClient;

namespace billing
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            SetupPlaceholders();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\source\repos\billing\Database1.mdf;Integrated Security=True");

        private void login_btn_Click_1(object sender, EventArgs e)
        {
            string username = txt_username.Text;
            string password = txt_password.Text;

            try
            {
                string query = "SELECT * FROM users WHERE username = @username AND password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {
                    main mainForm = new main();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Login Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_username.Clear();
                    txt_password.Clear();
                    txt_username.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        // Inside your form's constructor or Load event
        private void SetupPlaceholders()
        {
            // Set placeholders
            txt_username.Text = "Username";
            txt_username.ForeColor = Color.Gray;

            txt_password.Text = "Password";
            txt_password.ForeColor = Color.Gray;
            txt_password.UseSystemPasswordChar = false; // Initially, show the password text as plain text

            // Attach event handlers for username textbox
            txt_username.Enter += RemoveUsernamePlaceholder;
            txt_username.Leave += SetUsernamePlaceholder;

            // Attach event handlers for password textbox
            txt_password.Enter += RemovePasswordPlaceholder;
            txt_password.Leave += SetPasswordPlaceholder;
        }

        private void RemoveUsernamePlaceholder(object sender, EventArgs e)
        {
            if (txt_username.Text == "Username")
            {
                txt_username.Text = "";
                txt_username.ForeColor = Color.Black;
            }
        }

        private void SetUsernamePlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_username.Text))
            {
                txt_username.Text = "Username";
                txt_username.ForeColor = Color.Gray;
            }
        }

        private void RemovePasswordPlaceholder(object sender, EventArgs e)
        {
            if (txt_password.Text == "Password")
            {
                txt_password.Text = "";
                txt_password.ForeColor = Color.Black;
                txt_password.UseSystemPasswordChar = true; // Mask the password text
            }
        }

        private void SetPasswordPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_password.Text))
            {
                txt_password.UseSystemPasswordChar = false; // Show password text as plain
                txt_password.Text = "Password";
                txt_password.ForeColor = Color.Gray;
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

      
    }
}