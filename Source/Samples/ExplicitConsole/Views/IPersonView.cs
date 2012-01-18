using System;

namespace ExplicitConsole.Views
{
	public interface IPersonView
	{
		Person[] GetAll();
		Person Get(Guid id);
	}
}