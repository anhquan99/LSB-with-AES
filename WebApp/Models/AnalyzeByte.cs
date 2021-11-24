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
        public AnalyzeByte(List<int> original, List<int> watermarked)
        {
            this.original = original;
            this.watermarked = watermarked;
        }
    }
}
