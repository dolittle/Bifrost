
namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public interface IViewModel
    {
        bool CanExecutePropertyReturningBool { get; set; }
        int CanExecutePropertyReturningNonBool { get; set; }
        int CanExecuteMethodReturningNonBool();

        bool CanExecuteMethodWithOneParameter(object parameter);
        bool CanExecuteMethodWithoutParameter();
        bool CanExecuteMethodWithTwoParameters(object first, object second);

        void MethodWithoutParameters();
        void MethodWithOneParameter(object parameter);
        void MethodWithTwoParameters(object first, object second);
    }
}
