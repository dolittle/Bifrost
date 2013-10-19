using Bifrost.CodeGeneration;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.CodeGeneration.for_CodeGenerator
{
    public class when_generating_from_a_language_element
    {
        static Mock<ILanguageElement> language_element_mock;
        static CodeGenerator generator;
        static string result;

        Establish context = () =>
        {
            language_element_mock = new Mock<ILanguageElement>();
            language_element_mock.Setup(l => l.Write(Moq.It.IsAny<ICodeWriter>())).Callback((ICodeWriter writer) => writer.Write("Hello"));

            generator = new CodeGenerator();
        };

        Because of = () => result = generator.GenerateFrom(language_element_mock.Object);

        It should_ask_the_language_element_to_write = () => language_element_mock.Verify(l => l.Write(Moq.It.IsAny<ICodeWriter>()), Moq.Times.Once());
        It should_return_the_result_of_the_write_from_the_language_element = () => result.ShouldEqual("Hello");
    }
}
