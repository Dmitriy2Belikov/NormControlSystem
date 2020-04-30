using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using System.Xml;
using System.Xml.Linq;
using Project.Templates;

namespace Project
{
    public class Program
    {
        static void Main(string[] args)
        {
            var document = new Document(@"C:\Users\Дмитрий\Desktop\NormControlAlghorithm\Project\TestDocuments\СХС_Отчёт(04.05.19) — копия.docx", new CourseWork());
            var errors = document.GetErrors();

            foreach (var error in errors)
                Console.WriteLine("{0}: Ожидалось - {1}, Было - {2}", error.ParameterName, error.Expected, error.Actual);
        }
    }
}
