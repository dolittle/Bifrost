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
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandTypeManager"/>
    /// </summary>
	public class CommandTypeManager : ICommandTypeManager
	{
        ITypeDiscoverer _typeDiscoverer;
        Dictionary<string, Type> _commandTypes = new Dictionary<string, Type>();
		
        /// <summary>
        /// Initializes a new instance of <see cref="CommandTypeManager"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to use for discovering <see cref="ICommand">commands</see></param>
		public CommandTypeManager (ITypeDiscoverer typeDiscoverer)
		{
			_typeDiscoverer = typeDiscoverer;
			PopulateCommandTypes();
		}

#pragma warning disable 1591 // Xml Comments
        public Type GetFromName (string name)
		{
			if( !_commandTypes.ContainsKey(name) )
				throw new UnknownCommandException(name);
			
			return _commandTypes[name];
		}
#pragma warning restore 1591 // Xml Comments

        void PopulateCommandTypes()
        {
            var commandTypes = _typeDiscoverer.FindMultiple<ICommand>();
            foreach (var commandType in commandTypes)
            {
                var name = commandType.Name;
                if (_commandTypes.ContainsKey(name))
                    throw new AmbiguousCommandException(_commandTypes[name], commandType);

                _commandTypes[name] = commandType;
            }
        }
	}
}

