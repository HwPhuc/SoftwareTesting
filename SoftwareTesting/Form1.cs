using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareTesting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTinhDienTich59Phuc_Click(object sender, EventArgs e)
        {
            // Hình 1
            Diem trenTraiHinh1 = new Diem(int.Parse(txtX159Phuc.Text), int.Parse(txtY159Phuc.Text));
            Diem duoiPhaiHinh1 = new Diem(int.Parse(txtX259Phuc.Text), int.Parse(txtY259Phuc.Text));

            HinhChuNhat hcn1 = new HinhChuNhat(trenTraiHinh1, duoiPhaiHinh1);
            lblDienTichHinh159Phuc.Text = $"{ hcn1.TinhDienTich() }";

            // Hình 2
            Diem trenTraiHinh2 = new Diem(int.Parse(txtX359Phuc.Text), int.Parse(txtY359Phuc.Text));
            Diem duoiPhaiHinh2 = new Diem(int.Parse(txtX459Phuc.Text), int.Parse(txtY459Phuc.Text));

            HinhChuNhat hcn2 = new HinhChuNhat(trenTraiHinh2, duoiPhaiHinh2);
            lblDienTichHinh259Phuc.Text = $"{ hcn2.TinhDienTich() }";
        }

        private void btnKiemTraGiaoNhau59Phuc_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ ô nhập cho hình chữ nhật 1
            Diem trenTrai1 = new Diem(int.Parse(txtX159Phuc.Text), int.Parse(txtY159Phuc.Text));
            Diem duoiPhai1 = new Diem(int.Parse(txtX259Phuc.Text), int.Parse(txtY259Phuc.Text));

            // Lấy dữ liệu từ ô nhập cho hình chữ nhật 2
            Diem trenTrai2 = new Diem(int.Parse(txtX359Phuc.Text), int.Parse(txtY359Phuc.Text));
            Diem duoiPhai2 = new Diem(int.Parse(txtX159Phuc.Text), int.Parse(txtY159Phuc.Text));

            // Tạo 2 hình chữ nhật từ dữ liệu nhập vào
            HinhChuNhat hcn1 = new HinhChuNhat(trenTrai1, duoiPhai1);
            HinhChuNhat hcn2 = new HinhChuNhat(trenTrai2, duoiPhai2);

            // Kiểm tra giao nhau
            bool giaoNhau = hcn1.CoGiaoNhau(hcn2);

            // Hiển thị kết quả
            lblGiaoNhau59Phuc.Text = giaoNhau ? "Hai hình giao nhau" : "Hai hình không giao nhau";
        }
    }
}
