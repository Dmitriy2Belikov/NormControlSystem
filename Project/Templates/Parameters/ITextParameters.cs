
using Project.Structures;
using System.Collections.Generic;

namespace Project.Templates.Parameters
{
    public interface ITextParameters
    {
        List<Parameter> Parameters { get; }
        void AddParameter(string parameterName, string parameterValue, ValueManager.ValueTypes valueType);
        string GetParameterValue(int id);
        string GetParameterName(int id);
    }
}
