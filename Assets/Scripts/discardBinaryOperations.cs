using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace binaryDiscard
{
    public static class discardBinaryOperations
    {
        public static void PopulateDictionary(Dictionary<int, string> discardBinary)
        {
            for (int i = 0; i < 31; i++)
            {
                string value = Convert.ToString(i, 2);
                value = value.PadLeft(5, '0');
                discardBinary.Add(i, value);
            }
        }
    }
}
