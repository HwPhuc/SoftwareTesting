using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareTesting;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using Microsoft.Office.Interop.Excel;
using System.Security.Cryptography;
using ExcelDataReader;
using System.Data;
using System;
using ExcelDataReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SoftwareTesting.Tests
{
    [TestClass]
    public class HinhChuNhatTests
    {
        [TestMethod]
        public void TinhDienTich_HinhChuNhat1_Dung()
        {
            // Arrange
            Diem trenTrai = new Diem(2, 5);
            Diem duoiPhai = new Diem(6, 2);
            HinhChuNhat hcn = new HinhChuNhat(trenTrai, duoiPhai);

            // Act
            int dienTich = hcn.TinhDienTich();

            // Assert
            Assert.AreEqual(12, dienTich); // (6-2) * (5-2) = 12
        }

        [TestMethod]
        public void TinhDienTich_HinhChuNhat2_Dung()
        {
            // Arrange
            Diem trenTrai = new Diem(1, 4);
            Diem duoiPhai = new Diem(4, 1);
            HinhChuNhat hcn = new HinhChuNhat(trenTrai, duoiPhai);

            // Act
            int dienTich = hcn.TinhDienTich();

            // Assert
            Assert.AreEqual(9, dienTich); // (4-1) * (4-1) = 9
        }

        [TestMethod]
        public void KiemTraGiaoNhau_CoGiaoNhau()
        {
            // Arrange
            Diem trenTrai1 = new Diem(1, 5);
            Diem duoiPhai1 = new Diem(4, 2);
            HinhChuNhat hcn1 = new HinhChuNhat(trenTrai1, duoiPhai1);

            Diem trenTrai2 = new Diem(3, 6);
            Diem duoiPhai2 = new Diem(6, 3);
            HinhChuNhat hcn2 = new HinhChuNhat(trenTrai2, duoiPhai2);

            // Act
            bool giaoNhau = hcn1.CoGiaoNhau(hcn2);

            // Assert
            Assert.IsTrue(giaoNhau);
        }

        [TestMethod]
        public void KiemTraGiaoNhau_KhongGiaoNhau()
        {
            // Arrange
            Diem trenTrai1 = new Diem(1, 5);
            Diem duoiPhai1 = new Diem(4, 2);
            HinhChuNhat hcn1 = new HinhChuNhat(trenTrai1, duoiPhai1);

            Diem trenTrai2 = new Diem(5, 6);
            Diem duoiPhai2 = new Diem(8, 3);
            HinhChuNhat hcn2 = new HinhChuNhat(trenTrai2, duoiPhai2);

            // Act
            bool giaoNhau = hcn1.CoGiaoNhau(hcn2);

            // Assert
            Assert.IsFalse(giaoNhau);
        }

        // ---- Đọc dữ liệu từ file CSV để test tự động ----
        public static IEnumerable<object[]> ReadCsvData()
        {
            string filePath = @".\Data\TestData.csv";
            foreach (var line in File.ReadLines(filePath))
            {
                string[] values = line.Split(',');

                // Chuyển đổi dữ liệu từ chuỗi sang kiểu số
                int X1 = int.Parse(values[0]);
                int Y1 = int.Parse(values[1]);
                int X2 = int.Parse(values[2]);
                int Y2 = int.Parse(values[3]);
                int X3 = int.Parse(values[4]);
                int Y3 = int.Parse(values[5]);
                int X4 = int.Parse(values[6]);
                int Y4 = int.Parse(values[7]);

                int expectedArea1 = int.Parse(values[8]);
                int expectedArea2 = int.Parse(values[9]);
                bool expectedIntersection = bool.Parse(values[10]);

                yield return new object[] { X1, Y1, X2, Y2, X3, Y3, X4, Y4, expectedArea1, expectedArea2, expectedIntersection };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(ReadCsvData), DynamicDataSourceType.Method)]
        public void Test_HinhChuNhat_FromCSV(int X1, int Y1, int X2, int Y2, int X3, int Y3, int X4, int Y4, int expectedArea1, int expectedArea2, bool expectedIntersection)
        {
            // Tạo hình chữ nhật từ CSV
            Diem trenTrai1 = new Diem(X1, Y1);
            Diem duoiPhai1 = new Diem(X2, Y2);
            HinhChuNhat hcn1 = new HinhChuNhat(trenTrai1, duoiPhai1);

            Diem trenTrai2 = new Diem(X3, Y3);
            Diem duoiPhai2 = new Diem(X4, Y4);
            HinhChuNhat hcn2 = new HinhChuNhat(trenTrai2, duoiPhai2);

            // Kiểm tra diện tích
            Assert.AreEqual(expectedArea1, hcn1.TinhDienTich(), $"Diện tích hình 1 sai: {X1},{Y1},{X2},{Y2}");
            Assert.AreEqual(expectedArea2, hcn2.TinhDienTich(), $"Diện tích hình 2 sai: {X3},{Y3},{X4},{Y4}");

            // Kiểm tra giao nhau
            bool giaoNhau = hcn1.CoGiaoNhau(hcn2);
            Assert.AreEqual(expectedIntersection, giaoNhau, $"Giao nhau sai cho hình 1: {X1},{Y1},{X2},{Y2} và hình 2: {X3},{Y3},{X4},{Y4}");
        }

        public static IEnumerable<object[]> ReadExcelData()
        {
            string filePath = @".\Data\TestDataExcel.xlsx";

            // Mở file Excel để đọc dữ liệu
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    System.Data.DataTable table = result.Tables[0]; // Đọc sheet đầu tiên

                    foreach (DataRow row in table.Rows)
                    {
                        // Chuyển đổi dữ liệu từ Excel
                        int X1 = Convert.ToInt32(row[0]);
                        int Y1 = Convert.ToInt32(row[1]);
                        int X2 = Convert.ToInt32(row[2]);
                        int Y2 = Convert.ToInt32(row[3]);
                        int X3 = Convert.ToInt32(row[4]);
                        int Y3 = Convert.ToInt32(row[5]);
                        int X4 = Convert.ToInt32(row[6]);
                        int Y4 = Convert.ToInt32(row[7]);

                        int expectedArea1 = Convert.ToInt32(row[8]);
                        int expectedArea2 = Convert.ToInt32(row[9]);
                        bool expectedIntersection = Convert.ToBoolean(row[10]);

                        yield return new object[] { X1, Y1, X2, Y2, X3, Y3, X4, Y4, expectedArea1, expectedArea2, expectedIntersection };
                    }
                }
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(ReadExcelData), DynamicDataSourceType.Method)]
        public void Test_HinhChuNhat_FromExcel(int X1, int Y1, int X2, int Y2, int X3, int Y3, int X4, int Y4, int expectedArea1, int expectedArea2, bool expectedIntersection)
        {
            // Tạo hình chữ nhật từ dữ liệu trong Excel
            Diem trenTrai1 = new Diem(X1, Y1);
            Diem duoiPhai1 = new Diem(X2, Y2);
            HinhChuNhat hcn1 = new HinhChuNhat(trenTrai1, duoiPhai1);

            Diem trenTrai2 = new Diem(X3, Y3);
            Diem duoiPhai2 = new Diem(X4, Y4);
            HinhChuNhat hcn2 = new HinhChuNhat(trenTrai2, duoiPhai2);

            // Kiểm tra diện tích
            Assert.AreEqual(expectedArea1, hcn1.TinhDienTich(), $"Diện tích hình 1 sai: {X1},{Y1},{X2},{Y2}");
            Assert.AreEqual(expectedArea2, hcn2.TinhDienTich(), $"Diện tích hình 2 sai: {X3},{Y3},{X4},{Y4}");

            // Kiểm tra giao nhau
            bool giaoNhau = hcn1.CoGiaoNhau(hcn2);
            Assert.AreEqual(expectedIntersection, giaoNhau, $"Giao nhau sai cho hình 1: {X1},{Y1},{X2},{Y2} và hình 2: {X3},{Y3},{X4},{Y4}");
        }
    }
}
