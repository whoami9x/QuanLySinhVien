using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHocSinh.Login___Register____Recover
{
    public partial class Recover : Form
    {
        public Recover()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yêu cầu reset password đã được gửi tới bộ phận admin, vui lòng đợi mail trả về để nhận mật khẩu mới !", "Thông báo", MessageBoxButtons.OK);
            this.Hide();
            var Login = new Login_Temp();
            Login.Show();
        }

        private void picCloseApp_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Login = new Login_Temp();
            Login.Show();
        }
    }
}
