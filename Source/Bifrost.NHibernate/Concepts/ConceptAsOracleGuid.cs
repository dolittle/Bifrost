/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Concepts;
using Bifrost.NHibernate.UserTypes;

namespace Bifrost.NHibernate.Concepts
{
    /// <summary>
    /// NHibernate mapping of a custom user type that works in Oracle for a type T deriving from ConceptAs'Guid
    /// </summary>
    /// <typeparam name="T">The type that inherits from <see cref="ConceptAs{Guid}"/></typeparam>
    public class ConceptAsOracleGuid<T> : ConceptValueType<T, Guid>
        where T : ConceptAs<Guid>
    {
        static OracleGuidMapping _oracleGuidMapping = new OracleGuidMapping();

        /// <summary>
        /// Creates an instance of <see cref="ConceptAsOracleGuid{T}"/>
        /// </summary>
        public ConceptAsOracleGuid()
            : base(_oracleGuidMapping)
        {
            
        }
    }
}