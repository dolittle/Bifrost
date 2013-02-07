using System.IO;

namespace Bifrost.Web.Proxies
{
    public class CodeWriter : ICodeWriter
    {
        int _indentLevel;
        TextWriter _actualWriter;

        public CodeWriter(TextWriter writer)
        {
            _actualWriter = writer;
        }

        public void Indent()
        {
            _indentLevel++;
        }

        public void Unindent()
        {
            _indentLevel--;
        }

        public void WriteWithIndentation(string format, params object[] args)
        {
            for (var i = 0; i < _indentLevel; i++) _actualWriter.Write("\t");
            Write(format, args);
        }

        public void Write(string format, params object[] args)
        {
            _actualWriter.Write(format, args);
        }

        public void Newline()
        {
            _actualWriter.Write("\n");
        }
    }
}
