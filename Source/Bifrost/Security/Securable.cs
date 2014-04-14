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
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a base implementation of<see cref="ISecurable"/>
    /// </summary>
    public class Securable : ISecurable
    {
        readonly List<ISecurityActor> _actors = new List<ISecurityActor>();

        /// <summary>
        /// Instantiates an instance of <see cref="Securable"/>
        /// </summary>
        /// <param name="securableDescription">Description of the Securable</param>
        public Securable(string securableDescription)
        {
            Description = securableDescription ?? string.Empty;
        }

#pragma warning disable 1591 // Xml Comments

        public void AddActor(ISecurityActor actor)
        {
            _actors.Add(actor);
        }

        public IEnumerable<ISecurityActor> Actors { get { return _actors;  } }

        public virtual bool CanAuthorize(object actionToAuthorize)
        {
            return false;
        }

        public virtual AuthorizeSecurableResult Authorize(object actionToAuthorize)
        {
            var result = new AuthorizeSecurableResult(this);
            foreach (var actor in _actors)
            {
                result.ProcessAuthorizeActorResult(actor.IsAuthorized(actionToAuthorize));
            }
            return result;
        }

        public string Description { get; private set; }
#pragma warning restore 1591 // Xml Comments

    }
}
