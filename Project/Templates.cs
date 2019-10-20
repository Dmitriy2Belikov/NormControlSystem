using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    enum TemplateType
    {
        coursework
    }
    class Template
    {
        public GlobalParameters GlobalParameters;
        public LocalParameters LocalParameters;

        public Template(TemplateType type)
        {
            if (type == TemplateType.coursework)
            {
                GlobalParameters = new GlobalParameters(2*Consts.Sm, Math.Round(1.5*Consts.Sm), 2*Consts.Sm, 3*Consts.Sm);
                LocalParameters = new LocalParameters(14*Consts.Pt, "Times New Roman");
            }
        }
    }
}
