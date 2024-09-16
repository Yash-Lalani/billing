namespace billing
{
    partial class stocks
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewstock = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.CustomerdataGridView = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1671, 100);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::billing.Properties.Resources._41;
            this.pictureBox2.Location = new System.Drawing.Point(1291, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(107, 97);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bauhaus 93", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(613, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stocks Info";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // viewstock
            // 
            this.viewstock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewstock.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewstock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.viewstock.Location = new System.Drawing.Point(1107, 477);
            this.viewstock.Name = "viewstock";
            this.viewstock.Size = new System.Drawing.Size(236, 53);
            this.viewstock.TabIndex = 18;
            this.viewstock.Text = "View Stock";
            this.viewstock.UseVisualStyleBackColor = true;
            // 
            // update
            // 
            this.update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.update.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(214)))));
            this.update.Location = new System.Drawing.Point(1107, 252);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(236, 53);
            this.update.TabIndex = 17;
            this.update.Text = "Update Stock";
            this.update.UseVisualStyleBackColor = true;
            // 
            // CustomerdataGridView
            // 
            this.CustomerdataGridView.AllowUserToAddRows = false;
            this.CustomerdataGridView.AllowUserToDeleteRows = false;
            this.CustomerdataGridView.AllowUserToOrderColumns = true;
            this.CustomerdataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CustomerdataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CustomerdataGridView.BackgroundColor = System.Drawing.Color.Silver;
            this.CustomerdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomerdataGridView.GridColor = System.Drawing.Color.Silver;
            this.CustomerdataGridView.Location = new System.Drawing.Point(132, 252);
            this.CustomerdataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.CustomerdataGridView.Name = "CustomerdataGridView";
            this.CustomerdataGridView.ReadOnly = true;
            this.CustomerdataGridView.RowHeadersWidth = 51;
            this.CustomerdataGridView.Size = new System.Drawing.Size(917, 278);
            this.CustomerdataGridView.TabIndex = 2;
            // 
            // stocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1671, 737);
            this.Controls.Add(this.viewstock);
            this.Controls.Add(this.update);
            this.Controls.Add(this.CustomerdataGridView);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "stocks";
            this.Text = "stocks";
            this.Load += new System.EventHandler(this.stocks_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button viewstock;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.DataGridView CustomerdataGridView;
    }
}