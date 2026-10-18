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
    public partial class frmKhachHang : Form
    {
        Classes.DataProcesser dtBase = new Classes.DataProcesser();
        Classes.Function ft = new Classes.Function();
        
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnThoat.Enabled = true;
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThem.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
            btnThoat.Enabled = true;

            txtMaKhach.Text = dgvKhachHang.CurrentRow.Cells[0].Value.ToString();
            txtTenKhach.Text = dgvKhachHang.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells[2].Value.ToString();
            txtDienThoai.Text = dgvKhachHang.CurrentRow.Cells[3].Value.ToString();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            //DataTable dtKhachHang = dtBase.DocBang("select * from tblKhach");
            dgvKhachHang.DataSource = dtBase.DocBang("select * from tblKhach");//dtKhachHang;

            dgvKhachHang.Columns[0].HeaderText = "Ma khach";
            dgvKhachHang.Columns[1].HeaderText = "Ten khach";
            dgvKhachHang.Columns[2].HeaderText = "Dia chi";
            dgvKhachHang.Columns[3].HeaderText = "Dien thoai";

            dgvKhachHang.Columns[0].Width = 150;
            dgvKhachHang.Columns[1].Width = 200;
            dgvKhachHang.Columns[2].Width = 150;
            dgvKhachHang.Columns[3].Width = 150;

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThoat.Enabled = true;

        }

        private void ResetValue()
        {
            txtMaKhach.Text = "";
            txtTenKhach.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnThoat.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban co muon xoa k",
                "Xoa khac hang",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string strDelete = "delete tblKhach where MaKhach = '" + txtMaKhach.Text + "'";
                dtBase.CapNhatDuLieu(strDelete);
                dgvKhachHang.DataSource = dtBase.DocBang("select * from tblKhach");
            }
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThoat.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaKhach.Text.Trim() == "")
            {
                MessageBox.Show("Hay nhap ma khach");
                txtMaKhach.Focus();
                return;
            }
            if (txtTenKhach.Text.Trim() == "")
            {
                MessageBox.Show("Hay nhap ten khach");
                txtTenKhach.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Hay nhap dia chi");
                txtDiaChi.Focus();
                return;
            }
            if (txtDienThoai.Text.Trim() == "")
            {
                MessageBox.Show("Hay nhap dien thoai");
                txtDienThoai.Focus();
                return;
            }
            if (btnThem.Enabled == true)
            {
                DataTable dtKhachHang = dtBase.DocBang("select * from tblKhach where Makhach = '" + txtMaKhach.Text + "'");
                if (dtKhachHang.Rows.Count > 0)
                {
                    MessageBox.Show("Hay nhap ma khach hang khac");
                    txtMaKhach.Focus();
                    return;
                }
                string strInsert = "insert into tblKhach values ('" + txtMaKhach.Text + "', N'" +
                    txtTenKhach.Text + "', N'" + txtDiaChi.Text + "','" + txtDienThoai.Text + "')";
                dtBase.CapNhatDuLieu(strInsert);
            }
            if (btnSua.Enabled == true)
            {
                string strUpdate = "update tblKhach set Tenkhach = N'" + txtTenKhach.Text + "', Diachi = N'" +
                    txtDiaChi.Text + "', Dienthoai = '" + txtDienThoai.Text + "' where Makhach = '" + txtMaKhach.Text + "'";
                dtBase.CapNhatDuLieu(strUpdate);
            }
            dgvKhachHang.DataSource = dtBase.DocBang("select * from tblKhach");
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThoat.Enabled = true;
            ResetValue();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
