using System;
using Microsoft.Practices.Unity;

namespace Bifrost.Unity
{
    // Based on : http://xhalent.wordpress.com/2011/02/02/resolving-instances-using-delegates-in-unity/
    public class DelegateLifetimeManager : LifetimeManager
    {
        private LifetimeManager _baseManager;
        private Func<object> _resolveDelegate = null;

        public DelegateLifetimeManager(
            Func<object> sourceFunc,
            LifetimeManager baseManager = null)
        {
            this._resolveDelegate = sourceFunc;
            this._baseManager = baseManager;
        }

        public override object GetValue()
        {
            object result = _baseManager.GetValue();
            if (result == null)
            {
                result = _resolveDelegate();

                if (_baseManager != null)
                    _baseManager.SetValue(result);
            }

            return result;
        }

        public override void RemoveValue()
        {
            if (_baseManager != null)
                _baseManager.RemoveValue();
        }

        public override void SetValue(object newValue)
        {
            if (_baseManager != null)
                _baseManager.SetValue(newValue);
        }
    }
}
