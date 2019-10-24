using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class LocalParameters : ILocalParameters
    {
        public string Font { get; }
        public string FontFamily { get; }
        public Dictionary<string, string> Parameters;

        public LocalParameters(string font, string fontFamily)
        {
            Font = font;
            FontFamily = fontFamily;

            Parameters = new Dictionary<string, string>();
            Parameters.Add("Размер", font);
            Parameters.Add("Шрифт", fontFamily);
        }
    }
}
