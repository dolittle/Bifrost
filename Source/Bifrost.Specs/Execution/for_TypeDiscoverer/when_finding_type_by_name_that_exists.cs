//using System;
//using System.Collections.Generic;
//using Bifrost.Execution;
//using Machine.Specifications;

//namespace Bifrost.Specs.Execution.for_TypeDiscoverer
//{
//    [Subject(typeof(TypeDiscoverer))]
//    public class when_finding_type_by_name_that_exists : given.a_type_discoverer
//    {
//        static Type type_found;

//        Establish context = () => type_finder_mock.Setup(t => t.FindTypeByFullName(Moq.It.IsAny<IEnumerable<Type>>(), Moq.It.IsAny<string>())).Returns(typeof(Single));

//        Because of = () => type_found = type_discoverer.FindTypeByFullName(typeof(Single).FullName);

//        It should_not_return_null = () => type_found.ShouldNotBeNull();
//        It should_return_the_correct_type = () => type_found.ShouldEqual(typeof(Single));
//    }
//}