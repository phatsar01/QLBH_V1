using BLL_PolyCafe;
using DAL_PolyCafe;
using DTO_PolyCafe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_PolyCafe
{
    public partial class frmChiTietPhieu : Form
    {
        private TheLuuDong theLuuDong;
        private PhieuBanHang phieuBanHang;
        private NhanVien nhanVien;
        private List<ChiTietPhieu> lstChiTiet;
        private List<SanPham> lstSanPham;
        bool isActive = true;

        public frmChiTietPhieu(TheLuuDong the, PhieuBanHang phieu, NhanVien nv)
        {
            InitializeComponent();
            theLuuDong = the;
            phieuBanHang = phieu;
            nhanVien = nv;
            lstChiTiet = new List<ChiTietPhieu>();
            lstSanPham = new List<SanPham>();
            isActive = phieu.TrangThai;
        }

        private void activeTranfer()
        {
            if (isActive)
            {
                btnThemChiTiet.Enabled = false;
                btnXoaChiTiet.Enabled = false;
                txtPhanTram.Enabled = false;
            }
        }

        private void LoadInfo()
        {
            lbChuSoHuu.Text = $"{theLuuDong.MaThe} - {theLuuDong.ChuSoHuu}";
            lbMaPhieu.Text = phieuBanHang.MaPhieu;
            lbNgayLap.Text = phieuBanHang.NgayTao.ToString("dd/MM/yyyy");
        }

        private void frmChiTietPhieu_Load(object sender, EventArgs e)
        {
            loadThanhToan();
            LoadInfo();
            loadSanPham();
            loadChiTietPhieu(phieuBanHang.MaPhieu);
            activeTranfer();
            CalculateTotal(); // Tính tổng tiền khi load
        }

        private void loadSanPham()
        {
            BUSSanPham sp = new BUSSanPham();
            lstSanPham = sp.GetSanPhamList(1);
            dgrSanPham.DataSource = lstSanPham;
            dgrSanPham.Columns["MaSanPham"].HeaderText = "Mã Sản Phẩm";
            dgrSanPham.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            dgrSanPham.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgrSanPham.Columns["TenLoai"].HeaderText = "Loại Sản Phẩm";
            dgrSanPham.Columns["HinhAnh"].HeaderText = "Hình Ảnh";

            dgrSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void loadChiTietPhieu(string maPhieu)
        {
            BusChiTietPhieu bus = new BusChiTietPhieu();
            lstChiTiet = bus.GetChiTietPhieuList(maPhieu); // Đảm bảo phương thức trả về TenSanPham
            dgrChiTiet.DataSource = lstChiTiet;
            dgrChiTiet.Columns["Id"].Visible = false;
            dgrChiTiet.Columns["MaPhieu"].Visible = false;
            dgrChiTiet.Columns["MaSanPham"].Visible = false;
            dgrChiTiet.Columns["TenSanPham"].HeaderText = "Sản phẩm"; // Đặt tiêu đề cột
            dgrChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgrChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
            if (!dgrChiTiet.Columns.Contains("ThanhTien"))
            {
                dgrChiTiet.Columns.Add("ThanhTien", "Thành Tiền");
            }
            UpdateThanhTienColumn(); // Cập nhật cột ThanhTien
            dgrChiTiet.Columns["SoLuong"].ReadOnly = false;

            dgrChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn col in dgrChiTiet.Columns)
            {
                col.ReadOnly = true;
            }
            dgrChiTiet.Columns["SoLuong"].ReadOnly = false;
            CalculateTotal(); // Cập nhật tổng tiền
        }

        private void UpdateThanhTienColumn()
        {
            foreach (DataGridViewRow row in dgrChiTiet.Rows)
            {
                if (row.Cells["SoLuong"].Value != null && row.Cells["DonGia"].Value != null)
                {
                    int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value);
                    row.Cells["ThanhTien"].Value = soLuong * donGia;
                }
            }
        }

        private void dgrSanPham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isActive)
            {
                return;
            }
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgrSanPham.Rows[e.RowIndex];

                SanPham sanPham = new SanPham
                {
                    MaSanPham = row.Cells["MaSanPham"].Value.ToString(),
                    TenSanPham = row.Cells["TenSanPham"].Value.ToString(),
                    DonGia = Convert.ToDecimal(row.Cells["DonGia"].Value)
                };

                transfer(sanPham);
            }
        }

        private void transfer(SanPham sp, int soLuong = 1)
        {
            if (isActive)
            {
                return;
            }
            if (sp != null)
            {
                BusChiTietPhieu bus = new BusChiTietPhieu();
                ChiTietPhieu existingItem = lstChiTiet.FirstOrDefault(item => item.MaSanPham == sp.MaSanPham);
                if (existingItem != null)
                {
                    existingItem.SoLuong += soLuong;
                    string result = bus.UpdateSoLuong(existingItem);
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Cập nhật số lượng không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    ChiTietPhieu ct = new ChiTietPhieu()
                    {
                        MaPhieu = phieuBanHang.MaPhieu,
                        MaSanPham = sp.MaSanPham,
                        SoLuong = soLuong,
                        DonGia = sp.DonGia
                    };
                    string result = bus.InsertChiTietPhieu(ct);
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Thêm sản phẩm không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                loadChiTietPhieu(phieuBanHang.MaPhieu);
                CalculateTotal(); // Cập nhật tổng tiền sau khi thêm/sửa
            }
        }

        private void deleteChiTiet()
        {
            if (dgrChiTiet.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgrChiTiet.CurrentRow.Cells["Id"].Value);
                string name = dgrChiTiet.CurrentRow.Cells["TenSanPham"].Value?.ToString() ?? "Sản phẩm";
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {name}?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    BusChiTietPhieu bus = new BusChiTietPhieu();
                    string kq = bus.DeleteChiTiet(id.ToString());
                    if (string.IsNullOrEmpty(kq))
                    {
                        loadChiTietPhieu(phieuBanHang.MaPhieu);
                    }
                    else
                    {
                        MessageBox.Show("Xóa sản phẩm không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgrChiTiet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isActive)
            {
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                BusChiTietPhieu bus = new BusChiTietPhieu();
                ChiTietPhieu ct = lstChiTiet[e.RowIndex];
                int newQuantity;
                if (int.TryParse(dgrChiTiet.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString(), out newQuantity))
                {
                    ct.SoLuong = newQuantity;
                    string result = bus.UpdateSoLuong(ct);
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Cập nhật số lượng không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    UpdateThanhTienColumn(); // Cập nhật ThanhTien ngay lập tức
                    CalculateTotal(); // Cập nhật tổng tiền sau khi chỉnh sửa
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXoaChiTiet_Click(object sender, EventArgs e)
        {
            if (isActive)
            {
                return;
            }
            deleteChiTiet();
            CalculateTotal(); // Cập nhật tổng tiền sau khi xóa
        }

        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            if (isActive)
            {
                return;
            }
            if (dgrSanPham.CurrentRow != null)
            {
                string id = Convert.ToString(dgrSanPham.CurrentRow.Cells["MaSanPham"].Value);
                string ten = Convert.ToString(dgrSanPham.CurrentRow.Cells["TenSanPham"].Value);
                decimal dongia = Convert.ToDecimal(dgrSanPham.CurrentRow.Cells["DonGia"].Value);
                SanPham sanPham = new SanPham
                {
                    MaSanPham = id,
                    TenSanPham = ten,
                    DonGia = dongia
                };

                transfer(sanPham);
            }
        }

        private void loadThanhToan()
        {
            txtTong.Text = "0";
            txtGiamGia.Text = "0";
            txtPhanTram.Text = "0";
            txtThanhTien.Text = "0";
            txtDichVu.Text = "0";
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgrChiTiet.Rows)
            {
                if (row.Cells["ThanhTien"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                }
            }
            txtTong.Text = total.ToString("N0"); // Hiển thị tổng tiền với định dạng số
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dgrChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}