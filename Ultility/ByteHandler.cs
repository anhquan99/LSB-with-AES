using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ByteHandler
    {

        public byte[] byteMessage
        {
            get;
            private set;
        }
        public ByteHandler(byte[] byteMessage)
        {
            this.byteMessage = byteMessage;
            this.byteMessage = addEOFByte();
        }
        private string converBytesToBit(byte data)
        {
            return Convert.ToString(data, 2);
        }
        private byte[] addEOFByte()
        {
            List<Byte> temp = this.byteMessage.ToList();
            byte eof = 26;
            temp.Add(eof);
            return temp.ToArray();
        }
        public string[] parseBytesToString()
        {
            string[] result = new string[byteMessage.Length];
            for (int i = 0; i < this.byteMessage.Length; i++)
            {
                string temp = converBytesToBit(byteMessage[i]);
                if (temp.Length < 8)
                {
                    string newstr = new string('0', 3);
                    temp = new string('0', 8 - temp.Length) + temp;
                }
                result[i] = temp;
            }
            //foreach(var i in result)
            //{
            //    Console.WriteLine(i);
            //}
            return result;
        }
    }
}
