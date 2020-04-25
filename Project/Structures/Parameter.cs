using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Structures
{
    public class Parameter
    {
        public string Name { get; }
        public string Value { get; }
        public ValueManager.ValueTypes ValueType { get; }

        public Parameter(string name, string value, ValueManager.ValueTypes valueType)
        {
            Name = name;
            Value = value;
            ValueType = valueType;
        }
    }
}
