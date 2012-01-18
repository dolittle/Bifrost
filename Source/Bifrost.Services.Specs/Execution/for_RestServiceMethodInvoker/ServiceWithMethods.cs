namespace Bifrost.Services.Specs.Execution.for_RestServiceMethodInvoker
{
    public class ServiceWithMethods
    {
        public const string NoInputOrOutputMethod = "NoInputOrOutput";
        public bool NoInputOrOutputCalled = false;
        public void NoInputOrOutput()
        {
            NoInputOrOutputCalled = true;
        }
    }
}
