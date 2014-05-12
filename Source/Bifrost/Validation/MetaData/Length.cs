namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents the metadata for the Length validation rule
    /// </summary>
    public class Length : Rule
    {
        /// <summary>
        /// Gets or sets the value that values validated up against must be greater or equal to
        /// </summary>
        public object Min { get; set; }

        /// <summary>
        /// Gets or sets the value that values validated up against must be less than or equal to
        /// </summary>
        public object Max { get; set; }
    }
}