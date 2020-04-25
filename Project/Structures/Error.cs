using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Structures
{
    class Error
    {
        public string ParameterName { get; }
        public string Expected { get; }
        public string Actual { get; }

        public Error(string parameterName, string expected, string actual)
        {
            ParameterName = parameterName;
            Expected = expected;
            Actual = actual;
        }
    }
}
