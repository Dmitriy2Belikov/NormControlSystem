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
    class Error
    {
        public string Parameter;
        public string Value;
        public string ExpectValue;

        public Error(string param, string value, string expect)
        {
            Parameter = param;
            Value = value;
            ExpectValue = expect;
        }
    }
}
