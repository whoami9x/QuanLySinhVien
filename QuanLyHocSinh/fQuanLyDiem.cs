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
    public partial class fQuanLyDiem : Form
    {
        public static string getMaSinhVien = "";
        public fQuanLyDiem()
        {
            InitializeComponent();
        }

        private void QLDiem_Load(object sender, EventArgs e)
        {
            DisableTextbox();
            formatDatagridview();
            LoadData();
        }
        public void LoadData()
        {
            //DisableTextbox();
            formatDatagridview();
            DataTable dt = new DataTable();
            Database db = new Database();
            dt = db.GetData($"SELECT MaSinhVien,MaMonHoc, DiemLan1, DiemLan2, DiemTB FROM [StudentManagement].[dbo].[Diem]");
            dgvDisplay.DataSource = dt;
            //GetKetQua();
        }

        void GetKetQua(double grade)
        {
            if(Convert.ToString(grade) != "")
            {
                //string getDTB = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["DiemTB"].Value.ToString();
                if (Convert.ToDouble(grade) > 4)
                    txtKetQua.Text = "Đạt";
                else
                    txtKetQua.Text = "Rớt";
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void EnableTextbox()
        {
            txtMaSinhVien.Enabled = true;
            txtMaMonHoc.Enabled = true;
            txtDiemLan1.Enabled = true;
            txtDiemLan2.Enabled = true;
            txtKetQua.Enabled = true;
        }
        public void DisableTextbox()
        {
            btnApply.Enabled = false;
            txtMaSinhVien.Enabled = false;
            txtMaMonHoc.Enabled = false;
            txtDiemLan1.Enabled = false;
            txtDiemLan2.Enabled = false;
            txtKetQua.Enabled = false;
        }

        public void ClearTextbox()
        {
            txtMaSinhVien.Text = "";
            txtMaMonHoc.Text = "";
            txtDiemLan1.Text = "";
            txtDiemLan2.Text = "";
            txtKetQua.Text = "";
        }
        public void formatDatagridview()
        {

            dgvDisplay.Columns[0].HeaderText = "Mã sinh viên";
            dgvDisplay.Columns[1].HeaderText = "Mã môn học";
            dgvDisplay.Columns[2].HeaderText = "Điểm lân 1";
            dgvDisplay.Columns[3].HeaderText = "Điểm lân 2";
            dgvDisplay.Columns[4].HeaderText = "Điểm trung bình";

            dgvDisplay.Columns[0].Width = 100;
            dgvDisplay.Columns[1].Width = 130;
            dgvDisplay.Columns[2].Width = 100;
            dgvDisplay.Columns[3].Width = 100;
            dgvDisplay.Columns[4].Width = 100;

        }

        private void dgvDisplay_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvDisplay.Rows.Count > 0)
                {
                    
                    txtMaSinhVien.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["MaSinhVien"].Value.ToString();
                    txtMaMonHoc.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["MaMonHoc"].Value.ToString();
                    txtDiemLan1.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["DiemLan1"].Value.ToString();
                    txtDiemLan2.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["DiemLan2"].Value.ToString();
                    string getDTB = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["DiemTB"].Value.ToString();
                    if (Convert.ToDouble(getDTB) > 4)
                    {
                        txtKetQua.Text = "Đạt";
                    }    
                    else
                        txtKetQua.Text = "Rớt";
                }
                else

                {
                    ClearTextbox();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Thêm")
            {
                btnApply.Enabled = true;
                ClearTextbox();
                EnableTextbox();
                btnAdd.Text = "Hủy thêm";
            }    
            else
            {
                DisableTextbox();
                btnAdd.Text = "Thêm";
                LoadData();
                btnApply.Enabled = false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Sửa")
            {
                getMaSinhVien = txtMaSinhVien.Text;
                btnApply.Enabled = true;

                EnableTextbox();
                btnEdit.Text = "Hủy sửa";
            }
            else
            {
                DisableTextbox();
                btnEdit.Text = "Sửa";
                LoadData();
                btnApply.Enabled = false;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            DialogResult dialogResult = MessageBox.Show($"Đồng ý cập nhật / thêm mới dữ liệu?", "Thông báo", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                if (btnEdit.Text == "Hủy sửa")
                {
                    db.UpdateData($"UPDATE [StudentManagement].[dbo].[Diem] SET MaSinhVien = '{txtMaSinhVien.Text}', MaMonHoc = '{txtMaMonHoc.Text}', DiemLan1 = '{txtDiemLan1.Text}', DiemLan2 = '{txtDiemLan2.Text}', DiemTB = N'{txtKetQua.Text}' WHERE MaSinhVien ='{getMaSinhVien}' and MaMonHoc = '{txtMaMonHoc.Text}'");
                    MessageBox.Show("Cập nhật thành công !");
                    btnEdit.Text = "Sửa";
                    DisableTextbox();
                    btnApply.Enabled = false;
                    LoadData();
                }
                else
                {
                    dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Diem] WHERE MaSinhVien = '{txtMaSinhVien.Text}' and MaMonHoc = '{txtMaMonHoc.Text}'");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show($"Mã sinh viên hoặc mã môn học đã tồn tại trong hệ thống !");
                    }
                    else
                    {
                        String query = $"INSERT INTO [StudentManagement].[dbo].[Diem] (MaSinhVien,MaMonHoc,DiemLan1,DiemLan2,DiemTB)  VALUES(N'{txtMaSinhVien.Text}',N'{txtMaMonHoc.Text}','{txtDiemLan1.Text}','{txtDiemLan2.Text}',N'{txtKetQua.Text}')";
                        db.UpdateData(query, "Thêm điểm số thành công !");
                        LoadData();
                        DisableTextbox();
                        btnAdd.Text = "Thêm";
                    }

                }

            }
            else
            {
                btnAdd.Text = "Thêm";
                btnApply.Enabled = false;
                DisableTextbox();
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDisplay.Rows.Count > 0)
            {
                Database db = new Database();
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn xóa dữ liệu?", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    db.UpdateData($"DELETE FROM [StudentManagement].[dbo].[Diem] WHERE MaMonHoc = '{txtMaMonHoc.Text}'");
                    MessageBox.Show("Xóa dữ liệu thành công !");
                    LoadData();
                }
            }
            ClearTextbox();
            LoadData();
        }

        private void txtMaSinhVien_DropDown(object sender, EventArgs e)
        {
            txtMaSinhVien.Items.Clear();
            Database db = new Database();
            DataTable dt = new DataTable();
            dt = db.GetData($"SELECT DISTINCT [MaSinhVien] FROM [StudentManagement].[dbo].[SinhVien]");
            foreach (DataRow dr in dt.Rows)
            {
                txtMaSinhVien.Items.Add(dr[0].ToString());
            }
        }

        private void txtMaMonHoc_DropDown(object sender, EventArgs e)
        {
            txtMaMonHoc.Items.Clear();
            Database db = new Database();
            DataTable dt = new DataTable();
            dt = db.GetData($"SELECT DISTINCT [MaMonHoc] FROM [StudentManagement].[dbo].[MonHoc]");
            foreach (DataRow dr in dt.Rows)
            {
                txtMaMonHoc.Items.Add(dr[0].ToString());
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
                case "Mã môn học":
                    dt = db.GetData($"SELECT DISTINCT [MaMonHoc] FROM [StudentManagement].[dbo].[Diem]");
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbResult.Items.Add(dr[0].ToString());
                    }
                    break;
                case "Mã sinh viên":
                    dt = db.GetData($"SELECT DISTINCT [MaSinhVien] FROM [StudentManagement].[dbo].[Diem]");
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            
            
            if (cbSelect.Text == "Tất cả")
            {
                LoadData();
                MessageBox.Show($"Lấy tất cả dữ liệu !");
            }
            else if(cbSelect.Text == "Mã môn học")
            {
                dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Diem] WHERE MaMonHoc = N'{cbResult.Text}'");
                if (dt.Rows.Count > 0)
                {
                    dgvDisplay.DataSource = dt;
                    MessageBox.Show($"Đã tìm thấy: {dt.Rows.Count} kết quả !");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu theo yêu cầu !");
                }
            }
            else
            {
                dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[Diem] WHERE MaSinhVien = N'{cbResult.Text}'");
                if (dt.Rows.Count > 0)
                {
                    dgvDisplay.DataSource = dt;
                    MessageBox.Show($"Đã tìm thấy: {dt.Rows.Count} kết quả !");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu theo yêu cầu !");
                }
            }    
        }

        private void txtKetQua_Click(object sender, EventArgs e)
        {
            
            
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (txtDiemLan2.Text == "" || txtDiemLan1.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho Điểm lần 1 và Điểm lần 2 ");
            }
            else
            {
                GetKetQua((Convert.ToDouble(txtDiemLan1.Text) + Convert.ToDouble(txtDiemLan2.Text)) / 2);
            }
        }
    }
}
