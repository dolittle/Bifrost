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

using System;
using System.Collections.Generic;
using System.Web;
using Bifrost.Sagas;

namespace Bifrost.Web.Sagas
{
	public class SagaLibrarian : ISagaLibrarian
	{
		readonly ISagaConverter _sagaConverter;

		public SagaLibrarian(ISagaConverter sagaConverter)
		{
			_sagaConverter = sagaConverter;
		}


		public void Close(ISaga saga)
		{
			HttpContext.Current.Session.Remove(saga.Id.ToString());
		}

		public void Catalogue(ISaga saga)
		{
			var sagaHolder = _sagaConverter.ToSagaHolder(saga);
			HttpContext.Current.Session[saga.Id.ToString()] = sagaHolder;
		}

		public ISaga Get(Guid id)
		{
			var sagaInSession = HttpContext.Current.Session[id.ToString()];
			var exists = sagaInSession != null;
			if (!exists)
				return null;

			var sagaHolder = sagaInSession as SagaHolder;
			var saga = _sagaConverter.ToSaga(sagaHolder);
			return saga;
		}

		public ISaga Get(string partition, string key)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ISaga> GetForPartition(string partition)
		{
			throw new NotImplementedException();
		}
	}
}
