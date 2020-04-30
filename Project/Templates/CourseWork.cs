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

            TextParameters = new TextParameters(false);
            TextParameters.AddParameter(NameForUsers.FontFamily, "Times New Roman", ValueManager.ValueTypes.FontFamily);
            TextParameters.AddParameter(NameForUsers.FontSize, "14", ValueManager.ValueTypes.Pt);
        }

        public GlobalParameters GlobalParameters { get; }
        public TextParameters TextParameters { get; }
    }
}
