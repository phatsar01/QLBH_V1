using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_PolyCafe;
using Microsoft.Data.SqlClient;

namespace DAL_PolyCafe
{
    public class DALChiTietPhieu
    {
        public string generateChiTietID()
        {
            string prefix = "CTP";
            string sql = "SELECT MAX(CAST(SUBSTRING(MaPhieu, 4, 3) AS INT)) FROM ChiTietPhieu WHERE MaPhieu LIKE 'CTP%'";
            List<object> thamSo = new List<object>();
            object result = DBUtil.ScalarQuery(sql, thamSo);
            if (result != null && result != DBNull.Value)
            {
                int newNumber = Convert.ToInt32(result) + 1;
                return $"{prefix}{newNumber:D3}";
            }
            return $"{prefix}001";
        }

        public List<ChiTietPhieu> selectChiTietByMaPhieu(string maPhieu)
        {
            string sql = "SELECT ct.Id, ct.MaPhieu, ct.MaSanPham, ct.SoLuong, ct.DonGia, sp.TenSanPham " +
                         "FROM ChiTietPhieu ct " +
                         "LEFT JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham " +
                         "WHERE ct.MaPhieu = @0";
            List<object> thamSo = new List<object>();
            thamSo.Add(maPhieu);
            var list = SelectBySql(sql, thamSo);

            // Debug: Kiểm tra dữ liệu
            foreach (var item in list)
            {
                Console.WriteLine($"MaSanPham: {item.MaSanPham}, TenSanPham: {item.TenSanPham}");
            }

            return list;
        }

        public List<ChiTietPhieu> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<ChiTietPhieu> list = new List<ChiTietPhieu>();
            try
            {
                using (SqlDataReader reader = DBUtil.Query(sql, args))
                {
                    while (reader.Read())
                    {
                        ChiTietPhieu entity = new ChiTietPhieu();
                        entity.Id = reader.GetInt32("id");
                        entity.MaPhieu = reader.GetString("MaPhieu");
                        entity.MaSanPham = reader.GetString("MaSanPham");
                        entity.SoLuong = reader.GetInt32("SoLuong");
                        entity.DonGia = reader.GetDecimal("DonGia");
                        entity.TenSanPham = reader.IsDBNull(reader.GetOrdinal("TenSanPham")) ? null : reader.GetString("TenSanPham");
                        list.Add(entity);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public void insertChiTiet(ChiTietPhieu ct)
        {
            try
            {
                string sql = @"INSERT INTO ChiTietPhieu (MaPhieu, MaSanPham, SoLuong, DonGia) VALUES (@0, @1, @2, @3)";
                List<object> thamSo = new List<object>();
                thamSo.Add(ct.MaPhieu);
                thamSo.Add(ct.MaSanPham);
                thamSo.Add(ct.SoLuong);
                thamSo.Add(ct.DonGia);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void updateSoluong(ChiTietPhieu ct)
        {
            try
            {
                string sql = @"UPDATE ChiTietPhieu SET SoLuong = @1 WHERE id = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(ct.Id);
                thamSo.Add(ct.SoLuong);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void deleteChiTietPhieu(int id)
        {
            try
            {
                string sql = "DELETE FROM ChiTietPhieu WHERE id = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(id);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}