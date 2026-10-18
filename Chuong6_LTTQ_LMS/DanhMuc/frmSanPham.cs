using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chuong6_LTTQ_LMS.DanhMuc
{
    public partial class frmSanPham : Form
    {
        Classes.DataProcesser dtBase = new Classes.DataProcesser();
        Classes.Function ft = new Classes.Function();
        string fileAnh = "";
        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            //Lay du lieu cua bang chat lieu do vao cboChatLieu
            DataTable dtChatLieu = dtBase.DocBang("select * from tblChatLieu");
            ft.FillComBox(cboChatLieu, dtChatLieu, "TenChatLieu", "MaChatLieu");
            cboChatLieu.SelectedIndex = -1;
            
            //Load du leiu len dgvHang
            DataTable dtHang = dtBase.DocBang("select * from tblhang");
            dgvHang.DataSource = dtHang;

            //Dinh dang dataGrid
            dgvHang.Columns[0].HeaderText = "Ma hang";
            dgvHang.Columns[1].HeaderText = "Ten hang";
            dgvHang.Columns[2].HeaderText = "Ma CL";
            dgvHang.Columns[3].HeaderText = "So luong";
            dgvHang.Columns[4].HeaderText = "Gia nhap";
            dgvHang.Columns[5].HeaderText = "Gia ban";
            dgvHang.Columns[6].HeaderText = "File anh";

            dgvHang.Columns[0].Width = 150;
            dgvHang.Columns[1].Width = 250;
            dgvHang.Columns[2].Width = 150;
            dgvHang.Columns[3].Width = 150;
            dgvHang.Columns[4].Width = 150;
            dgvHang.Columns[5].Width = 150;
            dgvHang.Columns[6].Width = 150;

            dgvHang.BackgroundColor = Color.LightBlue;
            dtHang.Dispose();

            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThemMoi.Enabled = true;
            ResetValue();
        }

        void ResetValue()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cboChatLieu.Text = "";
            txtSoLuong.Text = "";
            txtDonGiaBan.Text = "";
            txtDonGiaNhap.Text = "";
            pictureBox1.Image = null;
            txtGhiChu.Text = "";
            txtMaHang.Focus();
            pictureBox1.Image = null;
            fileAnh = "";
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgAnh = new OpenFileDialog();
            dlgAnh.Filter = "All files(*.*)|*.*";
            dlgAnh.FilterIndex = 1;
            dlgAnh.Title = "Chon anh de hien thi";
            if (dlgAnh.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Lay ten, duong dan anh khi nguoi dung chon trong hop hoi thoai
                //Sau do gan ch thuoc tinh Image cua pictureBox
                pictureBox1.Image = Image.FromFile(dlgAnh.FileName);
                string[] str = dlgAnh.FileName.Split('\\');
                fileAnh = str[str.Length - 1].ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaHang.Text.Trim() == "")
            {
                MessageBox.Show("Ban phai nhap ma hang");
                txtMaHang.Focus();
                return;
            }
            if (txtTenHang.Text.Trim() == "")
            {
                MessageBox.Show("Ban phai nhap ten hang");
                txtTenHang.Focus();
                return;
            }
            if (cboChatLieu.Text.Trim() == "")
            {
                MessageBox.Show("Ban phai nhap chat lieu");
                cboChatLieu.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim() == "")
            {
                MessageBox.Show("Ban phai nhap so luong");
                txtSoLuong.Focus();
                return;
            }
            if (txtDonGiaBan.Text.Trim() == "")
            {
                MessageBox.Show("Ban phai nhap don gia ban");
                txtDonGiaBan.Focus();
                return;
            }
            if (txtDonGiaNhap.Text.Trim() == "")
            {
                MessageBox.Show("Ban phai nhap don gia nhap");
                txtDonGiaNhap.Focus();
                return;
            }
            if (btnThemMoi.Enabled == true)
            {
                DataTable dtSp = dtBase.DocBang("select * from tblHang where MaHang='" + txtMaHang.Text + "'");
                if (dtSp.Rows.Count > 0)
                {
                    MessageBox.Show("Ma san pham da ton tai. Moi ban nhap ma khac");
                    txtMaHang.Focus();
                    return;
                }
                string strInsert = "insert into tblHang values('" + txtMaHang.Text + "',N'" +
                    txtTenHang.Text + "','" + cboChatLieu.SelectedValue + "','" + int.Parse(txtSoLuong.Text) +
                    "','" + float.Parse(txtDonGiaNhap.Text) + "','" + float.Parse(txtDonGiaBan.Text) +
                    "','" + fileAnh + "',N'" + txtGhiChu.Text + "')";
                dtBase.CapNhatDuLieu(strInsert);
            }
            if (btnSua.Enabled == true)
            {
                string sqlUpdate =
                    "update tblHang set TenHang=N'" + txtTenHang.Text +
                    "', MaChatlieu='" + cboChatLieu.SelectedValue +
                    "', SoLuong=" + int.Parse(txtSoLuong.Text) +
                    ", DonGiaNhap=" + float.Parse(txtDonGiaNhap.Text) +
                    ", DonGiaBan=" + float.Parse(txtDonGiaBan.Text) +
                    ", Anh='" + fileAnh + "', GhiChu=N'" +
                    txtGhiChu.Text + "' where MaHang='" + txtMaHang.Text + "'";
                dtBase.CapNhatDuLieu(sqlUpdate);

            }
            DataTable dtHang = dtBase.DocBang("select * from tblHang");
            dgvHang.DataSource = dtHang;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThemMoi.Enabled = true;
            ResetValue();
        
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThemMoi.Enabled = false;
        }

        private void dgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
            btnThemMoi.Enabled = false;
            btnLuu.Enabled = false;

            txtMaHang.Text = dgvHang.CurrentRow.Cells[0].Value.ToString();
            txtTenHang.Text = dgvHang.CurrentRow.Cells[1].Value.ToString();
            cboChatLieu.SelectedValue = dgvHang.CurrentRow.Cells[2].Value.ToString();
            txtSoLuong.Text = dgvHang.CurrentRow.Cells[3].Value.ToString();
            txtDonGiaNhap.Text = dgvHang.CurrentRow.Cells[4].Value.ToString();
            txtDonGiaBan.Text = dgvHang.CurrentRow.Cells[5].Value.ToString();
            fileAnh = dgvHang.CurrentRow.Cells[6].Value.ToString();
            txtGhiChu.Text = dgvHang.CurrentRow.Cells[7].Value.ToString();
            try
            {
                pictureBox1.Image = Image.FromFile("Images\\Hang\\" +
                    dgvHang.CurrentRow.Cells[6].Value.ToString());
            }
            catch
            {
                pictureBox1.Image = null;
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThemMoi.Enabled = true;
            ResetValue();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban co muon xoa k", "Xoa mat hang",
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question ) == DialogResult.Yes)
            {
                dtBase.CapNhatDuLieu("delete tblhang where Mahang='" + txtMaHang.Text + "'");
                dgvHang.DataSource = dtBase.DocBang("select * from tblHang");
            }
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnThemMoi.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            frmChatLieu frm = new frmChatLieu();
            frm.Show();
            frm.Dock = DockStyle.Fill;
        }
    }
}
