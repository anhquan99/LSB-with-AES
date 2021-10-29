using System;
using System.Drawing;
using System.Text;
using Utility;

namespace Algorithm
{
    public class LSB
    {
        public static string watermark(string path, string message)
        {
            MessageHandler messageHandler = new MessageHandler(message);
            ImageHandler imageHandler = new ImageHandler(path, messageHandler.toltalBits);
            if (messageHandler.toltalBits > imageHandler.LSBBit) return "Image too small to watermark!!!";
            bool flag = true;
            if ((messageHandler.zeroBit > messageHandler.oneBit && imageHandler.lastZeroBit > imageHandler.lastOneBit)
                || (messageHandler.oneBit > messageHandler.zeroBit && imageHandler.lastOneBit > imageHandler.lastZeroBit))
            {
                flag = false;
            }
            else
            {
                messageHandler.reverseStringBits();
                Console.WriteLine("Reverse");
            }
            imageHandler.makeImage(messageHandler.byteMessage, "jpeg", flag);
            return "Success";

        }
        //public static string extract(string path)
        //{

        //}
    }
}
