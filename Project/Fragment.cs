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
        private LocalParameters lcParameters;
        public List<Error> ErrorsList { get; private set; }
        public Dictionary<string, List<string>> Attributes { get; set; }
        private ITemplate template;

        public Fragment(DocumentFormat.OpenXml.OpenXmlElement data, ITemplate temp)
        {
            XmlData = data;
            template = temp;
            SetAttributes();
            GetErrors();
        }

        public List<Error> GetErrors()
        {
            ErrorsList = new List<Error>();

            if (Attributes.ContainsKey("sz") && Attributes.ContainsKey("rFonts"))
            {
                lcParameters = new LocalParameters(Attributes["sz"][0], Attributes["rFonts"][0]);

                for (int i = 0; i < lcParameters.Parameters.Keys.Count; i++)
                    if (lcParameters.Parameters.Values.ElementAt(i) != template.LocalParameters.Parameters.Values.ElementAt(i)) ErrorsList.Add(new Error(lcParameters.Parameters.Keys.ElementAt(i), lcParameters.Parameters.Values.ElementAt(i), template.LocalParameters.Parameters.Values.ElementAt(i)));
            }

            return ErrorsList;
        }

        private void SetAttributes()
        {
            Attributes = new Dictionary<string, List<string>>();

            for (int i = 0; i < XmlData.ChildElements.Count; i++)
            {
                var childAttr = new List<string>(); // Аттрибуты отдельного ребёнка

                foreach (var attr in XmlData.ChildElements[i].GetAttributes())
                    childAttr.Add(attr.Value);

                if (!Attributes.ContainsKey(XmlData.ChildElements[i].LocalName))
                    Attributes.Add(XmlData.ChildElements[i].LocalName, childAttr);
            }
        }
    }
}