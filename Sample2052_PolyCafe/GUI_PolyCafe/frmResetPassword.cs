﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_PolyCafe;
using UTIL_PolyCafe;

namespace GUI_PolyCafe
{
    public partial class frmResetPassword : Form
    {
        BUSNhanVien busNhanVien = new BUSNhanVien();
        public frmResetPassword()
        {
            InitializeComponent();
            btnShow.BringToFront();
            btnShowNew.BringToFront();
            btnShowNewA.BringToFront();
        }

        private void ResetPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //}
        }



        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetPassword_Load(object sender, EventArgs e)
        {
            if (AuthUtil.IsLogin())
            {
                txtCPMaNV.Text = AuthUtil.user.MaNhanVien;
                txtCPTenNv.Text = AuthUtil.user.HoTen;
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            if (txtMatKhauCu.PasswordChar == '\0')
            {
                btnShow.BringToFront();
                txtMatKhauCu.PasswordChar = '*';
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (txtMatKhauCu.PasswordChar == '*')
            {
                btnHide.BringToFront();
                txtMatKhauCu.PasswordChar = '\0';
            }
        }

        private void btnShowNewA_Click(object sender, EventArgs e)
        {
            if (txtXacNhanMatKhau.PasswordChar == '*')
            {
                btnHideNewA.BringToFront();
                txtXacNhanMatKhau.PasswordChar = '\0';
            }
        }

        private void btnHideNewA_Click(object sender, EventArgs e)
        {
            if (txtXacNhanMatKhau.PasswordChar == '\0')
            {
                btnShowNewA.BringToFront();
                txtXacNhanMatKhau.PasswordChar = '*';
            }
        }

        private void btnShowNew_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.PasswordChar == '*')
            {
                btnHideNew.BringToFront();
                txtMatKhauMoi.PasswordChar = '\0';
            }
        }

        private void btnHideNew_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.PasswordChar == '\0')
            {
                btnShowNew.BringToFront();
                txtMatKhauMoi.PasswordChar = '*';
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (!AuthUtil.user.MatKhau.Equals(txtMatKhauCu.Text))
            {
                MessageBox.Show(this, "Mật khẩu cũ chưa đúng!!!");
            }
            else
            {
                if (!txtMatKhauMoi.Text.Equals(txtXacNhanMatKhau.Text))
                {
                    MessageBox.Show(this, "Xác nhận mật khẩu mới chưa trùng khớp!!!");
                }
                else
                {
                    AuthUtil.user.MatKhau = txtMatKhauMoi.Text;

                    if (busNhanVien.ResetMatKhau(AuthUtil.user.Email, txtMatKhauMoi.Text))
                    {
                        MessageBox.Show("Cập nhật mật khẩu thành công!!!");
                    }
                    else MessageBox.Show("Đổi mật khẩu thất bại, vui lòng kiểm tra lại!!!");
                }
            }
        }
    }
}
