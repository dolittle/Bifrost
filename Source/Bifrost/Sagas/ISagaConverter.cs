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

namespace Bifrost.Sagas
{
    /// <summary>
    /// Defines a converter for converting a <see cref="ISaga"/> to a <see cref="SagaHolder"/> and back
    /// </summary>
	public interface ISagaConverter
	{
        /// <summary>
        /// Convert a <see cref="SagaHolder"/> to a <see cref="ISaga"/>
        /// </summary>
        /// <param name="sagaHolder"><see cref="SagaHolder"/> to convert from</param>
        /// <returns>Converter <see cref="ISaga"/> in the correct type</returns>
		ISaga ToSaga(SagaHolder sagaHolder);

        /// <summary>
        /// Convert a <see cref="ISaga"/> to a <see cref="SagaHolder"/>
        /// </summary>
        /// <param name="saga"><see cref="ISaga"/> to convert from</param>
        /// <returns>A <see cref="SagaHolder"/> with the <see cref="ISaga"/> and its data serialized</returns>
		SagaHolder ToSagaHolder(ISaga saga);

        /// <summary>
        /// Populate an existing <see cref="SagaHolder"/> from a <see cref="ISaga"/>
        /// </summary>
        /// <param name="sagaHolder"><see cref="SagaHolder"/> to populate into</param>
        /// <param name="saga"><see cref="ISaga"/> to populate the <see cref="SagaHolder"/> with</param>
		void Populate(SagaHolder sagaHolder, ISaga saga);
	}
}