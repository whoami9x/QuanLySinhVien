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
    public partial class fThemLop : Form
    {
        public fThemLop()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Lop] WHERE MaLop = '{txtMaLop.Text}' or TenLop = '{txtTenLop.Text}'");
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show($"Mã lớp hoặc tên lớp đã tồn tại trong hệ thống !");
            }
            else
            {
                String query = $"INSERT INTO [StudentManagement].[dbo].[Lop] (MaLop,TenLop,MaKhoa,MaHeDT)  VALUES(N'{txtMaLop.Text}',N'{txtTenLop.Text}',N'{txtMaKhoa.Text.Substring(0, txtMaKhoa.Text.IndexOf(" -"))}',N'{txtMaHeDT.Text.Substring(0, txtMaHeDT.Text.IndexOf(" -"))}')";
                db.UpdateData(query, "Thêm lớp thành công !");
            }
        }

        private void fThemLop_Load(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            dt = db.GetData($"SELECT [MaKhoa],[TENKHOA] FROM [StudentManagement].[dbo].[Khoa]");
            foreach (DataRow dr in dt.Rows)
            {
                txtMaKhoa.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }

            dt = db.GetData($"SELECT [MaHeDT],[TenHeDT] FROM [StudentManagement].[dbo].[HeDaoTao]");
            foreach (DataRow dr in dt.Rows)
            {
                txtMaHeDT.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
        }
    }
}
