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
using System.Linq;
using System.Text;

namespace Bifrost.Web.Services
{
    public class HttpStatus
    {
        public class HttpStatusException : Exception
        {
            public HttpStatusException(int code, string description)
            {
                Code = code;
                Description = description;
            }

            public int Code { get; private set; }
            public string Description { get; private set; }
        }

        public static void NotFound(string description = "Not found")
        {
            throw new HttpStatusException(404, description);
        }
    }
}
