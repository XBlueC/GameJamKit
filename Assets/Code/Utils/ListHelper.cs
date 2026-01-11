using System;
using System.Collections.Generic;

namespace Code.Utils
{
    public static class ListHelper
    {
        public static void Shuffle<T>(List<T> list)
        {
            var n = list.Count;
            var random = new Random();
            while (n > 1)
            {
                n--;
                var k = random.Next(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}