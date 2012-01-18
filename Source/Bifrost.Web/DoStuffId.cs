namespace Bifrost.Web
{
    public class DoStuffId
    {
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as DoStuffId;
            return other != null && other.Value == Value;
        }

        public static implicit operator string(DoStuffId value)
        {
            return value == null ? string.Empty : value.Value;
        }

        public static implicit operator DoStuffId(string stringValue)
        {
            return new DoStuffId() { Value = stringValue };
        }
    }
}