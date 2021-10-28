using System;
using System.Text;
using System.Collections;
using System.Drawing;

namespace Ultility
{
    public class ImageHandler
    {
        public void readImage(string path)
        {
            Bitmap img = new Bitmap(path);
            //int[][] result = new int[img.Width][img.Height];
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    Color temp = img.GetPixel(j, i);
                    Console.WriteLine(temp.R);
                    Console.WriteLine(Convert.ToString(temp.R, 2));
                }
            }
        }
        public void readString(string message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            foreach (var i in bytes)
            {
                Console.WriteLine(i);
            }
        }
    }
}
