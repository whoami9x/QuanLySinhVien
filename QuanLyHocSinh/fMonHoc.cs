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
    public partial class fMonHoc : Form
    {
        public string getMaMonHoc = "";
        public fMonHoc()
        {
            InitializeComponent();
        }

        private void fMonHoc_Load(object sender, EventArgs e)
        {
            DisableTextbox();
            LoadData();
        }

        void DisableTextbox()
        {
            btnApply.Enabled = false;
            txtTenMonHoc.Enabled = false;
            txtMaMonHoc.Enabled = false;
            txtSoTinChi.Enabled = false;
        }

        void ClearTextbox()
        {
            txtTenMonHoc.Text = "";
            txtMaMonHoc.Text = "";
            txtSoTinChi.Text = "";
        }
        void EnableTextbox()
        {
            txtTenMonHoc.Enabled = true;
            txtMaMonHoc.Enabled = true;
            txtSoTinChi.Enabled = true;
        }
        public void LoadData()
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[MonHoc]");
            dgvData.DataSource = dt;
            formatDatagridview();

        }

        public void formatDatagridview()
        {
            dgvData.Columns[0].HeaderText = "Mã môn học";
            dgvData.Columns[1].HeaderText = "Tên môn học";
            dgvData.Columns[2].HeaderText = "Tín chỉ";


            dgvData.Columns[0].Width = 150;
            dgvData.Columns[1].Width = 170;
            dgvData.Columns[2].Width = 150;

        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count > 0)
                {
                    txtMaMonHoc.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["MaMonHoc"].Value.ToString();
                    txtTenMonHoc.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["TenMonHoc"].Value.ToString();
                    txtSoTinChi.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["SoTinChi"].Value.ToString();
                    //txtDienThoai.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["DienThoai"].Value.ToString();
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

                btnAdd.Text = "Hủy thêm";
                btnApply.Enabled = true;
                ClearTextbox();
                EnableTextbox();
            }    
            else

            {
                btnAdd.Text = "Thêm";
                btnApply.Enabled = false;
                DisableTextbox();
                LoadData();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            DialogResult dialogResult = MessageBox.Show($"Đồng ý cập nhật / thêm mới dữ liệu?", "Thông báo", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                if(btnEdit.Text == "Hủy sửa")
                {
                    Console.WriteLine($"UPDATE [StudentManagement].[dbo].[MonHoc] SET MaMonHoc = N'{txtMaMonHoc.Text}',TenMonHoc = N'{txtTenMonHoc.Text}', SoTinChi = N'{txtSoTinChi.Text}' WHERE MaMonHoc = '{getMaMonHoc}'");
                    //db.UpdateData($"UPDATE [StudentManagement].[dbo].[MonHoc] SET MaMonHoc = N'{txtMaMonHoc.Text}', TenMonHoc = N'{txtTenMonHoc.Text}, SoTinChi = N'{txtSoTinChi.Text}' WHERE MaMonHoc = N'{getMaMonHoc}'");
                    db.UpdateData($"UPDATE [StudentManagement].[dbo].[MonHoc] SET MaMonHoc = N'{txtMaMonHoc.Text}',TenMonHoc = N'{txtTenMonHoc.Text}', SoTinChi = N'{txtSoTinChi.Text}' WHERE MaMonHoc = '{getMaMonHoc}'");
                    MessageBox.Show("Cập nhật thành công !");
                    btnEdit.Text = "Sửa";
                    DisableTextbox();
                    btnApply.Enabled = false;
                    LoadData();
                }   
                else
                {
                    dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[MonHoc] WHERE MaMonHoc = '{txtMaMonHoc.Text}' or TenMonHoc = '{txtTenMonHoc.Text}'");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show($"Mã môn học hoặc tên môn học đã tồn tại trong hệ thống !");
                    }
                    else
                    {
                        String query = $"INSERT INTO [StudentManagement].[dbo].[MonHoc] (MaMonHoc,TenMonHoc,SoTinChi)  VALUES(N'{txtMaMonHoc.Text}',N'{txtTenMonHoc.Text}','{txtSoTinChi.Text}')";
                        db.UpdateData(query, "Thêm môn học thành công !");
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
            if (dgvData.Rows.Count > 0)
            {
                Database db = new Database();
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn xóa môn học: {txtTenMonHoc.Text}", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    db.UpdateData($"DELETE FROM [StudentManagement].[dbo].[MonHoc] WHERE MaMonHoc = '{txtMaMonHoc.Text}'");
                    MessageBox.Show("Xóa dữ liệu thành công !");
                    LoadData();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Cập nhật dữ liệu thành công !");
        }

        private void cbSelect_TextChanged(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
           
            string getSelected = cbSelect.SelectedItem.ToString();
            cbResult.Items.Clear();
            cbResult.Text = "";
            switch(getSelected)
            {
                case "Tên môn học":
                    dt = db.GetData($"SELECT [TenMonHoc] FROM [StudentManagement].[dbo].[MonHoc]");
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbResult.Items.Add(dr[0].ToString());
                    }
                    break;
                case "Mã môn học":
                    dt = db.GetData($"SELECT [MaMonHoc] FROM [StudentManagement].[dbo].[MonHoc]");
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbResult.Items.Add(dr[0].ToString());
                    }
                    break;
                case "Tín chỉ":
                    dt = db.GetData($"SELECT DISTINCT [SoTinChi] FROM [StudentManagement].[dbo].[MonHoc]");
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
            Console.WriteLine($"SELECT * FROM [StudentManagement].[dbo].[MonHoc] WHERE TenMonHoc = N'{cbResult.Text}' OR MaMonHoc = N'{cbResult.Text}' OR SoTinChi = N'{cbResult.Text}'");
            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[MonHoc] WHERE TenMonHoc = N'{cbResult.Text}' OR MaMonHoc = N'{cbResult.Text}' OR SoTinChi = N'{cbResult.Text}'");
            if(cbResult.Text == "Tất cả" )
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            getMaMonHoc = txtMaMonHoc.Text;
            if (btnEdit.Text == "Sửa")
            {
                btnEdit.Text = "Hủy sửa";
                EnableTextbox();
                btnApply.Enabled = true;
            }
            else
            {
                btnEdit.Text = "Sửa";
                DisableTextbox();
                btnApply.Enabled = false;
            }    
        }
    }
}
