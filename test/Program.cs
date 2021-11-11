﻿using Algorithm;
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


            Algorithm.AES aes = new AES();
            byte[] encrypted = aes.encrypt("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                , "password", 256);
            Console.WriteLine(LSB.watermark("E:\\Work\\PTIT\\ATPM\\FinalATPM\\test\\apple.jpg", encrypted));
            byte[] lsb = LSB.extract("E:\\Work\\PTIT\\ATPM\\FinalATPM\\test\\bin\\Debug\\net5.0\\result-image.png");
            aes = new AES();
            Console.WriteLine(aes.decrypt(lsb, "password", 256));
            //aes.run();
            //byte[] temp2 = Encoding.UTF8.GetBytes("Do Anh Quan sdjkwjqd wdqwpoidfjdspofjew;fljmef;lewkf;ldkfc;dsflk'jdlk'fj s l'kfjsdlfkjdsfl;kjfwel;kfjdslkfjdslk;fj'l;kdsakjf");
            //Console.WriteLine(temp2.Length);
        }
    }
}
