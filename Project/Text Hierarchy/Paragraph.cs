using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using Project.Structures;

namespace Project.Text_Hierarchy
{
    class Paragraph
    {
        private OpenXmlElement _xmlData;
        private List<Error> _errors;

        public Paragraph(OpenXmlElement paragraphXML)
        {
            _xmlData = paragraphXML;
        }

        public List<Error> GetErrors()
        {
            _errors = new List<Error>();



            return _errors;
        }

        public OpenXmlElement GetXML()
        {
            return _xmlData;
        }
    }
}
