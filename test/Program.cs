using Algorithm;
using System;
using System.Collections.Generic;
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

            ////Console.WriteLine(LSB.watermark("E:\\Work\\PTIT\\ATPM\\FinalATPM\\test\\apple.jpg", "Đỗ Anh Quân"));
            //Console.WriteLine(LSB.extract("E:\\Work\\PTIT\\ATPM\\FinalATPM\\test\\result-image.png"));
            Algorithm.AES aes = new AES();
            aes.run();
        }
    }
}
