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
using System.Windows.Input;

namespace Bifrost.Interaction
{
	public delegate bool CanExecuteEventHandler<T>(T parameter);

	public delegate void ExecuteEventHandler<T>(T parameter);

	public delegate bool CanExecuteWithoutParameterEventHandler();

	public delegate void ExecuteWithoutParameterEventHandler();

	public class DelegateCommand<T> : DelegateCommand
	{
		public ExecuteEventHandler<T> ExecuteEventHandler { get; private set; }
		public CanExecuteEventHandler<T> CanExecuteEventHandler { get; private set; }

		public DelegateCommand(ExecuteEventHandler<T> executeEventHandler)
			: this(executeEventHandler, null)
		{

		}

		public DelegateCommand(ExecuteEventHandler<T> executeEventHandler, CanExecuteEventHandler<T> canExecuteEventHandler)
		{
			ExecuteEventHandler = executeEventHandler;
			CanExecuteEventHandler = canExecuteEventHandler;
		}


		public override bool CanExecute(object parameter)
		{
			if (null == CanExecuteEventHandler)
			{
				return true;
			}
			return CanExecuteEventHandler((T)parameter);
		}

		public override void Execute(object parameter)
		{
			if (null != ExecuteEventHandler)
			{
				ExecuteEventHandler((T)parameter);
			}
		}

		public override string ExecuteTargetName
		{
			get
			{
				return ExecuteEventHandler.Method.Name;
			}
		}

		public override string CanExecuteTargetName
		{
			get
			{
				return CanExecuteEventHandler.Method.Name;
			}
		}

		public override Type ExecuteTargetType
		{
			get
			{
				return ExecuteEventHandler.Target.GetType();
			}
		}

		public override Type CanExecuteTargetType
		{
			get
			{
				return CanExecuteEventHandler.Target.GetType();
			}
		}

	}

	public class DelegateCommand : ICommand
	{
		public ExecuteWithoutParameterEventHandler ExecuteWithoutParameterEventHandler { get; private set; }
		public CanExecuteWithoutParameterEventHandler CanExecuteWithoutParameterEventHandler { get; private set; }

		protected DelegateCommand()
		{
		}

		public DelegateCommand(ExecuteWithoutParameterEventHandler executeWithoutParameterEventHandler)
			: this(executeWithoutParameterEventHandler, null)
		{

		}

		public DelegateCommand(ExecuteWithoutParameterEventHandler executeWithoutParameterEventHandler,
		                       CanExecuteWithoutParameterEventHandler canExecuteWithoutParameterEventHandler)
		{
			ExecuteWithoutParameterEventHandler = executeWithoutParameterEventHandler;
			CanExecuteWithoutParameterEventHandler = canExecuteWithoutParameterEventHandler;
		}

		public event EventHandler CanExecuteChanged;
		public virtual bool CanExecute(object parameter)
		{
			if (null == CanExecuteWithoutParameterEventHandler)
			{
				return true;
			}
			return CanExecuteWithoutParameterEventHandler();
		}

		public virtual void Execute(object parameter)
		{
			if (null != ExecuteWithoutParameterEventHandler)
			{
				ExecuteWithoutParameterEventHandler();
			}
		}


		protected void OnCanExecuteChanged()
		{
			if (null != CanExecuteChanged)
			{
				CanExecuteChanged(this, new EventArgs());
			}
		}


		public static DelegateCommand	Create(ExecuteEventHandler<object> execute)
		{
			return new DelegateCommand<object>(execute);
		}

		public static DelegateCommand Create(ExecuteEventHandler<object> execute, CanExecuteEventHandler<object> canExecute)
		{
			return new DelegateCommand<object>(execute, canExecute);
		}

		public static DelegateCommand Create(ExecuteWithoutParameterEventHandler execute)
		{
			return new DelegateCommand(execute);

		}

		public static DelegateCommand Create(ExecuteWithoutParameterEventHandler execute, CanExecuteWithoutParameterEventHandler canExecute)
		{
			return new DelegateCommand(execute,canExecute);
		}

		public static DelegateCommand Create<T>(ExecuteEventHandler<T> execute)
		{
			return new DelegateCommand<T>(execute);
		}

		public static DelegateCommand Create<T>(ExecuteEventHandler<T> execute, CanExecuteEventHandler<T> canExecute)
		{
			return new DelegateCommand<T>(execute, canExecute);
		}


		public virtual string ExecuteTargetName
		{
			get
			{
				return ExecuteWithoutParameterEventHandler.Method.Name;
			}
		}

		public virtual string CanExecuteTargetName
		{
			get
			{
				return CanExecuteWithoutParameterEventHandler.Method.Name;
			}
		}

		public virtual Type ExecuteTargetType
		{
			get
			{
				return ExecuteWithoutParameterEventHandler.Target.GetType();
			}
		}

		public virtual Type CanExecuteTargetType
		{
			get
			{
				return CanExecuteWithoutParameterEventHandler.Target.GetType();
			}
		}
	}
}
