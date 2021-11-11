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
        }
        private string converBytesToBit(byte data)
        {
            return Convert.ToString(data, 2);
        }
        public string[] parseBytesToString()
        {
            List<string> result = new List<string>();
            string byteMessage_length_length = Convert.ToString(this.byteMessage.Length, 2); 
            result.Add(new string('0', 8 - Convert.ToString(byteMessage_length_length.Length, 2).Length)
                + Convert.ToString(byteMessage_length_length.Length, 2));
            result.Add(Convert.ToString(this.byteMessage.Length, 2));
            for (int i = 0; i < this.byteMessage.Length; i++)
            {
                string temp = converBytesToBit(byteMessage[i]);
                if (temp.Length < 8)
                {
                    temp = new string('0', 8 - temp.Length) + temp;
                }
                result.Add(temp);
            }
            return result.ToArray();
        }
    }
}
