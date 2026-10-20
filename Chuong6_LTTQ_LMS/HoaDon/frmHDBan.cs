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
            Classes.DataProcesser dtBase = new Classes.DataProcesser();
            Classes.Function ft = new Classes.Function();

            ft.FillComBox(cbMaNhanVien, dtBase.DocBang("select * from tblNhanVien"), "MaNhanVien", "TenNhanVien");
            cbMaNhanVien.SelectedIndex = -1;

            ft.FillComBox(cbMaHang, dtBase.DocBang("select * from tblHang"), "Mahang", "Tenhang");
            cbMaHang.SelectedIndex = -1;

            ft.FillComBox(cbMaHoaDon, dtBase.DocBang("select * from tblHDBan"), "MaHDBan", "MaHDBan");
            cbMaHoaDon.SelectedIndex = -1;

            ft.FillComBox(cbMaKhachHang, dtBase.DocBang("select * from tblKhach"), "Makhach", "Tenkhach");
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
    }
}
