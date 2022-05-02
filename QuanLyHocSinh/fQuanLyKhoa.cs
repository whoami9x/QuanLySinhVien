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
    public partial class fQuanLyKhoa : Form
    {
        public static string saveMaKhoa;
        public fQuanLyKhoa()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvData.Rows.Count > 0)
            {
                Database db = new Database();
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn xóa khoa: {txtTenKhoa.Text}", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    db.UpdateData($"DELETE FROM [StudentManagement].[dbo].[Khoa] WHERE MAKHOA = '{txtMaKhoa.Text}'");
                    MessageBox.Show("Xóa dữ liệu thành công !");
                    LoadData();
                }
            }    
        }

        private void fQuanLyKhoaLop_Load(object sender, EventArgs e)
        {
            //btnDelete.Enabled = false;
            //btnEdit.Enabled = false;
            btnApply.Enabled = false;
            txtMaKhoa.ReadOnly = true;
            txtTenKhoa.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtDienThoai.ReadOnly = true;
            LoadData();
        }

        public void LoadData()
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Khoa]");
            dgvData.DataSource = dt;
            formatDatagridview();

        }

        public void formatDatagridview()
        {
            dgvData.Columns[0].HeaderText = "Mã khoa";
            dgvData.Columns[1].HeaderText = "Tên khoa";
            dgvData.Columns[2].HeaderText = "Địa chỉ";
            dgvData.Columns[3].HeaderText = "Điện thoại";


            dgvData.Columns[0].Width = 130;
            dgvData.Columns[1].Width = 130;
            dgvData.Columns[2].Width = 130;
            dgvData.Columns[3].Width = 130;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count > 0)
                {
                    txtMaKhoa.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["MaKhoa"].Value.ToString();
                    txtTenKhoa.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["TenKhoa"].Value.ToString();
                    txtDiaChi.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["DiaChi"].Value.ToString();
                    txtDienThoai.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["DienThoai"].Value.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            fThemKhoa f = new fThemKhoa();
            f.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtMaKhoa.ReadOnly = false;
            txtTenKhoa.ReadOnly = false;
            txtDiaChi.ReadOnly = false;
            txtDienThoai.ReadOnly = false;
            btnApply.Enabled = true;
            saveMaKhoa = txtMaKhoa.Text;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn cập nhật?", "Thông báo", MessageBoxButtons.YesNo);
            
            if (dialogResult == DialogResult.Yes)
            {
                db.UpdateData($"UPDATE [StudentManagement].[dbo].[Khoa] SET MAKHOA = '{txtMaKhoa.Text}', TENKHOA = '{txtTenKhoa.Text}', DIACHI = '{txtDiaChi.Text}', DIENTHOAI = '{txtDienThoai.Text}' WHERE MAKHOA ='{saveMaKhoa}'");
                MessageBox.Show("Cập nhật thành công !");
                LoadData();
            }
            btnApply.Enabled = false;
            txtMaKhoa.ReadOnly = true;
            txtTenKhoa.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtDienThoai.ReadOnly = true;
            LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //txtFind.ReadOnly = false;
            //if(txtFind.Text != "")
            //{
            //    Database db = new Database();
            //    DataTable dt = new DataTable();
            //    dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Khoa] WHERE MAKHOA LIKE '%{txtFind.Text}%' OR TENKHOA LIKE '%{txtFind.Text}%'");

            //    if (dt.Rows.Count > 0)
            //    {
            //        dgvData.DataSource = dt;
            //    }
                
            //}
            //else
            //{
            //    LoadData();
            //}

        }

        private void btnCancelFind_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            
            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Khoa] WHERE MaKhoa = N'{cbResult.Text}' OR TenKhoa = N'{cbResult.Text}'");
            if (cbResult.Text == "Tất cả")
            {
                LoadData();
                MessageBox.Show($"Lấy tất cả dữ liệu !");
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    dgvData.DataSource = dt;
                    MessageBox.Show($"Đã tìm thấy: {dt.Rows.Count} kết quả !");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu theo yêu cầu !");
                }
            }
        }

        private void cbSelect_TextChanged(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();

            string getSelected = cbSelect.SelectedItem.ToString();
            cbResult.Items.Clear();
            cbResult.Text = "";
            switch (getSelected)
            {
                case "Mã khoa":
                    dt = db.GetData($"SELECT [MaKhoa] FROM [StudentManagement].[dbo].[Khoa]");
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbResult.Items.Add(dr[0].ToString());
                    }
                    break;
                case "Tên khoa":
                    dt = db.GetData($"SELECT [TenKhoa] FROM [StudentManagement].[dbo].[Khoa]");
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbResult.Items.Add(dr[0].ToString());
                    }
                    break;
                case "Tất cả":
                    cbResult.Items.Add("Tất cả");
                    cbResult.SelectedItem = "Tất cả";
                    break;
                default:
                    MessageBox.Show("Vui lòng chọn dữ liệu muốn tìm kiếm");
                    break;
            }

        }

        private void cbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cbResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
