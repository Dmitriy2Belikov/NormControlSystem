using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class CourseWork : ITemplate
    {
        public GlobalParameters GlobalParameters { get; }
        public LocalParameters LocalParameters { get; }

        public CourseWork()
        {
            GlobalParameters = new GlobalParameters(Math.Round(2 * Consts.Sm).ToString(), Math.Round(1.5 * Consts.Sm).ToString(), Math.Round(2 * Consts.Sm).ToString(), Math.Round(3 * Consts.Sm).ToString());
            LocalParameters = new LocalParameters((14 * Consts.Pt).ToString(), "Times New Roman");
        }
    }
}
