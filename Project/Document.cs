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
    class Document
    {
        private List<Paragraph> paragraphs;
        private string filePath;
        private GlobalParameters GlParameters;

        public Document(string path)
        {
            paragraphs = new List<Paragraph>();
            filePath = path;
        }

        public Dictionary<int, List<Error>> GetErrors()
        {
            var ErrorsDict = new Dictionary<int, List<Error>>();

            using (var document = WordprocessingDocument.Open(filePath, true))
            {
                foreach (var paragraph in document.MainDocumentPart.Document.Body.ChildElements)
                    paragraphs.Add(new Paragraph(paragraph));

                var attributes = paragraphs.Last().GetParagraphAttributes();

                GlParameters = new GlobalParameters(attributes["pgMar"][0], attributes["pgMar"][1], attributes["pgMar"][2], attributes["pgMar"][3]);
            }

            return ErrorsDict;
        }
    }
}
