/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
