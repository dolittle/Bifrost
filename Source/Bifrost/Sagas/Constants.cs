/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Sagas
{
    /// <summary>
    /// Constants that are used within Bifrost.Sagas
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Represents the <see cref="SagaState"/> of "new"
        /// </summary>
        public const string NEW = "new";
        /// <summary>
        /// Represents the <see cref="SagaState"/> of "begun"
        /// </summary>
        public const string BEGUN = "begun";
        /// <summary>
        /// Represents the <see cref="SagaState"/> of "continuing"
        /// </summary>
        public const string CONTINUING = "continuing";
        /// <summary>
        /// Represents the <see cref="SagaState"/> of "concluded"
        /// </summary>
        public const string CONCLUDED = "concluded";
    }
}