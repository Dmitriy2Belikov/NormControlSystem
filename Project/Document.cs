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
        private GlobalParameters GlParameters;
        private ITemplate DocTemplate;

        public Document(string path, ITemplate template)
        {
            paragraphs = new List<Paragraph>();
            filePath = path;
            DocTemplate = template;
        }

        public Dictionary<int, List<Error>> GetErrors()
        {
            var ErrorsDict = new Dictionary<int, List<Error>>();
            var GlobalErrors = new List<Error>();

            using (var document = WordprocessingDocument.Open(filePath, true))
            {
                foreach (var paragraph in document.MainDocumentPart.Document.Body.ChildElements)
                    paragraphs.Add(new Paragraph(paragraph));

                var globAttributes = paragraphs.Last().GetParagraphAttributes();
                GlParameters = new GlobalParameters(globAttributes["pgMar"][0], globAttributes["pgMar"][1], globAttributes["pgMar"][2], globAttributes["pgMar"][3]);

                for (int i = 0; i < DocTemplate.GlobalParameters.Parameters.Count; i++)
                    if (GlParameters.Parameters.Values.ElementAt(i) != DocTemplate.GlobalParameters.Parameters.Values.ElementAt(i))
                        GlobalErrors.Add(new Error(GlParameters.Parameters.Keys.ElementAt(i)));

                if (GlobalErrors.Count > 0)
                    ErrorsDict.Add(0, GlobalErrors); // 0 абзац - Глобальные параметры
            }

            return ErrorsDict;
        }
    }
}
