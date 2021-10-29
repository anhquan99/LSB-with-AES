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
        /// <summary>
        /// when parse string to bits if prefix is 0 it will not assign to bits
        /// idea to get full 8 bits of each char in message
        /// </summary>
        public string[] strByteMessage
        {
            get;
            private set;
        }

        public MessageHandler(string message)
        {
            this.message = message;
            // ASCII should not be used cause 128 bit not enough for all symbol
            // UTF16 take more work to do
            // UTF8 with max byte is 255
            ByteHandler byteHandler = new ByteHandler(Encoding.UTF8.GetBytes(this.message));
            this.strByteMessage = byteHandler.parseBytesToString();
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
            foreach (var i in strByteMessage)
            {
                foreach (var k in i)
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



        public void reverseStringBits()
        {
            for (int i = 0; i < strByteMessage.Length; i++)
            {
                string temp = "";
                foreach (var k in strByteMessage[i])
                {
                    if (k == '1')
                    {
                        temp += "0";
                    }
                    else temp += "1";
                }
                strByteMessage[i] = temp;
            }
        }


    }
}
