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
            ImageHandler handler = new ImageHandler(args[0]);
            handler.numberOfBitNeedToReplace = handler.image.Width * handler.image.Height;
            Console.WriteLine(handler.lastOneBit);
            Console.WriteLine(handler.lastZeroBit);
            Console.WriteLine(handler.lastOneBit);
            Console.WriteLine(handler.LSBBit);
            Console.WriteLine(handler.numberOfBitNeedToReplace);
            //Console.OutputEncoding = Encoding.UTF8;
            //MessageHandler stringHandler = new MessageHandler("QUân");
            //foreach (var i in stringHandler.getBytes())
            //{
            //    Console.WriteLine(Convert.ToString(i, 2));
            //}
            //Console.WriteLine(stringHandler.toltalBits);
            //Console.WriteLine(stringHandler.oneBit);
            //Console.WriteLine(stringHandler.zeroBit);
        }
    }
}
