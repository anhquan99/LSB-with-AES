using Algorithm;
using System;
using Ultility;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageHandler lsb = new ImageHandler();
            lsb.readString(args[0]);
        }
    }
}
