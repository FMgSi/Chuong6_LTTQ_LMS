using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chuong6_LTTQ_LMS.HoaDon
{
    public partial class frmHDBan : Form
    {
        Classes.DataProcesser dtBase = new Classes.DataProcesser();
        Classes.Function ft = new Classes.Function();
        public static string MaNhanVien = "";
        public frmHDBan()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHDBan_Load(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.ShowDialog();
            
            ft.FillComBox(cbMaNhanVien, dtBase.DocBang("select * from tblNhanVien"), "MaNhanVien", "MaNhanVien");
            cbMaNhanVien.SelectedIndex = -1;
            cbMaNhanVien.SelectedValue = MaNhanVien;

            ft.FillComBox(cbMaHang, dtBase.DocBang("select * from tblHang"), "Mahang", "Mahang");
            cbMaHang.SelectedIndex = -1;

            ft.FillComBox(cbMaHoaDon, dtBase.DocBang("select * from tblHDBan"), "MaHDBan", "MaHDBan");
            cbMaHoaDon.SelectedIndex = -1;

            ft.FillComBox(cbMaKhachHang, dtBase.DocBang("select * from tblKhach"), "Makhach", "Makhach");
            cbMaKhachHang.SelectedIndex = -1;            

            dgvHoaDon.DataSource = dtBase.DocBang(
                "select " +
                "cthdb.Mahang," +
                "h.TenHang," +
                "cthdb.Soluong," +
                "h.Dongiaban," +
                "cthdb.Giamgia," +
                "Thanhtien " +
                "from tblChitietHDBan cthdb " +
                "join tblHang h on h.MaHang = cthdb.MaHang ");

            dgvHoaDon.Columns[0].HeaderText = "Ma Hang";
            dgvHoaDon.Columns[1].HeaderText = "Ten Hang";
            dgvHoaDon.Columns[2].HeaderText = "So Luong";
            dgvHoaDon.Columns[3].HeaderText = "Don Gia";
            dgvHoaDon.Columns[4].HeaderText = "Giam Gia";
            dgvHoaDon.Columns[5].HeaderText = "Thanh Tien";

            btnThemHoaDon.Enabled = true;
            btnLuuHoaDon.Enabled = false;
            btnHuyHoaDon.Enabled = false;
            btnInHoaDon.Enabled = false;
            btnThoat.Enabled = true;
        }

        private string SinhMaHoaDon()
        {
            string ngay = DateTime.Now.ToString("ddMMyyyy");
            string tienTo = "HDB_" + ngay;

            string sql = "select count(*) from tblHDBan where MaHDBan like '" + tienTo + "%'";
            DataTable dt = dtBase.DocBang(sql);
            int stt = 1;
            if (dt.Rows.Count > 0)
            {
                stt = Convert.ToInt32(dt.Rows[0][0]) + 1;
            }
            string maHD = tienTo + stt.ToString("D4");
            return maHD;
        }

        private void btnThemHoaDon_Click(object sender, EventArgs e)
        {
            btnThemHoaDon.Enabled = true;
            btnLuuHoaDon.Enabled = true;
            btnHuyHoaDon.Enabled = false;
            btnInHoaDon.Enabled = false;
            btnThoat.Enabled = true;

            cbMaNhanVien.SelectedValue = MaNhanVien;
            dtpNgayban.Value = DateTime.Now;
            cbMaKhachHang.Text = "";
            txtMaHoaDon.Text = SinhMaHoaDon();
        }
        void TinhThanhTien()
        {
            double dongia = 0, soluong = 0, giamgia = 0;
            double.TryParse(txtDonGia.Text, out dongia);
            double.TryParse(txtSoLuong.Text, out soluong);
            double.TryParse(txtGiamGia.Text, out giamgia);

            double thanhtien = soluong * dongia - (soluong * dongia * giamgia / 100);

            txtThanhTien.Text = thanhtien.ToString();
        }

        private void cbMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaNhanVien.SelectedIndex != -1 && cbMaNhanVien.Text != "")
            {
                DataTable dtNhanVien = dtBase.DocBang("select TenNhanVien from tblNhanvien where MaNhanVien = '" + cbMaNhanVien.Text + "'");
                if (dtNhanVien.Rows.Count > 0)
                    txtTenNhanVien.Text = dtNhanVien.Rows[0]["TenNhanVien"].ToString();
            }
            else
            {
                txtTenNhanVien.Text = "";
            }

        }

        private void cbMaKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaKhachHang.SelectedIndex != -1 && cbMaKhachHang.Text != "")
            {
                DataTable dtKhach = dtBase.DocBang("select TenKhach, Diachi, DienThoai from tblKhach where Makhach = '" + cbMaKhachHang.Text + "'");
                if (dtKhach.Rows.Count > 0)
                {
                    txtTenKhachHang.Text = dtKhach.Rows[0]["Tenkhach"].ToString();
                    txtDiaChi.Text = dtKhach.Rows[0]["Diachi"].ToString();
                    txtDienThoai.Text = dtKhach.Rows[0]["Dienthoai"].ToString();
                }
            }
            else
            {
                txtTenKhachHang.Text = "";
                txtDienThoai.Text = "";
                txtDiaChi.Text = "";
            }
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThemHoaDon.Enabled = false;
            btnLuuHoaDon.Enabled = true;
            btnInHoaDon.Enabled = true;
            btnHuyHoaDon.Enabled = true;

            cbMaHang.SelectedValue = dgvHoaDon.CurrentRow.Cells[0].Value.ToString();
            txtTenHang.Text = dgvHoaDon.CurrentRow.Cells[1].Value.ToString();
            txtSoLuong.Text = dgvHoaDon.CurrentRow.Cells[2].Value.ToString();
            txtDonGia.Text = dgvHoaDon.CurrentRow.Cells[3].Value.ToString();
            txtGiamGia.Text = dgvHoaDon.CurrentRow.Cells[4].Value.ToString();
            txtThanhTien.Text = dgvHoaDon.CurrentRow.Cells[5].Value.ToString();



        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            btnThemHoaDon.Enabled = true;
            btnLuuHoaDon.Enabled = false;
            btnHuyHoaDon.Enabled = false;
            btnInHoaDon.Enabled = false;
            btnThoat.Enabled = true;
            DataTable dtHoaDon = dtBase.DocBang("select * from tblHDBan where MaHDBan = '" + cbMaHoaDon.SelectedValue.ToString() + "'");
            txtMaHoaDon.Text = dtHoaDon.Rows[0]["MaHDBan"].ToString();
            dtpNgayban.Value = Convert.ToDateTime(dtHoaDon.Rows[0]["NgayBan"].ToString());
            cbMaNhanVien.SelectedValue = dtHoaDon.Rows[0]["Manhanvien"].ToString();
            cbMaKhachHang.SelectedValue = dtHoaDon.Rows[0]["Makhach"].ToString();
            dgvHoaDon.DataSource = dtBase.DocBang(
                            "select " +
                            "cthdb.Mahang," +
                            "h.TenHang," +
                            "cthdb.Soluong," +
                            "h.Dongiaban," +
                            "cthdb.Giamgia," +
                            "Thanhtien " +
                            "from tblChitietHDBan cthdb " +
                            "join tblHang h on h.MaHang = cthdb.MaHang " +
                            "where MaHDBan = '" + cbMaHoaDon.SelectedValue.ToString() + "'");
        }

        private void cbMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaHang.SelectedIndex >= 0)
            {
                DataTable dt = dtBase.DocBang("select * from tblHang where Mahang = '" + cbMaHang.SelectedValue.ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    txtTenHang.Text = dt.Rows[0]["Tenhang"].ToString();
                    txtDonGia.Text = dt.Rows[0]["Dongiaban"].ToString();
                }

            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            if (cbMaKhachHang.SelectedIndex == -1)
            {
                MessageBox.Show("Hay chon ma khach hang");
                cbMaKhachHang.Focus();
                return;
            }
            if (cbMaHang.SelectedIndex == -1)
            {
                MessageBox.Show("Hay chon ma hang");
                cbMaHang.Focus();
                return;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Hay nhap so luong");
                txtSoLuong.Focus();
                return;
            }
            if (txtGiamGia.Text == "")
            {
                MessageBox.Show("Hay nhap giam gia");
                txtGiamGia.Focus();
                return;
            }
            if (btnThemHoaDon.Enabled == true)
            {

            }
        }

    }
}
