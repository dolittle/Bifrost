/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Concepts;
using FluentNHibernate.Mapping;

namespace Bifrost.NHibernate.Concepts
{
    /// <summary>
    /// Extends the FluentNHibernate fluent interface
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Uses the correct nhibernate custom mapping type for an Identity Property that is based on a Concept
        /// </summary>
        /// <typeparam name="T">Concrete type of the concept, that inherits from <see cref="ConceptAs{U}"/></typeparam>
        /// <typeparam name="U">The primitive that is the concept is based on</typeparam>
        /// <param name="identityPart">Fluent NHibernate IdentityPart</param>
        /// <returns>Fluent NHibernate IdentityPart<</returns>
        public static IdentityPart ConceptOf<T, U>(this IdentityPart identityPart)
            where T : ConceptAs<U>
        {
            identityPart.CustomType<ConceptValueType<T,U>>();
            return identityPart;
        }


        /// <summary>
        /// Uses the correct nhibernate custom mappingtype for an Identity Property that is based on a Concept{Guid}
        /// </summary>
        /// <typeparam name="T">Concrete type of the concept, that inherits from <see cref="ConceptAs{Guid}"/></typeparam>
        /// <param name="identityPart">Fluent NHibernate IdentityPart</param>
        /// <returns>Fluent NHibernate IdentityPart</returns>
        public static IdentityPart ConceptAsOracleGuid<T>(this IdentityPart identityPart)
            where T : ConceptAs<Guid>
        {
            identityPart.CustomType<ConceptAsOracleGuid<T>>();
            return identityPart;
        }

        /// <summary>
        /// Uses the correct nhibernate custom mapping type for a Property that is based on a Concept
        /// </summary>
        /// <typeparam name="T">Concrete type of the concept, that inherits from <see cref="ConceptAs{U}"/></typeparam>
        /// <typeparam name="U">The primitive that is the concept is based on</typeparam>
        /// <param name="propertyPart">Fluent NHibernate PropertyPart</param>
        /// <returns>Fluent NHibernate PropertyPart<</returns>
        public static PropertyPart ConceptOf<T, U>(this PropertyPart propertyPart)
            where T : ConceptAs<U>
        {
            propertyPart.CustomType<ConceptValueType<T,U>>();
            return propertyPart;
        }

        /// <summary>
        /// Uses the correct nhibernate custom mappingtype for a Property that is based on a Concept{Guid}
        /// </summary>
        /// <typeparam name="T">Concrete type of the concept, that inherits from <see cref="ConceptAs{Guid}"/></typeparam>
        /// <param name="identityPart">Fluent NHibernate PropertyPart</param>
        /// <returns>Fluent NHibernate PropertyPart</returns> 
        public static PropertyPart ConceptAsOracleGuid<T>(this PropertyPart propertyPart)
            where T : ConceptAs<Guid>
        {
            propertyPart.CustomType<ConceptAsOracleGuid<T>>();
            return propertyPart;
        }
    }
}
