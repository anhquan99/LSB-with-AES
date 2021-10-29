﻿using System;
using System.Text;
using System.Collections;
using System.Drawing;

namespace Utility
{
    public class ImageHandler
    {
        public Bitmap image
        {
            get;
            private set;
        }
        public int lastZeroBit { get; private set; }
        public int lastOneBit { get; private set; }
        public int numberOfBitNeedToReplace { get; private set; } = 0;
        public int LSBBit
        {
            get;
            private set;
        }
        public ImageHandler(string path, int messageBits)
        {
            this.image = new Bitmap(path);
            this.LSBBit = image.Width * image.Height * 3;
            this.numberOfBitNeedToReplace = messageBits;
            if (numberOfBitNeedToReplace < LSBBit)
            {
                countLastPixelBit();
            }
        }
        public void makeImage(byte[] messageByte, string extension, bool flag)
        {
            Bitmap watermarkedImage = new Bitmap(this.image.Width, this.image.Height);
            int messageByteIndex = 0;
            int charByteIndex = 0;
            string currentStringByte = Convert.ToString(messageByte[messageByteIndex], 2);
            string strFlag = flag ? "0" : "1";
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color originPixel = this.image.GetPixel(j, i);
                    if (messageByteIndex < messageByte.Length - 1)
                    {
                        string[] subBit = new string[3];
                        for (int k = 0; k < 3; k++)
                        {
                            if (charByteIndex > currentStringByte.Length - 1)
                            {
                                charByteIndex = 0;
                                messageByteIndex++;
                                currentStringByte = Convert.ToString(messageByte[messageByteIndex], 2);
                            }
                            switch (k)
                            {
                                //red
                                case 0:
                                    string byteRed = Convert.ToString(originPixel.R, 2);
                                    // put flag
                                    if (i == 0 && j == 0)
                                    {
                                        subBit[0] = subLastByte(0, byteRed, strFlag);
                                    }
                                    else
                                    {
                                        subBit[0] = subLastByte(charByteIndex, byteRed, currentStringByte);
                                    }
                                    break;
                                //green
                                case 1:
                                    string byteGreen = Convert.ToString(originPixel.G, 2);
                                    subBit[1] = subLastByte(charByteIndex, byteGreen, currentStringByte);
                                    break;
                                //blue
                                case 2:
                                    string byteBlue = Convert.ToString(originPixel.B, 2);
                                    subBit[2] = subLastByte(charByteIndex, byteBlue, currentStringByte);
                                    break;
                            }
                            if (i != 0 && j != 0)
                            {
                                charByteIndex++;
                            }
                        }
                        Color tempPixel = Color.FromArgb(Convert.ToByte(subBit[0], 2), Convert.ToByte(subBit[1], 2), Convert.ToByte(subBit[2], 2));
                        watermarkedImage.SetPixel(j, i, tempPixel);
                    }
                    else
                    {
                        watermarkedImage.SetPixel(j, i, originPixel);
                    }
                }
            }
            switch (extension)
            {
                //case "jpeg"
            }
            image.Save("result-image.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        private void countLastPixelBit()
        {
            for (int i = 0; i < image.Height && i * image.Width * 3 <= numberOfBitNeedToReplace; i++)
            {
                for (int j = 0; j < image.Width && (i + 1) * j * 3 <= numberOfBitNeedToReplace; j++)
                {
                    Color temp = image.GetPixel(j, i);
                    string red = Convert.ToString(temp.R, 2);
                    if (red[red.Length - 1] == '1')
                    {
                        this.lastOneBit++;
                    }
                    else this.lastZeroBit++;
                    string green = Convert.ToString(temp.G, 2);
                    if (green[green.Length - 1] == '1')
                    {
                        this.lastOneBit++;
                    }
                    else this.lastZeroBit++;
                    string blue = Convert.ToString(temp.B, 2);
                    if (blue[blue.Length - 1] == '1')
                    {
                        this.lastOneBit++;
                    }
                    else this.lastZeroBit++;
                }
            }
        }
        public static int getGrayPixel(int red, int blue, int green)
        {
            return (red + blue + green) / 3;
        }
        private string subLastByte(int index, string imageBits, string messageBits)
        {
            char[] imageCharBits = imageBits.ToCharArray();
            imageCharBits[imageCharBits.Length - 1] = messageBits[index];
            return new string(imageCharBits);
        }
    }
}
