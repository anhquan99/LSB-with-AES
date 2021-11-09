using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class AES
    {
        static int Nb = 4;
        int Nr = 0, Nk = 0;
        byte[] inAES; byte[] outAES; byte[,] state;
        byte[] RoundKey;
        byte[] Key;
        int getSBoxValue(int num)
        {
            int[] sbox = new int[256]{
                0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f,
                0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab,
                0x76, //0
                0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47,
                0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72,
                0xc0, //1
                0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7,
                0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31,
                0x15, //2
                0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05,
                0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2,
                0x75, //3
                0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a,
                0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f,
                0x84, //4
                0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1,
                0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58,
                0xcf, //5
                0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33,
                0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f,
                0xa8, //6
                0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38,
                0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3,
                0xd2, //7
                0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44,
                0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19,
                0x73, //8
                0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90,
                0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b,
                0xdb, //9
                0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24,
                0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4,
                0x79, //A
                0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e,
                0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae,
                0x08, //B
                0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4,
                0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b,
                0x8a, //C
                0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6,
                0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d,
                0x9e, //D
                0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e,
                0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28,
                0xdf, //E
                0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42,
                0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb,
                0x16 }; //F
            return sbox[num];
        }
        int[] Rcon = new int[255]{
            0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20,
            0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d,
            0x9a,
            0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35,
            0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91,
            0x39,0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f,
            0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d,
            0x3a,
            0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04,
            0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c,
            0xd8,
            0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63,
            0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa,
            0xef,
            0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd,
            0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66,
            0xcc,
            0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d,
            0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80,
            0x1b,
            0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f,
            0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4,
            0xb3,
            0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72,
            0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a,
            0x94,
            0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74,
            0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10,
            0x20,
            0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab,
            0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97,
            0x35,
            0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5,
            0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2,
            0x9f,
            0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83,
            0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02,
            0x04,
            0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36,
            0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc,
            0x63,
            0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d,
            0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3,
            0xbd,
            0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33,
            0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb
            };
        int getSBoxInvert(int num)
        {
            int[] rsbox = new int[256]{
            0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 
            0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb,
    
            0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87,
            0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb,
    
            0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d,
            0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e,

            0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2,
            0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25,

            0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16,
            0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92,

            0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda,
            0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84,

            0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a,
            0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06,

            0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02,
            0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b,

            0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea,
            0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73,

            0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85,
            0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e,

            0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89,
            0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b,

            0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20,
            0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4,

            0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31,
            0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f,

            0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d,
            0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef,

            0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0,
            0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61,

            0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26,
            0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c,0x7d
            };
            return rsbox[num];
        }

        void KeyExpansion()
        {// qua trinh sinh khoa gom 4 buoc
         // rotword : quay trai 8 buoc
         // subyte
         // r con=x(i-1) mod(x8+x4+x3+x+1)
         // shift row
            int i, j;
            byte[] temp = new byte[4];
            byte k;
            for (i = 0; i < Nk; i++)// moi khoa chua 4 byte
            {// moi vong co 4 tu .ap dung tu vong lap 1 toi vong lap Nr
                RoundKey[i * 4] = Key[i * 4];
                RoundKey[i * 4 + 1] = Key[i * 4 + 1];
                RoundKey[i * 4 + 2] = Key[i * 4 + 2];
                RoundKey[i * 4 + 3] = Key[i * 4 + 3];
            }
            // nb * nr + 1 = 20 voi 128 bit
            // i = 4
            while (i < (Nb * (Nr + 1)))
            {
                for (j = 0; j < 4; j++)
                {
                    temp[j] = RoundKey[(i - 1) * 4 + j];
                }
                if (i % Nk == 0)
                {
                    {
                        k = temp[0];
                        temp[0] = temp[1];
                        temp[1] = temp[2];
                        temp[2] = temp[3];
                        temp[3] = k;
                    }
                    {
                        temp[0] = (byte) getSBoxValue(temp[0]);
                        temp[1] = (byte) getSBoxValue(temp[1]);
                        temp[2] = (byte) getSBoxValue(temp[2]);
                        temp[3] = (byte) getSBoxValue(temp[3]);
                    }
                    temp[0] = (byte) (temp[0] ^ Rcon[i / Nk]);
                }
                else if (Nk > 6 && i % Nk == 4)
                {
                    {
                        temp[0] = (byte) getSBoxValue(temp[0]);
                        temp[1] = (byte) getSBoxValue(temp[1]);
                        temp[2] = (byte) getSBoxValue(temp[2]);
                        temp[3] = (byte) getSBoxValue(temp[3]);
                    }
                }
                RoundKey[i * 4 + 0] = (byte) (RoundKey[(i - Nk) * 4 + 0] ^ temp[0]);
                RoundKey[i * 4 + 1] = (byte) (RoundKey[(i - Nk) * 4 + 1] ^ temp[1]);
                RoundKey[i * 4 + 2] = (byte) (RoundKey[(i - Nk) * 4 + 2] ^ temp[2]);
                RoundKey[i * 4 + 3] = (byte) (RoundKey[(i - Nk) * 4 + 3] ^ temp[3]);
                i++;
            }
        }
        void AddRoundKey(int round)
        {// khoi dong vong lap .
            int i, j;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    state[j,i] ^= RoundKey[round * Nb * 4 + i * Nb + j];
                }
            }
        }// moi cot cua tt dau tien ll ke hop voi 1 khoa con theo thu tu tu dau cua day
        void SubBytes()
        {// su dung sbox thay the o tren .moi phesp the moi byte trong tt thay bang byte trong bang tra(Rijndeal)
            int i, j;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    state[i,j] = (byte) getSBoxValue(state[i,j]);
                }
            }
        }
        void InvSubBytes()
        {
            int i, j;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    state[i,j] = (byte) getSBoxInvert(state[i,j]);
                }
            }
        }
        void ShiftRows()
        {// cac byte 3 hang cuoi cung dc dich vong di voi so byte khac nhau
            byte temp;
            temp =  state[1,0];//vd shift(1,4)=1;shift(2,4)=2
            state[1,0] = state[1,1];// hang 1 giu nguyen
            state[1,1] = state[1,2];
            state[1,2] = state[1,3];
            state[1,3] = temp;
            temp = state[2,0];// hang 2 dich trai 1 vong
            state[2,0] = state[2,2];
            state[2,2] = temp;
            temp = state[2,1];
            state[2,1] = state[2,3];
            state[2,3] = temp;
            temp = state[3,0];
            state[3,0] = state[3,3];// hang 3 dich trai 2 lan
            state[3,3] = state[3,2];
            state[3,2] = state[3,1];
            state[3,1] = temp;
        }
        // dịch dòng
        void InvShiftRows()
        {
            byte temp;
            temp = state[1,3];
            state[1,3] = state[1,2];
            state[1,2] = state[1,1];
            state[1,1] = state[1,0];
            state[1,0] = temp;
            temp = state[2,0];
            state[2,0] = state[2,2];
            state[2,2] = temp;
            temp = state[2,1];
            state[2,1] = state[2,3];
            state[2,3] = temp;
            temp = state[3,0];
            state[3,0] = state[3,1];
            state[3,1] = state[3,2];
            state[3,2] = state[3,3];
            state[3,3] = temp;
        }
        int xtime(int x) {
            return  ((x << 1) ^ (((x >> 7) & 1) * 0x1b));
        }
        int Multiply(int x, int y) {
            return (((y & 1) * x) ^ ((y >> 1 & 1) * xtime(x)) ^ ((y >> 2 & 1) * xtime(xtime(x))) ^ ((y >> 3 & 1) * xtime(xtime(xtime(x)))) ^ ((y >> 4 & 1) * xtime(xtime(xtime(xtime(x))))));
        }
        void MixColumns()
        {// truong GF(2^8)//tron lan bien doi tuen tinh
            int i;
            int Tmp, Tm, t;
            for (i = 0; i < 4; i++)
            {
                t = state[0,i];
                Tmp =  (state[0,i] ^ state[1,i] ^ state[2,i] ^ state[3,i]);
                Tm =  (state[0,i] ^ state[1,i]); 
                Tm =  xtime(Tm); 
                state[0,i] ^= (byte) (Tm ^ Tmp);
                Tm =  (state[1,i] ^ state[2,i]); 
                Tm = xtime(Tm); 
                state[1,i] ^= (byte) (Tm ^ Tmp);
                Tm =  (state[2,i] ^ state[3,i]); 
                Tm =  xtime(Tm); 
                state[2,i] ^= (byte) (Tm ^ Tmp);
                Tm =  (state[3,i] ^ t); 
                Tm = xtime(Tm);
                state[3,i] ^= (byte) (Tm ^ Tmp);
            }
        }
        // trộn cột
        void InvMixColumns()
        {
            int i;
            byte a, b, c, d;
            for (i = 0; i < 4; i++)
            {
                a = state[0,i];
                b = state[1,i];
                c = state[2,i];
                d = state[3,i];
                state[0,i] = (byte) (Multiply(a, 0x0e) ^
                  Multiply(b, 0x0b) ^ Multiply(c, 0x0d) ^
                  Multiply(d, 0x09));
                state[1,i] = (byte) (Multiply(a, 0x09) ^
                  Multiply(b, 0x0e) ^ Multiply(c, 0x0b) ^
                  Multiply(d, 0x0d));
                state[2,i] = (byte) (Multiply(a, 0x0d) ^
                  Multiply(b, 0x09) ^ Multiply(c, 0x0e) ^
                  Multiply(d, 0x0b));
                state[3,i] = (byte) (Multiply(a, 0x0b) ^
                  Multiply(b, 0x0d) ^ Multiply(c, 0x09) ^
                  Multiply(d, 0x0e));
            }
        }
        void Cipher()
        {
            int i, j, round = 0;
            // gan ma tran message thanh ma tran message 4 x 4 
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    state[j,i] = inAES[i *4 + j];
                }
            }
            // buoc add round key dau tien
            AddRoundKey(0);
            for (round = 1; round < Nr; round++)
            {
                SubBytes();
                ShiftRows();
                MixColumns();
                AddRoundKey(round);// khoa con ket hop voi khoi
            }
            // vong lap cuoi
            SubBytes();
            ShiftRows();
            AddRoundKey(Nr);
            // gan ma tran message 4 x 4 da ma hoa thanh ma tran message 1 chieu
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
			        outAES[i *4 + j] = state[j,i];
                }
            }
        }
        void InvCipher()
        {
            int i, j, round = 0;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    state[j,i] = inAES [i *4 + j];
                }
            }
            AddRoundKey(Nr);
            for (round = Nr - 1; round > 0; round--)
            {
                InvShiftRows();
                InvSubBytes();
                AddRoundKey(round);
                InvMixColumns();
            }
            InvShiftRows();
            InvSubBytes();
            AddRoundKey(0);
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    outAES[i *4 + j] = state[j,i];
                }
            }
        }
        private byte[] fillShortString(int length, byte[] input)
        {
            List<byte> result = input.ToList();
            for( int i = 0; i < length - input.Length; i++)
            {
                result.Add(32);
            }
            return result.ToArray();
        }
        public byte[] encrypt(string message, string key, int keySize)
        {
            if (keySize != 128 && keySize != 192 && keySize != 256) throw new InvalidKeySizeException();
            int i;
            Nr = keySize;
            Nk = Nr / 32;
            Nr = Nk + 6;
            int index = 0;
            byte[] strKey = Encoding.UTF8.GetBytes(key);
            strKey = fillShortString(Nk * 4, strKey);
            byte[] messageByte = Encoding.UTF8.GetBytes(message);
            if (message.Length % 16 != 0)
            {
                messageByte = fillShortString(((messageByte.Length / 16 + 1) * 16), messageByte);
            }
            List<byte> encrypted = new List<byte>();
            for (i = 0; i < Nk * 4; i++)
            {
                Key[i] = strKey[i];
            }
            KeyExpansion();
            while (index < messageByte.Length - 1)
            {

                for (i = 0; i < 16; i++)
                {
                    inAES[i] = messageByte[index];
                    index++;
                }
                Cipher();
                foreach (var k in outAES)
                {
                    encrypted.Add(k);
                }

            }
            return encrypted.ToArray();
        }
        public string decrypt(byte[] cipherText, string key, int keySize)
        {
            if (keySize != 128 && keySize != 192 && keySize != 256) throw new InvalidKeySizeException();
            int i;
            Nr = keySize;
            Nk = Nr / 32;
            Nr = Nk + 6;
            int index = 0;
            byte[] strKey = Encoding.UTF8.GetBytes(key);
            strKey = fillShortString(Nk * 4, strKey);
            List<byte> decrypted = new List<byte>();
            for (i = 0; i < Nk * 4; i++)
            {
                Key[i] = strKey[i];
            }
            KeyExpansion();
            while (index < cipherText.Length)
            {

                for (i = 0; i < 16; i++)
                {
                    inAES[i] = cipherText[index];
                    index++;
                }

                InvCipher();
                foreach (var k in outAES)
                {
                    decrypted.Add(k);
                }

            }
            return Encoding.UTF8.GetString(decrypted.ToArray());
        }
        public AES()
        {
            inAES = new byte[16];
            outAES = new byte[16];
            state = new byte[4, 4];
            RoundKey = new byte[240];
            Key = new byte[32];
        }
        public void run()
        {
            int i;
            Nr = 256;
            Nk = Nr / 32;
            Nr = Nk + 6;
            int index = 0;
            byte[] strKey = Encoding.UTF8.GetBytes("Passwo");
            strKey = fillShortString(Nk * 4, strKey);
            byte[] temp = strKey;
            byte[] message = Encoding.UTF8.GetBytes("VIQR (viết tắt của tiếng Anh Vietnamese Quoted-Readable), còn gọi là Vietnet là một quy ước để viết chữ tiếng Việt dùng bảng mã UTF8 7 bit. Vì tính tiện lợi của nó, quy ước này được sử dụng phổ biến trên Internet, nhất là khi bảng mã UTF8 chưa được áp dụng rộng rãi.  ");
            byte[] temp2;
            if (message.Length % 16 != 0)
            {
                temp2 = fillShortString(((message.Length / 16 + 1) * 16), message);
            }
            else
            {
                temp2 = message;
            }
            List<byte> encryted = new List<byte>();
            List<byte> decryted = new List<byte>();
            for (i = 0; i < Nk * 4; i++)
            {
                Key[i] = temp[i];
            }
            KeyExpansion();
            while (index < temp2.Length)
            {
                for (i = 0; i < 16; i++)
                {
                    inAES[i] = temp2[index];
                    index++;
                }
                Cipher();
                foreach(var k in outAES)
                {
                    encryted.Add(k);
                }

            }
            Console.WriteLine(Encoding.UTF8.GetString(encryted.ToArray()));
            index = 0;
            while (index < message.Length)
            {
                for (i = 0; i < 16; i++)
                {
                    inAES[i] = encryted[index];
                    index++;
                }

                InvCipher();
                foreach (var k in outAES)
                {
                    decryted.Add(k);
                }
            }
            Console.WriteLine(Encoding.UTF8.GetString(decryted.ToArray()));
        }
    }
}
