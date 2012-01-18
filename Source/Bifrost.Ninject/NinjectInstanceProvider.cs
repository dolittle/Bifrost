using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Ninject;

namespace Bifrost.Ninject
{
    public class NinjectInstanceProvider : IInstanceProvider
    {
        private readonly Type _type;
        private static IKernel _kernel;
        public static IKernel Kernel
        {
            get
            {
                return _kernel;
            }
            set
            {
                if (null != _kernel)
                {
                    throw new ArgumentException("You can't set the kernel twice");
                }
                _kernel = value;
            }
        }

        public NinjectInstanceProvider(Type type)
        {
            _type = type;
        }


        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            var instance = Kernel.Get(_type);
            return instance;
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            if (instance != null && instance is IDisposable)
            {
                ((IDisposable)instance).Dispose();
            }
        }
    }
}
