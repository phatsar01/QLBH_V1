using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_PolyCafe;
using DTO_PolyCafe;

namespace BLL_PolyCafe
{
    public class BusChiTietPhieu
    {
        DALChiTietPhieu dalChiTietPhieu = new DALChiTietPhieu();

        public List<ChiTietPhieu> GetChiTietPhieuList(string maPhieu)
        {
            return dalChiTietPhieu.selectChiTietByMaPhieu(maPhieu);
        }

        public string InsertChiTietPhieu(ChiTietPhieu ct)
        {
            try
            {
                // Vì Id là tự tăng, không cần generateChiTietID cho Id
                // Thay vào đó, có thể sử dụng generateChiTietID để tạo MaPhieu nếu cần
                ct.MaPhieu = dalChiTietPhieu.generateChiTietID(); // Sử dụng MaPhieu thay MaChiTiet
                if (string.IsNullOrEmpty(ct.MaPhieu))
                {
                    return "Mã phiếu không hợp lệ.";
                }

                dalChiTietPhieu.insertChiTiet(ct);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateSoLuong(ChiTietPhieu pbh)
        {
            try
            {
                // Sử dụng Id thay vì MaChiTiet
                if (pbh.Id <= 0) // Kiểm tra Id hợp lệ (Id là int)
                {
                    return "Mã chi tiết phiếu không hợp lệ.";
                }

                dalChiTietPhieu.updateSoluong(pbh);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteChiTiet(string id) // Thay maPhieu bằng id (kiểu string để khớp với form)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return "Mã chi tiết phiếu không hợp lệ.";
                }

                int idValue;
                if (!int.TryParse(id, out idValue))
                {
                    return "Mã chi tiết phiếu không hợp lệ.";
                }

                dalChiTietPhieu.deleteChiTietPhieu(idValue); // Truyền int id
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
    }
}