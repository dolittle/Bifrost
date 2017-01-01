using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using Ninject;
using Ninject.Activation;

namespace Bifrost.Ninject.Specs.for_KernelExtensions.given
{
    public class a_kernel_and_a_type_importer
    {
        protected static Mock<ITypeImporter> type_importer_mock;
        protected static Mock<IKernel> kernel_mock;

        Establish context = () =>
        {
            type_importer_mock = new Mock<ITypeImporter>();
            kernel_mock = new Mock<IKernel>();
            kernel_mock.Setup(k => k.Resolve(Moq.It.IsAny<IRequest>())).Returns(new[] { type_importer_mock.Object });
        };

    }
}
