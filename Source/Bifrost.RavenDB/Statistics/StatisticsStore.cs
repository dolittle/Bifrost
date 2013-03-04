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
using Bifrost.RavenDB.Serialization;
using Bifrost.Statistics;
using Raven.Client.Document;
using System;
using Bifrost.Extensions;

namespace Bifrost.RavenDB.Statistics
{
    public class StatisticsStore : IStatisticsStore
    {
        const string CollectionName = "Statistics";
        StatisticsStoreConfiguration _configuration;
        DocumentStore _documentStore;

        public StatisticsStore(StatisticsStoreConfiguration configuration)
        {
            _configuration = configuration;
            InitializeDocumentStore();
        }

        void InitializeDocumentStore()
        {
            _documentStore = _configuration.CreateDocumentStore();

            var keyGenerator = new SequentialKeyGenerator(_documentStore);
            _documentStore.Conventions.DocumentKeyGenerator = (a, b, c) => string.Format("{0}/{1}", CollectionName, keyGenerator.NextFor<IStatistic>());

            _documentStore.Conventions.CustomizeJsonSerializer = s =>
            {
                s.Converters.Add(new ConceptConverter());
            };
            
           var originalFindTypeTagNam =  _documentStore.Conventions.FindTypeTagName;
           _documentStore.Conventions.FindTypeTagName = t =>
           {
               if (t.HasInterface<IStatistic>() || t == typeof(IStatistic)) return CollectionName;
               return originalFindTypeTagNam(t);
           };
        }

        public void Add(IStatistic statistic)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Store(statistic);
                session.SaveChanges();
            }
        }
    }
}
