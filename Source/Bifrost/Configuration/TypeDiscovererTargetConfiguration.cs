using System.Collections.Generic;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Ad
    /// </summary>
    public class TypeDiscovererTargetConfiguration : ITypeDiscovererTargetConfiguration
    {
        private IList<string> _assemblyNames;
        
        /// <summary>
        /// Createa a new instance
        /// </summary>
        public TypeDiscovererTargetConfiguration()
        {
            _assemblyNames = new List<string>();
        }

        /// <summary>
        /// Add approved assembly name
        /// </summary>
        /// <param name="name">Full/partial assembly name</param>
        public void AddAssembly(string name)
        {
            _assemblyNames.Add(name);   
        }
        
        /// <summary>
        /// Initizlize the configuration
        /// </summary>
        /// <param name="container"></param>
        public void Initialize(IContainer container)
        {
            _assemblyNames.ForEach(TypeDiscoverer.AddAssembly);
        }
    }
}
