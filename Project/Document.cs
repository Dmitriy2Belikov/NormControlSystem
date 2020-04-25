using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using System.Xml;
using System.Xml.Linq;
using Project.Structures;
using Project.Templates.Interfaces;
using Project.Text_Hierarchy;
using DocumentFormat.OpenXml;
using Project.Templates.Parameters;
using Project.Constants;

namespace Project
{
    class Document
    {
        private ITemplate _template;
        private List<Error> _errors;
        private string _filePath;
        private OpenXmlElement _xmlData;

        public Document(string path, ITemplate template)
        {
            _filePath = path;
            _template = template;
        }

        public List<Error> GetErrors()
        {
            _errors = new List<Error>();

            using (var document = WordprocessingDocument.Open(_filePath, true))
            {
                _xmlData = document.MainDocumentPart.Document.Body;

                var paragraphs = GetParagraphs();

                var globalParamsXML = paragraphs.Last().GetXML();

                _errors.AddRange(GetGlobalErrors(globalParamsXML));
                _errors.AddRange(GetParagraphErrors(paragraphs));
            }

            return _errors;
        }

        private List<Paragraph> GetParagraphs()
        {
            var paragraphs = new List<Paragraph>();

            foreach (var paragraph in _xmlData)
                paragraphs.Add(new Paragraph(paragraph));

            return paragraphs;
        }

        private List<Error> GetGlobalErrors(OpenXmlElement XML)
        {
            var globalErrors = new List<Error>();

            GlobalParameters globalParameters = GetGlobalParameters(XML);
            var templateParameters = _template.GlobalParameters;

            for (int i = 0; i < templateParameters.Parameters.Count; i++)
                if (globalParameters.GetParameterValue(i) != templateParameters.GetParameterValue(i))
                {
                    globalErrors.Add
                    (
                        new Error
                        (
                            templateParameters.GetParameterName(i),
                            templateParameters.GetParameterValue(i),
                            globalParameters.GetParameterValue(i)
                        )
                    );
                }

            return globalErrors;
        }

        private GlobalParameters GetGlobalParameters(OpenXmlElement XML)
        {
            GlobalParameters globalParameters = new GlobalParameters(true);

            foreach (var parameter in XML)
            {
                if (parameter.LocalName == ConstantsName.PageMargin)
                {
                    var attributes = parameter.GetAttributes();

                    var top = attributes.First(a => a.LocalName == "top").Value.ToString();
                    globalParameters.AddParameter(NameForUsers.MarginTop, top, ValueManager.ValueTypes.Sm);

                    var bottom = attributes.First(a => a.LocalName == "bottom").Value.ToString();
                    globalParameters.AddParameter(NameForUsers.MarginBottom, bottom, ValueManager.ValueTypes.Sm);

                    var left = attributes.First(a => a.LocalName == "left").Value.ToString();
                    globalParameters.AddParameter(NameForUsers.MarginLeft, left, ValueManager.ValueTypes.Sm);

                    var right = attributes.First(a => a.LocalName == "right").Value.ToString();
                    globalParameters.AddParameter(NameForUsers.MarginRight, right, ValueManager.ValueTypes.Sm);
                }
            }

            return globalParameters;
        }

        private List<Error> GetParagraphErrors(List<Paragraph> paragraphs)
        {
            var errors = new List<Error>();

            foreach (var paragraph in paragraphs)
                errors.AddRange(paragraph.GetErrors());

            return errors;
        }
    }
}
