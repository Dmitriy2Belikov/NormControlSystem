using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    interface ITemplate
    {
        GlobalParameters GlobalParameters { get; }
        LocalParameters LocalParameters { get; }
    }
}
