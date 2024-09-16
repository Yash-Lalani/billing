namespace billing
{
    partial class clientlist
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.CustomerdataGridView = new System.Windows.Forms.DataGridView();
            this.srnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cusmobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nettotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.search = new System.Windows.Forms.TextBox();
            this.srno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orddate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ststus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerdataGridView)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.8960574F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95.78853F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.315412F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.705426F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96.89922F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.24031F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1116, 645);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(13, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1063, 577);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1063, 577);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clients List";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.CustomerdataGridView, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 521F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1057, 554);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // CustomerdataGridView
            // 
            this.CustomerdataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CustomerdataGridView.BackgroundColor = System.Drawing.Color.Silver;
            this.CustomerdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomerdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.srnum,
            this.cusname,
            this.cusmobile,
            this.invno,
            this.invdate,
            this.orderdate,
            this.nettotal,
            this.status});
            this.CustomerdataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomerdataGridView.Location = new System.Drawing.Point(3, 36);
            this.CustomerdataGridView.Name = "CustomerdataGridView";
            this.CustomerdataGridView.Size = new System.Drawing.Size(1051, 515);
            this.CustomerdataGridView.TabIndex = 0;
            this.CustomerdataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomerdataGridView_CellContentClick);
            // 
            // srnum
            // 
            this.srnum.HeaderText = "SR NO.";
            this.srnum.Name = "srnum";
            // 
            // cusname
            // 
            this.cusname.HeaderText = "Customer Name";
            this.cusname.Name = "cusname";
            // 
            // cusmobile
            // 
            this.cusmobile.HeaderText = "Customer mobile";
            this.cusmobile.Name = "cusmobile";
            // 
            // invno
            // 
            this.invno.HeaderText = "Invoice Number";
            this.invno.Name = "invno";
            // 
            // invdate
            // 
            this.invdate.HeaderText = "Invoice Date";
            this.invdate.Name = "invdate";
            // 
            // orderdate
            // 
            this.orderdate.HeaderText = "Order Date";
            this.orderdate.Name = "orderdate";
            // 
            // nettotal
            // 
            this.nettotal.HeaderText = "Total";
            this.nettotal.Name = "nettotal";
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.search);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(347, 26);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search:";
            // 
            // search
            // 
            this.search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.Location = new System.Drawing.Point(80, 3);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(255, 21);
            this.search.TabIndex = 2;
            this.search.TextChanged += new System.EventHandler(this.search_TextChanged);
            // 
            // srno
            // 
            this.srno.HeaderText = "Sr No,";
            this.srno.Name = "srno";
            // 
            // clientname
            // 
            this.clientname.HeaderText = "Client Name";
            this.clientname.Name = "clientname";
            // 
            // mobile
            // 
            this.mobile.HeaderText = "Mobile";
            this.mobile.Name = "mobile";
            // 
            // invoiceno
            // 
            this.invoiceno.HeaderText = "Invoice Number";
            this.invoiceno.Name = "invoiceno";
            // 
            // orddate
            // 
            this.orddate.HeaderText = "Order Date";
            this.orddate.Name = "orddate";
            // 
            // total
            // 
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            // 
            // ststus
            // 
            this.ststus.HeaderText = "Status";
            this.ststus.Name = "ststus";
            // 
            // action
            // 
            this.action.HeaderText = "Action";
            this.action.Name = "action";
            // 
            // clientlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 645);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "clientlist";
            this.Load += new System.EventHandler(this.clientlist_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CustomerdataGridView)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn srno;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientname;
        private System.Windows.Forms.DataGridViewTextBoxColumn mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceno;
        private System.Windows.Forms.DataGridViewTextBoxColumn orddate;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn ststus;
        private System.Windows.Forms.DataGridViewTextBoxColumn action;
        private System.Windows.Forms.DataGridView CustomerdataGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn srnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusname;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusmobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn invno;
        private System.Windows.Forms.DataGridViewTextBoxColumn invdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn nettotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}