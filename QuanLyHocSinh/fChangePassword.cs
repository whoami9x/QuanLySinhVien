﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHocSinh
{
    public partial class fChangePassword : Form
    {
        public fChangePassword()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            String getUsername = txtUsername.Text;
            String getPassword = txtPassword.Text;
            String getNewPassword = txtNewPassword.Text;

            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[User] WHERE USERNAME ='{getUsername}' AND PASSWORD = '{getPassword}'");
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine($"UPDATE [StudentManagement].[dbo].[User] SET PASSWORD = '{getNewPassword}' WHERE USERNAME = {getUsername}");
                db.UpdateData($"UPDATE [StudentManagement].[dbo].[User] SET PASSWORD = '{getNewPassword}' WHERE USERNAME = '{getUsername}'","Đổi mật khẩu thành công !");

            }
            else
            {
                MessageBox.Show($"Tài khoản hoặc mật khẩu không đúng, vui lòng kiểm tra lại !");
            }

            txtUsername.Text = "";
            txtPassword.Text = "";
            txtNewPassword.Text = "";
        }
    }
}
