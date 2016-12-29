using System;
using System.Reflection;
using Machine.Specifications;
using Moq;
using Ninject;
using Ninject.MockingKernel;
using Ninject.MockingKernel.Moq;
using Ninject.Planning.Bindings.Resolvers;
using Ninject.Syntax;

namespace Bifrost.Testing
{
    /// <summary>
    /// Subclass this class to use dependency injections in test.
    /// Any request for an unbound interface or abstract class will return a mock instance.
    /// Use GetMock to retrieve the mock for any given type, to set up expectations.
    /// 
    /// This class is deliberately hiding the DI framework.
    /// </summary>
    public class dependency_injection
    {
        static dependency_injection()
        {
            // HACK: Add a manual resolver for Moq, since Ninject.MockingKernel.Moq is compiled against an older Moq assembly.
            AppDomain.CurrentDomain.AssemblyResolve +=
                (sender, args) => new AssemblyName(args.Name).Name == "Moq" ? typeof(Mock).Assembly : null;
        }

        [ThreadStatic]
        static MoqMockingKernel kernel;

        static MoqMockingKernel Kernel
        {
            get { return kernel ?? (kernel = CreateMockingKernel()); }
        }

        // --- Add support to bind delegates. (Submitted as pull request #22 of Ninject.MockingKernel.)
        static MoqMockingKernel CreateMockingKernel()
        {
            var mockingKernel = new MoqMockingKernel();
            mockingKernel.Components.RemoveAll<IMissingBindingResolver>();
            mockingKernel.Components.Add<IMissingBindingResolver, AlsoMockDelegatesBindingResolver>();
            mockingKernel.Components.Add<IMissingBindingResolver, SingletonSelfBindingResolver>();
            return mockingKernel;
        }

        class AlsoMockDelegatesBindingResolver : MockMissingBindingResolver
        {
            public AlsoMockDelegatesBindingResolver(IMockProviderCallbackProvider mockProviderCallbackProvider)
                : base(mockProviderCallbackProvider)
            {
            }

            protected override bool TypeIsInterfaceOrAbstract(Type service)
            {
                return base.TypeIsInterfaceOrAbstract(service) || typeof(MulticastDelegate).IsAssignableFrom(service);
            }
        }
        // ---

        // The kernel is new for each test, effectively binding all services in test scope.
        Establish context = () => kernel = null;

        /// <summary>
        /// Gets an instance of the specified service. This will typically be one of these:
        /// 1) A self bound (concrete) object, typically the subject under test. DI will be used for its constructor.
        /// 2) A service specifically bound with Bind, or BindConstant.
        /// 3) The Object of a Moq.Mock for unbound interfaces or abstract classes.
        /// </summary>
        /// <remarks>
        /// All services are bound in test scope, so repeated calls for the same service
        /// will yield the same object within a test.
        /// </remarks>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <returns>An instance of the specified service.</returns>
        protected static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Gets a new instance of the specified service. Use this method if you have rebound some services and want
        /// to instantiate a new concrete class with the new services.
        /// </summary>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <returns>A new instance of the specified service.</returns>
        protected static T GetNew<T>()
        {
            Kernel.Unbind<T>();
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Gets the mock of the specified service.
        /// </summary>
        /// <remarks>
        /// All services are bound in test scope, so repeated calls for the same mock service
        /// will yield the same mock within a test.
        /// </remarks>
        /// <typeparam name="T">The service to resolve.</typeparam>
        /// <returns>The mock of the service.</returns>
        protected static Mock<T> GetMock<T>() where T : class
        {
            return Kernel.GetMock<T>();
        }

        /// <summary>
        /// Removes any existing bindings for a service and declares a new one.
        /// </summary>
        /// <remarks>Typically used for binding to concrete implementations instead of a mock.</remarks>
        /// <typeparam name="T">The service to bind.</typeparam>
        /// <returns>A fluent builder. Call either To or ToConstant on this to object finish the bind operation.</returns>
        protected static BindingWrapper<T> Rebind<T>()
        {
            return new BindingWrapper<T>(Kernel.Rebind<T>());
        }

        /// <summary>
        /// Removes any existing bindings for a service and declares a binding to a constant.
        /// </summary>
        /// <remarks>Equivalent to Rebind{T}.ToConstant{T}(value), this has a
        /// handy syntax for delegates (i.e. validation queries).</remarks>
        /// <typeparam name="T">The service to bind.</typeparam>
        /// <param name="value">The constant to bind this service to.</param>
        protected static void RebindConstant<T>(T value)
        {
            Kernel.Rebind<T>().ToConstant(value);
        }

        // Helper class to hide dependency on Ninject.
        // Please add forwarded calls in this class instead of exposing Ninject to sub classes.
        protected class BindingWrapper<T>
        {
            readonly IBindingToSyntax<T> _realBinding;

            public BindingWrapper(IBindingToSyntax<T> realBinding)
            {
                _realBinding = realBinding;
            }

            public void To<TImplementation>() where TImplementation : T
            {
                _realBinding.To<TImplementation>();
            }

            public void ToConstant<TImplementation>(TImplementation value) where TImplementation : T
            {
                _realBinding.ToConstant(value);
            }
        }
    }
}
