/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Web.Configuration;

namespace Bifrost.Web.Assets
{
    [Singleton]
    public class AssetsManager : IAssetsManager
    {
        Dictionary<string, List<string>> _assetsByExtension = new Dictionary<string, List<string>>();
        private WebConfiguration _webConfiguration;
        
        public AssetsManager(WebConfiguration webConfiguration)
        {
            _webConfiguration = webConfiguration;
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
            var root = _webConfiguration.ApplicationPhysicalPath;
            var files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var relativePath = FormatPath(file.Replace(root, string.Empty));
                
                if (!_webConfiguration.Assets.PathsToExclude.Any(relativePath.StartsWith)) { 
                    AddAsset(relativePath);
                }
            }

        }

        public void AddAsset(string relativePath)
        {
            var extension = Path.GetExtension(relativePath);
            if (relativePath.StartsWith("/") || relativePath.StartsWith("\\")) relativePath = relativePath.Substring(1);

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
