using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTiemTraSuaUWU
{
    public partial class frmNhanVien : Form
    {
        private int maNV = 0; 
        public frmNhanVien()
        {
            InitializeComponent();
            TaiDanhSachNhanVien();
        }

        private void TaiDanhSachNhanVien()
        {
            List<NHANVIEN> danhSachNhanVien;

            using (var dbcontext = new QuanTraSuaUwu())
            {
                danhSachNhanVien = dbcontext.NHANVIENs.ToList();
            }

            int soThuTu = 1;
            dgvNhanVien.Rows.Clear();
            //Kiểm tra danh sách rỗng
            if (danhSachNhanVien.Count <= 0) return;
            //Đổ dữ liệu ra bảng
            foreach (var nv in danhSachNhanVien)
            {
                int indexRow = dgvNhanVien.Rows.Add();
                dgvNhanVien.Rows[indexRow].Cells[0].Value = soThuTu++;
                dgvNhanVien.Rows[indexRow].Cells[1].Value = nv.HoTen;
                dgvNhanVien.Rows[indexRow].Cells[2].Value = nv.SDT;
                dgvNhanVien.Rows[indexRow].Cells[3].Value = nv.DiaCHi;
                dgvNhanVien.Rows[indexRow].Cells[4].Value = nv.NamSinh;
                dgvNhanVien.Rows[indexRow].Cells[5].Value = nv.GioiTinh;
                dgvNhanVien.Rows[indexRow].Cells[6].Value = nv.MaChucVu;
                dgvNhanVien.Rows[indexRow].Cells[7].Value = nv.TaiKhoan;
                dgvNhanVien.Rows[indexRow].Cells[8].Value = nv.MatKhau;
                dgvNhanVien.Rows[indexRow].Cells[9].Value = nv.MANV;
            }
            dgvNhanVien.Rows[0].Selected = false;

        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (dgvNhanVien.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;

            dgvNhanVien.CurrentRow.Selected = true;
            maNV = int.Parse(dgvNhanVien.Rows[e.RowIndex].Cells[9].FormattedValue.ToString());
            txtHoTen.Text = dgvNhanVien.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            txtSdt.Text = dgvNhanVien.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            txtDiaChi.Text = dgvNhanVien.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            txtNgaySinh.Text = dgvNhanVien.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            txtGioiTinh.Text = dgvNhanVien.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
            txtChucVu.Text = dgvNhanVien.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
            txtUser.Text = dgvNhanVien.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();
            txtPass.Text = dgvNhanVien.Rows[e.RowIndex].Cells[8].FormattedValue.ToString();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var nv = new NHANVIEN();
            nv.HoTen = txtHoTen.Text;
            nv.SDT= txtSdt.Text;
            nv.DiaCHi = txtDiaChi.Text;
            nv.NamSinh = DateTime.Parse(txtNgaySinh.Text);
            nv.GioiTinh = txtGioiTinh.Text;
            nv.MaChucVu = txtChucVu.Text;
            nv.TaiKhoan = txtUser.Text;
            nv.MatKhau = txtPass.Text;

            try
            {
                using (var dbcontext = new QuanTraSuaUwu())
                {
                    dbcontext.NHANVIENs.Add(nv);
                    dbcontext.SaveChanges();// done
                }

                TaiDanhSachNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            

            try
            {
                using (var dbcontext = new QuanTraSuaUwu())
                {

                    var nhanVien = (from a in dbcontext.NHANVIENs
                              where a.MANV == maNV
                              select a).SingleOrDefault();

                    if (nhanVien == null)
                    {
                        MessageBox.Show("Không cập nhật được !!!!");
                        return;
                    }

                   var nv = dbcontext.NHANVIENs.SingleOrDefault(nV => nV.MANV == maNV);
                    nv.HoTen = txtHoTen.Text;
                    nv.SDT = txtSdt.Text;
                    nv.DiaCHi = txtDiaChi.Text;
                    nv.NamSinh = DateTime.Parse(txtNgaySinh.Text);
                    nv.GioiTinh = txtGioiTinh.Text;
                    nv.MaChucVu = txtChucVu.Text;
                    nv.TaiKhoan = txtUser.Text;
                    nv.MatKhau = txtPass.Text;
                    dbcontext.SaveChanges();
                    MessageBox.Show("Cập nhật thành công !!!!");
                }

                TaiDanhSachNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dbcontext = new QuanTraSuaUwu())
                {
                    var nhanVien = (from nv in dbcontext.NHANVIENs
                              where nv.MANV == maNV
                              select nv).SingleOrDefault();

                    if (nhanVien == null) {
                        MessageBox.Show("Không xóa được !!");
                        return;
                    }

                    dbcontext.NHANVIENs.Remove(nhanVien);
                    dbcontext.SaveChanges();

                    TaiDanhSachNhanVien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn muốn thoát ???", " thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ;
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}