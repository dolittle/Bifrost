namespace Bifrost.Specs.Execution.for_WeakDelegate
{
    public class ClassWithMethod
    {
        public const int ReturnValue = 42;

        public int SomeMethod(string stringParameter, double intParameter)
        {
            return ReturnValue;
        }

        public int SomeOtherMethod(IInterface input)
        {
            return ReturnValue;
        }
    }
}
