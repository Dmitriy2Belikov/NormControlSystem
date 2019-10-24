using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class GlobalParameters : IGlobalParameters
    {
        public string MarginTop { get; }
        public string MarginBottom { get; }
        public string MarginLeft { get; }
        public string MarginRight { get; }
        public Dictionary<string, string> Parameters;

        public GlobalParameters(string margTop, string margRight, string margBottom, string margLeft)
        {
            MarginBottom = margBottom;
            MarginTop = margTop;
            MarginLeft = margLeft;
            MarginRight = margRight;

            Parameters = new Dictionary<string, string>();
            Parameters.Add("Верхнее поле", MarginTop);
            Parameters.Add("Правое поле", MarginRight);
            Parameters.Add("Нижнее поле", MarginBottom);
            Parameters.Add("Левое поле", MarginLeft);
        }
    }
}
