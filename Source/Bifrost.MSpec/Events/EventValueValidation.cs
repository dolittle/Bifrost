/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.MSpec.Events
{
    public class EventValueValidation<T> where T : IEvent
    {
        readonly T _event;

        public EventValueValidation(T @event)
        {
            _event = @event;
        }

        public void WithValues(params Func<T, bool>[] expectedValues)
        {
            foreach (var expectedValue in expectedValues)
            {
                expectedValue(_event).ShouldBeTrue();
            }
        }

        public void Where(params Action<T>[] validators)
        {
            foreach (var validator in validators)
            {
                validator(_event);
            }
            
        }
    }
}