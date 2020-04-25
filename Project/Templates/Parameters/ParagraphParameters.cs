using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Templates.Parameters
{
    public class ParagraphParameters : IParagraphParameters
    {
        public string Font { get; }

        public string FontFamily { get; }

        public ParagraphParameters(string font, string fontFamily)
        {
            Font = font;
            FontFamily = fontFamily;
        }
    }
}
