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


        public const string ComplexInputNoOutputMethod = "ComplexInputNoOutput";
        public bool ComplexInputNoOutputCalled = false;
        public ComplexType ComplexInputNoOutputResult;
        public void ComplexInputNoOutput(ComplexType input)
        {
            ComplexInputNoOutputCalled = true;
            ComplexInputNoOutputResult = input;
        }



    }
}
