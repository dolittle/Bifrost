/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Reflection
{
    /// <summary>
    /// Defines a system that needs target instance
    /// </summary>
    /// <remarks>
    /// This is a marker interface. By convention the system will look for a property
    /// called TargetInstance matching the type expected and then set the 
    /// value on it
    /// </remarks>
    public interface INeedTargetInstance
    {
    }
}
