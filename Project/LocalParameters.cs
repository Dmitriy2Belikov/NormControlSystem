using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class LocalParameters : ILocalParameters
    {
        public double Font { get; }
        public string FontFamily { get; }
        public LocalParameters(double font, string fontFamily)
        {
            Font = font;
            FontFamily = fontFamily;
        }
    }
}
