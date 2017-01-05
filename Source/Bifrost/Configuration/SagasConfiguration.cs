/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Sagas;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Represents a <see cref="ISagasConfiguration"/> implementation
	/// </summary>
	public class SagasConfiguration : ConfigurationStorageElement, ISagasConfiguration
	{

        /// <summary>
        /// Initializes a new instance of <see cref="SagasConfiguration"/>
        /// </summary>
        public SagasConfiguration()
        {
            LibrarianType = typeof(NullSagaLibrarian);
        }

#pragma warning disable 1591 // Xml Comments
		public Type LibrarianType { get; set; }

		public override void Initialize(IContainer container)
		{
			if( LibrarianType != null )
				container.Bind<ISagaLibrarian>(LibrarianType);


            if (EntityContextConfiguration != null)
            {
                EntityContextConfiguration.BindEntityContextTo<SagaHolder>(container);
                EntityContextConfiguration.BindEntityContextTo<ChapterHolder>(container);
            }

            base.Initialize(container);
		}
#pragma warning restore 1591 // Xml Comments

	}
}