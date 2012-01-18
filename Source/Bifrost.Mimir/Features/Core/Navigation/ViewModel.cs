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
using System.Linq;

namespace Bifrost.Mimir.Features.Core.Navigation
{
	public class ViewModel
	{
		public ViewModel()
		{
			Categories = new ObservableCollection<Category>();
			Populate();
		}


		private void Populate()
		{
			var types = typeof(ViewModel).Assembly.GetTypes();
			var query = from t in types
						where t.Name.Equals("View") && !t.Namespace.Contains("Core")
						select t;
			foreach (var type in query)
			{
				AddType(type);
			}
		}

		private void AddType(Type type)
		{
			var category = GetOrAddCategoryFromType(type);
			category.AddPageFromType(type);
		}


		private Category GetOrAddCategoryFromType(Type type)
		{
			var categoryName = GetCategoryName(type);
			foreach (var existingCategory in Categories)
			{
				if (existingCategory.Name.Equals(categoryName))
				{
					return existingCategory;
				}
			}
			var categoryNamespace = GetCategoryNamespace(type);

			var category = new Category
							{
								Name = categoryName,
								Namespace = categoryNamespace
							};
			Categories.Add(category);
			return category;
		}

		private static string GetCategoryNamespace(Type type)
		{
			var ns = type.Namespace;
			var lastDot = ns.LastIndexOf('.');
			var nsWithoutSampleName = ns.Substring(0, lastDot);
			lastDot = nsWithoutSampleName.LastIndexOf('.');
			var categoryNamespace = nsWithoutSampleName.Substring(0,lastDot);

			return categoryNamespace;
			
		}

		private static string GetCategoryName(Type type)
		{
			var ns = type.Namespace;
			var lastDot = ns.LastIndexOf('.');
			var nsWithoutSampleName = ns.Substring(0, lastDot);
			lastDot = nsWithoutSampleName.LastIndexOf('.');
			var category = nsWithoutSampleName.Substring(lastDot + 1);

			return category;
		}

		public ObservableCollection<Category> Categories { get; set; }
	}

}
