#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
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

using Ninject;

namespace Bifrost.Mimir
{
	public class ViewModelLocator
	{
		public ViewModelLocator()
		{
			InitializeViewModels();
		}

		private void InitializeViewModels()
		{
			var properties = GetType().GetProperties();
			foreach( var property in properties )
			{
				var instance = App.Container.Get(property.PropertyType);
				property.SetValue(this, instance, null);
			}
		}


		public Features.Core.MainWindow.ViewModel MainWindow { get; private set; }
		public Features.Core.Navigation.ViewModel Navigation { get; private set; }
		public Features.Core.Toolbar.ViewModel Toolbar { get; private set; }

		public Features.Events.EventBrowser.ViewModel EventBrowser { get; private set; }
		public Features.Objects.ObjectHistory.ViewModel ObjectHistory { get; private set; }
		public Features.General.Pivot.ViewModel Pivot { get; private set; }
	}
}