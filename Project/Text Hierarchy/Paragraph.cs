using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using Project.Structures;
using Project.Templates.Interfaces;

namespace Project.Text_Hierarchy
{
    class Paragraph
    {
        private OpenXmlElement _xmlData;
        private OpenXmlElement _styleXmlData;
        private List<Error> _errors;
        private ITemplate _template;

        public Paragraph(OpenXmlElement paragraphXML, OpenXmlElement styleXmlData, ITemplate template)
        {
            _xmlData = paragraphXML;
            _styleXmlData = styleXmlData;
            _template = template;
        }

        public List<Error> GetErrors()
        {
            _errors = new List<Error>();

            var fragments = GetFragments();

            _errors.AddRange(GetFragmentErrors(fragments));

            return _errors;
        }

        private List<Fragment> GetFragments()
        {
            var fragments = new List<Fragment>();

            foreach (var fragment in _xmlData)
                fragments.Add(new Fragment(fragment, _styleXmlData, _template));

            return fragments;
        }
        private List<Error> GetFragmentErrors(List<Fragment> fragments)
        {
            var errors = new List<Error>();

            foreach (var fragment in fragments)
                errors.AddRange(fragment.GetErrors());

            return errors;
        }

        public OpenXmlElement GetXML()
        {
            return _xmlData;
        }
    }
}
