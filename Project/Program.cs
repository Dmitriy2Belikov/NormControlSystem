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
    class Program
    {
        static void Main(string[] args)
        {
            var document = new Document(@"C:\Users\Дмитрий\Desktop\Документ Microsoft Word.docx");
            document.GetErrors();
        }
    }
}
