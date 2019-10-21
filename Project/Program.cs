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
            var document = new Document(@"C:\Users\Дмитрий\Desktop\Документ Microsoft Word.docx", new CourseWork());
            var errorsDict = document.GetErrors();

            if (errorsDict.Keys.Count > 0)
            {
                foreach (var paragraphIndex in errorsDict)
                    if (paragraphIndex.Value.Count > 0)
                        foreach (var error in paragraphIndex.Value)
                        {
                            Console.WriteLine("Error in {0} paragraph in {1} parameter", paragraphIndex.Key, error.Parameter);
                        }
            }
            else Console.WriteLine("Succesful!!!");
        }
    }
}
