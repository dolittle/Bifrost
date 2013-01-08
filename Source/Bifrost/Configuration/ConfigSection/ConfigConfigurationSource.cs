#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System.Reflection;
using Bifrost.Configuration.Xml;
using Bifrost.Entities;

namespace Bifrost.Configuration.ConfigSection
{
	/// <summary>
	/// Represents an implementation of <see cref="IConfigurationSource"/> that works with App.config/web.config
	/// </summary>
	public class ConfigConfigurationSource : IConfigurationSource
    {
		readonly IConfigurationManager _configurationManager;

		/// <summary>
		/// Initializes an instance of <see cref="ConfigConfigurationSource"/>
		/// </summary>
		/// <param name="configurationManager"><see cref="IConfigurationManager"/> that is used to get the config section from</param>
		public ConfigConfigurationSource(IConfigurationManager configurationManager)
		{
			_configurationManager = configurationManager;
		}

#pragma warning disable 1591 // Xml Comments
		public void Initialize(Configure configure)
        {
            var section = _configurationManager.GetSection("BifrostConfig") as BifrostConfig;
            if( section != null )
            {
                if( section.Commands != null )
                {
                    
                }
                if( section.DefaultStorage != null )
                {
                    var configuration = section.DefaultStorage.GetConfiguration();
                    configure.Container.Bind<IEntityContextConfiguration>(configuration);
                    configure.Container.Bind(configuration.Connection.GetType(), configuration.Connection);
                    configure.Container.Bind(typeof(IEntityContext<>), section.DefaultStorage.EntityContextType);
                }
				if( section.Sagas != null )
				{
					configure.Sagas.LibrarianType = section.Sagas.LibrarianType;
				}
            }
		}
#pragma warning restore 1591 // Xml Comments
	}
}
