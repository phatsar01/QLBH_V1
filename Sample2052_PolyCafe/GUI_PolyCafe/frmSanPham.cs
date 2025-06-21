using BLL_PolyCafe;
using DTO_PolyCafe;
using GUI_PolyCafe.helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTIL_PolyCafe;

namespace GUI_PolyCafe
{
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
            txtDonGia.Mask = "0000000000";
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadLoaiSanPham();
            LoadDanhSachSanPham();
        }

        private void ClearForm()
        {
            btnThemSanPham.Enabled = true;
            btnSuaSanPham.Enabled = false;
            btnXoaSanPham.Enabled = true;
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            txtDonGia.Clear();
            rdbActive.Checked = true;
            pbHinhAnh.Image = null;
        }

        private void LoadLoaiSanPham()
        {
            try
            {
                BUSLoaiSanPham bUSLoaiSanPham = new BUSLoaiSanPham();
                List<LoaiSanPham> dsLoai = bUSLoaiSanPham.GetLoaiSanPhamList();
                cboLoaiSanPham.DataSource = dsLoai;
                cboLoaiSanPham.ValueMember = "MaLoai";
                cboLoaiSanPham.DisplayMember = "TenLoai";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại sản phẩm" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachSanPham()
        {
            BUSSanPham bUSSanPham = new BUSSanPham();
            dgrDanhSachSP.DataSource = null;
            List<SanPham> lstSP = bUSSanPham.GetSanPhamList();
            dgrDanhSachSP.DataSource = lstSP;
        }
        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            try
            {
                string tenSP = txtTenSanPham.Text.Trim();
                string donGiaText = txtDonGia.Text.Trim();
                string maLoai = cboLoaiSanPham.SelectedValue?.ToString();
                bool trangThai = rdbActive.Checked;

                // Kiểm tra dữ liệu nhập vào
                if (string.IsNullOrEmpty(tenSP) || string.IsNullOrEmpty(donGiaText) || string.IsNullOrEmpty(maLoai))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chuyển đổi đơn giá
                if (!decimal.TryParse(donGiaText, out decimal donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                string savePath = imageutil.SaveImage(pbHinhAnh.Image, "Uploads");
                // Tạo đối tượng sản phẩm
                SanPham sp = new SanPham
                {
                    TenSanPham = tenSP,
                    DonGia = donGia,
                    MaLoai = maLoai,
                    TrangThai = trangThai,
                    HinhAnh = savePath
                };

                // Thêm sản phẩm vào cơ sở dữ liệu
                BUSSanPham bUSSanPham = new BUSSanPham();
                bUSSanPham.InsertSanPham(sp);

                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới form sau khi thêm
                ClearForm();
                LoadDanhSachSanPham();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaSanPham_Click(object sender, EventArgs e)
        {
           try
    {
        string tenSP = txtTenSanPham.Text.Trim();
        string donGiaText = txtDonGia.Text.Trim();
        string maLoai = cboLoaiSanPham.SelectedValue?.ToString();
        bool trangThai = rdbActive.Checked;
        string maSP = txtMaSanPham.Text.Trim();

        // Kiểm tra dữ liệu nhập vào
        if (string.IsNullOrEmpty(tenSP) || string.IsNullOrEmpty(donGiaText) || string.IsNullOrEmpty(maLoai))
        {
            MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Chuyển đổi đơn giá
        if (!decimal.TryParse(donGiaText, out decimal donGia))
        {
            MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string savePath = imageutil.SaveImage(pbHinhAnh.Image, "Uploads"); // Thêm dòng này để lưu hình ảnh
        // Tạo đối tượng sản phẩm
        SanPham sp = new SanPham
        {
            MaSanPham = maSP,
            TenSanPham = tenSP,
            DonGia = donGia,
            MaLoai = maLoai,
            TrangThai = trangThai,
            HinhAnh = savePath // Gán đường dẫn hình ảnh
        };

        // Cập nhật sản phẩm vào cơ sở dữ liệu
        BUSSanPham bUSSanPham = new BUSSanPham();
        string result = bUSSanPham.UpdateSanPham(sp);

        if (string.IsNullOrEmpty(result))
        {
            MessageBox.Show("Cập nhật thông tin thành công");
            ClearForm();
            LoadDanhSachSanPham();
        }
        else
        {
            MessageBox.Show(result);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
        }

        private void btnXoaSanPham_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSanPham.Text.Trim();
            string tenSP = string.Empty;
            string hinhAnh = string.Empty;

            if (string.IsNullOrEmpty(maSP))
            {
                if (dgrDanhSachSP.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgrDanhSachSP.SelectedRows[0];
                    maSP = selectedRow.Cells["MaSanPham"].Value.ToString();
                    tenSP = selectedRow.Cells["TenSanPham"].Value.ToString();
                    hinhAnh = selectedRow.Cells["HinhAnh"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một sản phẩm để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                tenSP = txtTenSanPham.Text.Trim();
            }

            if (string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm {maSP} - {tenSP}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSSanPham bus = new BUSSanPham();
                string kq = bus.DeleteSanPham(maSP);

                if (string.IsNullOrEmpty(kq))
                {

                    MessageBox.Show($"Xóa thông tin sản phẩm {maSP} - {tenSP} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachSanPham();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnMoiSanPham_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadLoaiSanPham();
            LoadDanhSachSanPham();
        }

        private void dgrDanhSachSP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem có dòng nào được chọn không
            {
                DataGridViewRow row = dgrDanhSachSP.Rows[e.RowIndex];
                txtMaSanPham.Text = row.Cells["MaSanPham"].Value.ToString();
                txtTenSanPham.Text = row.Cells["TenSanPham"].Value.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
                cboLoaiSanPham.SelectedValue = row.Cells["MaLoai"].Value.ToString();

                string relativePath = row.Cells["HinhAnh"].Value.ToString();
                string fullPath = Path.Combine(Application.StartupPath, relativePath);
                if (File.Exists(fullPath))
                {
                    pbHinhAnh.Image = Image.FromFile(fullPath);
                    pbHinhAnh.Tag = fullPath;
                }
                else
                {
                    pbHinhAnh.Image = null;
                    pbHinhAnh.Tag = null;
                }

                bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                rdbActive.Checked = trangThai;
                rdbDeActive.Checked = !trangThai;

                btnThemSanPham.Enabled = false;
                btnSuaSanPham.Enabled = true;
                btnXoaSanPham.Enabled = true;
                txtMaSanPham.Enabled = false;
            }
        }

        private void frmSanPham_Resize(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = 10;
        }

        private void btnUploadSanPham_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbHinhAnh.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
    }
}
