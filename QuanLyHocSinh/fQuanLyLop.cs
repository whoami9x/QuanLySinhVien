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
    public partial class fQuanLyLop : Form
    {
        public static string getMaLop;
        public fQuanLyLop()
        {
            InitializeComponent();
        }

        private void fQuanLyLop_Load(object sender, EventArgs e)
        {
            btnApply.Enabled = false;
            txtMaLop.ReadOnly = true;
            txtTenLop.ReadOnly = true;
            txtMaHeDT.Enabled = false;
            txtMaKhoa.Enabled = false;
            LoadData();
        }
        public void LoadData()
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Lop]");
            dgvData.DataSource = dt;
            formatDatagridview();

        }

        public void formatDatagridview()
        {
            dgvData.Columns[0].HeaderText = "Mã lớp";
            dgvData.Columns[1].HeaderText = "Tên lớp";
            dgvData.Columns[2].HeaderText = "Mã khoa";
            dgvData.Columns[3].HeaderText = "Mã hệ đào tạo";


            dgvData.Columns[0].Width = 130;
            dgvData.Columns[1].Width = 200;
            dgvData.Columns[2].Width = 130;
            dgvData.Columns[3].Width = 130;

        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count > 0)
                {
                    txtMaLop.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["MaLop"].Value.ToString();
                    txtTenLop.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["Tenlop"].Value.ToString();
                    txtMaKhoa.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["Makhoa"].Value.ToString();
                    txtMaHeDT.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["MaHeDT"].Value.ToString();

                }
            }
            catch (Exception)
            {
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //txtFind.ReadOnly = false;
            //if (txtFind.Text != "")
            //{
            //    Database db = new Database();
            //    DataTable dt = new DataTable();
            //    dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Lop] WHERE MALOP LIKE '%{txtFind.Text}%' OR TENLOP LIKE '%{txtFind.Text}%'");

            //    if (dt.Rows.Count > 0)
            //    {
            //        MessageBox.Show($"Tìm thấy {dt.Rows.Count} kết quả.");
            //        dgvData.DataSource = dt;
            //    }

            //}
            //else
            //{
            //    LoadData();
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            fThemLop f = new fThemLop();
            f.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            getMaLop = txtMaLop.Text;
            if (dgvData.Rows.Count <=1)
            {
                MessageBox.Show("Không có dữ liệu để sửa !");
            }   
            else
            {
                if (btnEdit.Text == "Sửa")
                {
                    btnEdit.Text = "Hủy sửa";
                    btnApply.Enabled = true;
                    txtMaLop.ReadOnly = false;
                    txtTenLop.ReadOnly = false;
                    txtMaHeDT.Enabled = true;
                    txtMaKhoa.Enabled = true;

                }
                else
                {
                    btnEdit.Text = "Sửa";
                    btnApply.Enabled = false;
                    txtMaLop.ReadOnly = true;
                    txtTenLop.ReadOnly = true;
                    txtMaHeDT.Enabled = false;
                    txtMaKhoa.Enabled = false;
                }
            }
              

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string getMaKhoa = txtMaKhoa.Text;
            if(getMaKhoa.IndexOf("-") >-1)
            {
                getMaKhoa = getMaKhoa.Substring(0, getMaKhoa.IndexOf(" -"));
            }
            string getHeDT = txtMaHeDT.Text;
            if (getHeDT.IndexOf("-") > -1)
            {
                getHeDT = getHeDT.Substring(0, getHeDT.IndexOf(" -"));
            }
            Database db = new Database();
            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn cập nhật?", "Thông báo", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                //MessageBox.Show($"UPDATE [StudentManagement].[dbo].[Lop] SET MALOP = '{txtMaLop.Text}', TenLop = N'{txtTenLop.Text}', MaKhoa = '{getMaKhoa}', MaHeDT = '{getHeDT}' WHERE MALOP ='{getMaLop}'");
                db.UpdateData($"UPDATE [StudentManagement].[dbo].[Lop] SET MALOP = '{txtMaLop.Text}', TenLop = N'{txtTenLop.Text}', MaKhoa = '{txtMaKhoa.Text.Substring(0, txtMaKhoa.Text.IndexOf(" -"))}', MaHeDT = '{txtMaHeDT.Text.Substring(0, txtMaHeDT.Text.IndexOf(" -"))}' WHERE MALOP ='{getMaLop}'");
                
                //MessageBox.Show("Cập nhật thành công !");
            }
            btnApply.Enabled = false;
            txtMaLop.ReadOnly = true;
            txtTenLop.ReadOnly = true;
            txtMaHeDT.Enabled = false;
            txtMaKhoa.Enabled = false;
            btnEdit.Text = "Sửa";
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvData.Rows.Count > 1)
            {
                Database db = new Database();
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn xóa lớp: {txtTenLop.Text}", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    db.UpdateData($"DELETE FROM [StudentManagement].[dbo].[Lop] WHERE MALOP = '{txtMaLop.Text}'");
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xóa !");
            }    
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Console.WriteLine(txtMaKhoa.Text);
            Console.WriteLine(txtMaHeDT.Text);
            LoadData();
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            

        }

        private void txtMaHeDT_DropDown(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            dt = db.GetData($"SELECT [MaHeDT],[TenHeDT] FROM [StudentManagement].[dbo].[HeDaoTao]");
            txtMaHeDT.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                txtMaHeDT.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
        }

        private void txtMaKhoa_DropDown(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            dt = db.GetData($"SELECT [MaKhoa],[TENKHOA] FROM [StudentManagement].[dbo].[Khoa]");
            txtMaKhoa.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                txtMaKhoa.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();

            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Lop] WHERE MaLop = N'{cbResult.Text}' OR TenLop = N'{cbResult.Text}' OR MaKhoa = N'{cbResult.Text}' OR MaHeDT = N'{cbResult.Text}'");
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
                    formatDatagridview();
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
                case "Mã lớp":
                    dt = db.GetData($"SELECT [MaLop] FROM [StudentManagement].[dbo].[Lop]");
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbResult.Items.Add(dr[0].ToString());
                    }
                    break;
                case "Tên lớp":
                    dt = db.GetData($"SELECT [TenLop] FROM [StudentManagement].[dbo].[Lop]");
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbResult.Items.Add(dr[0].ToString());
                    }
                    break;
                case "Mã khoa":
                    dt = db.GetData($"SELECT [MaKhoa] FROM [StudentManagement].[dbo].[Lop]");
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbResult.Items.Add(dr[0].ToString());
                    }
                    break;
                case "Mã hệ đào tạo":
                    dt = db.GetData($"SELECT [MaHeDT] FROM [StudentManagement].[dbo].[Lop]");
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
    }
}
