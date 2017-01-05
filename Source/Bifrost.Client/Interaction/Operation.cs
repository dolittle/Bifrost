/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Interaction
{
    /// <summary>
    /// Represents an operation in the system
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Perform the operation
        /// </summary>
        /// <param name="context"><see cref="OperationContext"/> in which the operation is performed in</param>
        /// <returns>Any state as a result of performing the operation</returns>
        public virtual OperationState Perform(OperationContext context)
        {
            return null;
        }

        /// <summary>
        /// Check if the operation can be performed
        /// </summary>
        /// <param name="context"><see cref="OperationContext"/> in which the operation is performed in</param>
        /// <returns>True if it can be performed, false if not</returns>
        public virtual bool CanPerform(OperationContext context)
        {
            return false;
        }


        /// <summary>
        /// Undo the given operation based on the state coming out of the perform
        /// </summary>
        /// <param name="context"><see cref="OperationContext"/> in which the operation was performed in</param>
        /// <param name="state">State as a result from when it was performed</param>
        public virtual void Undo(OperationContext context, OperationState state)
        {
        }

        /// <summary>
        /// Check if the operation can be undoed
        /// </summary>
        /// <param name="context"><see cref="OperationContext"/> in which the operation was performed in</param>
        /// <returns>True if it can be undoed, false if not</returns>
        public virtual bool CanUndo(OperationContext context)
        {
            return false;
        }
    }
}
