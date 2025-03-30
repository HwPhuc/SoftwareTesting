using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareTesting
{
    //internal class HinhChuNhat
    //{
    //}

    public class HinhChuNhat
    {
        public Diem TrenTrai { get; set; }
        public Diem DuoiPhai { get; set; }

        public HinhChuNhat(Diem trenTrai, Diem duoiPhai)
        {
            TrenTrai = trenTrai;
            DuoiPhai = duoiPhai;
        }

        //public int TinhDienTich()
        //{
        //    int width = DuoiPhai.X - TrenTrai.X;
        //    int height = TrenTrai.Y - DuoiPhai.Y;
        //    return width * height;
        //}

        public int TinhDienTich()
        {
            int width = Math.Abs(DuoiPhai.X - TrenTrai.X);  // Chiều rộng luôn dương
            int height = Math.Abs(TrenTrai.Y - DuoiPhai.Y); // Chiều cao luôn dương
            return width * height;
        }


        public bool CoGiaoNhau(HinhChuNhat hcnKhac)
        {
            return !(hcnKhac.DuoiPhai.X < TrenTrai.X ||  // HCN 2 bên trái HCN 1
                     hcnKhac.TrenTrai.X > DuoiPhai.X ||  // HCN 2 bên phải HCN 1
                     hcnKhac.DuoiPhai.Y > TrenTrai.Y ||  // HCN 2 ở trên HCN 1
                     hcnKhac.TrenTrai.Y < DuoiPhai.Y);   // HCN 2 ở dưới HCN 1
        }

    }
}
