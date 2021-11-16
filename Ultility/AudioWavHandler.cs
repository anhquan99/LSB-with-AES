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
        public List<byte> test
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
        public AudioWavHandler(string path)
        {
            test = new List<byte>();
            header = new List<byte>();
            data = new List<byte>();
            readWav(path);
        }
        private bool readWav(string filename)
        {
            try
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(fs);

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
                    foreach(var i in readerBytes)
                    {
                        header.Add(i);
                    }
                    readerBytes = BitConverter.GetBytes(fmtSize);
                    foreach(var i in readerBytes)
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
                        foreach(var i in readerBytes)
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
                    foreach(var i in readerBytes)
                    {
                        data.Add(i);
                    }
                    this.bytesPerSample = (uint) bitDepth / 8;
                    this.bytesAvailable = readerBytes.Length / this.bytesPerSample;
                    this.bitsAvailable = this.bytesAvailable * 2;
                    return true;
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return false;
        }
        //public bool readWav(string filename, out float[] L, out float[] R)
        //{
        //    L = R = null;

        //    try
        //    {
        //        using (FileStream fs = File.Open(filename, FileMode.Open))
        //        {
        //            BinaryReader reader = new BinaryReader(fs);


        //            // chunk 0
        //            Int32 chunkID = reader.ReadInt32();
        //            Int32 fileSize = reader.ReadInt32();
        //            Int32 riffType = reader.ReadInt32();

        //            byte[] temp;

        //            temp = BitConverter.GetBytes(chunkID);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }
        //            temp = BitConverter.GetBytes(fileSize);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }
        //            temp = BitConverter.GetBytes(riffType);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }

        //            // chunk 1
        //            Int32 fmtID = reader.ReadInt32();
        //            Int32 fmtSize = reader.ReadInt32(); // bytes for this chunk (expect 16 or 18)

        //            temp = BitConverter.GetBytes(fmtID);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }
        //            temp = BitConverter.GetBytes(fmtSize);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }

        //            // 16 bytes coming...
        //            Int16 fmtCode = reader.ReadInt16();
        //            Int16 channels = reader.ReadInt16();
        //            Int32 sampleRate = reader.ReadInt32();
        //            Int32 byteRate = reader.ReadInt32();
        //            Int16 fmtBlockAlign = reader.ReadInt16();
        //            Int16 bitDepth = reader.ReadInt16();

        //            temp = BitConverter.GetBytes(fmtCode);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }
        //            temp = BitConverter.GetBytes(channels);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }
        //            temp = BitConverter.GetBytes(sampleRate);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }
        //            temp = BitConverter.GetBytes(byteRate);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }
        //            temp = BitConverter.GetBytes(fmtBlockAlign);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }
        //            temp = BitConverter.GetBytes(bitDepth);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }


        //            if (fmtSize == 18)
        //            {
        //                // Read any extra values
        //                int fmtExtraSize = reader.ReadInt16();

        //                temp = BitConverter.GetBytes(fmtExtraSize);
        //                foreach (var i in temp)
        //                {
        //                    test.Add(i);
        //                }
        //                temp = reader.ReadBytes(fmtExtraSize);
        //                foreach (var i in temp)
        //                {
        //                    test.Add(i);
        //                }
        //            }

        //            // chunk 2
        //            Int32 dataID = reader.ReadInt32();
        //            Int32 bytes = reader.ReadInt32();

        //            temp = BitConverter.GetBytes(dataID);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }
        //            temp = BitConverter.GetBytes(bytes);
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }

        //            // DATA!
        //            byte[] byteArray = reader.ReadBytes(bytes);

        //            temp = byteArray;
        //            foreach (var i in temp)
        //            {
        //                test.Add(i);
        //            }

        //            int bytesForSamp = bitDepth / 8;
        //            int nValues = bytes / bytesForSamp;


        //            float[] asFloat = null;
        //            switch (bitDepth)
        //            {
        //                case 64:
        //                    double[]
        //                        asDouble = new double[nValues];
        //                    Buffer.BlockCopy(byteArray, 0, asDouble, 0, bytes);
        //                    asFloat = Array.ConvertAll(asDouble, e => (float)e);
        //                    break;
        //                case 32:
        //                    asFloat = new float[nValues];
        //                    Buffer.BlockCopy(byteArray, 0, asFloat, 0, bytes);
        //                    break;
        //                case 16:
        //                    Int16[]
        //                        asInt16 = new Int16[nValues];
        //                    Buffer.BlockCopy(byteArray, 0, asInt16, 0, bytes);
        //                    asFloat = Array.ConvertAll(asInt16, e => e / (float)(Int16.MaxValue + 1));
        //                    break;
        //                default:
        //                    return false;
        //            }

        //            switch (channels)
        //            {
        //                case 1:
        //                    L = asFloat;
        //                    R = null;
        //                    return true;
        //                case 2:
        //                    // de-interleave
        //                    int nSamps = nValues / 2;
        //                    L = new float[nSamps];
        //                    R = new float[nSamps];
        //                    for (int s = 0, v = 0; s < nSamps; s++)
        //                    {
        //                        L[s] = asFloat[v++];
        //                        R[s] = asFloat[v++];
        //                    }
        //                    return true;
        //                default:
        //                    return false;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("...Failed to load: " + filename);
        //        return false;
        //    }

        //    return false;
        //}
    }
}
