using Bifrost.Events;
using NUnit.Framework;

namespace Bifrost.Specs.Events
{
	[TestFixture]
	public class MethodEventArgumentsSpecs
	{
		[Test]
		public void SettingArgumentByIndexingShouldReturnItDynamically()
		{
			var expected = "Else";
			dynamic arguments = new MethodEventArguments();
			arguments["Something"] = expected;
			Assert.That(arguments.Something, Is.EqualTo(expected));
		}

		[Test]
		public void SettingArgumentDynamicallyShouldReturnItWhenIndexingIt()
		{
			var expected = "Else";
			dynamic arguments = new MethodEventArguments();
			arguments.Something = expected;
			Assert.That(arguments["Something"], Is.EqualTo(expected));
		}
	}
}
