using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNE03
{
    public static class MathLib
    {
        public static int? MaxItem(int[] arr)
        {
            if (arr == null || arr.Length == 0)
                return null;

            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }
            return max;
        }

        public static bool IsPowOf(int num, int baseNum)
        {
            if (baseNum <= 1) return num == baseNum;
            while (num > 1)
            {
                if (num % baseNum != 0)
                    return false;
                num /= baseNum;
            }
            return num == 1;
        }

        public static string DecToBin(uint decNum)
        {
            if (decNum == 0) return "0";

            string bin = "";
            while (decNum > 0)
            {
                bin = (decNum % 2) + bin;
                decNum /= 2;
            }
            return bin;
        }
    }
}
