using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            string flag = "1";
            if ((messageHandler.zeroBit > messageHandler.oneBit && imageHandler.lastZeroBit > imageHandler.lastOneBit)
                || (messageHandler.oneBit > messageHandler.zeroBit && imageHandler.lastOneBit > imageHandler.lastZeroBit))
            {
                flag = "0";
            }
            else
            {
                messageHandler.reverseStringBits();
                Console.WriteLine("Reverse");
            }
            imageHandler.makeImageLSB(messageHandler.strByteMessage, "jpeg", flag);
            return "Success";

        }
        public static string extract(string path)
        {
            ImageHandler imageHandler = new ImageHandler(path);
            Bitmap image = new Bitmap(path);
            char flag = '0';
            char[] checkEOF = Enumerable.Repeat('0', 8).ToArray();
            int eofIndex = 0;
            List<string> result = new List<string>();
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color pixel = image.GetPixel(j, i);
                    for (int k = 0; k < 3; k++)
                    {
                        if (eofIndex == 8)
                        {
                            byte temp = Convert.ToByte(new string(checkEOF), 2);
                            if (temp == 26) break;
                            result.Add(new string(checkEOF));
                            eofIndex = 0;
                        }
                        switch (k)
                        {
                            // red
                            case 0:
                                string byteRed = Convert.ToString(pixel.R, 2);
                                if (i == 0 && j == 0)
                                {
                                    flag = byteRed[byteRed.Length - 1];
                                }
                                else
                                {
                                    checkEOF[eofIndex] = byteRed[byteRed.Length - 1];
                                }
                                break;
                            //green
                            case 1:
                                string byteGreen = Convert.ToString(pixel.G, 2);
                                checkEOF[eofIndex] = byteGreen[byteGreen.Length - 1];

                                break;
                            //blue
                            case 2:
                                string byteBlue = Convert.ToString(pixel.B, 2);
                                checkEOF[eofIndex] = byteBlue[byteBlue.Length - 1];
                                break;
                        }
                        if (i != 0 || j != 0 || k != 0)
                        {
                            eofIndex++;
                        }
                    }
                }
                if (Convert.ToByte(new string(checkEOF), 2) == 26 || eofIndex == 8) break;
            }
            string[] resultArr = result.ToArray();
            if (flag == '1')
            {
                for (int i = 0; i < resultArr.Length; i++)
                {
                    string temp = "";
                    foreach (var k in resultArr[i])
                    {
                        if (k == '1')
                        {
                            temp += "0";
                        }
                        else temp += "1";
                    }
                    resultArr[i] = temp;
                }
            }
            for (int i = 0; i < resultArr.Length; i++)
            {
                byte[] tempByte = new byte[] { Convert.ToByte(resultArr[i], 2) };
                resultArr[i] = Encoding.UTF8.GetString(tempByte);
            }
            return String.Concat(resultArr);
        }
    }
}
