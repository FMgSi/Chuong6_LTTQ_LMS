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

            ft.FillComBox(cbMaNhanVien, dtBase.DocBang("select * from tblNhanVien"), "MaNhanVien", "MaNhanVien");
            cbMaNhanVien.SelectedIndex = -1;

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
            btnLuuHoaDon.Enabled = true;
            btnHuyHoaDon.Enabled = false;
            btnInHoaDon.Enabled = false;
            btnThoat.Enabled = true;
        }

        private void btnThemHoaDon_Click(object sender, EventArgs e)
        {

        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThemHoaDon.Enabled = false;
            btnLuuHoaDon.Enabled = true;
            btnInHoaDon.Enabled = true;
            btnHuyHoaDon.Enabled = true;


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


    }
}
