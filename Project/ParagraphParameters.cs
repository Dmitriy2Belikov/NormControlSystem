using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class ParagraphParameters : IParagraphParameters
    {
        public string Font { get; }
        public string FontFamily { get; }
        public Dictionary<string, string> Parameters;

        public ParagraphParameters(string font, string fontFamily)
        {
            Font = font;
            FontFamily = fontFamily;

            Parameters = new Dictionary<string, string>();
            Parameters.Add("Размер", font);
            Parameters.Add("Шрифт", fontFamily);
        }
    }
}
