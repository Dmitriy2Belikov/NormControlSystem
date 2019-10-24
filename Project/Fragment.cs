using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Fragment : IDocument
    {
        public DocumentFormat.OpenXml.OpenXmlElement XmlData { get; }
        private LocalParameters LcParameters;
        private List<Error> ErrorsList;
        public Dictionary<string, List<string>> Attributes { get; set; }
        private ITemplate template;

        public Fragment(DocumentFormat.OpenXml.OpenXmlElement data, ITemplate temp)
        {
            XmlData = data;
            template = temp;
            SetAttributes();
        }

        public List<Error> GetErrors()
        {
            ErrorsList = new List<Error>();

            

            return ErrorsList;
        }

        public void SetAttributes()
        {
            Attributes = new Dictionary<string, List<string>>();

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
