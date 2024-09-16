namespace billing
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.t1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.totalamount = new System.Windows.Forms.Label();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.TotalPaid = new System.Windows.Forms.Label();
            this.tp = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.totalunpaid = new System.Windows.Forms.Label();
            this.tu = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.totalcli = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.wellcome = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.progressBarGold = new System.Windows.Forms.ProgressBar();
            this.progressPercentageLabelGold = new System.Windows.Forms.Label();
            this.currentgold = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.usedAndTotalLabelGold = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.progressBarSilver = new System.Windows.Forms.ProgressBar();
            this.progressPercentageLabelSilver = new System.Windows.Forms.Label();
            this.currentsilver = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.usedAndTotalLabelSilver = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1606, 100);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::billing.Properties.Resources.ph_parekh;
            this.pictureBox1.Location = new System.Drawing.Point(1184, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(181, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dashboard";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panel2.Controls.Add(this.t1);
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.flowLayoutPanel4);
            this.panel2.Controls.Add(this.flowLayoutPanel3);
            this.panel2.Controls.Add(this.flowLayoutPanel2);
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(263, 685);
            this.panel2.TabIndex = 1;
            // 
            // t1
            // 
            this.t1.AutoSize = true;
            this.t1.Font = new System.Drawing.Font("Britannic Bold", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t1.ForeColor = System.Drawing.SystemColors.Control;
            this.t1.Location = new System.Drawing.Point(32, 18);
            this.t1.Name = "t1";
            this.t1.Size = new System.Drawing.Size(182, 41);
            this.t1.TabIndex = 4;
            this.t1.Text = "UserName";
            this.t1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.totalamount);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(109, 113);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(125, 75);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // totalamount
            // 
            this.totalamount.AutoSize = true;
            this.totalamount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.totalamount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalamount.ForeColor = System.Drawing.Color.White;
            this.totalamount.Location = new System.Drawing.Point(3, 0);
            this.totalamount.Name = "totalamount";
            this.totalamount.Size = new System.Drawing.Size(98, 16);
            this.totalamount.TabIndex = 2;
            this.totalamount.Text = "Total Amount";
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.TotalPaid);
            this.flowLayoutPanel4.Controls.Add(this.tp);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(109, 441);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(125, 79);
            this.flowLayoutPanel4.TabIndex = 4;
            // 
            // TotalPaid
            // 
            this.TotalPaid.AutoSize = true;
            this.TotalPaid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.TotalPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalPaid.ForeColor = System.Drawing.Color.White;
            this.TotalPaid.Location = new System.Drawing.Point(3, 0);
            this.TotalPaid.Name = "TotalPaid";
            this.TotalPaid.Size = new System.Drawing.Size(79, 16);
            this.TotalPaid.TabIndex = 2;
            this.TotalPaid.Text = "Total Paid";
            // 
            // tp
            // 
            this.tp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tp.AutoSize = true;
            this.tp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.tp.ForeColor = System.Drawing.SystemColors.Control;
            this.tp.Location = new System.Drawing.Point(3, 16);
            this.tp.Name = "tp";
            this.tp.Size = new System.Drawing.Size(82, 32);
            this.tp.TabIndex = 6;
            this.tp.Text = "Total paid Client";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.totalunpaid);
            this.flowLayoutPanel3.Controls.Add(this.tu);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(109, 333);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(125, 79);
            this.flowLayoutPanel3.TabIndex = 4;
            // 
            // totalunpaid
            // 
            this.totalunpaid.AutoSize = true;
            this.totalunpaid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.totalunpaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalunpaid.ForeColor = System.Drawing.Color.White;
            this.totalunpaid.Location = new System.Drawing.Point(3, 0);
            this.totalunpaid.Name = "totalunpaid";
            this.totalunpaid.Size = new System.Drawing.Size(97, 16);
            this.totalunpaid.TabIndex = 2;
            this.totalunpaid.Text = "Total Unpaid";
            // 
            // tu
            // 
            this.tu.AutoSize = true;
            this.tu.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.tu.ForeColor = System.Drawing.SystemColors.Control;
            this.tu.Location = new System.Drawing.Point(3, 16);
            this.tu.Name = "tu";
            this.tu.Size = new System.Drawing.Size(101, 32);
            this.tu.TabIndex = 5;
            this.tu.Text = "Total Unpaid Client";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.totalcli);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(109, 223);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(125, 77);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // totalcli
            // 
            this.totalcli.AutoSize = true;
            this.totalcli.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.totalcli.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalcli.ForeColor = System.Drawing.Color.White;
            this.totalcli.Location = new System.Drawing.Point(3, 0);
            this.totalcli.Name = "totalcli";
            this.totalcli.Size = new System.Drawing.Size(86, 16);
            this.totalcli.TabIndex = 2;
            this.totalcli.Text = "Total Client";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::billing.Properties.Resources._8;
            this.pictureBox5.Location = new System.Drawing.Point(28, 427);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(71, 103);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 1;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::billing.Properties.Resources._9;
            this.pictureBox4.Location = new System.Drawing.Point(28, 318);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(71, 103);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 1;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::billing.Properties.Resources._5;
            this.pictureBox3.Location = new System.Drawing.Point(28, 209);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(71, 103);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::billing.Properties.Resources._4;
            this.pictureBox2.Location = new System.Drawing.Point(28, 100);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(71, 103);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.button4.FlatAppearance.BorderSize = 5;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(97, 427);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(146, 103);
            this.button4.TabIndex = 0;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button3.FlatAppearance.BorderSize = 5;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(97, 318);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(146, 103);
            this.button3.TabIndex = 0;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.button2.FlatAppearance.BorderSize = 5;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(97, 209);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 103);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button1.FlatAppearance.BorderSize = 5;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(97, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 103);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.wellcome);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(263, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1343, 58);
            this.panel3.TabIndex = 2;
            // 
            // wellcome
            // 
            this.wellcome.AutoSize = true;
            this.wellcome.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wellcome.Location = new System.Drawing.Point(454, 18);
            this.wellcome.Name = "wellcome";
            this.wellcome.Size = new System.Drawing.Size(121, 26);
            this.wellcome.TabIndex = 3;
            this.wellcome.Text = "UserName";
            this.wellcome.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Blue;
            this.panel8.Controls.Add(this.progressBarGold);
            this.panel8.Controls.Add(this.progressPercentageLabelGold);
            this.panel8.Controls.Add(this.currentgold);
            this.panel8.Controls.Add(this.label12);
            this.panel8.Controls.Add(this.usedAndTotalLabelGold);
            this.panel8.ForeColor = System.Drawing.Color.Cyan;
            this.panel8.Location = new System.Drawing.Point(299, 183);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(494, 188);
            this.panel8.TabIndex = 7;
            // 
            // progressBarGold
            // 
            this.progressBarGold.Location = new System.Drawing.Point(63, 88);
            this.progressBarGold.Name = "progressBarGold";
            this.progressBarGold.Size = new System.Drawing.Size(322, 23);
            this.progressBarGold.TabIndex = 3;
            // 
            // progressPercentageLabelGold
            // 
            this.progressPercentageLabelGold.AutoSize = true;
            this.progressPercentageLabelGold.Location = new System.Drawing.Point(391, 89);
            this.progressPercentageLabelGold.Name = "progressPercentageLabelGold";
            this.progressPercentageLabelGold.Size = new System.Drawing.Size(43, 16);
            this.progressPercentageLabelGold.TabIndex = 2;
            this.progressPercentageLabelGold.Text = "0.00%";
            // 
            // currentgold
            // 
            this.currentgold.AutoSize = true;
            this.currentgold.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentgold.Location = new System.Drawing.Point(182, 140);
            this.currentgold.Name = "currentgold";
            this.currentgold.Size = new System.Drawing.Size(140, 16);
            this.currentgold.TabIndex = 0;
            this.currentgold.Text = "Currect Stock: Gold";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(182, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "Gold Stock Ratio";
            // 
            // usedAndTotalLabelGold
            // 
            this.usedAndTotalLabelGold.AutoSize = true;
            this.usedAndTotalLabelGold.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usedAndTotalLabelGold.Location = new System.Drawing.Point(127, 17);
            this.usedAndTotalLabelGold.Name = "usedAndTotalLabelGold";
            this.usedAndTotalLabelGold.Size = new System.Drawing.Size(234, 16);
            this.usedAndTotalLabelGold.TabIndex = 0;
            this.usedAndTotalLabelGold.Text = "Used Stock(Total Stock)-In Gram";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Blue;
            this.panel9.Controls.Add(this.progressBarSilver);
            this.panel9.Controls.Add(this.progressPercentageLabelSilver);
            this.panel9.Controls.Add(this.currentsilver);
            this.panel9.Controls.Add(this.label15);
            this.panel9.Controls.Add(this.usedAndTotalLabelSilver);
            this.panel9.ForeColor = System.Drawing.Color.Cyan;
            this.panel9.Location = new System.Drawing.Point(890, 183);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(475, 188);
            this.panel9.TabIndex = 8;
            // 
            // progressBarSilver
            // 
            this.progressBarSilver.Location = new System.Drawing.Point(68, 88);
            this.progressBarSilver.Name = "progressBarSilver";
            this.progressBarSilver.Size = new System.Drawing.Size(322, 23);
            this.progressBarSilver.TabIndex = 4;
            // 
            // progressPercentageLabelSilver
            // 
            this.progressPercentageLabelSilver.AutoSize = true;
            this.progressPercentageLabelSilver.Location = new System.Drawing.Point(396, 89);
            this.progressPercentageLabelSilver.Name = "progressPercentageLabelSilver";
            this.progressPercentageLabelSilver.Size = new System.Drawing.Size(43, 16);
            this.progressPercentageLabelSilver.TabIndex = 2;
            this.progressPercentageLabelSilver.Text = "0.00%";
            // 
            // currentsilver
            // 
            this.currentsilver.AutoSize = true;
            this.currentsilver.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentsilver.Location = new System.Drawing.Point(183, 140);
            this.currentsilver.Name = "currentsilver";
            this.currentsilver.Size = new System.Drawing.Size(147, 16);
            this.currentsilver.TabIndex = 0;
            this.currentsilver.Text = "Currect Stock: Silver";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Cyan;
            this.label15.Location = new System.Drawing.Point(183, 54);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(131, 16);
            this.label15.TabIndex = 0;
            this.label15.Text = "Silver Stock Ratio";
            // 
            // usedAndTotalLabelSilver
            // 
            this.usedAndTotalLabelSilver.AutoSize = true;
            this.usedAndTotalLabelSilver.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usedAndTotalLabelSilver.Location = new System.Drawing.Point(128, 17);
            this.usedAndTotalLabelSilver.Name = "usedAndTotalLabelSilver";
            this.usedAndTotalLabelSilver.Size = new System.Drawing.Size(235, 16);
            this.usedAndTotalLabelSilver.TabIndex = 0;
            this.usedAndTotalLabelSilver.Text = "Used StocK(Total Stock)-In Gram";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(299, 391);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1066, 300);
            this.chart1.TabIndex = 9;
            this.chart1.Text = "chart1";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1606, 785);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label wellcome;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label usedAndTotalLabelGold;
        private System.Windows.Forms.Label currentgold;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label currentsilver;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label usedAndTotalLabelSilver;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label totalamount;
        private System.Windows.Forms.Label progressPercentageLabelGold;
        private System.Windows.Forms.Label progressPercentageLabelSilver;
        private System.Windows.Forms.ProgressBar progressBarGold;
        private System.Windows.Forms.ProgressBar progressBarSilver;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label TotalPaid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label totalunpaid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label totalcli;
        private System.Windows.Forms.Label tu;
        private System.Windows.Forms.Label tp;
        private System.Windows.Forms.Label t1;
    }
}