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


using System;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a holder for a <see cref="ISaga"/> for persisting purposes
    /// </summary>
    public class SagaHolder
    {
        /// <summary>
        /// Gets or sets the Id of the actual <see cref="ISaga"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Partition in which the <see cref="ISaga"/> belongs to
        /// </summary>
        public string Partition { get; set; }

        /// <summary>
        /// Gets or sets the Key that represents the <see cref="ISaga"/> within a partition
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the name of the <see cref="ISaga"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the <see cref="ISaga"/>
        /// </summary>
        /// <remarks>
        /// Fully qualified type string
        /// </remarks>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SagaState"/> represented as string
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the serialized version of the <see cref="ISaga"/>
        /// </summary>
        public string SerializedSaga { get; set; }

        /// <summary>
        /// Gets or sets the current chapters type
        /// </summary>
        /// <remarks>
        /// Fully qualified type string
        /// </remarks>
        public string CurrentChapterType { get; set; }

        /// <summary>
        /// Gets or sets the chapters as serialized data
        /// </summary>
        public string SerializedChapters { get; set; }

        /// <summary>
        /// Gets or sets the uncommited events as serialized data
        /// </summary>
        public string UncommittedEvents { get; set; }
    }
}
