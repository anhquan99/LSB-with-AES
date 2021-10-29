using Algorithm;
using System;
using System.Drawing;
using System.Text;
using Utility;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine(LSB.watermark("E:\\Work\\PTIT\\ATPM\\FinalATPM\\test\\apple.jpg", "Đỗ Anh Quân"));
            Console.WriteLine(LSB.extract("E:\\Work\\PTIT\\ATPM\\FinalATPM\\test\\result-image.png"));
            //Bitmap temp = new Bitmap("E:\\Work\\PTIT\\ATPM\\FinalATPM\\test\\result-image.png");
            //Color tempC = temp.GetPixel(0, 0);
            //byte eof = 26;

            //byte[] eof2 = new byte[] { Convert.ToByte("11111111", 2) };
            //foreach (var i in eof2)
            //{
            //    Console.WriteLine(Convert.ToString(i, 2));
            //    Console.WriteLine(i);
            //}
            //Console.WriteLine(Encoding.UTF8.GetString(eof2));
            //string strEof = Convert.ToString(eof2, 2);
            //Console.WriteLine(eof2);
            //Console.WriteLine(strEof);

        }
    }
}
