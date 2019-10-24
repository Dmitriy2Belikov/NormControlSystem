using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using System.Xml;
using System.Xml.Linq;

namespace Project
{
    static class Consts // Константы в значениях ворда
    {
        public const double Sm = 567;
        public const double Pt = 2;
    }

    class Document : IDocument
    {
        private List<Paragraph> paragraphs;
        private string filePath;
        private GlobalParameters glParameters;
        private ITemplate template;
        public Dictionary<string, List<string>> Attributes { get; set; }
        public List<Error> ErrorsList;
        DocumentFormat.OpenXml.OpenXmlElement XmlData;

        public Document(string path, ITemplate temp)
        {
            paragraphs = new List<Paragraph>();
            filePath = path;
            template = temp;
        }

        public List<Error> GetErrors()
        {
            ErrorsList = new List<Error>();

            using (var document = WordprocessingDocument.Open(filePath, true))
            {
                foreach (var paragraph in document.MainDocumentPart.Document.Body.ChildElements)
                    paragraphs.Add(new Paragraph(paragraph, template));

                glParameters = new GlobalParameters(paragraphs.Last().Attributes["pgMar"][0],
                                                    paragraphs.Last().Attributes["pgMar"][1],
                                                    paragraphs.Last().Attributes["pgMar"][2],
                                                    paragraphs.Last().Attributes["pgMar"][3]);

                for (int i = 0; i < template.GlobalParameters.Parameters.Count; i++)
                    if (glParameters.Parameters.Values.ElementAt(i) != template.GlobalParameters.Parameters.Values.ElementAt(i))
                        ErrorsList.Add(new Error(glParameters.Parameters.Keys.ElementAt(i), glParameters.Parameters[glParameters.Parameters.Keys.ElementAt(i)].ToString(), template.GlobalParameters.Parameters[glParameters.Parameters.Keys.ElementAt(i)].ToString()));
            }

            return ErrorsList;
        }

        public void SetAttributes()
        {
            Attributes = new Dictionary<string, List<string>>(); // Все аттрибуты абзаца

            for (int i = 0; i < XmlData.ChildElements.Count; i++)
            {
                var childAttr = new List<string>(); // Аттрибуты отдельного ребёнка

                foreach (var attribute in XmlData.ChildElements[i].GetAttributes())
                    childAttr.Add(attribute.Value);

                if (!Attributes.ContainsKey(XmlData.ChildElements[i].LocalName))
                    Attributes.Add(XmlData.ChildElements[i].LocalName, childAttr);
            }
        }
    }
}
