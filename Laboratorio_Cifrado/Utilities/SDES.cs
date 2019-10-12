using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Laboratorio_Cifrado.Utilities
{
    public class SDES
    {
        #region Definitions

        private string K1;
        private string K2;

        private int[] P10 = {2, 4, 1, 6, 3, 9, 0, 8, 7, 5};
        private int[] P8 = {5, 2, 6, 3, 7, 4, 9, 8};
        private int[] P4 = {1, 3, 2, 0};
        private int[] EP = {3, 0, 1, 2, 1, 2, 3, 0};
        private int[] PI = {1, 5, 2, 0, 3, 7, 4, 6};
        private int[] PIn;

        #region SBOXES

        private static readonly string[,] S0 = new string[4, 4]
        {
            {"01", "00", "11", "10"},
            {"11", "10", "01", "00"},
            {"00", "10", "01", "11"},
            {"11", "01", "11", "10"}
        };

        private static readonly string[,] S1 = new string[4, 4]
        {
            {"00", "01", "10", "11"},
            {"10", "00", "01", "11"},
            {"11", "00", "01", "00"},
            {"10", "01", "00", "11"}
        };

        public SDES(int[] pIn)
        {
            PIn = pIn;
        }

        #endregion

        #endregion

        public SDES()
        {
            PIn = this.getInversePermutation();
        }

        private int[] getInversePermutation()
        {
            int[] PI = this.PI;
            int[] PIn = new int[8];

            for (int i = 0; i < 8; i++)
            {
                PIn[PI[i]] = i;
            }

            return PIn;
        }

        private string LeftShift(string value, int n)
        {
            string output = value + value;
            return output.Substring(n, value.Length);
        }

        private string Permutate(string value, int[] function)
        {
            string output = "";
            for (int i = 0; i < function.Length; i++)
            {
                output += value[function[i]];
            }
            return output;
        }

        private string XOR(string input, string key)
        {
            string output = "";
            for (int i = 0; i < key.Length; i++)
            {
                if (input[i] == key[i])
                {
                    output += "0";
                }
                else
                {
                    output += "1";
                }
            }
            return output;
        }

        private string SBOXES(string input)
        {
            string block1 = input.Substring(0, 4);
            string block2 = input.Substring(4, 4);

            return getSBoxValue(block1, SDES.S0) + getSBoxValue(block2, SDES.S1);
        }

        private string getSBoxValue(string input, string[,] sbox)
        {
            int row = Convert.ToInt32((string.Concat(input[0] + input[3])), 2);
            int column = Convert.ToInt32((string.Concat(input[1] + input[2])), 2);
            return sbox[row, column];
        }

        private string Combine(string value1, string value2)
        {
            return value1 + value2;
        }

        private byte ConvertStringToByte(string value)
        {
            return Convert.ToByte(Convert.ToInt32(value, 2));
        }

        private string ConvertByteToString(byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0'); // produce cadena "00111111";
        }

        private void WriteToFile(string path, byte[] value)
        {
            FileStream fs = new FileStream(path, FileMode.Append);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(value);
            bw.Close();
        }

        private string ConvertKeyToString(int key)
        {
            return Convert.ToString(key, 2).PadLeft(10, '0');
        }

        private void GenerateKeys(int input)
        {
            string key = ConvertKeyToString(input);

            string _P10 = Permutate(key, P10); //Permutacion 10
            string LS1 = LeftShift(_P10, 1); //Left Shift 1

            this.K1 = Permutate(LS1, P8); //Permutacion 8 -> Primer llave

            string LS2 = LeftShift(LS1, 2); //Left Shift 2

            this.K2 = Permutate(LS2, P8); //Permutacion 8 -> Segunda llave
        }


    }
}