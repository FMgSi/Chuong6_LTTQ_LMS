using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chuong6_LTTQ_LMS.DanhMuc
{
    public partial class frmChatLieu : Form
    {

        public static string userName = "";
        
        Classes.Function ft = new Classes.Function();
        Classes.DataProcesser dtBase = new Classes.DataProcesser();
        public frmChatLieu()
        {
            InitializeComponent();
        }

        private void frmChatLieu_Load(object sender, EventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.ShowDialog();

            lbXinChao.Text = "Xin chao: " + userName;
            
            DataTable dtChatLieu = dtBase.DocBang("select * from tblChatLieu");
            dgvChatLieu.DataSource = dtChatLieu;

            dgvChatLieu.Columns[0].HeaderText = "Ma chat lieu";
            dgvChatLieu.Columns[1].HeaderText = "Ten chat lieu";

            dgvChatLieu.Columns[0].Width = 200;
            dgvChatLieu.Columns[1].Width = 200;
            
            dtChatLieu.Dispose();

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThoat.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetValue()
        {
            txtMaChatLieu.Text = "";
            txtTenChatLieu.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = true;
            ResetValue();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaChatLieu.Text.Trim() == "")
            {
                MessageBox.Show("Ban nhap ma chat lieu");
                txtMaChatLieu.Focus();
                return;
            }
            if (txtTenChatLieu.Text.Trim() == "")
            {
                MessageBox.Show("Ban nhap ten chat lieu");
                txtTenChatLieu.Focus();
                return;
            }
            if (btnThem.Enabled == true)
            {
                DataTable dtCl = dtBase.DocBang("select * from tblChatLieu where MaChatLieu='" + txtMaChatLieu.Text + "'");
                if (dtCl.Rows.Count > 0)
                {
                    MessageBox.Show("Ma hang da ton tai");
                    txtMaChatLieu.Focus();
                    return;
                }
                string strInsert = "insert into tblChatLieu values ('" +
                    txtMaChatLieu.Text + "',N'" + txtTenChatLieu.Text + "')";
                dtBase.CapNhatDuLieu(strInsert);
            }
            if (btnSua.Enabled == true)
            {
                string strUpdate = "update tblChatLieu set MaChatLieu='" +
                    txtMaChatLieu.Text + "', TenChatLieu=N'" +
                    txtTenChatLieu.Text + "' where MaChatLieu = '" + txtMaChatLieu.Text +"'";
                dtBase.CapNhatDuLieu(strUpdate);

            }
            DataTable dtHang = dtBase.DocBang("select * from tblChatLieu");
            dgvChatLieu.DataSource = dtHang;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            ResetValue();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnThoat.Enabled = true;
        }

        private void dgvChatLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            btnLuu.Enabled = false;

            txtMaChatLieu.Text = dgvChatLieu.CurrentRow.Cells[0].Value.ToString();
            txtTenChatLieu.Text = dgvChatLieu.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThoat.Enabled = true;
            ResetValue();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban co muon xoa k",
                "Xoa mat hang",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dtBase.CapNhatDuLieu("delete tblChatLieu where MaChatLieu='" + txtMaChatLieu.Text + "'");
                dgvChatLieu.DataSource = dtBase.DocBang("select * from tblChatLieu");
            }
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
        }

        private void lbXinChao_Click(object sender, EventArgs e)
        {

        }
    }
}
