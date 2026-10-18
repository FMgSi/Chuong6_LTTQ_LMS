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
    public partial class frmNhanVien : Form
    {
        Classes.DataProcesser dtBase = new Classes.DataProcesser();
        Classes.Function ft = new Classes.Function();
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            dgvNhanVien.DataSource = dtBase.DocBang("select * from tblNhanvien");

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThoat.Enabled = true;

            dgvNhanVien.Columns[0].HeaderText = "Ma nhan vien";
            dgvNhanVien.Columns[1].HeaderText = "Ten nhan vien";
            dgvNhanVien.Columns[2].HeaderText = "Gioi tinh";
            dgvNhanVien.Columns[3].HeaderText = "Dia chi";
            dgvNhanVien.Columns[4].HeaderText = "Dien thoai";
            dgvNhanVien.Columns[5].HeaderText = "Ngay sinh";
        }

        private void ResetValue()
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            rbNam.Checked = true;
            mtbBirthday.Text = "";
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNhanVien.Text = dgvNhanVien.CurrentRow.Cells[0].Value.ToString();
            txtTenNhanVien.Text = dgvNhanVien.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells[3].Value.ToString();
            txtDienThoai.Text = dgvNhanVien.CurrentRow.Cells[4].Value.ToString();
            if (dgvNhanVien.CurrentRow.Cells[2].Value.ToString() == "Nam")
            {
                rbNam.Checked = true;
            } else
            {
                rbNu.Checked = true;
            }
            DateTime dt = Convert.ToDateTime(dgvNhanVien.CurrentRow.Cells[5].Value);
            mtbBirthday.Text = dt.ToString("dd/MM/yyyy");

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnThem.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNhanVien.Text.Trim() == "")
            {
                MessageBox.Show("Hay nhap ma nhan vien");
                txtMaNhanVien.Focus();
                return;
            }
            if (txtTenNhanVien.Text.Trim() == "")
            {
                MessageBox.Show("Hay nhap ten nhan vien");
                txtTenNhanVien.Focus();
                return;
            }
            if (txtDienThoai.Text.Trim() == "")
            {
                MessageBox.Show("Hay nhap dien thoai");
                txtDienThoai.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Hay nhap dia chi");
                txtDiaChi.Focus();
                return;
            }
            if (! mtbBirthday.MaskFull)
            {
                MessageBox.Show("Hay nhap Ngay sinh");
                mtbBirthday.Focus();
                return;
            }
            if (btnThem.Enabled == true)
            {
                DataTable res = dtBase.DocBang("select * from tblNhanVien where MaNhanVien = '" + txtMaNhanVien.Text + "'");
                if (res.Rows.Count > 0)
                {
                    MessageBox.Show("Nhap ma nhan vien khac");
                    txtMaNhanVien.Focus();
                    return;
                }
                string GioiTinh;
                if (rbNam.Checked == true)
                {
                    GioiTinh = "Nam";
                } else
                {
                    GioiTinh = "Nữ";
                }
                string[] birth = mtbBirthday.Text.Split('/');
                string NgaySinh = birth[2] + "-" + birth[1] + "-" + birth[0];
                string strInsert = "insert into tblNhanvien values ('" + txtMaNhanVien.Text + "', N'"
                    + txtTenNhanVien.Text + "', N'" + GioiTinh + "', N'" + txtDiaChi.Text + "', '"
                    + txtDienThoai.Text + "', '" + NgaySinh + "')";
                dtBase.CapNhatDuLieu(strInsert);                
            }
            if (btnSua.Enabled == true)
            {
                string GioiTinh;
                if (rbNam.Checked == true)
                {
                    GioiTinh = "Nam";
                }
                else
                {
                    GioiTinh = "Nữ";
                }
                string[] birth = mtbBirthday.Text.Split('/');
                string NgaySinh = birth[2] + "-" + birth[1] + "-" + birth[0];
                string strUpdate = "update tblNhanVien set MaNhanVien = '" + txtMaNhanVien.Text
                    + "', TenNhanVien=N'" + txtTenNhanVien.Text + "', Gioitinh = N'" + GioiTinh + "', DiaChi = N'"
                    + txtDiaChi.Text + "', DienThoai = '" + txtDienThoai.Text + "', NgaySinh='" + NgaySinh + "'";
                dtBase.CapNhatDuLieu(strUpdate);
            }

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThoat.Enabled = true;

            dgvNhanVien.DataSource = dtBase.DocBang("Select * from tblNhanVien");

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ban co chac muon xoa k", "Xoa nhan vien", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dtBase.CapNhatDuLieu("delete tblNhanVien where MaNhanVien = '" + txtMaNhanVien.Text + "'");
                dgvNhanVien.DataSource = dtBase.DocBang("select * from tblNhanVien");
            }
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
