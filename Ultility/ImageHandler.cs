using System;
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
        public int numberOfBitNeedToReplace { get; set; } = 0;
        public int LSBBit
        {
            get;
            private set;
        }
        public ImageHandler(string path)
        {
            this.image = new Bitmap(path);
            this.LSBBit = image.Width * image.Height * 3;
            countLastPixelBit();
        }
        public void makeImage(Bitmap image, string name, string extension, bool flag)
        {
            if (flag)
            {

            }
            else
            {

            }
            switch (extension)
            {
                //case "jpeg"
            }
            //image.Save("NewImage.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        private void countLastPixelBit()
        {
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
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
    }
}
