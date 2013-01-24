using System;
using System.Collections.Generic;
using Bifrost.Security;

namespace Bifrost.Testing.Fakes.Security
{
    public class MySecurityAction : ISecurityAction
    {
        readonly Func<object, bool> _canAuthorize;
        readonly Func<object, AuthorizeActionResult> _authorize;

        public MySecurityAction(Func<object, bool> canAuthorize, Func<object, AuthorizeActionResult> authorize)
        {
            _canAuthorize = canAuthorize;
            _authorize = authorize;
        }

        public void AddTarget(ISecurityTarget securityTarget)
        {
        }

        public IEnumerable<ISecurityTarget> Targets { get; private set; }
        public bool CanAuthorize(object actionToAuthorize)
        {
            return _canAuthorize.Invoke(actionToAuthorize);
        }

        public AuthorizeActionResult Authorize(object actionToAuthorize)
        {
            return _authorize.Invoke(actionToAuthorize);
        }
    }
}