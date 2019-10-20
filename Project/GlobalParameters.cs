using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class GlobalParameters : IGlobalParameters
    {
        public double MarginTop { get; }
        public double MarginBottom { get; }
        public double MarginLeft { get; }
        public double MarginRight { get; }
        public Dictionary<string, double> Parameters;
        public GlobalParameters(double margTop, double margRight, double margBottom, double margLeft)
        {
            MarginBottom = margBottom;
            MarginTop = margTop;
            MarginLeft = margLeft;
            MarginRight = margRight;

            Parameters = new Dictionary<string, double>();
            Parameters.Add("Margin Top", MarginTop);
            Parameters.Add("Margin Right", MarginRight);
            Parameters.Add("Margin Bottom", MarginBottom);
            Parameters.Add("Margin Left", MarginLeft);
        }
    }
}
