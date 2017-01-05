/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a holder for a <see cref="ISaga"/> for persisting purposes
    /// </summary>
    public class SagaHolder
    {
        /// <summary>
        /// Gets or sets the Id of the actual <see cref="ISaga"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Partition in which the <see cref="ISaga"/> belongs to
        /// </summary>
        public string Partition { get; set; }

        /// <summary>
        /// Gets or sets the Key that represents the <see cref="ISaga"/> within a partition
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the name of the <see cref="ISaga"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the <see cref="ISaga"/>
        /// </summary>
        /// <remarks>
        /// Fully qualified type string
        /// </remarks>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SagaState"/> represented as string
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the serialized version of the <see cref="ISaga"/>
        /// </summary>
        public string SerializedSaga { get; set; }

        /// <summary>
        /// Gets or sets the current chapters type
        /// </summary>
        /// <remarks>
        /// Fully qualified type string
        /// </remarks>
        public string CurrentChapterType { get; set; }

        /// <summary>
        /// Gets or sets the chapters as serialized data
        /// </summary>
        public string SerializedChapters { get; set; }

        /// <summary>
        /// Gets or sets the uncommited events as serialized data
        /// </summary>
        public string UncommittedEvents { get; set; }
    }
}
