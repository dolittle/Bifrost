namespace Bifrost.Web.Proxies
{
    public interface ICodeWriter
    {
        void Indent();
        void Unindent();
        void WriteWithIndentation(string format, params object[] args);
        void Write(string format, params object[] args);
        void Newline();
    }
}
