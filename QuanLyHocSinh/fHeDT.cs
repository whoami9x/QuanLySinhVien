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
    public partial class fHeDT : Form
    {
        public static string getMaHDT = "";
        public fHeDT()
        {
            InitializeComponent();
        }

        private void fHeDT_Load(object sender, EventArgs e)
        {
            btnApply.Enabled = false;
            txtMaHeDT.Enabled = false;
            txtTenHeDT.Enabled = false;

            LoadData();
        }

        public void LoadData()
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[HeDaoTao]");
            dgvData.DataSource = dt;
            formatDatagridview();

        }

        public void formatDatagridview()
        {
            dgvData.Columns[0].HeaderText = "Mã hệ đào tạo";
            dgvData.Columns[1].HeaderText = "Tên hệ đào tạo";


            dgvData.Columns[0].Width = 130;
            dgvData.Columns[1].Width = 400;


        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count > 0)
                {
                    txtMaHeDT.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["MaHeDT"].Value.ToString();
                    txtTenHeDT.Text = dgvData.Rows[dgvData.CurrentCell.RowIndex].Cells["TenHeDT"].Value.ToString();

                }
            }
            catch (Exception)
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Thêm")
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

        private void DisableTextbox()
        {
            txtMaHeDT.Enabled = false;
            txtTenHeDT.Enabled = false;
        }

        void ClearTextbox()
        {
            txtTenHeDT.Text = "";
            txtMaHeDT.Text = "";
        }
        void EnableTextbox()
        {
            txtTenHeDT.Enabled = true;
            txtMaHeDT.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(btnEdit.Text == "Sửa")
            {
                EnableTextbox();
                getMaHDT = txtMaHeDT.Text;
                btnApply.Enabled = true;
                btnEdit.Text = "Hủy sửa";
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
                    db.UpdateData($"UPDATE [StudentManagement].[dbo].[HeDaoTao] SET MaHeDT = N'{txtMaHeDT.Text}',TenHeDT = N'{txtTenHeDT.Text}' WHERE MaHeDT = N'{getMaHDT}'");
                    MessageBox.Show("Cập nhật thành công !");
                    btnEdit.Text = "Sửa";
                    DisableTextbox();
                    btnApply.Enabled = false;
                    LoadData();
                }   
                else
                {
                    dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[HeDaoTao] WHERE MaHeDT = '{txtMaHeDT.Text}' or TenHeDT = '{txtTenHeDT.Text}'");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show($"Mã đào tạo hoặc tên đào tạo đã tồn tại trong hệ thống !");
                    }
                    else
                    {
                        String query = $"INSERT INTO [StudentManagement].[dbo].[HeDaoTao] (MaHeDT,TenHeDT)  VALUES(N'{txtMaHeDT.Text}',N'{txtTenHeDT.Text}')";
                        db.UpdateData(query, "Thêm hệ đào tạo thành công !");
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Cập nhật dữ liệu thành công !");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count > 0)
            {
                Database db = new Database();
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn xóa hệ đào tạo: {txtTenHeDT.Text}", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    db.UpdateData($"DELETE FROM [StudentManagement].[dbo].[HeDaoTao] WHERE MaHeDT = '{txtMaHeDT.Text}'");
                    LoadData();
                    MessageBox.Show("Xóa dữ liệu thành công !");
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
                case "Mã hệ đào tạo":
                    dt = db.GetData($"SELECT [MaHeDT] FROM [StudentManagement].[dbo].[HeDaoTao]");
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbResult.Items.Add(dr[0].ToString());
                    }
                    break;
                case "Tên hệ đào tạo":
                    dt = db.GetData($"SELECT [TenHeDT] FROM [StudentManagement].[dbo].[HeDaoTao]");
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
           
            dt = db.GetData($"SELECT * FROM [StudentManagement].[dbo].[HeDaoTao] WHERE MaHeDT = N'{cbResult.Text}' OR TenHeDT = N'{cbResult.Text}'");
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
    }
}
