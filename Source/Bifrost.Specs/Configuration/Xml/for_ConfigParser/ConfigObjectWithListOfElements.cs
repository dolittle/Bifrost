using System.Collections.Generic;
using Bifrost.Configuration.Xml;

namespace Bifrost.Specs.Configuration.Xml.for_ConfigParser
{
    public class ConfigObjectWithListOfElements : IConfigElement
    {
        public IEnumerable<ConfigObjectWithConfigObjectWithString> Elements { get; set; }
    }
}