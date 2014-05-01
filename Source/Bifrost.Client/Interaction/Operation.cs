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
namespace Bifrost.Interaction
{
    /// <summary>
    /// Represents an operation in the system
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Perform the operation
        /// </summary>
        /// <param name="context"><see cref="OperationContext"/> in which the operation is performed in</param>
        /// <returns>Any state as a result of performing the operation</returns>
        public virtual OperationState Perform(OperationContext context)
        {
            return null;
        }

        /// <summary>
        /// Undo the given operation based on the state coming out of the perform
        /// </summary>
        /// <param name="context"><see cref="OperationContext"/> in which the operation was performed in</param>
        /// <param name="state">State as a result from when it was performed</param>
        public virtual void Undo(OperationContext context, OperationState state)
        {
        }
    }
}
