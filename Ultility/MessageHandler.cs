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
            this.byteMessage = Encoding.UTF8.GetBytes(this.message);
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
        public byte[] getBytes()
        {
            // ASCII should not be used cause 128 bit not enough for all symbol
            // UTF16 take more work to do
            return byteMessage;
        }
        private void calculateBits()
        {
            int result = 0;
            foreach (var i in getBytes())
            {
                result += converToBits(i).Length;
                foreach (var k in converToBits(i))
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
        public string convertByteToString()
        {
            return Encoding.UTF8.GetString(this.byteMessage);
        }
        private string converToBits(byte data)
        {
            return Convert.ToString(data, 2);
        }
    }
}
