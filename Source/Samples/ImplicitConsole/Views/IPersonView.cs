using System;

namespace ImplicitConsole.Views
{
	public interface IPersonView
	{
		Person[] GetAll();
		Person Get(Guid id);
	}   
}