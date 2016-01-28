#region License

//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Web.Mvc;
using Bifrost.Serialization;

namespace Bifrost.Web.Mvc
{
    /// <summary>
    /// Represents an <see cref="ITempDataProvider">ITempDataProvider</see>.
    /// </summary>
    public class BifrostTempDataProvider : ITempDataProvider
    {
        const string TEMP_DATA_SESSION_STATE_KEY = "__BifrostTempDataSessionStateKey";
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="BifrostTempDataProvider"/>
        /// </summary>
        /// <param name="serializer">A <see cref="ISerializer"/> used to serialize to and from the session</param>
        public BifrostTempDataProvider(ISerializer serializer)
        {
            _serializer = serializer;
        }

#pragma warning disable 1591 // Xml Comments
        public IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            var httpContext = controllerContext.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("Cannot use temp data without a valid HttpContext");
            }

            var session = httpContext.Session;
            if (session == null)
            {
                throw new InvalidOperationException("Cannot use temp data without a valid Session");
            }

            var serializedTempData = session[TEMP_DATA_SESSION_STATE_KEY] as string;

            if (serializedTempData == null)
                return new Dictionary<string, object>();

            return _serializer.FromJson<Dictionary<string, object>>(serializedTempData, SerializationOptions.IncludeTypeNames);
        }

        public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            var httpContext = controllerContext.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("Cannot use temp data without HttpContext");
            }

            var session = controllerContext.HttpContext.Session;
            if (session == null)
            {
                throw new InvalidOperationException("Cannot use temp data without session");
            }

            var hasValues = values != null && values.Count > 0;

            if (hasValues)
            {
                var serializedTempData = _serializer.ToJson(values, SerializationOptions.IncludeTypeNames);
                session[TEMP_DATA_SESSION_STATE_KEY] = serializedTempData;
                return;
            }
            if (session[TEMP_DATA_SESSION_STATE_KEY] != null)
            {
                session.Remove(TEMP_DATA_SESSION_STATE_KEY);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}