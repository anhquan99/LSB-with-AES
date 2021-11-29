using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Algorithm
{
    public partial class LSB
    {
        public static byte[] watermarkAudio(MemoryStream stream, byte[] byteMessage)
        {
            MessageHandler messageHandler = new MessageHandler(byteMessage);
            AudioWavHandler audioHandler = new AudioWavHandler(stream);
            if (audioHandler.bitsAvailable + 1 < messageHandler.toltalBits) throw new Exception("Audio too small to watermark!!!");

            List<byte> data = new List<byte>();
            foreach (var i in audioHandler.header)
            {
                data.Add(i);
            }
            int indexMessage = 0;
            int indexMessageChar = 0;
            int audioIndex = 0;
            string currentMessageStr = messageHandler.strByteMessage[indexMessage];
            while (indexMessage < messageHandler.strByteMessage.Length)
            {
                byte currentByte = audioHandler.data[audioIndex];
                char[] currentByteChar = (new string('0', 8 - Convert.ToString(currentByte, 2).Length) + Convert.ToString(currentByte, 2)).ToCharArray();
                if (indexMessageChar + 1 >= currentMessageStr.Length)
                {
                    if(currentByteChar[currentByteChar.Length - 4] != currentMessageStr[indexMessageChar])
                    {
                        currentByteChar[currentByteChar.Length - 1] = getFlipBit(currentByteChar[currentByteChar.Length - 1]);
                        currentByteChar[currentByteChar.Length - 2] = getFlipBit(currentByteChar[currentByteChar.Length - 2]);
                        currentByteChar[currentByteChar.Length - 3] = getFlipBit(currentByteChar[currentByteChar.Length - 3]);
                    }
                    currentByteChar[currentByteChar.Length - 4] = currentMessageStr[indexMessageChar];
                    indexMessageChar++;
                }
                else
                {
                    if (currentByteChar[currentByteChar.Length - 4] != currentMessageStr[indexMessageChar]
                        || currentByteChar[currentByteChar.Length - 3] != currentMessageStr[indexMessageChar + 1])
                    {
                        currentByteChar[currentByteChar.Length - 1] = getFlipBit(currentByteChar[currentByteChar.Length - 1]);
                        currentByteChar[currentByteChar.Length - 2] = getFlipBit(currentByteChar[currentByteChar.Length - 2]);
                    }
                    currentByteChar[currentByteChar.Length - 4] = currentMessageStr[indexMessageChar];
                    indexMessageChar++;
                    currentByteChar[currentByteChar.Length - 3] = currentMessageStr[indexMessageChar];
                    indexMessageChar++;
                }
                audioIndex++;
                data.Add(Convert.ToByte(new string(currentByteChar), 2));
                if (indexMessageChar == messageHandler.strByteMessage[indexMessage].Length)
                {
                    indexMessageChar = 0;
                    indexMessage++;
                    if (indexMessage == messageHandler.strByteMessage.Length) break;
                    currentMessageStr = messageHandler.strByteMessage[indexMessage];
                }
            }
            for (int i = audioIndex; i < audioHandler.data.Count; i++)
            {
                data.Add(audioHandler.data[i]);
            }
            return data.ToArray();
        }
        public static byte[] extractAudio(MemoryStream stream)
        {
            AudioWavHandler audioHandler = new AudioWavHandler(stream);
            int audioIndex = 0;

            //get length of length of message
            char[] messageLength_legthChar = new char[8];
            for(int i = 0; i < 8; i++)
            {
                byte currentByte = audioHandler.data[audioIndex];
                char[] currentByteChar = (new string('0', 8 - Convert.ToString(currentByte, 2).Length) + Convert.ToString(currentByte, 2)).ToCharArray();
                messageLength_legthChar[i] = currentByteChar[currentByteChar.Length - 4];
                messageLength_legthChar[++i] = currentByteChar[currentByteChar.Length - 3];
                audioIndex++;
            }

            // get length of message
            int messageLength_length = Convert.ToInt32(new string(messageLength_legthChar), 2);
            int messageLengthIndex = 0;
            char[] messageLengthByte = new char[messageLength_length];
            while(messageLengthIndex < messageLength_length)
            {
                byte currentByte = audioHandler.data[audioIndex];
                char[] currentByteChar = (new string('0', 8 - Convert.ToString(currentByte, 2).Length) + Convert.ToString(currentByte, 2)).ToCharArray();
                messageLengthByte[messageLengthIndex] = currentByteChar[currentByteChar.Length - 4];
                messageLengthIndex++;
                if (messageLengthIndex == messageLength_length)
                {
                    audioIndex++;
                    break;
                }
                messageLengthByte[messageLengthIndex] = currentByteChar[currentByteChar.Length - 3];
                messageLengthIndex++;
                audioIndex++;
            }
            // get message
            int messageLength = Convert.ToInt32(new string(messageLengthByte), 2);
            char[] messageChar = new char[8];
            List<byte> messageByte = new List<byte>();
            int messageCharIndex = 0;
            while (messageByte.Count < messageLength)
            {
                byte currentByte = audioHandler.data[audioIndex];
                char[] currentByteChar = (new string('0', 8 - Convert.ToString(currentByte, 2).Length) + Convert.ToString(currentByte, 2)).ToCharArray();
                messageChar[messageCharIndex] = currentByteChar[currentByteChar.Length - 4];
                messageCharIndex++;
                messageChar[messageCharIndex] = currentByteChar[currentByteChar.Length - 3];
                messageCharIndex++;
                if (messageCharIndex == 8)
                {
                    byte temp = Convert.ToByte(new string(messageChar), 2);
                    messageByte.Add(temp);
                    messageCharIndex = 0;
                }
                audioIndex++;
            }
            return messageByte.ToArray();
        }
        private static char getFlipBit(char bit)
        {
            if (bit == '0') return '1';
            else return '0';
        }
    }
}
