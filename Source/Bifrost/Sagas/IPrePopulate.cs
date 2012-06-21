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
using Bifrost.Commands;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Pre-population of commands of CommandType sent to the Saga of the given SagaType
    /// </summary>
    /// <typeparam name="SagaType">The type of <see cref="ISaga"/> to pre populate for</typeparam>
    /// <typeparam name="CommandType">The type of <see cref="ICommand"/> to pre populate</typeparam>
    public interface IPrePopulate<in SagaType, in CommandType>
        where SagaType : ISaga
        where CommandType : ICommand
    {
        /// <summary>
        /// Modifies the command based on state in the saga.
        /// </summary>
        /// <param name="saga">The saga holding state the command must get</param>
        /// <param name="command">The incoming, perhaps incomplete, command</param>
        void PrePopulate(SagaType saga, CommandType command);

    }
}