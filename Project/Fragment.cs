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
        public List<Error> ErrorsList { get; private set; }
        public Dictionary<string, List<string>> Attributes { get; set; }
        private ITemplate template;
        private LocalParameters lcParams;
        private Dictionary<string, List<string>> docDefaults;

        public Fragment(DocumentFormat.OpenXml.OpenXmlElement data, ITemplate temp, Dictionary<string, List<string>> docDef)
        {
            XmlData = data;
            template = temp;
            docDefaults = docDef;
            SetAttributes();
            GetErrors();
        }

        public Fragment(ITemplate temp, Dictionary<string, List<string>> docDef)
        {
            template = temp;
            docDefaults = docDef;
            GetErrors();
        }

        public List<Error> GetErrors()
        {
            ErrorsList = new List<Error>();

            if (Attributes == null) Attributes = new Dictionary<string, List<string>>();
            if (!Attributes.ContainsKey("sz"))
                Attributes.Add(docDefaults.Keys.Where(x => x == "sz").First(), docDefaults["sz"]);
            if (!Attributes.ContainsKey("rFonts"))
                Attributes.Add(docDefaults.Keys.Where(x => x == "rFonts").First(), docDefaults["rFonts"]);

            lcParams = new LocalParameters(Attributes["sz"][0], Attributes["rFonts"][0]);

            foreach (var param in lcParams.Parameters)
                if (lcParams.Parameters[param.Key] != template.LocalParameters.Parameters[param.Key])
                    ErrorsList.Add(new Error(param.Key, param.Value, template.LocalParameters.Parameters[param.Key]));

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