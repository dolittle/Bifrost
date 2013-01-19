using Bifrost.Security;

namespace Bifrost.Commands
{
    /// <summary>
    /// Extensions for building a security descriptor specific for <see cref="ICommand">commands</see>
    /// </summary>
    public static class CommandSecurityExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static ISecurityActionBuilder Handling(this ISecurityDescriptorBuilder ruleBuilder)
        {
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionBuilder"></param>
        /// <returns></returns>
        public static ISecurityRuleBuilder Commands(this ISecurityActionBuilder actionBuilder)
        {
            return null;
        }
    }
}
