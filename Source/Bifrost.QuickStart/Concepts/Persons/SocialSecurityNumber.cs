using Bifrost.Concepts;

namespace Bifrost.QuickStart.Concepts.Persons
{
    public class SocialSecurityNumber : ConceptAs<string>
    {
        public static implicit operator SocialSecurityNumber(string socialSecurityNumber)
        {
            return new SocialSecurityNumber { Value = socialSecurityNumber };
        }
    }
}