using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    interface IDocument
    {
        Dictionary<string, List<string>> Attributes { get; set; }
        List<Error> GetErrors();
    }
}
