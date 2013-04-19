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
using Bifrost.Sagas;

namespace Bifrost.Web.Sagas
{
    public class SagaNarratorService
    {
        ISagaLibrarian _sagaLibrarian;
        ISagaNarrator _sagaNarrator;

        public SagaNarratorService(
            ISagaLibrarian sagaLibrarian,
            ISagaNarrator sagaNarrator
            )
        {
            _sagaLibrarian = sagaLibrarian;
            _sagaNarrator = sagaNarrator;
        }

        public SagaConclusion Conclude(Guid sagaId)
        {
            var saga = _sagaLibrarian.Get(sagaId);
            var conclusion = _sagaNarrator.Conclude(saga);
            return conclusion;
        }
    }
}
