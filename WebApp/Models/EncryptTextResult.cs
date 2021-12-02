using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class EncryptTextResult
    {
        public string resultString { get; set; }
        public int[] resultByte { get; set; }
        public EncryptTextResult(string resultString, byte[] resultByte)
        {
            this.resultString = resultString;
            List<int> temp = new List<int>();
            foreach(var i in resultByte)
            {
                temp.Add(i);
            }
            this.resultByte = temp.ToArray();
        }
    }
}
