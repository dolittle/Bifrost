/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Concepts;
using Bifrost.NHibernate.UserTypes;


namespace Bifrost.NHibernate.Concepts
{
    public class ConceptValueType<T,U> : ImmutableUserType<T> where T : ConceptAs<U>
    {
        public ConceptValueType()
        {
            MapProperty(concept => concept.Value);
        }

        public ConceptValueType(NullSafeMapping mapping)
        {
            MapProperty(concept => concept.Value, mapping);
        }
    }
}
