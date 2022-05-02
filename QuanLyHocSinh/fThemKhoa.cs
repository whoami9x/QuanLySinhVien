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
    public partial class fThemKhoa : Form
    {
        public fThemKhoa()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Khoa] WHERE TENKHOA = '{txtTenKhoa.Text}' or MaKhoa = '{txtMaKhoa.Text}'");
            if(dt.Rows.Count > 0)
            {
                MessageBox.Show($"Mã Khoa hoặc Tên khoa đã tồn tại trong hệ thống !");
            }    
            else
            {
                String query = $"INSERT INTO [StudentManagement].[dbo].[Khoa] (MaKhoa,TenKhoa,DiaChi,DienThoai)  VALUES(N'{txtMaKhoa.Text}',N'{txtTenKhoa.Text}',N'{txtDiaChi.Text}',N'{txtDienThoai.Text}')";
                db.UpdateData(query, "Thêm khoa thành công !");
            }    

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtMaKhoa.Text = "";
            txtDiaChi.Text = "";
            txtTenKhoa.Text = "";
            txtDienThoai.Text = "";
        }
    }
}
