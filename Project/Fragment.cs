using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Fragment : ITextElement
    {
        public DocumentFormat.OpenXml.OpenXmlElement XmlData { get; }
        public List<Error> ErrorList { get; private set; }
        public Dictionary<string, List<string>> Attributes { get; set; }
        private ITemplate template;
        private ParagraphParameters parParams;
        private Dictionary<string, List<string>> docDefaults;
        private int checkNum; //Delete this

        public Fragment(DocumentFormat.OpenXml.OpenXmlElement data, ITemplate temp, Dictionary<string, List<string>> docDef, int check)
        {
            XmlData = data;
            template = temp;
            docDefaults = docDef;
            SetAttributes();
            SetErrors(check);
        }

        public Fragment(ITemplate temp, Dictionary<string, List<string>> docDef, int check)
        {
            template = temp;
            docDefaults = docDef;
            SetErrors(check);
        }

        public void SetErrors(int check)
        {
            ErrorList = new List<Error>();

            if (Attributes == null) Attributes = new Dictionary<string, List<string>>();
            if (!Attributes.ContainsKey("sz"))
            {
                Attributes.Add(docDefaults.Keys.First(x => x == "sz"), docDefaults["sz"]);
                checkNum = check; //Delete this
            }
            if (!Attributes.ContainsKey("rFonts"))
            {
                Attributes.Add(docDefaults.Keys.First(x => x == "rFonts"), docDefaults["rFonts"]);
                checkNum = check; //Delete this
            }

            parParams = new ParagraphParameters(Attributes["sz"][0], Attributes["rFonts"][0]);

            foreach (var param in parParams.Parameters)
                if (parParams.Parameters[param.Key] != template.ParagraphParameters.Parameters[param.Key])
                    ErrorList.Add(new Error(param.Key, param.Value, template.ParagraphParameters.Parameters[param.Key]));
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