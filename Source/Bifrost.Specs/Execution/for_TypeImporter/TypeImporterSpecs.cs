using System;
using Bifrost.Execution;
using Moq;
using NUnit.Framework;

namespace Bifrost.Specs.Execution.for_TypeImporter
{
	[TestFixture]
	public class TypeImporterSpecs
	{
		[Test]
		public void ImportingMultipleShouldReturnAllInstances()
		{
		    var containerMock = new Mock<IContainer>();
			var typeDiscovererMock = new Mock<ITypeDiscoverer>();
			typeDiscovererMock.Setup(t => t.FindMultiple<IMultipleInterface>()).Returns(new[]
			                                                                            	{
																								typeof(FirstMultipleClass),
																								typeof(SecondMultipleClass)
			                                                                            	});
			var typeImporter = new TypeImporter(containerMock.Object, typeDiscovererMock.Object);
			var instances = typeImporter.ImportMany<IMultipleInterface>();
			Assert.That(instances.Length, Is.EqualTo(2));
			Assert.That(instances[0], Is.InstanceOf<FirstMultipleClass>());
			Assert.That(instances[1], Is.InstanceOf<SecondMultipleClass>());
		}

		[Test]
		public void ImportingMultipleAndThereIsOnlyOneShouldReturnThatInstance()
		{
            var containerMock = new Mock<IContainer>();
			var typeDiscovererMock = new Mock<ITypeDiscoverer>();
			typeDiscovererMock.Setup(t => t.FindMultiple<ISingleInterface>()).Returns(new[]
			                                                                            	{
																								typeof(SingleClass),
			                                                                            	});
			var typeImporter = new TypeImporter(containerMock.Object, typeDiscovererMock.Object);
			var instances = typeImporter.ImportMany<ISingleInterface>();
			Assert.That(instances.Length, Is.EqualTo(1));
			Assert.That(instances[0], Is.InstanceOf<SingleClass>());
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void ImportingMultipleAndDiscovererReturnsNullShouldCauseAnArgumentException()
		{
            var containerMock = new Mock<IContainer>();
			var typeDiscovererMock = new Mock<ITypeDiscoverer>();
			typeDiscovererMock.Setup(t => t.FindMultiple<IMultipleInterface>()).Returns((Type[])null);
			var typeImporter = new TypeImporter(containerMock.Object, typeDiscovererMock.Object);
			typeImporter.ImportMany<IMultipleInterface>();
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void ImportingMultipleAndDiscovererThrowExceptionShouldCauseAnArgumentException()
		{
            var containerMock = new Mock<IContainer>();
			var typeDiscovererMock = new Mock<ITypeDiscoverer>();
			typeDiscovererMock.Setup(t => t.FindMultiple<IMultipleInterface>()).Throws(new ArgumentException());

			var typeImporter = new TypeImporter(containerMock.Object, typeDiscovererMock.Object);
			typeImporter.ImportMany<IMultipleInterface>();
		}
	}
}
