﻿    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using BLL_PolyCafe;
    using DTO_PolyCafe;
    using UTIL_PolyCafe;

    namespace GUI_PolyCafe
    {
        public partial class frmLogin : Form
        {
            BUSNhanVien BUSNhanVien = new BUSNhanVien();
            public frmLogin()
            {
                InitializeComponent();
            }

            private void Login_FormClosing(object sender, FormClosingEventArgs e)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                }
            }

            private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
            {
                txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
            }

            private void btnLogin_Click(object sender, EventArgs e)
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                NhanVien nv = BUSNhanVien.DangNhap(username, password);
                if (nv == null)
                {
                    MessageBox.Show(this, "Tài khoản hoặc mật khẩu không chính xác");
                }
                else
                {
                    if (nv.TrangThai == false)
                    {
                        MessageBox.Show(this, "Tài khoản đang tạm khóa, vui lòng viên hệ QTV!!!");
                        return;
                    }
                    AuthUtil.user = nv;

                    frmMainForm main = new frmMainForm();
                    main.Show();
                    this.Hide();
                }
            }

            private void lbQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {

            }

            private void btnExit_Click(object sender, EventArgs e)
            {
                DialogResult result = MessageBox.Show("Bạn muốn thoát khỏi chương trình", "Thoát",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question
                               );
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }

            private void frmLogin_Load(object sender, EventArgs e)
            {

            }
        }
    }
