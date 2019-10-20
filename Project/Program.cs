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
            var document = new Document(@"C:\Users\Дмитрий\Desktop\Документ Microsoft Word.docx", TemplateType.coursework);
            var errors = document.GetErrors(new GlobalParameters(2*Consts.Sm, 1.5*Consts.Sm, 2*Consts.Sm, 3*Consts.Sm));

            if (errors.Keys.Count > 0)
            {
                foreach (var paragraph in errors)
                    if (paragraph.Value.Count > 0)
                        foreach (var error in paragraph.Value)
                        {
                            Console.WriteLine("Error in {0} paragraph in {1} parameter", paragraph.Key, error.Parameter);
                        }
            }
            else Console.WriteLine("Succesful!!!");
        }
    }
}
