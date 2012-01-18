using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;

namespace Bifrost.Mimir.Services
{
    public class ServiceHelper
    {
        static bool HasPort(Uri uri)
        {
            var url = uri.ToString();
            var segments = url.Split(':');
            return segments.Length > 2;
        }

        public static object GetInstance(Type type, string relativePath)
        {
            var getInstanceMethod = (from m in typeof (ServiceHelper).GetMethods()
                                     where m.Name == "GetInstance" && m.IsGenericMethod
                                     select m).SingleOrDefault();
            if( getInstanceMethod != null )
            {
                var genericGetInstanceMethod = getInstanceMethod.MakeGenericMethod(type);
                var instance = genericGetInstanceMethod.Invoke(null, new[] {relativePath});
                return instance;
            }
            return null;
        }

        public static T GetInstance<T>(string relativePath) where T : class
        {
            var source = Application.Current.Host.Source;
            if (source == null) return null;

            var url = string.Empty;
            if (HasPort(source))
            {
                url = string.Format("{0}://{1}:{2}/{3}", source.Scheme, source.Host, source.Port, relativePath);
            }
            else
            {
                url = string.Format("{0}://{1}/{2}", source.Scheme, source.Host, relativePath);
            }

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            var endpointAddress = new EndpointAddress(url);

            var factory = new ChannelFactory<T>(binding, endpointAddress);

            var client = factory.CreateChannel();
            return client;
        }
    }
}
