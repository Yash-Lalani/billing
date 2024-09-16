using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using iText.StyledXmlParser.Node;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace billing
{
    class DatabaseHelper
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\source\repos\billing\Database1.mdf;Integrated Security=True";

        public static void GeneratePdf(string invoiceNumber)
        {
            string customerName;
            string clientDataHtml;

            // Fetch the HTML content and customer name
            (clientDataHtml, customerName) = FetchClientDataAsHtml(invoiceNumber);

            // Construct the file path
            string sanitizedCustomerName = Regex.Replace(customerName, @"[^a-zA-Z0-9]", "_");
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"{invoiceNumber}_{sanitizedCustomerName}.pdf"
            );

            using (Document document = new Document(PageSize.A4))
            {
                try
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                    document.Open();

                    using (var stringReader = new StringReader(clientDataHtml))
                    {
                        var xmlWorker = XMLWorkerHelper.GetInstance();
                        xmlWorker.ParseXHtml(writer, document, stringReader);
                    }

                    document.Close();

                    MessageBox.Show($"PDF generated successfully at {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error generating PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static (string htmlContent, string customerName) FetchClientDataAsHtml(string invoiceNumber)
        {
            string queryCustomer = @"
            SELECT * 
            FROM customers 
            WHERE invoice_number = @invoiceNumber";

                    string queryItems = @"
            SELECT * 
            FROM item_details 
            WHERE cus_id = @cusId";

                    string querySettings = @"
            SELECT parameter, sname 
            FROM settings 
            WHERE sname IN ('companyPan', 'CompanyGSTNO', 'BankName', 'BankACCNO', 'BankBranch', 'BankIFSCCode', 'BankMICRCode')";

            string htmlContent = @"
            <div class='logo-container'>
                <img src='images/ph_parekh.png' />
            </div>
            <hr />
            <h3 style='text-align:center;'>Tax Invoice - Original</h3>
            <table style='width: 100%; border-collapse: collapse; font-size:13px;' cellpadding='2' cellspacing='2' border='1'>
                <tr>
            <td style='width: 15%; padding: 5px; vertical-align: top; border-top: 1px solid; border-left: 1px solid;'>Name:</td>
            <td style='width: 35%; padding: 5px; vertical-align: top; border-top: 1px solid; border-left: 1px solid;'>{0}</td>
            <td style='width: 15%; padding: 5px; vertical-align: top; border-top: 1px solid; border-left: 1px solid;'>Invoice No.:</td>
            <td style='width: 22%; padding: 5px 5px 5px 30px; vertical-align: top; text-align: center; border-top: 1px solid ; border-right: 1px solid ; border-left: 1px solid ;'>{1}</td>
            </tr>
            <tr>
                <td style='width: 15%; padding: 5px; vertical-align: top; border-top: 1px solid; border-left: 1px solid;'>Address:</td>
                <td style='width: 35%; padding: 5px; vertical-align: top; border-top: 1px solid; border-left: 1px solid;' rowspan='4'>{2}</td>
                <td style='width: 18%; padding: 5px; vertical-align: top; border-top: 1px solid; border-left: 1px solid;'>Invoice Date:</td>
                <td style='width: 22%; padding: 5px 5px 5px 30px; vertical-align: top; text-align: center; border-top: 1px solid ; border-right: 1px solid ;border-left: 1px solid ;'>{3}</td>
            </tr>
            <tr>
                <td style='width: 15%; padding: 5px; vertical-align: top; border-top: 1px solid; border-left: 1px solid;' rowspan='3'></td>
                <td style='width: 18%; padding: 5px; vertical-align: top; border-top: 1px solid; border-left: 1px solid;'>Buyer's Order No.:</td>
                <td style='width: 22%; padding: 5px 5px 5px 30px; vertical-align: top; text-align: center; border-top: 1px solid ; border-right: 1px solid ;border-left: 1px solid ;'>{4}</td>
            </tr>
            <tr>
                <td style='width: 18%; padding: 5px; vertical-align: top; border-top: 1px solid; border-left: 1px solid ;'>Buyer's Order Dt.:</td>
                <td style='width: 22%; padding: 5px 5px 5px 30px; vertical-align: top; text-align: center; border-top: 1px solid ; border-right: 1px solid ;border-left: 1px solid ;'>{5}</td>
            </tr>
            <tr>
                <td style='width: 18%; padding: 5px; vertical-align: top; border-top: 1px solid ; border-left: 1px solid ;'>Dispatch Through:</td>
                <td style='width: 22%; padding: 5px 5px 5px 30px; vertical-align: top; text-align: center; border-top: 1px solid ; border-right: 1px solid ;border-left: 1px solid ;'>{6}</td>
            </tr>
            <tr>
                <td style='width: 15%; padding: 5px; vertical-align: top; border-top: 1px solid ; border-left: 1px solid ;'>Mobile No.:</td>
                <td style='width: 35%; padding: 5px; vertical-align: top; border-top: 1px solid ; border-left: 1px solid ;'>{7}</td>
                <td style='width: 18%; padding: 5px; vertical-align: top; border-top: 1px solid ; border-left: 1px solid ;'>Dispatch Date:</td>
                <td style='width: 22%; padding: 5px 5px 5px 30px; vertical-align: top; text-align: center; border-top: 1px solid ; border-right: 1px solid ;border-left: 1px solid ;'>{8}</td>
            </tr>
            <tr>
                <td style='width: 15%; padding: 5px; vertical-align: top; border-top: 1px solid ; border-left: 1px solid ; border-bottom: 1px solid ;'>GST No.:</td>
                <td style='width: 35%; padding: 5px; vertical-align: top; border-top: 1px solid ; border-left: 1px solid ; border-bottom: 1px solid ;'>{9}</td>
                <td style='width: 18%; padding: 5px; vertical-align: top; border-top: 1px solid ; border-left: 1px solid ; border-bottom: 1px solid ;'>Payment Terms:</td>
                <td style='width: 22%; padding: 5px 5px 5px 30px; vertical-align: top; text-align: center; border: 1px solid ;'>{10} - DAYS</td>
            </tr>
            <tr>
                <td style='width: 15%; padding: 5px; vertical-align: top; border-left: 1px solid ; border-bottom: 1px solid ;'>Payment by:</td>
                <td style='width: 35%; padding: 5px; vertical-align: top; border-left: 1px solid ; border-bottom: 1px solid ;'>{11}</td>
                <td style='width: 18%; padding: 5px; vertical-align: top; border-left: 1px solid ; border-bottom: 1px solid ;'>Payment Detail:</td>
                <td style='width: 22%; padding: 5px 5px 5px 30px; vertical-align: top; text-align: center; border: 1px solid ;'>{12}</td>
            </tr>
            </table>

            <table style='width: 100%; border-collapse: collapse; font-size:14px; margin-top:10px;' cellpadding='2' cellspacing='2' border='1'>
            <tr>
                <th style='width: 50px; text-align: center;'>No.</th>
                <th style='width: 240px; text-align: center;'>Item</th>
                <th style='width: 50px; text-align: center;'>HNS</th>
                <th style='width: 50px; text-align: center;'>PCS</th>
                <th style='width: 80px; text-align: center;'>KT.</th>
                <th style='width: 80px; text-align: center;'>Net WGT</th>
                <th style='width: 85px; text-align: center;'>Fine WGT</th>
                <th style='width: 115px; text-align: center;'>Rate</th>
                <th style='width: 150px; text-align: right;'>Amount</th>
            </tr>
            {13} <!-- This placeholder is for the item rows -->
            </table>

            <!-- Settings Table -->
           <table class='invoice-farm-container' cellpadding='5' cellspacing='5' border='1' style='border-collapse: collapse; width: 100%; margin-top:10px; font-size:13px;'>
                <tr>
                    <td class='border-top-left' style='width: 20%; border: 1px solid ;'>Company's PAN</td>
                    <td class='border-top-left' style='width: 46%; border: 1px solid ;'>{14}</td>
                    <td rowspan='8' class='text_center borders' style='vertical-align: middle; width: 34%; border: 1px solid ; text-align: center;'>
                        For, P.H. Parekh & Sons Jewellery Proprietor
                    </td>
                </tr>
                <tr>
                    <td class='border-top-left' style='border: 1px solid ;'>Company's GST No</td>
                    <td class='border-top-left' style='border: 1px solid ;'>{15}</td>
                </tr>
                <tr>
                    <td class='border-top-left' colspan='2' style='border: 1px solid ;'><strong>Company's BANK Details</strong></td>
                </tr>
                <tr>
                    <td class='border-top-left' style='border: 1px solid ;'>Bank Name</td>
                    <td class='border-top-left' style='border: 1px solid ;'>{16}</td>
                </tr>
                <tr>
                    <td class='border-top-left' style='border: 1px solid ;'>A/C. No.</td>
                    <td class='border-top-left' style='border: 1px solid ;'>{17}</td>
                </tr>
                <tr>
                    <td class='border-top-left' style='border: 1px solid ;'>Branch</td>
                    <td class='border-top-left' style='border: 1px solid ;'>{18}</td>
                </tr>
                <tr>
                    <td class='border-top-left' style='border: 1px solid ;'>IFS Code</td>
                    <td class='border-top-left' style='border: 1px solid ;'>{19}</td>
                </tr>
                <tr>
                    <td class='border-top-left-bottom' style='border: 1px solid ;'>MICR Code</td>
                    <td class='border-top-left-bottom' style='border: 1px solid ;'>{20}</td>
                </tr>
            </table>";

            string itemRows = "";
            string companyPan = "";
            string companyGSTNo = "";
            string bankName = "";
            string bankAccNo = "";
            string bankBranch = "";
            string bankIFSCCode = "";
            string bankMICRCode = "";
            decimal totalAmount = 0m;
            string customerName = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Fetch customer data and cus_id
                    string customerQuery = queryCustomer;
                    using (SqlCommand commandCustomer = new SqlCommand(customerQuery, connection))
                    {
                        commandCustomer.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
                        using (SqlDataReader readerCustomer = commandCustomer.ExecuteReader())
                        {
                            if (readerCustomer.Read())
                            {
                                // Extract customer data
                                customerName = readerCustomer["customer_name"].ToString();
                                string invoiceNo = readerCustomer["invoice_number"].ToString();
                                string address = readerCustomer["address"].ToString();
                                string invoiceDate = readerCustomer["invoice_date"] != DBNull.Value ? Convert.ToDateTime(readerCustomer["invoice_date"]).ToString("dd/MM/yyyy") : "N/A";
                                string buyersOrderNo = readerCustomer["buyers_ord_no"].ToString();
                                string buyersOrderDate = readerCustomer["buyers_ord_date"] != DBNull.Value ? Convert.ToDateTime(readerCustomer["buyers_ord_date"]).ToString("dd/MM/yyyy") : "N/A";
                                string dispatchThrough = readerCustomer["dispetch_through"].ToString();
                                string mobileNo = readerCustomer["mobile_no"].ToString();
                                string dispatchDate = readerCustomer["dispetch_date"] != DBNull.Value ? Convert.ToDateTime(readerCustomer["dispetch_date"]).ToString("dd/MM/yyyy") : "N/A";
                                string gstNo = readerCustomer["customer_gst"].ToString();
                                string paymentTerms = readerCustomer["payment_term"].ToString();
                                string paymentBy = readerCustomer["cuspaymentby"].ToString();
                                string paymentDetail = readerCustomer["paymentdetail"].ToString();
                                string labourCharge = readerCustomer["labour_charge"].ToString();
                                string rounfOff = readerCustomer["roundoff"].ToString();
                                string netPayable= readerCustomer["net_total"].ToString();

                                // Read GST percentages
                                string igstPercentage = readerCustomer["igst"].ToString();
                                string cgstPercentage = readerCustomer["cgst"].ToString();
                                string sgstPercentage = readerCustomer["sgst"].ToString();
                                

                                // Fetch cus_id
                                string cusId = readerCustomer["id"].ToString();
                                readerCustomer.Close();

                                // Fetch item details using cus_id
                                string itemQuery = queryItems;
                                using (SqlCommand commandItems = new SqlCommand(itemQuery, connection))
                                {
                                    commandItems.Parameters.AddWithValue("@cusId", cusId);
                                    using (SqlDataReader readerItems = commandItems.ExecuteReader())
                                    {
                                        int itemNo = 1;
                                        while (readerItems.Read())
                                        {
                                            // Extract item data
                                            string itemName = readerItems["item_name"].ToString();
                                            string hns = readerItems["hsn"].ToString();
                                            string pcs = readerItems["pcs"].ToString();
                                            string kt = readerItems["item_type"].ToString();
                                            string netWgt = readerItems["net_wgt"].ToString();
                                            string fineWgt = readerItems["fine_wgt"].ToString();
                                            string rate = readerItems["rate"].ToString();
                                            string amountStr = readerItems["item_total"].ToString();
                                            decimal amount = decimal.TryParse(amountStr, out var result) ? result : 0m;

                                            // Accumulate total amount
                                            totalAmount += amount;

                                            itemRows += $@"
                                            <tr>
                                                <td style='text-align: center; border: 1px solid;'>{itemNo++}</td>
                                                <td style='border: 1px solid;'>{itemName}</td>
                                                <td style='text-align: center; border: 1px solid;'>{hns}</td>
                                                <td style='text-align: center; border: 1px solid;'>{pcs}</td>
                                                <td style='text-align: center; border: 1px solid;'>{kt}</td>
                                                <td style='text-align: center; border: 1px solid;'>{netWgt}</td>
                                                <td style='text-align: center; border: 1px solid;'>{fineWgt}</td>
                                                <td style='text-align: center; border: 1px solid;'>{rate}</td>
                                                <td style='text-align: right; border: 1px solid;'>{amount:N2}</td>
                                            </tr>";
                                        }
                                    }
                                }

                                // Calculate GST amounts
                                decimal cgstAmount = totalAmount * decimal.Parse(cgstPercentage) / 100;
                                decimal sgstAmount = totalAmount * decimal.Parse(sgstPercentage) / 100;


                                itemRows += $@"
                                
                                <tr>
                                    <td colspan='6' style='border: 1px solid;'>&nbsp;</td>
                                    <td colspan='2' style='border: 1px solid;'>Basic Val.</td>
                                    <td style='border: 1px solid; text-align: right;'>
                                        <table cellpadding='0' cellspacing='0' border='0' style='width: 100%;'>
                                            <tr>
                                                <td align='center' style='width: 20px;'><img src='images/rupee.png' height='18px' width='18px' /></td>
                                                <td>{totalAmount:N2}</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='6' style='border: 1px solid;'></td>
                                    <td colspan='2' style='border: 1px solid;'>IGST ({igstPercentage} %)</td>
                                    <td style='border: 1px solid; text-align: right;'>
                                        <table cellpadding='0' cellspacing='0' border='0' style='width: 100%;'>
                                            <tr>
                                                <td align='center' style='width: 20px;'>&#8377;</td>
                                                <td align='center'>-</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='6' style='border: 1px solid;'></td>
                                    <td colspan='2' style='border: 1px solid;'>CGST ({cgstPercentage} %)</td>
                                    <td style='border: 1px solid; text-align: right;'>
                                        <table cellpadding='0' cellspacing='0' border='0' style='width: 100%;'>
                                            <tr>
                                                <td align='center' style='width: 20px;'><img src='images/rupee.png' height='18px' width='18px' /></td>
                                                <td>{cgstAmount:N2}</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='6' style='border: 1px solid;'></td>
                                    <td colspan='2' style='border: 1px solid;'>SGST ({sgstPercentage} %)</td>
                                    <td style='border: 1px solid; text-align: right;'>
                                        <table cellpadding='0' cellspacing='0' border='0' style='width: 100%;'>
                                            <tr>
                                                <td align='center' style='width: 20px;'><img src='images/rupee.png' height='18px' width='18px' /></td>
                                                <td>{sgstAmount:N2}</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='6' style='border: 1px solid;'></td>
                                    <td colspan='2' style='border: 1px solid;'>LABOUR CHARGE</td>
                                    <td style='border: 1px solid; text-align: right;'>
                                        <table cellpadding='0' cellspacing='0' border='0' style='width: 100%;'>
                                            <tr>
                                                <td align='center' style='width: 20px;'><img src='images/rupee.png' height='18px' width='18px' /></td>
                                                <td>{decimal.Parse(labourCharge):N2}</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='6' style='border: 1px solid;'></td>
                                    <td colspan='2' style='border: 1px solid;'>Round Off</td>
                                    <td style='border: 1px solid; text-align: right;'>
                                        <table cellpadding='0' cellspacing='0' border='0' style='width: 100%;'>
                                            <tr>
                                                <td align='center' style='width: 20px;'><img src='images/rupee.png' height='18px' width='18px' /></td>
                                                <td>{decimal.Parse(rounfOff):N2}</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='6' style='border: 1px solid;'></td>
                                    <td colspan='2' style='border: 1px solid;'>Net Payable</td>
                                    <td style='border: 1px solid; text-align: right;'>
                                        <table cellpadding='0' cellspacing='0' border='0' style='width: 100%;'>
                                            <tr>
                                                <td align='center' style='width: 20px;'><img src='images/rupee.png' height='18px' width='18px' /></td>
                                                <td>{decimal.Parse(netPayable):N2}</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='9' style='border: 1px solid ; height: 5px;'><b>Rs. In Words:</b> {NumberToWords(decimal.Parse(netPayable))}</td>
                                </tr>";

                                // Fetch settings data
                                string settingsQuery = querySettings;
                                using (SqlCommand commandSettings = new SqlCommand(settingsQuery, connection))
                                {
                                    using (SqlDataReader readerSettings = commandSettings.ExecuteReader())
                                    {
                                        while (readerSettings.Read())
                                        {
                                            string parameter = readerSettings["parameter"].ToString();
                                            string settingName = readerSettings["sname"].ToString();

                                            switch (settingName)
                                            {
                                                case "companyPan":
                                                    companyPan = parameter;
                                                    break;
                                                case "CompanyGSTNO":
                                                    companyGSTNo = parameter;
                                                    break;
                                                case "BankName":
                                                    bankName = parameter;
                                                    break;
                                                case "BankACCNO":
                                                    bankAccNo = parameter;
                                                    break;
                                                case "BankBranch":
                                                    bankBranch = parameter;
                                                    break;
                                                case "BankIFSCCode":
                                                    bankIFSCCode = parameter;
                                                    break;
                                                case "BankMICRCode":
                                                    bankMICRCode = parameter;
                                                    break;
                                            }
                                        }
                                    }
                                }

                                // Format HTML content
                                htmlContent = string.Format(
                                    htmlContent,
                                    customerName,
                                    invoiceNo,
                                    address,
                                    invoiceDate,
                                    buyersOrderNo,
                                    buyersOrderDate,
                                    dispatchThrough,
                                    mobileNo,
                                    dispatchDate,
                                    gstNo,
                                    paymentTerms,
                                    paymentBy,
                                    paymentDetail,
                                    itemRows,
                                    companyPan,
                                    companyGSTNo,
                                    bankName,
                                    bankAccNo,
                                    bankBranch,
                                    bankIFSCCode,
                                    bankMICRCode
                                );
                            }
                            else
                            {
                                htmlContent = "<html><body><h1>No data found for the given invoice number.</h1></body></html>";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                htmlContent = $"<html><body><h1>Error: {ex.Message}</h1></body></html>";
            }

            return (htmlContent, customerName);
        }

        public static string NumberToWords(decimal number)
        {
            if (number == 0)
                return "Zero";

            string words = "";
            string[] units = { "", "Thousand", "Million", "Billion" };
            string[] tens = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string[] belowTwenty = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            int[] num = { 1000000000, 1000000, 1000, 100 };
            string[] str = { "Billion", "Million", "Thousand", "Hundred" };

            for (int i = 0; i < num.Length; i++)
            {
                int divisor = num[i];
                if (number >= divisor)
                {
                    int quotient = (int)(number / divisor);
                    number %= divisor;
                    words += NumberToWords(quotient) + " " + str[i] + " ";
                }
            }

            if (number > 0)
            {
                if (number < 20)
                {
                    words += belowTwenty[(int)number];
                }
                else
                {
                    words += tens[(int)(number / 10)] + " " + belowTwenty[(int)(number % 10)];
                }
            }

            return words.Trim();
        }


        public void CalculateAndUpdateStock(int stockId, SqlConnection conn, SqlTransaction transaction)
        {
            try
            {
            string selectAllStocksQuery = "SELECT id FROM stocks";

            List<int> stockIds = new List<int>();

            using (SqlCommand command = new SqlCommand(selectAllStocksQuery, conn, transaction))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        stockIds.Add(id);
                    }
                    reader.Close();
                }
            }

            int[] stockIdsArray = stockIds.ToArray();
            Console.WriteLine("Stock IDs:");
            foreach (int id in stockIdsArray)
            {
                UpdateAllStockQuantities(id, conn, transaction);
            }
                 
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"InvalidOperationException: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in CalculateAndUpdateStock: {ex.Message}");
                throw;
            }
        }
        public void UpdateAllStockQuantities(int stockId, SqlConnection conn, SqlTransaction transaction)
        {
            decimal totalStock = 0;

            try
            {
                string totalAddedStockQuery = "SELECT ISNULL(SUM(added_stock), 0) FROM stock_in WHERE stock_id = @stockId";
                using (SqlCommand totalAddedStockCommand = new SqlCommand(totalAddedStockQuery, conn, transaction))
                {
                    totalAddedStockCommand.Parameters.AddWithValue("@stockId", stockId);
                    object addedStockResult = totalAddedStockCommand.ExecuteScalar();
                    decimal addedStock = Convert.ToDecimal(addedStockResult);

                    string totalUsedStockQuery = "SELECT ISNULL(SUM(used_stock), 0) FROM stock_out WHERE stock_id = @stockId";
                    using (SqlCommand totalUsedStockCommand = new SqlCommand(totalUsedStockQuery, conn, transaction))
                    {
                        totalUsedStockCommand.Parameters.AddWithValue("@stockId", stockId);
                        object usedStockResult = totalUsedStockCommand.ExecuteScalar();
                        decimal usedStock = Convert.ToDecimal(usedStockResult);

                        totalStock = addedStock - usedStock;

                        string updateStockQuery = "UPDATE stocks SET stock = @stocktotal WHERE id = @stockId";
                        using (SqlCommand updateStockCommand = new SqlCommand(updateStockQuery, conn, transaction))
                        {
                            updateStockCommand.Parameters.AddWithValue("@stocktotal", totalStock);
                            updateStockCommand.Parameters.AddWithValue("@stockId", stockId);
                            updateStockCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateAllStockQuantities: {ex.Message}");
                throw;
            }

        }

    }
}

