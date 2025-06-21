﻿namespace GUI_PolyCafe
{
    partial class frmDoanhThuLoaiSP
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
            label1 = new Label();
            panel1 = new Panel();
            btnThongKe = new Button();
            dtpDenNgay = new DateTimePicker();
            label4 = new Label();
            dtpTuNgay = new DateTimePicker();
            label3 = new Label();
            cbxLoaiSanPham = new ComboBox();
            label2 = new Label();
            dgrDanhSachThongKe = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgrDanhSachThongKe).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(354, 12);
            label1.Name = "label1";
            label1.Size = new Size(785, 46);
            label1.TabIndex = 4;
            label1.Text = "THỐNG KÊ DOANH THU THEO LOẠI SẢN PHẨM";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnThongKe);
            panel1.Controls.Add(dtpDenNgay);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(dtpTuNgay);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(cbxLoaiSanPham);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(14, 95);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1314, 81);
            panel1.TabIndex = 5;
            // 
            // btnThongKe
            // 
            btnThongKe.BackColor = Color.White;
            btnThongKe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThongKe.ForeColor = Color.Maroon;
            btnThongKe.Location = new Point(1112, 15);
            btnThongKe.Margin = new Padding(3, 4, 3, 4);
            btnThongKe.Name = "btnThongKe";
            btnThongKe.Size = new Size(136, 52);
            btnThongKe.TabIndex = 5;
            btnThongKe.Text = "Thống Kê";
            btnThongKe.UseVisualStyleBackColor = false;
            btnThongKe.Click += btnThongKe_Click;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Font = new Font("Segoe UI", 10F);
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(845, 24);
            dtpDenNgay.Margin = new Padding(3, 4, 3, 4);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(183, 30);
            dtpDenNgay.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.ForeColor = Color.Fuchsia;
            label4.Location = new Point(770, 29);
            label4.Name = "label4";
            label4.Size = new Size(75, 20);
            label4.TabIndex = 3;
            label4.Text = "Đến Ngày";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Font = new Font("Segoe UI", 10F);
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(488, 24);
            dtpTuNgay.Margin = new Padding(3, 4, 3, 4);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(195, 30);
            dtpTuNgay.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.ForeColor = Color.OrangeRed;
            label3.Location = new Point(423, 29);
            label3.Name = "label3";
            label3.Size = new Size(65, 20);
            label3.TabIndex = 3;
            label3.Text = "Từ Ngày";
            // 
            // cbxLoaiSanPham
            // 
            cbxLoaiSanPham.Font = new Font("Segoe UI", 10F);
            cbxLoaiSanPham.FormattingEnabled = true;
            cbxLoaiSanPham.Location = new Point(119, 26);
            cbxLoaiSanPham.Margin = new Padding(3, 4, 3, 4);
            cbxLoaiSanPham.Name = "cbxLoaiSanPham";
            cbxLoaiSanPham.Size = new Size(242, 31);
            cbxLoaiSanPham.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.Fuchsia;
            label2.Location = new Point(3, 32);
            label2.Name = "label2";
            label2.Size = new Size(106, 20);
            label2.TabIndex = 1;
            label2.Text = "Loại Sản Phẩm";
            // 
            // dgrDanhSachThongKe
            // 
            dgrDanhSachThongKe.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgrDanhSachThongKe.BackgroundColor = Color.FromArgb(255, 128, 128);
            dgrDanhSachThongKe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgrDanhSachThongKe.Location = new Point(9, 188);
            dgrDanhSachThongKe.Margin = new Padding(3, 4, 3, 4);
            dgrDanhSachThongKe.Name = "dgrDanhSachThongKe";
            dgrDanhSachThongKe.ReadOnly = true;
            dgrDanhSachThongKe.RowHeadersWidth = 51;
            dgrDanhSachThongKe.Size = new Size(1319, 652);
            dgrDanhSachThongKe.TabIndex = 6;
            // 
            // frmDoanhThuLoaiSP
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FloralWhite;
            ClientSize = new Size(1342, 856);
            Controls.Add(dgrDanhSachThongKe);
            Controls.Add(panel1);
            Controls.Add(label1);
            ForeColor = Color.CadetBlue;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmDoanhThuLoaiSP";
            Text = "frmDoanhThuLoaiSP";
            Load += frmDoanhThuLoaiSP_Load;
            Resize += frmDoanhThuLoaiSP_Resize;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgrDanhSachThongKe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private DateTimePicker dtpDenNgay;
        private Label label4;
        private DateTimePicker dtpTuNgay;
        private Label label3;
        private ComboBox cbxLoaiSanPham;
        private Label label2;
        private Button btnThongKe;
        private DataGridView dgrDanhSachThongKe;
    }
}