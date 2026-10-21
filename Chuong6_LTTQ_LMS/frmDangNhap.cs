using Chuong6_LTTQ_LMS.DanhMuc;
using Chuong6_LTTQ_LMS.HoaDon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chuong6_LTTQ_LMS
{
    public partial class frmDangNhap : Form
    {
        Classes.DataProcesser dtBase = new Classes.DataProcesser();
        Classes.Function ft = new Classes.Function();
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {


        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtMaNhanVien.Text.Trim() == "")
            {
                MessageBox.Show("Hay nhap ma nhan vien");
                txtMaNhanVien.Focus();
                return;
            }
            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Hay nhap mat khau");
                txtPassword.Focus();
                return;
            }
            DataTable dtNhanVien = dtBase.DocBang("select * from tblNhanvien1 where Manhanvien = '" + txtMaNhanVien.Text.Trim() + "' and Password = '" + txtPassword.Text.Trim() + "'");
            if (dtNhanVien.Rows.Count > 0)
            {
                frmChatLieu.userName = dtNhanVien.Rows[0]["Tennhanvien"].ToString();
                frmHDBan.MaNhanVien = dtNhanVien.Rows[0]["Manhanvien"].ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nhap sai ma nhan vien hoac mat khau");
                txtMaNhanVien.Focus();
                return;
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
