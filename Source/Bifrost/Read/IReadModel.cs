/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Conventions;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents the marker interface for a read model.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface will be picked up proxy generation and the read model service.
    /// It will also be deserialized, passed to <see cref="ICanFilterReadModels"/>
    /// and dispatched to the correct instance of <see cref="IReadModelOf{T}"/>.
    /// </remarks>
    public interface IReadModel : IConvention
    {
    }
}
