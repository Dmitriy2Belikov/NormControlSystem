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

    class Document
    {
        private List<Paragraph> paragraphs;
        private string filePath;
        private GlobalParameters glParameters;
        private ITemplate template;
        public Dictionary<string, List<string>> Attributes { get; set; }
        public Dictionary<int, List<Error>> ErrorsDict;
        DocumentFormat.OpenXml.OpenXmlElement XmlData;

        public Document(string path, ITemplate temp)
        {
            paragraphs = new List<Paragraph>();
            filePath = path;
            template = temp;
        }

        public Dictionary<int, List<Error>> GetErrors()
        {
            ErrorsDict = new Dictionary<int, List<Error>>();

            using (var document = WordprocessingDocument.Open(filePath, true))
            {
                XmlData = document.MainDocumentPart.Document.Body;
                GetParagraphs();
                SetAttributes();

                glParameters = new GlobalParameters(Attributes["pgMar"][0],
                                                    Attributes["pgMar"][1],
                                                    Attributes["pgMar"][2],
                                                    Attributes["pgMar"][3]);

                for (int i = 0; i < paragraphs.Count; i++)
                    if (paragraphs[i].ErrorList.Count > 0)
                        ErrorsDict.Add(i, paragraphs[i].ErrorList);

                return ErrorsDict;
            }
        }

        private void SetAttributes()
        {
            Attributes = new Dictionary<string, List<string>>();

            foreach (var param in paragraphs.Last().XmlData.ChildElements)
            {
                var attrs = new List<string>();
                foreach (var attr in param.GetAttributes())
                    attrs.Add(attr.Value);

                Attributes.Add(param.LocalName, attrs);
            }
        }

        private void GetParagraphs()
        {
            foreach (var paragraph in XmlData.ChildElements)
                paragraphs.Add(new Paragraph(paragraph, template));
        }
    }
}
