/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContextManager"/>
    /// </summary>
    public class ExecutionContextManager : IExecutionContextManager
    {
        /// <summary>
        /// Key identifying the current <see cref="IExectionContext"/> in a <see cref="ICallContext"/>
        /// </summary>
        public const string ExecutionContextKey = "ExecutionContext";

        IExecutionContextFactory _executionContextFactory;
        ICallContext _callContext;

        /// <summary>
        /// Initializes a new instance of <see cref="ExecutionContextManager"/>
        /// </summary>
        /// <param name="executionContextFactory"><see cref="IExecutionContextFactory"/> for creating <see cref="IExecutionContext">Exection Contexts</see></param>
        /// <param name="callContext"><see cref="ICallContext"/> to use for key/value store for holding current <see cref="IExecutionContext"/></param>
        public ExecutionContextManager(IExecutionContextFactory executionContextFactory, ICallContext callContext)
        {
            _executionContextFactory = executionContextFactory;
            _callContext = callContext;
        }


#pragma warning disable 1591 // Xml Comments
        public IExecutionContext Current
        {
            get 
            {
                IExecutionContext current = null;

                if (_callContext.HasData(ExecutionContextKey))
                    current = _callContext.GetData<IExecutionContext>(ExecutionContextKey);
                else
                {
                    current = _executionContextFactory.Create();
                    _callContext.SetData(ExecutionContextKey, current);
                }
                return current;
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
