using FluentNHibernate.Mapping;

namespace Bifrost.NHibernate.Concepts
{
    public static class MappingExtensions
    {
        public static IdentityPart ConceptOf<T>(this IdentityPart identityPart)
        {
            identityPart.CustomType<ConceptValueType<T>>();
            return identityPart;
        }


        public static PropertyPart ConceptOf<T>(this PropertyPart propertyPart)
        {
            propertyPart.CustomType<T>();
            return propertyPart;
        }
    }
}
