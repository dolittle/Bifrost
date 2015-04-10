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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Bifrost.Execution;

namespace Bifrost.Web.Assets
{
    [Singleton]
    public class AssetsManager : IAssetsManager
    {
        Dictionary<string, List<string>> _assetsByExtension = new Dictionary<string, List<string>>();

        public AssetsManager()
        {
            Initialize();

        }

        public IEnumerable<string> GetFilesForExtension(string extension)
        {
            extension = MakeSureExtensionIsPrefixedWithADot(extension);
            if (!_assetsByExtension.ContainsKey(extension)) return new string[0];
            var assets = _assetsByExtension[extension];
            return assets;
        }

        public IEnumerable<string> GetStructureForExtension(string extension)
        {
            extension = MakeSureExtensionIsPrefixedWithADot(extension);
            if (!_assetsByExtension.ContainsKey(extension)) return new string[0];
            var assets = _assetsByExtension[extension];
            return assets.Select(a => FormatPath(Path.GetDirectoryName(a))).Distinct().ToArray();
        }

        string MakeSureExtensionIsPrefixedWithADot(string extension)
        {
            if (!extension.StartsWith("."))
                return "." + extension;

            return extension;
        }

        string FormatPath(string input)
        {
            return input.Replace("\\", "/");
        }


        void Initialize()
        {
            var root = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var relativePath = FormatPath(file.Replace(root, string.Empty));
                AddAsset(relativePath);
            }

        }

        public void AddAsset(string relativePath)
        {
            var extension = Path.GetExtension(relativePath);

            List<string> assets;
            if (!_assetsByExtension.ContainsKey(extension))
            {
                assets = new List<string>();
                _assetsByExtension[extension] = assets;
            }
            else
                assets = _assetsByExtension[extension];

            assets.Add(relativePath);
        }
    }
}
