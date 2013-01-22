using System;

namespace Bifrost.Security
{
    /// <summary>
    /// Encapsulates a <see cref="ISecurityRule"/> that encountered an Exception when evaluating
    /// </summary>
    public class RuleEvaluationError
    {
        public RuleEvaluationError(ISecurityRule rule, Exception error)
        {
            Error = error;
            Rule = rule;
        }

        /// <summary>
        /// Gets the Exception that was encountered when evaluation the rule
        /// </summary>
        public Exception Error { get; private set; }
        /// <summary>
        /// The rule instance that encountered the exception when evaluation
        /// </summary>
        public ISecurityRule Rule { get; private set; }
    }
}