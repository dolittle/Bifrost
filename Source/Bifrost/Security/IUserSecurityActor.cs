namespace Bifrost.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityActor"/> for a user.
    /// </summary>
    public interface IUserSecurityActor
    {
        /// <summary>
        /// Checks whether the Current user has the requested role.
        /// </summary>
        /// <param name="role">Role to check for</param>
        /// <returns>True is the user has the role, False otherwise</returns>
        bool IsInRole(string role);
    }
}