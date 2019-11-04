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
        private Dictionary<int, List<Paragraph>> Chapters;
        private string filePath;
        private GlobalParameters glParameters;
        private ITemplate template;
        public Dictionary<string, List<string>> Attributes { get; set; }
        public Dictionary<int, List<Error>> ErrorsDict;
        private Dictionary<string, List<string>> docDefaults;
        DocumentFormat.OpenXml.OpenXmlElement XmlData;

        public Document(string path, ITemplate temp)
        {
            filePath = path;
            template = temp;
        }

        public Dictionary<int, List<Error>> GetErrors()
        {
            ErrorsDict = new Dictionary<int, List<Error>>();

            using (var document = WordprocessingDocument.Open(filePath, true))
            {
                var styles = document.MainDocumentPart.StyleDefinitionsPart.Styles.DocDefaults;
                XmlData = document.MainDocumentPart.Document.Body;

                GetDefaults(styles);
                GetChapters();
                SetAttributes();

                glParameters = new GlobalParameters(Attributes["pgMar"][0],
                                                    Attributes["pgMar"][1],
                                                    Attributes["pgMar"][2],
                                                    Attributes["pgMar"][3]);

                foreach (var chapter in Chapters)
                {
                    var erList = new List<Error>();
                    foreach (var paragraph in chapter.Value)
                        erList.AddRange(paragraph.ErrorList);

                    if(erList.Count > 0)
                        ErrorsDict.Add(chapter.Key, erList);
                }

                return ErrorsDict;
            }
        }

        private void GetDefaults(DocumentFormat.OpenXml.Wordprocessing.DocDefaults DocDef)
        {
            docDefaults = new Dictionary<string, List<string>>();

            foreach (var part in DocDef)
                foreach (var param in part.ChildElements)
                {
                    foreach (var attr in param.ChildElements)
                    {
                        var attrs = new List<string>();
                        foreach (var value in attr.GetAttributes())
                            attrs.Add(value.Value);
                        docDefaults.Add(attr.LocalName, attrs);
                    }
                }
        }

        private void SetAttributes()
        {
            Attributes = new Dictionary<string, List<string>>();

            foreach (var param in Chapters.Last().Value.Last().XmlData.ChildElements)
            {
                var attrs = new List<string>();
                foreach (var attr in param.GetAttributes())
                    attrs.Add(attr.Value);

                Attributes.Add(param.LocalName, attrs);
            }
        }

        private void GetChapters()
        {
            Chapters = new Dictionary<int, List<Paragraph>>();
            var paragraphs = new List<List<Paragraph>>();
            paragraphs.Add(new List<Paragraph>());
            var i = 0;
            var test = true;
            
            foreach(var paragraph in XmlData)
            {
                foreach (var child in paragraph)
                    if (child.LocalName == "pPr")
                        foreach (var el in child)
                            if (el.LocalName == "pStyle" && el.GetAttributes().First().Value == "1")
                                if (paragraphs.Last().Count > 0)
                                {
                                    Chapters.Add(i, paragraphs.Last());
                                    i++;
                                    paragraphs.Add(new List<Paragraph>());
                                    paragraphs.Last().Add(new Paragraph(paragraph, template, docDefaults));
                                    test = false;
                                }
                                else if (paragraphs.Last().Count == 0 && test)
                                {
                                    paragraphs.Last().Add(new Paragraph(paragraph, template, docDefaults));
                                    test = false;
                                }
                if (test) paragraphs.Last().Add(new Paragraph(paragraph, template, docDefaults));
                else test = true;
            }

            if (paragraphs.Last().Count > 0)
                Chapters.Add(i, paragraphs.Last());
        }
    }
}
