#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using Bifrost.Execution;
namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Represents an implementation of <see cref="IQueryValidationDescriptors"/> 
    /// </summary>
    public class QueryValidationDescriptors : IQueryValidationDescriptors
    {
        /// <summary>
        /// Initializes an instance of <see cref="QueryValidationDescriptors"/>
        /// </summary>
        public QueryValidationDescriptors(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            var descriptors = typeDiscoverer.FindMultiple(typeof(QueryValidationDescriptorFor<>));
        }


#pragma warning disable 1591 // Xml Comments
        public bool HasDescriptorFor<TQuery>() where TQuery : IQuery
        {
            return false; 
            //throw new System.NotImplementedException();
        }

        public IQueryValidationDescriptor GetDescriptorFor<TQuery>() where TQuery : IQuery
        {
            throw new System.NotImplementedException();
        }
#pragma warning restore 1591 // Xml Comments
    }
}
