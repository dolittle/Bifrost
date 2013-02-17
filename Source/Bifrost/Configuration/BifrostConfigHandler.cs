#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using Bifrost.Configuration.Xml;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Represents a <see cref="IConfigurationSectionHandler"/> for App.config/Web.config configuration sections
	/// </summary>
    public class BifrostConfigHandler : IConfigurationSectionHandler
    {
        readonly ConfigParser _parser;

        /// <summary>
        /// Initializes a new instance of <see cref="BifrostConfigHandler"/>
        /// </summary>
        public BifrostConfigHandler() : this(Configure.Instance.Container.Get<ConfigParser>())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BifrostConfigHandler"/>
        /// </summary>
        /// <param name="parser"><see cref="ConfigParser"/> to use for parsing</param>
        public BifrostConfigHandler(ConfigParser parser)
        {
            _parser = parser;
        }

#pragma warning disable 1591 // Xml Comments
        public object Create(object parent, object configContext, XmlNode section)
        {
        	var document = XDocument.Parse(section.OuterXml);
            var config = _parser.Parse<BifrostConfig>(document);
            return config;
        }
#pragma warning restore 1591 // Xml Comments
    }
}