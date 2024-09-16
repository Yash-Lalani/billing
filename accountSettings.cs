using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billing
{
    public partial class accountSettings : Form
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\source\repos\billing\Database1.mdf;Integrated Security=True";
        public accountSettings()
        {
            InitializeComponent();
        }
        private void accountSettings_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        public void BindGrid()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    String CustomerName = ExecuteScalar<String>("SELECT username from users", connection);
                    username.Text = $"{CustomerName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void save_Click(object sender, EventArgs e)
        {
            // Retrieve user input values
            string usernameInput = username.Text.Trim();
            string currentPassword = currentpass.Text.Trim();
            string newPassword = newpass.Text.Trim();
            string confirmPassword = confirmpass.Text.Trim();

            // Perform validation
            if (string.IsNullOrEmpty(usernameInput) || string.IsNullOrEmpty(currentPassword) ||
                string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields before proceeding.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("The new password and confirmation password do not match. Please re-enter them.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the current password is correct
            bool isCurrentPasswordValid = ValidateCurrentPassword(usernameInput, currentPassword);
            if (!isCurrentPasswordValid)
            {
                MessageBox.Show("The current password you entered is incorrect. Please try again.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update username and/or password in the database
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Update password
                            if (!string.IsNullOrEmpty(newPassword))
                            {
                                UpdatePassword(usernameInput, newPassword, connection, transaction);
                            }

                            // Update username
                            if (!string.IsNullOrEmpty(usernameInput))
                            {
                                UpdateUsername(usernameInput, connection, transaction);
                            }

                            // Commit transaction
                            transaction.Commit();
                            MessageBox.Show("Your details have been updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction in case of error
                            transaction.Rollback();
                            MessageBox.Show($"An error occurred while updating your details: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A connection error occurred: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateCurrentPassword(string username, string currentPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM users WHERE username = @username AND password = @currentPassword";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@currentPassword", currentPassword);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch
            {
                // Log exception details here if needed
                return false;
            }
        }

        private void UpdatePassword(string username, string newPassword, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "UPDATE users SET password = @newPassword WHERE username = @username";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@newPassword", newPassword);
                command.ExecuteNonQuery();
            }
        }

        private void UpdateUsername(string newUsername, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "UPDATE users SET username = @newUsername WHERE username = @currentUsername";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@newUsername", newUsername);
                command.Parameters.AddWithValue("@currentUsername", username.Text.Trim());
                command.ExecuteNonQuery();
            }
        }

        private T ExecuteScalar<T>(string query, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                return result != null ? (T)Convert.ChangeType(result, typeof(T)) : default;
            }
        }
        private void cancle_Click(object sender, EventArgs e)
        {
            currentpass.Clear();
            newpass.Clear();
            confirmpass.Clear();
        }
    }
}
