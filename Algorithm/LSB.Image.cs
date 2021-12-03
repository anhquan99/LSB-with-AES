using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Utility;

namespace Algorithm
{
    public partial class LSB
    {
        public static Bitmap watermarkImage(MemoryStream stream, byte[] byteMessage)
        {
            MessageHandler messageHandler = new MessageHandler(byteMessage);
            ImageHandler imageHandler = new ImageHandler(stream, messageHandler.toltalBits);

            if (messageHandler.toltalBits + 1 > imageHandler.LSBBit) throw new Exception("Image too small to watermark!!!");
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
            return imageHandler.makeImageLSB(messageHandler.strByteMessage, flag);

        }
        public static byte[] extractImage(MemoryStream stream)
        {
            Bitmap image = new Bitmap(stream);
            char flag = '0';

            List<string> result = new List<string>();
            int j = 0;
            int i = 0;
            string byteRed;
            string byteGreen;
            string byteBlue;

            // get flag 
            Color pixel = image.GetPixel(j, i);
            byteRed = Convert.ToString(pixel.R, 2);
            flag = byteRed[byteRed.Length - 1];

            //get length of length of message
            char[] messageLength_legthChar = new char[8];
            byteGreen = Convert.ToString(pixel.G, 2);
            messageLength_legthChar[0] = getCharWithFlag(flag, byteGreen[byteGreen.Length - 1]);
            byteBlue = Convert.ToString(pixel.B, 2);
            messageLength_legthChar[1] = getCharWithFlag(flag, byteBlue[byteBlue.Length - 1]);
            j++;
            for(int p = 2; p < 8; p+=3)
            {
                pixel = image.GetPixel(j, i);
                byteRed = Convert.ToString(pixel.R, 2);
                messageLength_legthChar[p] =  getCharWithFlag(flag, byteRed[byteRed.Length - 1]);
                byteGreen = Convert.ToString(pixel.G, 2);
                messageLength_legthChar[p + 1] = getCharWithFlag(flag, byteGreen[byteGreen.Length - 1]);
                byteBlue = Convert.ToString(pixel.B, 2);
                messageLength_legthChar[p + 2] = getCharWithFlag(flag, byteBlue[byteBlue.Length - 1]);
                j++;
                if(j == image.Width)
                {
                    j = 0;
                    i++;
                }
            }
            // get length of message
            int messageLength_length = Convert.ToInt32(new string(messageLength_legthChar), 2);
            int messageLengthIndex = 0;
            char[] messageLengthByte = new char[messageLength_length];
            int k = 0;
            while (messageLengthIndex < messageLength_length)
            {
                for (; i < image.Height; i++)
                {
                    for (; j < image.Width; j++)
                    {
                        pixel = image.GetPixel(j, i);
                        if (k == 3) k = 0;
                        for ( ; k < 3; k++)
                        {
                            if (messageLengthIndex == messageLength_length) break;
                            switch (k)
                            {
                                // red
                                case 0:
                                    byteRed = Convert.ToString(pixel.R, 2);
                                    messageLengthByte[messageLengthIndex] = getCharWithFlag(flag, byteRed[byteRed.Length - 1]);
                                    break;
                                //green
                                case 1:
                                    byteGreen = Convert.ToString(pixel.G, 2);
                                    messageLengthByte[messageLengthIndex] = getCharWithFlag(flag, byteGreen[byteRed.Length - 1]);

                                    break;
                                //blue
                                case 2:
                                    byteBlue = Convert.ToString(pixel.B, 2);
                                    messageLengthByte[messageLengthIndex] = getCharWithFlag(flag, byteBlue[byteBlue.Length - 1]);
                                    break;
                            }
                            messageLengthIndex++;
                        }
                        if (messageLengthIndex == messageLength_length) break;
                    }
                    if (messageLengthIndex == messageLength_length) break;
                }
            }

            // get message
            int messageLength = Convert.ToInt32(new string(messageLengthByte), 2);
            char[] messageChar = new char[8];
            int messageIndex = 0;
            List<byte> messageByte = new List<byte>();
            int messageCharIndex = 0;
            if(k == 3)
            {
                k = 0;
                j++;
            }
            for (; i < image.Height; i++)
            {
                if (j == image.Width) j = 0;
                for (; j < image.Width; j++)
                {
                    pixel = image.GetPixel(j, i);
                    if (k == 3) k = 0;
                    for (; k < 3; k++)
                    {
                        if (messageCharIndex == 8)
                        {
                            byte temp = Convert.ToByte(new string(messageChar), 2);
                            messageByte.Add(temp);
                            messageCharIndex = 0;
                            messageIndex++;
                        }
                        switch (k)
                        {
                            // red
                            case 0:
                                byteRed = Convert.ToString(pixel.R, 2);
                                messageChar[messageCharIndex] = getCharWithFlag(flag, byteRed[byteRed.Length - 1]);
                                break;
                            //green
                            case 1:
                                byteGreen = Convert.ToString(pixel.G, 2);
                                messageChar[messageCharIndex] = getCharWithFlag(flag, byteGreen[byteGreen.Length - 1]);

                                break;
                            //blue
                            case 2:
                                byteBlue = Convert.ToString(pixel.B, 2);
                                messageChar[messageCharIndex] = getCharWithFlag(flag, byteBlue[byteBlue.Length - 1]);
                                break;
                        }
                        messageCharIndex++;
                        if (messageIndex == messageLength) break;
                    }
                    if (messageIndex == messageLength) break;
                }
                if (messageIndex == messageLength) break;
            }
            return messageByte.ToArray();
        }
        private static char getCharWithFlag(char flag, char input)
        {
            if(flag == '1')
            {
                if (input == '1') return '0';
                return '1';
            }
            return input;
        }
    }
}
