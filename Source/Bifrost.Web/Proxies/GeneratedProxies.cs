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
using System.Text;
using Bifrost.Execution;

namespace Bifrost.Web.Proxies
{
    [Singleton]
    public class GeneratedProxies
    {
        public GeneratedProxies(
            CommandProxies commandProxies,
            QueryProxies queryProxies,
            ReadModelProxies readModelProxies)
        {
            CommandProxies = commandProxies.Generate();
            ReadModelProxies = readModelProxies.Generate();
            QueryProxies = queryProxies.Generate();

            var builder = new StringBuilder();
            builder.Append(CommandProxies);
            builder.Append(ReadModelProxies);
            builder.Append(QueryProxies);
            All = builder.ToString();
        }


        public string CommandProxies { get; private set; }
        public string ReadModelProxies { get; private set; }
        public string QueryProxies { get; private set; }

        public string All { get; private set; }

    }
}
