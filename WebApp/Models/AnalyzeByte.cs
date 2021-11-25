using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class AnalyzeByte
    {
        public List<int> original { get; set; }
        public List<int> watermarked { get; set; }
        public List<int> label { get; set; }
        public AnalyzeByte(List<int> original, List<int> watermarked)
        {
            this.original = new List<int>();
            this.watermarked = new List<int>();
            this.label = new List<int>();

            int dataSize = 0;
            int dataLeap = 0;

            if(original.Count < watermarked.Count)
            {
                dataSize = original.Count;
                dataLeap = original.Count / 100;
            }
            else
            {
                dataSize = watermarked.Count;
                dataLeap = watermarked.Count / 100;
            }

            int index = 0;
            int avgOrigin = 0;
            int avgWater = 0;
            for (int i = 0; i < dataSize; i++)
            {
                if ( (index  == dataLeap) || ( i + 1 ) == dataSize)
                {
                    avgOrigin = avgOrigin / index;
                    avgWater = avgWater / index;

                    this.original.Add(avgOrigin);
                    this.watermarked.Add(avgWater);

                    avgOrigin = 0;
                    avgWater = 0;
                    index = 0;
                }
                avgOrigin += original[i];
                avgWater += watermarked[i];
                index++;
            }
            for (int i = 0; i < this.original.Count; i++)
            {
                label.Add(i + 1);
            }
        }
    }
}
