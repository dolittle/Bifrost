namespace Bifrost.Web
{
    public class Amount
    {
        public virtual decimal Value { get; set; }
        public static implicit operator Amount(decimal decimalValue)
        {
            return new Amount() { Value = decimalValue };
        }
        public static implicit operator decimal(Amount amount)
        {
            return amount == null ? default(decimal) : amount.Value;
        }
    }
}