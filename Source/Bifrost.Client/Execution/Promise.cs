/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Execution
{
    public delegate void PromiseFailedCallback(Promise promise, object data);
    public delegate void PromiseContinueWithCallback(Promise promise, object data);

    /// <summary>
    /// Represents a a well defined continuable object that is typically used with operations that
    /// are asynchronous or potentially asynchronous in nature.
    /// </summary>
    public class Promise
    {
        List<PromiseFailedCallback> _failedCallbacks = new List<PromiseFailedCallback>();
        List<PromiseContinueWithCallback> _continueWithCallbacks = new List<PromiseContinueWithCallback>();
        bool _signalled = false;
        object _signalledData;

        bool _failed = false;
        object _failedData;


        /// <summary>
        /// Signal the promise to be done with optional data
        /// </summary>
        /// <param name="data">Optional data associated</param>
        public void Signal(object data = null)
        {
            _signalledData = data;
            CallContinueWithCallbacks();
            _signalled = true;
        }

        /// <summary>
        /// Fail the promise with error with optional data
        /// </summary>
        /// <param name="data">Optional data associated with the error</param>
        public void Fail(object data = null)
        {
            _failedData = data;
            CallFailedCallbacks();
            _failed = true;
        }


        /// <summary>
        /// Add a <see cref="PromiseFailedCallback"/> to the promise that gets called if the promise fails
        /// </summary>
        /// <param name="callback"><see cref="PromiseFailedCallback"/> to add</param>
        /// <returns>Chained Promise - itself</returns>
        public Promise Failed(PromiseFailedCallback callback)
        {
            _failedCallbacks.Add(callback);
            if (_failed) CallFailedCallbacks();
            return this;
        }

        /// <summary>
        /// Add a <see cref="PromiseContinueWithCallback"/> to the promise that gets called if the promise succeeds (gets signalled)
        /// </summary>
        /// <param name="callback"><see cref="PromiseContinueWithCallback"/> to add</param>
        /// <returns>Chained promise - itself</returns>
        public Promise ContinueWith(PromiseContinueWithCallback callback)
        {
            _continueWithCallbacks.Add(callback);
            if (_signalled) CallContinueWithCallbacks();
            return this;
        }


        void CallContinueWithCallbacks()
        {
            foreach (var callback in _continueWithCallbacks)
                callback(this, _signalledData);
        }

        void CallFailedCallbacks()
        {
            foreach (var callback in _failedCallbacks)
                callback(this, _failedData);
        }
    }
}
