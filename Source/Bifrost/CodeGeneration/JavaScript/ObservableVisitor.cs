namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents the method that can be provided as a visitor callback when an observable property is added
    /// </summary>
    /// <param name="propertyName">Name of property that is being visited</param>
    /// <param name="observable"><see cref="Observable"/> that is being visited</param>
    public delegate void ObservableVisitor(string propertyName, Observable observable);
}
