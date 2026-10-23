using System;

namespace QuanLyDaThuc
{
    public class DaThuc
    {
        private int bac;
        private double[] heSo; // Mạng chứa các hệ số từ a_0 đến a_n

        // 1. Hàm khởi tạo đa thức với 1 tham số là bậc n, các hệ số đều bằng 0
        public DaThuc(int n)
        {
            bac = n;
            heSo = new double[bac + 1];
            for (int i = 0; i <= bac; i++)
            {
                heSo[i] = 0;
            }
        }

        // Property cho biết bậc n của đa thức
        public int Bac
        {
            get { return bac; }
        }

        // 2. Indexer để truy cập và gán giá trị cho hệ số a_k (với k là chỉ số index)
        public double this[int k]
        {
            get
            {
                if (k >= 0 && k <= bac)
                    return heSo[k];
                throw new IndexOutOfRangeException("Chi so k vuot qua bac cua da thuc!");
            }
            set
            {
                if (k >= 0 && k <= bac)
                    heSo[k] = value;
                else
                    throw new IndexOutOfRangeException("Chi so k vuot qua bac cua da thuc!");
            }
        }

        // 3. Phép toán cộng hai đa thức
        public static DaThuc operator +(DaThuc dt1, DaThuc dt2)
        {
            int maxBac = Math.Max(dt1.Bac, dt2.Bac);
            DaThuc ketQua = new DaThuc(maxBac);

            for (int i = 0; i <= maxBac; i++)
            {
                double hs1 = (i <= dt1.Bac) ? dt1[i] : 0;
                double hs2 = (i <= dt2.Bac) ? dt2[i] : 0;
                ketQua[i] = hs1 + hs2;
            }

            return ketQua;
        }

        // 4. Phép toán trừ hai đa thức
        public static DaThuc operator -(DaThuc dt1, DaThuc dt2)
        {
            int maxBac = Math.Max(dt1.Bac, dt2.Bac);
            DaThuc ketQua = new DaThuc(maxBac);

            for (int i = 0; i <= maxBac; i++)
            {
                double hs1 = (i <= dt1.Bac) ? dt1[i] : 0;
                double hs2 = (i <= dt2.Bac) ? dt2[i] : 0;
                ketQua[i] = hs1 - hs2;
            }

            return ketQua;
        }

        // 5. Phương thức với 1 tham số truyền vào là b dùng để tính giá trị của đa thức khi x = b
        public double TinhGiaTri(double b)
        {
            double kq = 0;
            for (int i = 0; i <= bac; i++)
            {
                kq += heSo[i] * Math.Pow(b, i);
            }
            return kq;
        }

        // Phương thức ToString để hiển thị đa thức trực quan
        public override string ToString()
        {
            string s = "";
            for (int i = bac; i >= 0; i--)
            {
                if (heSo[i] != 0)
                {
                    if (s != "" && heSo[i] > 0) s += " + ";
                    else if (heSo[i] < 0) s += " - ";

                    double giaTriDoi = Math.Abs(heSo[i]);
                    if (i == 0) s += giaTriDoi;
                    else if (i == 1) s += giaTriDoi + "x";
                    else s += giaTriDoi + "x^" + i;
                }
            }
            return s == "" ? "0" : s;
        }
    }

    // --- CHƯƠNG TRÌNH CHÍNH KIỂM THỬ ---
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== KIEM THU BAI TAP 3: DA THUC ===\n");

            // Tạo đa thức P(x) = 2 + 3x + 4x^2 (bậc 2)
            DaThuc dt1 = new DaThuc(2);
            dt1[0] = 2;
            dt1[1] = 3;
            dt1[2] = 4;

            // Tạo đa thức Q(x) = 1 + x + 2x^2 (bậc 2)
            DaThuc dt2 = new DaThuc(2);
            dt2[0] = 1;
            dt2[1] = 1;
            dt2[2] = 2;

            Console.WriteLine("Da thuc 1: P(x) = " + dt1.ToString());
            Console.WriteLine("Da thuc 2: Q(x) = " + dt2.ToString());

            // Kiểm tra phép cộng đa thức
            DaThuc tong = dt1 + dt2;
            Console.WriteLine("Tong P(x) + Q(x) = " + tong.ToString());

            // Kiểm tra phép trừ đa thức
            DaThuc hieu = dt1 - dt2;
            Console.WriteLine("Hieu P(x) - Q(x) = " + hieu.ToString());

            // Kiểm tra tính giá trị đa thức tại x = b (ví dụ b = 2)
            double b = 2;
            Console.WriteLine("Gia tri cua P(x) tai x = " + b + " la: " + dt1.TinhGiaTri(b));

            Console.ReadKey();
        }
    }
}