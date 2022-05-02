using System;
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
    public partial class Login_Temp : Form
    {
        public static string globalUsername = "";
        public Login_Temp()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            String getUsername = txtUsername.Text;
            String getPassword = txtPassword.Text;

            if (checkUsernameAndPassword(getUsername, getPassword))
            {
                dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[User] WHERE USERNAME ='{getUsername}' AND PASSWORD = '{getPassword}'");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show($"Đăng nhập với tài khoản [{getUsername}] thành công !", "Thông báo");
                    globalUsername = txtUsername.Text;
                    var DrashBoard = new fMain();
                    DrashBoard.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show($"Đăng nhập thất bại, vui lòng kiểm tra lại tài khoản và mật khẩu !", "Lỗi đăng nhập", MessageBoxButtons.OK);
            }
        }

        private Boolean checkUsernameAndPassword(String getUsername, String getPassword)
        {
            String errorStatus = "";
            if (getUsername.Trim() == "" || getPassword == "")
            {
                errorStatus = "Tài khoản hoặc mật khẩu bị trống !";
                MessageBox.Show(errorStatus, "Lỗi đăng nhập", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Form Register = new Login___Register____Recover.Register();
            this.Hide();
            Register.ShowDialog();
            this.Close();
        }

        private void btnForgot_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Recover = new Login___Register____Recover.Recover();
            Recover.Show();
        }

        private void picCloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
