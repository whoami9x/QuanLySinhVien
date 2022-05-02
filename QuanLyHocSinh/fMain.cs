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
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void Drashboard_Test_Load(object sender, EventArgs e)
        {
            checkAdmin();
        }

        private void menuAccount_Click(object sender, EventArgs e)
        {
            Form new_mdi_child = new fQuanLyTaiKhoan();
            new_mdi_child.Show();
        }

        private void menuDiem_Click(object sender, EventArgs e)
        {
            Form new_mdi_child = new fQuanLyDiem();
            //new_mdi_child.Text = "Cửa sổ con MDI";
            //new_mdi_child.MdiParent = this;
            new_mdi_child.Show();
        }

        private void menuChangePassword_Click(object sender, EventArgs e)
        {
            Form new_mdi_child = new fChangePassword();
            new_mdi_child.Show();
        }

        private void menuSignout_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                Form new_mdi_child = new Login_Temp();
                new_mdi_child.Show();
                pictureBox1.Image.Dispose();
                this.Close();
                
            }
        }

        void checkAdmin()
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            String getUsername = Login_Temp.globalUsername;
            Console.WriteLine(getUsername);
            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[User] WHERE USERNAME ='{getUsername}' AND isAdmin ='Y'");
            if (dt.Rows.Count > 0)
            {
               
                menuAccount.Enabled = true;
                menuDiem.Enabled = true;
                menuDanhSachSinhVien.Enabled = true;
            }
            else
            {
                menuAccount.Enabled = false;
                menuDiem.Enabled = true;
                menuDanhSachSinhVien.Enabled = true;
            }
                
        }

        private void menuNhapSinhVien_Click(object sender, EventArgs e)
        {
            Form new_mdi_child = new fNhapSinhVien();
            new_mdi_child.Show();
        }


        private void menuDanhSachSinhVien_Click(object sender, EventArgs e)
        {
            Form new_mdi_child = new fDanhSachSinhVien();
            new_mdi_child.Show();
        }

        private void danhSáchKhoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_mdi_child = new fQuanLyKhoa();
            new_mdi_child.Show();
        }

        private void danhSáchLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_mdi_child = new fQuanLyLop();
            new_mdi_child.Show();
        }

        private void danhSáchMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_mdi_child = new fMonHoc();
            new_mdi_child.Show();
        }

        private void danhSáchHệĐàoTạoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_mdi_child = new fHeDT();
            new_mdi_child.Show();
        }
    }
}
