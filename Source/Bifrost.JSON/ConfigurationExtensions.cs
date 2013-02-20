using Bifrost.JSON.Serialization;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
		public static IConfigure UsingJson(this ISerializationConfiguration serializationConfiguration) 
		{
			serializationConfiguration.SerializerType = typeof(Serializer);
			return Configure.Instance;
		}
    }
}
