using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public static class ValueManager
    {
        public enum ValueTypes
        {
            Pt,
            Sm
        }

        private static int Pt = 2;
        private static int Sm = 567;

        public static string ConvertToSm(string value)
        {
            var notConverted = double.Parse(value);

            var converted = Math.Round(notConverted / Sm, 2);

            return converted.ToString();
        }

        public static string ConvertToPt(string value)
        {
            var notConverted = double.Parse(value);

            var converted = Math.Round(notConverted / Pt, 2);

            return converted.ToString();
        }
    }
}
