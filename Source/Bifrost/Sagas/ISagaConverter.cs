/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Sagas
{
    /// <summary>
    /// Defines a converter for converting a <see cref="ISaga"/> to a <see cref="SagaHolder"/> and back
    /// </summary>
	public interface ISagaConverter
	{
        /// <summary>
        /// Convert a <see cref="SagaHolder"/> to a <see cref="ISaga"/>
        /// </summary>
        /// <param name="sagaHolder"><see cref="SagaHolder"/> to convert from</param>
        /// <returns>Converter <see cref="ISaga"/> in the correct type</returns>
		ISaga ToSaga(SagaHolder sagaHolder);

        /// <summary>
        /// Convert a <see cref="ISaga"/> to a <see cref="SagaHolder"/>
        /// </summary>
        /// <param name="saga"><see cref="ISaga"/> to convert from</param>
        /// <returns>A <see cref="SagaHolder"/> with the <see cref="ISaga"/> and its data serialized</returns>
		SagaHolder ToSagaHolder(ISaga saga);

        /// <summary>
        /// Populate an existing <see cref="SagaHolder"/> from a <see cref="ISaga"/>
        /// </summary>
        /// <param name="sagaHolder"><see cref="SagaHolder"/> to populate into</param>
        /// <param name="saga"><see cref="ISaga"/> to populate the <see cref="SagaHolder"/> with</param>
		void Populate(SagaHolder sagaHolder, ISaga saga);
	}
}