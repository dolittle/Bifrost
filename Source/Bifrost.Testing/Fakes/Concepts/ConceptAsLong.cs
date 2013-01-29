using Bifrost.Concepts;

namespace Bifrost.Testing.Fakes.Concepts
{
    public class ConceptAsLong : ConceptAs<long>
    {
        public static implicit operator ConceptAsLong(long value)
        {
            return new ConceptAsLong { Value = value };
        }
    }
}