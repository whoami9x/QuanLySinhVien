using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QuanLyHocSinh
{
    public partial class fDanhSachSinhVien : Form
    {
        public static string fileName = "";
        public static bool updateImage = false;

        public fDanhSachSinhVien()
        {
            InitializeComponent();
        }
        private void DSSinhVien_Load(object sender, EventArgs e)
        {
            btnApply.Enabled = false;
            disableTextBox();
            LoadData();
        }
        private void dgvDisplay_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvDisplay.Rows.Count > 0)
                {
                    txtMaSV.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["MaSinhVien"].Value.ToString();
                    txtTenSV.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["TenSinhVien"].Value.ToString();
                    txtGioiTinh.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["GioiTinh"].Value.ToString();
                    txtNgaySinh.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["NgaySinh"].Value.ToString();
                    txtQueQuan.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["QueQuan"].Value.ToString();
                    txtSDT.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["SDT"].Value.ToString();
                    txtMaLop.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["MaLop"].Value.ToString();
                    txtTinhTrang.Text = dgvDisplay.Rows[dgvDisplay.CurrentCell.RowIndex].Cells["TinhTrang"].Value.ToString();
                    if (File.Exists(@"\QLSV\QuanLyHocSinh\Image 3x4\" + txtMaSV.Text + ".jpg") == true)
                    {
                        Image im = GetCopyImage(@"\QLSV\QuanLyHocSinh\Image 3x4\" + txtMaSV.Text + ".jpg");
                        pictureAvatar.Image = im;
                    }
                    else
                        pictureAvatar.Image = null;
                }
            }
            catch(Exception )
            {
            }
        }
        private void pictureAvatar_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                updateImage = true;
                btnApply.Enabled = true;

                fileName = dlg.FileName;
                Image im = GetCopyImage(fileName);
                pictureAvatar.Image = im;

                
                //pictureAvatar.Image = Image.FromFile(fileName);
            }
        }
        private void radioMaSV_CheckedChanged(object sender, EventArgs e)
        {
            if(radioMaSV.Checked == true)
                txtTimMaSV.Enabled = true;  
            else
                txtTimMaSV.Enabled = false;
        }
        private void btnAddSinhVien_Click_1(object sender, EventArgs e)
        {
            Form new_mdi_child = new fNhapSinhVien();
            new_mdi_child.Show();
        }
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            string query = "";
            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn xóa sinh viên: {txtMaSV.Text}", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                query = $"DELETE [StudentManagement].[dbo].[SinhVien] WHERE MaSinhVien ='{txtMaSV.Text}'";
                Database db = new Database();
                db.UpdateData(query);
                MessageBox.Show("Xóa sinh viên thành công !");
                LoadData();
            }
            if (dgvDisplay.Rows.Count < 1)
            {
                clearTextbox();
            }
        }
        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Sửa")
            {
                btnEdit.Text = "Hủy";
                btnApply.Enabled = true;
                enableTextBox();
            }
            else
            {
                btnEdit.Text = "Sửa";
                btnApply.Enabled = false;
                disableTextBox();
            }
        }
        private void btnApply_Click_1(object sender, EventArgs e)
        {
            string query = "";
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thực thi?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                query = $"UPDATE [StudentManagement].[dbo].[SinhVien] SET TENSINHVIEN = N'{txtTenSV.Text}', GIOITINH = N'{txtGioiTinh.Text}', NGAYSINH = '{txtNgaySinh.Text}', QUEQUAN = N'{txtQueQuan.Text}', SDT = '{txtSDT.Text}', TinhTrang = N'{txtTinhTrang.SelectedItem.ToString()}', MALOP = '{txtMaLop.Text}' " +
                    $"WHERE MaSinhVien ='{txtMaSV.Text}'";
                Console.WriteLine(query);
                Database db = new Database();
                db.UpdateData(query);
                MessageBox.Show("Cập nhật thông tin sinh viên thành công !");
                disableTextBox();
                btnEdit.Text = "Sửa";
                btnApply.Enabled = false;

                string pathSaveImage = @"\QLSV\QuanLyHocSinh\Image 3x4\" + txtMaSV.Text + Path.GetExtension(fileName);

                if (File.Exists(fileName) == true)
                {
                    File.Copy(fileName, pathSaveImage, true);
                }
                LoadData();
            }
        }
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if(radioMaSV.Checked == false && radioTenSV.Checked == false)
            {
                MessageBox.Show($"Vui lòng chọn điều kiện để tìm kiếm !");
            }
            else
            {
                string query = "";
                if(radioMaSV.Checked == true)
                {
                    query = $"SELECT MaSinhVien,TenSinhVien,GioiTinh,NgaySinh,QueQuan,SDT,MaLop FROM [StudentManagement].[dbo].[SinhVien] WHERE MASINHVIEN LIKE '%{txtTimMaSV.Text}%'";
                    Database db = new Database();
                    DataTable dt = new DataTable();
                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        dgvDisplay.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy mã sinh viên: {txtTimMaSV.Text}");
                    }
                }
                if(radioTenSV.Checked == true)
                {
                    query = $"SELECT MaSinhVien,TenSinhVien,GioiTinh,NgaySinh,QueQuan,SDT,MaLop FROM [StudentManagement].[dbo].[SinhVien] WHERE TENSINHVIEN LIKE '%{txtFindTenSV.Text}%'";
                    Database db = new Database();
                    DataTable dt = new DataTable();
                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        dgvDisplay.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy tên sinh viên: {txtFindTenSV.Text}");
                    }
                }
            }
        }
        private void radioMaSV_Click(object sender, EventArgs e)
        {
            Console.WriteLine(radioMaSV.Checked);
            if (radioMaSV.Checked == true)
            {
                txtFindTenSV.Text = "";
                txtTimMaSV.ReadOnly = false;
                txtFindTenSV.ReadOnly = true;
                radioTenSV.Checked = false;
            }

        }
        private void radioTenSV_Click(object sender, EventArgs e)
        {
            Console.WriteLine(radioTenSV.Checked);
            if (radioTenSV.Checked == true)
            {
                txtTimMaSV.Text = "";
                txtFindTenSV.ReadOnly = false;
                txtTimMaSV.ReadOnly = true;
                radioMaSV.Checked = false;
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            radioMaSV.Checked = false;
            radioTenSV.Checked = false;
            txtFindTenSV.Text = "";
            txtTimMaSV.Text = "";
            txtFindTenSV.ReadOnly = true;
            txtTimMaSV.ReadOnly = true;
            dgvDisplay.DataSource = null;
            LoadData();
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
 
            LoadData();
            MessageBox.Show($"Cập nhật dữ liệu thành công !");
        }
        private void btnChangeAvatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                updateImage = true;
                btnApply.Enabled = true;

                fileName = dlg.FileName;
                Image im = GetCopyImage(fileName);
                pictureAvatar.Image = im;


                //pictureAvatar.Image = Image.FromFile(fileName);
            }
        }

        /// <summary>
        /// Custom function
        /// </summary>
        public void LoadData()
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            dt = db.GetData($"SELECT MaSinhVien,TenSinhVien,GioiTinh,NgaySinh,QueQuan,SDT,MaLop,TinhTrang FROM [StudentManagement].[dbo].[SinhVien]");
            dgvDisplay.DataSource = dt;
            formatDatagridview();
        }
        public void formatDatagridview()
        {
            dgvDisplay.Columns[0].HeaderText = "Mã sinh viên";
            dgvDisplay.Columns[1].HeaderText = "Tên sinh viên";
            dgvDisplay.Columns[2].HeaderText = "Giới tính";
            dgvDisplay.Columns[3].HeaderText = "Ngày sinh";
            dgvDisplay.Columns[4].HeaderText = "Quê quán";
            dgvDisplay.Columns[5].HeaderText = "Số điện thoại";
            dgvDisplay.Columns[6].HeaderText = "Mã lớp";
            dgvDisplay.Columns[6].HeaderText = "Tình trạng";

            dgvDisplay.Columns[0].Width = 80;
            dgvDisplay.Columns[1].Width = 130;
            dgvDisplay.Columns[2].Width = 80;
            dgvDisplay.Columns[3].Width = 130;
            dgvDisplay.Columns[4].Width = 130;
            dgvDisplay.Columns[5].Width = 130;
            dgvDisplay.Columns[6].Width = 130;
            dgvDisplay.Columns[6].Width = 130;
        }
        void enableTextBox()
        {
            txtTenSV.Enabled = true;
            txtGioiTinh.Enabled = true;
            txtNgaySinh.Enabled = true;
            txtQueQuan.Enabled = true;
            txtSDT.Enabled = true;
            txtMaLop.Enabled = true;
            txtTinhTrang.Enabled = true;
        }
        void clearTextbox()
        {
            txtMaSV.Text = "";
            txtTenSV.Text = "";
            txtGioiTinh.Text = "";
            txtNgaySinh.Text = "";
            txtQueQuan.Text = "";
            txtSDT.Text = "";
            txtMaLop.Text = "";
            txtTinhTrang.Text = "";
            pictureAvatar.Image = null;
        }
        void disableTextBox()
        {
            txtMaSV.Enabled = false;
            txtTenSV.Enabled = false;
            txtGioiTinh.Enabled = false;
            txtNgaySinh.Enabled = false;
            txtQueQuan.Enabled = false;
            txtSDT.Enabled = false;
            txtMaLop.Enabled = false;
            txtTinhTrang.Enabled = false;
        }
        private Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }

        private void txtMaLop_DropDown(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            txtMaLop.Items.Clear();
            dt = db.GetData($"SELECT [MaLop] FROM [StudentManagement].[dbo].[Lop]");
            foreach (DataRow dr in dt.Rows)
            {
                txtMaLop.Items.Add(dr[0].ToString());
            }
        }
    }
}
