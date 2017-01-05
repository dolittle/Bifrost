/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{
    /// <summary>
    /// The exception that is thrown when an object is read only and one is writing to it
    /// </summary>
    public class ReadOnlyObjectException : ArgumentException
    {
    }
}
