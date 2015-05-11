using System.Reflection;

namespace Bifrost.Specs.Execution.for_AssemblyProvider
{
    public class FakeAssembly : Assembly
    {
        public string AssemblyNameToReturn = string.Empty;

        public override AssemblyName GetName()
        {
            return new AssemblyName(AssemblyNameToReturn);
        }

        public override Module[] GetModules(bool getResourceModules)
        {
            return new[] { new FakeModule() };
        }
    }
}
