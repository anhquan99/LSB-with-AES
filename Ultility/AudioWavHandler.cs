using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class AudioWavHandler
    {
        public uint bytesPerSample
        {
            get;
            private set;
        }
        public long bytesAvailable { get; private set; }
        public long bitsAvailable
        {
            get;
            private set;
        }
        public List<byte> header
        {
            get;
            private set;
        }
        public List<byte> data
        {
            get;
            set;
        }
        public AudioWavHandler(MemoryStream stream)
        {
            header = new List<byte>();
            data = new List<byte>();
            readWav(stream);
        }
        private bool readWav(MemoryStream memoryStream)
        {
            try
            {

                BinaryReader reader = new BinaryReader(memoryStream);
                reader.BaseStream.Position = 0;
                // chunk 0
                byte[] readerBytes = reader.ReadBytes(4 * 3);
                foreach (var i in readerBytes)
                {
                    header.Add(i);
                }

                // chunk 1
                Int32 fmtID = reader.ReadInt32();
                Int32 fmtSize = reader.ReadInt32(); // bytes for this chunk (expect 16 or 18)
                readerBytes = BitConverter.GetBytes(fmtID);
                foreach (var i in readerBytes)
                {
                    header.Add(i);
                }
                readerBytes = BitConverter.GetBytes(fmtSize);
                foreach (var i in readerBytes)
                {
                    header.Add(i);
                }

                readerBytes = reader.ReadBytes((2 * 3) + (4 * 2));
                foreach (var i in readerBytes)
                {
                    header.Add(i);
                }
                Int16 bitDepth = reader.ReadInt16();
                readerBytes = BitConverter.GetBytes(bitDepth);
                foreach (var i in readerBytes)
                {
                    header.Add(i);
                }
                if (fmtSize == 18)
                {
                    // Read any extra values
                    Int16 fmtExtraSize = reader.ReadInt16();
                    readerBytes = BitConverter.GetBytes(fmtExtraSize);
                    foreach (var i in readerBytes)
                    {
                        header.Add(i);
                    }
                    readerBytes = reader.ReadBytes(fmtExtraSize);
                    foreach (var i in readerBytes)
                    {
                        header.Add(i);
                    }
                }


                // chunk 2
                Int32 dataID = reader.ReadInt32();
                Int32 bytes = reader.ReadInt32();
                readerBytes = BitConverter.GetBytes(dataID);
                foreach (var i in readerBytes)
                {
                    header.Add(i);
                }
                readerBytes = BitConverter.GetBytes(bytes);
                foreach (var i in readerBytes)
                {
                    header.Add(i);
                }

                // DATA!
                readerBytes = reader.ReadBytes(bytes);
                foreach (var i in readerBytes)
                {
                    data.Add(i);
                }
                this.bytesPerSample = (uint)bitDepth / 8;
                this.bytesAvailable = readerBytes.Length / this.bytesPerSample;
                this.bitsAvailable = this.bytesAvailable * 2;
                return true;
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return false;
        }
    }
}
