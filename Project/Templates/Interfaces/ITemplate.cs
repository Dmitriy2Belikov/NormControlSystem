using Project.Templates.Parameters;

namespace Project.Templates.Interfaces
{
    public interface ITemplate
    {
        GlobalParameters GlobalParameters { get; }
        TextParameters TextParameters { get; }
    }
}
