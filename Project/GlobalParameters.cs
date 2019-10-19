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
        public GlobalParameters(double margTop, double margRight, double margBottom, double margLeft)
        {
            MarginBottom = margBottom;
            MarginTop = margTop;
            MarginLeft = margLeft;
            MarginRight = margRight;
        }
    }
}
