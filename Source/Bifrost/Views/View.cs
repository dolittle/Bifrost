/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Bifrost.Entities;
using Bifrost.Extensions;

namespace Bifrost.Views
{
	/// <summary>
	/// Represents a <see cref="IView{T}"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class View<T> : IView<T>
	{
		private readonly IEntityContext<T> _entityContext;

		/// <summary>
		/// Initializes a new instance of <see cref="View{T}">QueryRepository</see>
		/// </summary>
		/// <param name="entityContext">An <see cref="IEntityContext{T}">IEntityContext</see> to use for querying</param>
		public View(IEntityContext<T> entityContext)
		{
			_entityContext = entityContext;
		}

#pragma warning disable 1591 // Xml Comments
		public IQueryable<T> Query { get { return _entityContext.Entities; } }
        public T GetById(Guid id)
        {
            if( typeof(T).HasInterface<IHaveId>() )
            {
                return _entityContext.GetById(id);
            }

            throw new ObjectDoesNotHaveIdException();
        }
#pragma warning restore 1591 // Xml Comments
	}
}