using DocumentFormat.OpenXml;
using Project.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public static class Styles
    {
        private static string _docDefaultsName = "docDefaults";
        private static string _fragmentDefaultName = "rPrDefault";

        public static string GetDefaultFragmentFontFamily(OpenXmlElement stylesXML)
        {
            var docDefaults = stylesXML.ChildElements.FirstOrDefault(d => d.LocalName == _docDefaultsName);
            var fragmentDefault = docDefaults.ChildElements.FirstOrDefault(f => f.LocalName == _fragmentDefaultName);

            var parameters = fragmentDefault.ChildElements.FirstOrDefault(p => p.LocalName == NameInXML.FragmentParameters);

            return parameters.ChildElements
                .FirstOrDefault(p => p.LocalName == NameInXML.FragmentFontFamily)
                .GetAttributes()
                .First().Value;
        }

        public static string GetDefaultFragmentFontSize(OpenXmlElement stylesXML)
        {
            var docDefaults = stylesXML.ChildElements.FirstOrDefault(d => d.LocalName == _docDefaultsName);
            var fragmentDefault = docDefaults.ChildElements.FirstOrDefault(f => f.LocalName == _fragmentDefaultName);

            var parameters = fragmentDefault.ChildElements.FirstOrDefault(p => p.LocalName == NameInXML.FragmentParameters);

            return parameters.ChildElements
                .FirstOrDefault(p => p.LocalName == NameInXML.FragmentFontSize)
                .GetAttributes()
                .First().Value;
        }
    }
}
