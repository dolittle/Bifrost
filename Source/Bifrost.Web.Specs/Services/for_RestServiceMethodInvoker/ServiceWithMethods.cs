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



        public const string NoInputComplexOutputMethod = "NoInputComplexOutput";
        public bool NoInputComplexOutputCalled = false;
        public ComplexType NoInputComplexOutputReturn;
        public ComplexType NoInputComplexOutput()
        {
            NoInputComplexOutputCalled = true;
            return NoInputComplexOutputReturn;
        }


        public const string StringInputNoOutputMethod = "StringInputNoOutput";
        public bool StringInputNoOutputCalled = false;
        public string StringInputNoOutputInput;
        public void StringInputNoOutput(string input)
        {
            StringInputNoOutputCalled = true;
            StringInputNoOutputInput = input;
        }

        public const string IntInputNoOutputMethod = "IntInputNoOutput";
        public bool IntInputNoOutputCalled = false;
        public int IntInputNoOutputInput;
        public void IntInputNoOutput(int input)
        {
            IntInputNoOutputCalled = true;
            IntInputNoOutputInput = input;
        }

    }
}
