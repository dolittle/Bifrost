#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Collections.ObjectModel;

namespace Bifrost.Mimir.Features.Core.Navigation
{
	public class Category
	{
		public Category()
		{
			Pages = new ObservableCollection<FeaturePage>();
		}

		public string Name { get; set; }
		public string Namespace { get; set; }
		public string Icon
		{
			get
			{
				var icon = string.Format("/Bifrost.Administration;component/Features/{0}/icon.png", Name);
				return icon;
			}
		}
		public ObservableCollection<FeaturePage> Pages { get; private set; }

		public void AddPageFromType(Type type)
		{
			var page = new FeaturePage
			           	{
			           		Type = type,
			           		Name = GetSampleName(type),
							Category = Name,
			           		Description = GetSampleDescription(type)
			           	};
			Pages.Add(page);
		}

		private static string GetSampleName(Type type)
		{
			var ns = GetSampleNamespaceName(type);
			return ns;
		}

		private static string GetSampleNamespaceName(Type type)
		{
			var ns = type.Namespace;
			var lastDot = ns.LastIndexOf('.');
			var sampleNamespace = ns.Substring(lastDot + 1);
			return sampleNamespace;
		}

		private static string GetSampleDescription(Type type)
		{
			var ns = GetSampleNamespaceName(type);

			return string.Empty;
		}

	}
}