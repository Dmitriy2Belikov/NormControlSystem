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
            var document = new Document(@"C:\Users\Дмитрий\Desktop\СХС_Отчёт(04.05.19) — копия.docx", new CourseWork());
            var errors = document.GetErrors();

            if (errors.Keys.Count > 0)
                foreach (var index in errors.Keys)
                    foreach(var error in errors[index])
                        Console.WriteLine("Ошибка: {0} (Ожидалось: {1}, было: {2}) в {3} главе", error.Parameter, error.ExpectValue, error.Value, index + 1);
            else Console.WriteLine("Succesful!!!");
        }
    }
}
