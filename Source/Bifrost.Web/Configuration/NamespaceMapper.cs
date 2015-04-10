#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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

using System;
using Bifrost.Execution;
using Bifrost.Utils;

namespace Bifrost.Web.Configuration
{
    public class NamespaceMapper
    {
        readonly StringMapper _clientToServerMapper = new StringMapper();
        readonly StringMapper _serverToClientMapper = new StringMapper();
        readonly ITypeDiscoverer _typeDiscoverer;

        public NamespaceMapper(ITypeDiscoverer typeDiscoverer)
        {
            _typeDiscoverer = typeDiscoverer;
            _clientToServerMapper = new StringMapper();
            _serverToClientMapper = new StringMapper();
        }

        public void Add(string clientNamespace, string serverNamespace)
        {
            _clientToServerMapper.AddMapping(clientNamespace,serverNamespace);
            _serverToClientMapper.AddMapping(serverNamespace,clientNamespace);
        }

        public bool CanResolveToClient(string serverNamespace)
        {
            return _serverToClientMapper.HasMappingFor(serverNamespace);
        }

        public bool CanResolveToServer(string clientNamespace)
        {
            return _clientToServerMapper.HasMappingFor(clientNamespace);
        }

        public virtual Type GetServerTypeFrom(string fullyQualifiedClientName)
        {
            var mappers = _clientToServerMapper.GetAllMatchingMappingsFor(fullyQualifiedClientName);
            foreach (var mapper in mappers)
            {
                var fullyQualifiedName = mapper.Resolve(fullyQualifiedClientName);
                var matchingType = _typeDiscoverer.FindTypeByFullName(fullyQualifiedName);
                if (matchingType != null)
                    return matchingType;
            }
            return null;
        }

        public virtual string GetClientFullyQualifiedNameFrom(Type type)
        {
            return _serverToClientMapper.Resolve(type.FullName);
        }

        public virtual string GetClientNamespaceFrom(string serverNamespace)
        {
            return _serverToClientMapper.Resolve(serverNamespace);
        }
    }
}
