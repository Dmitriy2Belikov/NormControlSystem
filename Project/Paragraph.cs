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
    class Paragraph : IDocument
    {
        public DocumentFormat.OpenXml.OpenXmlElement XmlData { get; }
        public List<Error> ErrorList;
        private List<Fragment> fragments;
        private ITemplate template;
        public Dictionary<string, List<string>> Attributes { get; set; }
        private Dictionary<string, List<string>> docDefaults;

        public Paragraph(DocumentFormat.OpenXml.OpenXmlElement data, ITemplate temp, Dictionary<string, List<string>> docDef)
        {
            XmlData = data;
            template = temp;
            docDefaults = docDef;
            SetAttributes();
            GetFragments();
            GetErrors();
        }

        public List<Error> GetErrors()
        {
            ErrorList = new List<Error>();

            foreach(var fragment in fragments)
                foreach(var error in fragment.ErrorsList)
                    if (!IsContains(error))
                        ErrorList.Add(error);

            return ErrorList;
        }

        private bool IsContains(Error error)
        {
            for(int i = 0; i < ErrorList.Count; i++)
            {
                if (ErrorList[i].Value == error.Value && ErrorList[i].Parameter == error.Parameter)
                    return true;
            }
            return false;
        }

        private void GetFragments()
        {
            fragments = new List<Fragment>();
            var test = true;

            foreach (var child in XmlData.ChildElements)
            {
                var frNames = new List<string>();
                Dictionary<string, DocumentFormat.OpenXml.OpenXmlElement> frXml = new Dictionary<string, DocumentFormat.OpenXml.OpenXmlElement>();

                foreach (var child2 in child.ChildElements)
                {
                    if (child2.LocalName == "rPr")
                    {
                        fragments.Add(new Fragment(child2, template, docDefaults));
                        test = false;
                        break;
                    }
                    else if (child2.LocalName == "t" && test) fragments.Add(new Fragment(template, docDefaults));
                }
            }
        }

        private void SetAttributes()
        {
            Attributes = new Dictionary<string, List<string>>(); // Все аттрибуты абзаца

            for (int i = 0; i < XmlData.Count(); i++)
            {
                var childAttr = new List<string>(); // Аттрибуты отдельного ребёнка

                foreach (var attribute in XmlData.GetAttributes())
                    childAttr.Add(attribute.Value);

                if (!Attributes.ContainsKey(XmlData.LocalName))
                    Attributes.Add(XmlData.LocalName + i, childAttr);
            }
        }
    }
}
