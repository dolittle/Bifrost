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

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IApplicationManager"/>
    /// </summary>
    [Singleton]
    public class ApplicationManager : IApplicationManager
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        IApplication _application;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationManager"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to use for discovering an application</param>
        /// <param name="container"><see cref="IContainer"/> to use for instantiating an application</param>
        public ApplicationManager(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
        }

#pragma warning disable 1591 // Xml Comments
        public IApplication Get()
        {
            if (_application != null)
                return _application;

            Type applicationType = null;
            try
            {
                applicationType = _typeDiscoverer.FindSingle<IApplication>();
            } catch( MultipleTypesFoundException )
            {
                throw new MultipleApplicationsFoundException();
            }
            if( applicationType == null )
                throw new ApplicationNotFoundException();

            _application = _container.Get(applicationType) as IApplication;
            return _application;
        }

        public void Set(IApplication application)
        {
            _application = application;
        }
#pragma warning restore 1591 // Xml Comments

    }
}