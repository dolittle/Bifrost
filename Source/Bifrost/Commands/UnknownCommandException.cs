using System;

namespace Bifrost.Commands
{
	public class UnknownCommandException : ArgumentException
	{
		public UnknownCommandException (string name) : base("There is no command called : "+name)
		{
		}
	}
}

