using Project.Constants;
using Project.Templates.Interfaces;
using Project.Templates.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Templates
{
    class CourseWork : ITemplate
    {
        public CourseWork()
        {
            GlobalParameters = new GlobalParameters(false);
            GlobalParameters.AddParameter(NameForUsers.MarginTop, "2", ValueManager.ValueTypes.Sm);
            GlobalParameters.AddParameter(NameForUsers.MarginBottom, "2", ValueManager.ValueTypes.Sm);
            GlobalParameters.AddParameter(NameForUsers.MarginLeft, "3", ValueManager.ValueTypes.Sm);
            GlobalParameters.AddParameter(NameForUsers.MarginRight, "1,5", ValueManager.ValueTypes.Sm);

            ParagraphParameters = new ParagraphParameters("14", "Times New Roman");
        }

        public GlobalParameters GlobalParameters { get; }

        public ParagraphParameters ParagraphParameters { get; }
    }
}
