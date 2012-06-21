﻿using System;
using Bifrost.Specs.Resources.for_Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Resources.for_ResourcePropertiesResolver
{
    [Subject(Subjects.resolving)]
    public class when_resolving_properties_implemented_as_resource : given.a_resource_properties_resolver
    {
        static ClassWithResourceProperties InstanceToResolvePropertiesOn;

        Because of = () =>
                         {
                             InstanceToResolvePropertiesOn = new ClassWithResourceProperties();
                             resolver.ResolvePropertiesFor(InstanceToResolvePropertiesOn);
                         };

        It should_resolve_properties = () => container_mock.Verify(s => s.Get(Moq.It.IsAny<Type>()), Times.Once());
        It should_set_property_to_resolved_instance = () => InstanceToResolvePropertiesOn.Resources.ShouldEqual(resolved_instance);
    }
}