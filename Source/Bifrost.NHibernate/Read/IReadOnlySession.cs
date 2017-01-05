/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using NHibernate;

namespace Bifrost.NHibernate.Read
{
    public interface IReadOnlySession : IDisposable
    {
        ISession GetCurrentSession();
    }
}
