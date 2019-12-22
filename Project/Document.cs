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
        private Dictionary<int, Header> Chapters;
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

                SetDefaults(styles);
                SetChapters();
                SetAttributes();

                glParameters = new GlobalParameters(Attributes["pgMar"][0],
                                                    Attributes["pgMar"][1],
                                                    Attributes["pgMar"][2],
                                                    Attributes["pgMar"][3]);

                foreach (var chapter in Chapters)
                {
                    var erList = new List<Error>();
                    foreach (var paragraph in chapter.Value.Paragraphs)
                        erList.AddRange(paragraph.ErrorList);

                    if(erList.Count > 0)
                        ErrorsDict.Add(chapter.Key, erList);
                }

                return ErrorsDict;
            }
        }

        private void SetDefaults(DocumentFormat.OpenXml.Wordprocessing.DocDefaults DocDef)
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

            foreach (var param in GetParagraph(XmlData.Count() - 1).XmlData.ChildElements)
            {
                var attrs = new List<string>();
                foreach (var attr in param.GetAttributes())
                    attrs.Add(attr.Value);

                Attributes.Add(param.LocalName, attrs);
            }
        }

        private Paragraph GetParagraph(int num)
        {
            return new Paragraph(XmlData.ElementAt(num), template, docDefaults, -1);
        }

        private void SetChapters()
        {
            Chapters = new Dictionary<int, Header>();
            bool isSubHeader = false;
            bool isParagraph = false;
            var i = 0;
            int parNum = 0; // Delete this
            
            foreach(var paragraph in XmlData)
            {
                foreach (var child in paragraph)
                    if (child.LocalName == "pPr")
                        foreach (var el in child)
                        {
                            if (el.LocalName == "pStyle" && el.GetAttributes().First().Value == "1")
                            {
                                var header = new Header(paragraph, template, docDefaults, parNum);
                                Chapters.Add(i, header);
                                isSubHeader = false;
                                isParagraph = false;
                                i++;
                            }
                            if (el.LocalName == "pStyle" && el.GetAttributes().First().Value == "2")
                            {
                                var subHeader = new SubHeader(paragraph, template, docDefaults, parNum);
                                if (Chapters.Count > 0)
                                    Chapters.Last().Value.SubHeaders.Add(subHeader);
                                else
                                {
                                    var header = new Header(template, docDefaults, parNum);
                                    Chapters.Add(i, header);
                                    Chapters.Last().Value.SubHeaders.Add(subHeader);
                                }
                                isSubHeader = true;
                                isParagraph = false;
                            }
                        }
                if (Chapters.Count > 0 && !isSubHeader && isParagraph && paragraph != XmlData.Last())
                    Chapters.Last().Value.Paragraphs.Add(new Paragraph(paragraph, template, docDefaults, parNum));
                else if (Chapters.Count > 0 && isSubHeader && isParagraph && paragraph != XmlData.Last())
                    Chapters.Last().Value.SubHeaders.Last().Paragraphs.Add(new Paragraph(paragraph, template, docDefaults, parNum));
                else if (Chapters.Count < 1)
                {
                    var header = new Header(template, docDefaults, parNum);
                    Chapters.Add(i, header);
                    i++;
                }
                isParagraph = true;
                parNum++;
            }
        }
    }
}
