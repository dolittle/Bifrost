using System;
using Bifrost.Concepts;
using FluentNHibernate.Mapping;

namespace Bifrost.NHibernate.Concepts
{
    public static class MappingExtensions
    {
        public static IdentityPart ConceptOf<T, U>(this IdentityPart identityPart)
            where U : IEquatable<U>
            where T : ConceptAs<U>
        {
            identityPart.CustomType<ConceptValueType<T,U>>();
            return identityPart;
        }

        public static IdentityPart ConceptAsOracleGuid<T>(this IdentityPart identityPart)
            where T : ConceptAs<Guid>
        {
            identityPart.CustomType<OracleGuidValueType<T>>();
            return identityPart;
        }

        public static PropertyPart ConceptOf<T, U>(this PropertyPart propertyPart)
            where U : IEquatable<U>
            where T : ConceptAs<U>
        {
            propertyPart.CustomType<ConceptValueType<T,U>>();
            return propertyPart;
        }

        public static PropertyPart ConceptAsOracleGuid<T>(this PropertyPart propertyPart)
            where T : ConceptAs<Guid>
        {
            propertyPart.CustomType<OracleGuidValueType<T>>();
            return propertyPart;
        }
    }
}
