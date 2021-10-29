using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class MessageHandler
    {
        public string message
        {
            get;
            private set;
        }
        public byte[] byteMessage
        {
            get;
            private set;
        }
        public int zeroBit
        {
            get;
            private set;
        }
        public int oneBit
        {
            get;
            private set;
        }
        public int toltalBits
        {
            get;
            private set;
        }
        public MessageHandler(string message)
        {
            this.message = message;
            // ASCII should not be used cause 128 bit not enough for all symbol
            // UTF16 take more work to do
            this.byteMessage = Encoding.UTF8.GetBytes(this.message);
            this.byteMessage = addEOFByte();
            setDefault();
            calculateBits();
        }
        public MessageHandler(byte[] byteMessage)
        {
            this.byteMessage = byteMessage;
            setDefault();
            calculateBits();
        }
        private void setDefault()
        {
            this.oneBit = 0;
            this.zeroBit = 0;
            this.toltalBits = 0;
        }
        private void calculateBits()
        {
            int result = 0;
            foreach (var i in byteMessage)
            {
                result += converBytesToBits(i).Length;
                foreach (var k in converBytesToBits(i))
                {
                    if (k == '1')
                    {
                        this.oneBit++;
                    }
                    else this.zeroBit++;
                }
            }
            this.toltalBits = result;
        }
        private string converBytesToBits(byte data)
        {
            return Convert.ToString(data, 2);
        }
        public void reverseStringBits()
        {
            for (int i = 0; i < byteMessage.Length; i++)
            {
                string temp = "";
                foreach (var k in converBytesToBits(byteMessage[i]))
                {
                    if (k == '1')
                    {
                        temp += "0";
                    }
                    else temp += "1";
                }
                byteMessage[i] = Convert.ToByte(temp, 2);
            }
        }
        private byte[] addEOFByte()
        {
            List<Byte> temp = Encoding.UTF8.GetBytes(this.message).ToList();
            byte eof = 26;
            temp.Add(eof);
            return temp.ToArray();
        }
    }
}
