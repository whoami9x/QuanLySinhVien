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
    public partial class fLogin : Form
    {
        public static string globalUsername = "";
        public string GetUsername()
        {
            return globalUsername;
        }
        public fLogin()
        {
            InitializeComponent();
        }

        private void lbForgotPassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn đã click quên mật khẩu!");
        }

        private void lbRegister_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn đã click đăng ký!");
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            String getUsername = txtUsername.Text;
            String getPassword = txtPassword.Text;

            if(checkUsernameAndPassword(getUsername, getPassword))
            {
                dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[User] WHERE USERNAME ='{getUsername}' AND PASSWORD = '{getPassword}'");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show($"Đăng nhập với tài khoản [{getUsername}] thành công !","Thông báo");
                    globalUsername = txtUsername.Text;
                    var DrashBoard = new fMain();
                    DrashBoard.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show($"Đăng nhập thất bại, vui lòng kiểm tra lại tài khoản và mật khẩu !","Lỗi đăng nhập",MessageBoxButtons.OK);
            }    
        }

        private void linkForgetPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            var Recover = new fKhoiPhucTaiKhoan();
            Recover.Show();
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form Register = new fDangKy();
            clearPictureBox();
            this.Hide();
            Register.ShowDialog();
            this.Close();
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

        private void fLogin_Load(object sender, EventArgs e)
        {
            clearPictureBox();
            Image im = GetCopyImage(@"\QLSV\Image\HUTECH.png");
            pictureBox1.Image = im;
            im = GetCopyImage(@"\QLSV\Image\CloseApp.png");
            btnCloseApp.Image = im;
        }

        private Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }

        private void clearPictureBox()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            if (btnCloseApp.Image != null)
            {
                btnCloseApp.Image.Dispose();
            }
        }
    }
}
