using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace billing
{
    public partial class Dashboard : Form
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\source\repos\billing\Database1.mdf;Integrated Security=True";

        public Dashboard()
        {
            InitializeComponent();
            InitializeChart();
            LoadDashboardData();
        }

        private void InitializeChart()
        {
            // Set chart type
            chart1.Series.Clear();
            Series series = new Series("Month Chart")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.Int32,
                YValueType = ChartValueType.Double
            };

            chart1.Series.Add(series);

            chart1.ChartAreas[0].AxisX.Title = "Month";
            chart1.ChartAreas[0].AxisY.Title = "Total Amount";
        }

        private void LoadDashboardData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Fetch general data
                    string CustomerName = ExecuteScalar<String>("SELECT username from users", connection);
                    int customerCount = ExecuteScalar<int>("SELECT COUNT(*) FROM customers", connection);
                    decimal totalNetTotal = ExecuteScalar<decimal>("SELECT ISNULL(SUM(net_total), 0) FROM customers", connection);
                    decimal totalPaid = ExecuteScalar<decimal>("SELECT ISNULL(SUM(net_total), 0) FROM customers WHERE status = '1'", connection);
                    decimal totalUnpaid = ExecuteScalar<decimal>("SELECT ISNULL(SUM(net_total), 0) FROM customers WHERE status = '0'", connection);
                    int totalPaidClients = ExecuteScalar<int>("SELECT COUNT(*) FROM customers WHERE status = '1'", connection);
                    int totalUnpaidClients = ExecuteScalar<int>("SELECT COUNT(*) FROM customers WHERE status = '0'", connection);

                    // Fetch stock details
                    decimal totalAddedGold = ExecuteScalar<decimal>("SELECT ISNULL(SUM(added_stock), 0) FROM stock_in WHERE stock_id = 1", connection);
                    decimal totalUsedGold = ExecuteScalar<decimal>("SELECT ISNULL(SUM(used_stock), 0) FROM stock_out WHERE stock_id = 1", connection);
                    decimal totalGold = ExecuteScalar<decimal>("SELECT ISNULL(SUM(stock), 0) FROM stocks WHERE id = 1", connection);
                    decimal totalAddedSilver = ExecuteScalar<decimal>("SELECT ISNULL(SUM(added_stock), 0) FROM stock_in WHERE stock_id = 2", connection);
                    decimal totalUsedSilver = ExecuteScalar<decimal>("SELECT ISNULL(SUM(used_stock), 0) FROM stock_out WHERE stock_id = 2", connection);
                    decimal totalSilver = ExecuteScalar<decimal>("SELECT ISNULL(SUM(stock), 0) FROM stocks WHERE id = 2", connection);

                    // Calculate percentage for progress bar
                    decimal percentageGold = totalAddedGold > 0 ? (totalUsedGold / totalAddedGold) * 100 : 0;
                    decimal percentageSilver = totalAddedSilver > 0 ? (totalUsedSilver / totalAddedSilver) * 100 : 0;
                    percentageGold = Math.Min(percentageGold, 100);
                    percentageSilver = Math.Min(percentageSilver, 100);
                    progressBarGold.Value = (int)percentageGold;
                    progressBarSilver.Value = (int)percentageSilver;
                    progressPercentageLabelGold.Text = $"{percentageGold:F2}%";
                    progressPercentageLabelSilver.Text = $"{percentageSilver:F2}%";

                    // Update the used and total labels
                    string currentDay = DateTime.Now.ToString("dd");
                    string currentMonth = DateTime.Now.ToString("MMMM");
                    string labelText = $"{currentDay}, {currentMonth}";

                    t1.Text = labelText;

                    wellcome.Text = $"Hi, welcome back!, {CustomerName}";
                    usedAndTotalLabelGold.Text = $"Gold - Used: {totalUsedGold.ToString("N2")}, Added: {totalAddedGold.ToString("N2")} - In Gram";
                    usedAndTotalLabelSilver.Text = $"Silver - Used: {totalUsedSilver.ToString("N2")}, Added: {totalAddedSilver.ToString("N2")} - In Gram";
                    currentgold.Text = $"Current Gold: {totalGold.ToString("N2")}";
                    currentsilver.Text = $"Current Silver: {totalSilver.ToString("N2")}";
                    totalamount.Text = $"Total Amount ₹ {totalNetTotal.ToString("N2")}";
                    totalcli.Text = $"Total Client{customerCount.ToString("N0")}";
                    totalunpaid.Text = $"Total Unpaid ₹ {totalUnpaid.ToString("N2")}";
                    tu.Text = $"Total Unpaid Clients: {totalUnpaidClients.ToString("N0")}";
                    TotalPaid.Text = $"Total Paid ₹ {totalPaid.ToString("N2")}";
                    tp.Text = $"Total Paid Clients: {totalPaidClients.ToString("N0")}";


                    // Fetch data for chart
                    string query = @"
                        SELECT ISNULL(SUM(net_total), 0) AS Grand_Total, MONTH(invoice_date) AS Month
                        FROM customers
                        WHERE status = '1' 
                          AND YEAR(invoice_date) = @Year
                        GROUP BY MONTH(invoice_date)
                        ORDER BY MONTH(invoice_date)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int currentYear = DateTime.Now.Year;
                        command.Parameters.AddWithValue("@Year", currentYear);

                        SqlDataReader reader = command.ExecuteReader();

                        // Initialize chart
                        chart1.Series["Month Chart"].Points.Clear();
                        chart1.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
                        chart1.Series["Month Chart"].IsValueShownAsLabel = true;
                        chart1.Series["Month Chart"].LabelForeColor = System.Drawing.Color.Black;

                        // Define month names
                        string[] monthNames = new string[] {
                            "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                        };

                        var monthData = new Dictionary<int, decimal>();
                        for (int i = 1; i <= 12; i++)
                        {
                            monthData[i] = 0; 
                        }

                        while (reader.Read())
                        {
                            int month = reader.GetInt32(1);
                            decimal grandTotal = reader.GetDecimal(0);
                            monthData[month] = grandTotal;
                        }

                        foreach (var month in monthData)
                        {
                            string monthName = monthNames[month.Key - 1];
                            decimal grandTotal = month.Value;
                            chart1.Series["Month Chart"].Points.AddXY(monthName, grandTotal);
                        }

                        var chartArea = chart1.ChartAreas[0];
                        chartArea.AxisX.Title = "Month";
                        chartArea.AxisX.TitleFont = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
                        chartArea.AxisY.Title = "Total Amount";
                        chartArea.AxisY.TitleFont = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

                        chartArea.AxisX.LabelStyle.Format = "";
                        chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;
                        chartArea.AxisX.Interval = 1; 
                        chartArea.AxisX.MajorGrid.LineWidth = 0; 
                        chartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 8);
                        chartArea.AxisX.IntervalOffset = 0;

                        chart1.Invalidate();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
