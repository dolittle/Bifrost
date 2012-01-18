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
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Bifrost.Execution;

namespace Bifrost.Services.Execution
{
	public class ContainerInstanceProvider : IInstanceProvider
	{
		IContainer _container;
		Type _serviceType;

		public ContainerInstanceProvider(IContainer container, Type serviceType)
		{
			_container = container;
			_serviceType = serviceType;
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return _container.Get(_serviceType);
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			return GetInstance(instanceContext, null);
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
		}
	}
}