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
    class Paragraph 
    {
        public DocumentFormat.OpenXml.OpenXmlElement XmlData { get; }
        private LocalParameters LcParameters;
        public List<Error> ErrorList;

        public Paragraph(DocumentFormat.OpenXml.OpenXmlElement data)
        {
            XmlData = data;
        }

        public Dictionary<string, List<double>> GetParagraphAttributes()
        {
            var attributes = new Dictionary<string, List<double>>(); // Все аттрибуты абзаца по детям

            for (int i = 0; i < XmlData.ChildElements.Count; i++)
            {
                var childAttr = new List<double>(); // Аттрибуты отдельного ребёнка

                foreach (var attribute in XmlData.ChildElements[i].GetAttributes())
                    childAttr.Add(double.Parse(attribute.Value));

                attributes.Add(XmlData.ChildElements[i].LocalName, childAttr);
            }
            return attributes;
        }
    }
}
