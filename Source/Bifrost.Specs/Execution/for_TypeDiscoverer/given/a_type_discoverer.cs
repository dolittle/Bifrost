//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Runtime.InteropServices;
//using Bifrost.Execution;
//using Machine.Specifications;
//using Moq;

//namespace Bifrost.Specs.Execution.for_TypeDiscoverer.given
//{
//    public class a_type_discoverer
//    {
//        protected static TypeDiscoverer type_discoverer;
//        protected static Mock<_Assembly> assembly_mock;
//        protected static Type[] types;

//        protected static Mock<IAssemblies> assemblies_mock;
//        protected static Mock<ITypeFinder> type_finder_mock;

//        Establish context = () =>
//                                {
//                                    types = new[] {
//                                        typeof(ISingle),
//                                        typeof(Single),
//                                        typeof(IMultiple),
//                                        typeof(FirstMultiple),
//                                        typeof(SecondMultiple)
//                                    };

//                                    assembly_mock = new Mock<_Assembly>();
//                                    assembly_mock.Setup(a => a.GetTypes()).Returns(types);
//                                    assembly_mock.Setup(a => a.FullName).Returns("A.Full.Name");

//                                    assemblies_mock = new Mock<IAssemblies>();
//                                    assemblies_mock.Setup(x => x.GetAll()).Returns(new[] { assembly_mock.Object });

//                                    type_finder_mock = new Mock<ITypeFinder>();
//                                    type_discoverer = new TypeDiscoverer(assemblies_mock.Object, type_finder_mock.Object);
//                                };
//    }
//}
