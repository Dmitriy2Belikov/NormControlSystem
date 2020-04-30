using DocumentFormat.OpenXml;
using Project.Constants;
using Project.Structures;
using Project.Templates.Interfaces;
using Project.Templates.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Text_Hierarchy
{
    class Fragment
    {
        private OpenXmlElement _xmlData;
        private OpenXmlElement _styleXmlData;
        private List<Error> _errors;
        private ITemplate _template;

        public Fragment(OpenXmlElement paragraphXML, OpenXmlElement styleXmlData, ITemplate template)
        {
            _xmlData = paragraphXML;
            _styleXmlData = styleXmlData;
            _template = template;
        }

        public List<Error> GetErrors()
        {
            _errors = new List<Error>();

            var fragmentParameters = GetFragmentParameters();
            var templateParameters = _template.TextParameters;

            for (int i = 0; i < templateParameters.Parameters.Count; i++)
                for (int j = 0; j < fragmentParameters.Parameters.Count; j++)
                    if (fragmentParameters.GetParameterName(j) == templateParameters.GetParameterName(i))
                        if (fragmentParameters.GetParameterValue(j) != templateParameters.GetParameterValue(i))
                            _errors.Add
                            (
                                new Error
                                (
                                    templateParameters.GetParameterName(i),
                                    templateParameters.GetParameterValue(i),
                                    fragmentParameters.GetParameterValue(j)
                                )
                            );

            return _errors;
        }

        private TextParameters GetFragmentParameters()
        {
            var parameters = new TextParameters(true);

            if (IsContainsText())
            {
                var parametersInXML = _xmlData.FirstOrDefault(p => p.LocalName == NameInXML.FragmentParameters);

                var fontFamily = string.Empty;
                if (parametersInXML != null)
                    fontFamily = parametersInXML.ChildElements
                        .FirstOrDefault(f => f.LocalName == NameInXML.FragmentFontFamily)
                        .GetAttributes()
                        .First().Value;

                if (fontFamily == string.Empty) fontFamily = Styles.GetDefaultFragmentFontFamily(_styleXmlData);
                parameters.AddParameter(NameForUsers.FontFamily, fontFamily, ValueManager.ValueTypes.FontFamily);

                var fontSize = string.Empty;
                if (parametersInXML != null)
                    fontSize = GetFontSize(parametersInXML);

                if (fontSize == string.Empty) fontSize = Styles.GetDefaultFragmentFontSize(_styleXmlData);
                parameters.AddParameter(NameForUsers.FontSize, fontSize, ValueManager.ValueTypes.Pt);
            }

            return parameters;
        }

        private string GetFontSize(OpenXmlElement fragmentParameters)
        {
            var fontSizeXML = fragmentParameters.ChildElements.FirstOrDefault(s => s.LocalName == NameInXML.FragmentFontSize);
            var fontSizeXMLCs = fragmentParameters.ChildElements.FirstOrDefault(s => s.LocalName == NameInXML.FragmentFontSizeCs);

            if (fontSizeXML != null)
                return fontSizeXML.GetAttributes().First().Value;
            else return fontSizeXMLCs.GetAttributes().First().Value;
        }

        private bool IsContainsText()
        {
            foreach (var parameter in _xmlData)
                if (parameter.LocalName == NameInXML.Text)
                    return true;

            return false;
        }

        public OpenXmlElement GetXML()
        {
            return _xmlData;
        }
    }
}
