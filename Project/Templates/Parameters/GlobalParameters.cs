using Project.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Templates.Parameters
{
    public class GlobalParameters : ITextParameters
    {
        public List<Parameter> Parameters { get; }
        private bool _isEditable;

        public GlobalParameters(bool isEditable)
        {
            Parameters = new List<Parameter>();
            _isEditable = isEditable;
        }

        public void AddParameter(string parameterName, string parameterValue, ValueManager.ValueTypes valueType)
        {
            Parameters.Add(new Parameter(parameterName, parameterValue, valueType));
        }

        public string GetParameterValue(int id)
        {
            if (_isEditable)
            {
                if (Parameters[id].ValueType == ValueManager.ValueTypes.Sm)
                    return ValueManager.ConvertToSm(Parameters[id].Value);
                if (Parameters[id].ValueType == ValueManager.ValueTypes.Pt)
                    return ValueManager.ConvertToPt(Parameters[id].Value);
            }
            return Parameters[id].Value;
        }

        public string GetParameterName(int id)
        {
            return Parameters[id].Name;
        }
    }
}
