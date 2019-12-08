using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Header : ITextElement
    {
        public DocumentFormat.OpenXml.OpenXmlElement XmlData { get; }
        public List<Error> ErrorList { get; private set; }
        private List<Fragment> fragments;
        public List<Paragraph> Paragraphs;
        public List<SubHeader> SubHeaders;
        private ITemplate template;
        public Dictionary<string, List<string>> Attributes { get; set; }
        private Dictionary<string, List<string>> docDefaults;
        private int parNum; // Delete this

        public Header(DocumentFormat.OpenXml.OpenXmlElement data, ITemplate temp, Dictionary<string, List<string>> docDef, int num)
        {
            parNum = num;
            XmlData = data;
            Paragraphs = new List<Paragraph>();
            SubHeaders = new List<SubHeader>();
            template = temp;
            docDefaults = docDef;
            SetFragments();
            SetErrors();
        }

        public Header(ITemplate temp, Dictionary<string, List<string>> docDef, int num)
        {
            parNum = num;
            Paragraphs = new List<Paragraph>();
            SubHeaders = new List<SubHeader>();
            template = temp;
            docDefaults = docDef;
        }

        private void SetFragments()
        {
            fragments = new List<Fragment>();
            var IsDef = true;

            foreach (var child in XmlData.ChildElements)
            {
                var frNames = new List<string>();
                Dictionary<string, DocumentFormat.OpenXml.OpenXmlElement> frXml = new Dictionary<string, DocumentFormat.OpenXml.OpenXmlElement>();

                foreach (var child2 in child.ChildElements)
                {
                    if (child2.LocalName == "rPr")
                    {
                        fragments.Add(new Fragment(child2, template, docDefaults, parNum));
                        IsDef = false;
                        break;
                    }
                    else if (child2.LocalName == "t" && IsDef) fragments.Add(new Fragment(template, docDefaults, parNum));
                }
            }
        }

        private void SetErrors()
        {
            ErrorList = new List<Error>();

            foreach (var fragment in fragments)
                foreach (var error in fragment.ErrorList)
                    if (!IsContains(error))
                        ErrorList.Add(error);
        }

        private bool IsContains(Error error)
        {
            for (int i = 0; i < ErrorList.Count; i++)
            {
                if (ErrorList[i].Value == error.Value && ErrorList[i].Parameter == error.Parameter)
                    return true;
            }
            return false;
        }
    }
}
