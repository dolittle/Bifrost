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

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a thread safe context for the current callpath
    /// </summary>
    public interface ICallContext
    {
        /// <summary>
        /// Check if data exists for a given key
        /// </summary>
        /// <param name="key">Key to check if data exists for</param>
        /// <returns>True if exists, false if not</returns>
        bool HasData(string key);

        /// <summary>
        /// Get data with a specific key
        /// </summary>
        /// <typeparam name="T">Type of data you're getting</typeparam>
        /// <param name="key">Key representing the data</param>
        /// <returns>An instance of the data, if any</returns>
        T GetData<T>(string key);

        /// <summary>
        /// Set data for a given key
        /// </summary>
        /// <param name="key">Key to set for</param>
        /// <param name="data">Data to set</param>
        void SetData(string key, object data);
    }
}
