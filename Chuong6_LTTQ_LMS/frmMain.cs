using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chuong6_LTTQ_LMS.DanhMuc;
using Chuong6_LTTQ_LMS.HoaDon;
using Chuong6_LTTQ_LMS.Properties;
using Chuong6_LTTQ_LMS.ThongKe;

namespace Chuong6_LTTQ_LMS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            frmSanPham frm = new frmSanPham();
            frm.MdiParent = this;
            frm.Show();
            frm.Dock = DockStyle.Fill;

        }

        private void mnuChatLieu_Click(object sender, EventArgs e)
        {
            frmChatLieu frm = new frmChatLieu();
            frm.MdiParent = this;
            frm.Show();
            frm.Dock = DockStyle.Fill;

        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien frm = new frmNhanVien();
            frm.MdiParent = this;
            frm.Show();
            frm.Dock = DockStyle.Fill;
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.MdiParent = this;
            frm.Show();
            frm.Dock = DockStyle.Fill;
        }

        private void mnuHoaDon_Click(object sender, EventArgs e)
        {
            frmHDBan frm = new frmHDBan();
            frm.MdiParent = this;
            frm.Show();
            frm.Dock = DockStyle.Fill;
        }

        private void mnuTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void mnuBaoCao_Click(object sender, EventArgs e)
        {
            frmDoanhThu frm = new frmDoanhThu();
            frm.MdiParent = this;
            frm.Show();
            frm.Dock = DockStyle.Fill;
        }
    }
}
