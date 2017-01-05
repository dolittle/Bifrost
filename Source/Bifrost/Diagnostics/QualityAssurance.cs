/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Represents an implementation of <see cref="IQualityAssurance"/>
    /// </summary>
    public class QualityAssurance : IQualityAssurance
    {
        ITypeRules _typeRules;

        /// <summary>
        /// Initializes a new instance of <see cref="QualityAssurance"/>
        /// </summary>
        /// <param name="typeRules"></param>
        public QualityAssurance(ITypeRules typeRules)
        {
            _typeRules = typeRules;
        }

#pragma warning disable 1591 // Xml Comments
        public void Validate()
        {
            _typeRules.ValidateAll();
        }
#pragma warning restore 1591 // Xml Comments
    }
}
