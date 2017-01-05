/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Provides methods for working with observables
    /// </summary>
    public static class ObservableExtensions
    {
        /// <summary>
        /// Set the default value for the observable
        /// </summary>
        /// <param name="observable"><see cref="Observable"/> to set default value for</param>
        /// <param name="defaultValue">Default value to set</param>
        /// <returns>The <see cref="Observable"/> to build on</returns>
        public static Observable WithDefaultValue(this Observable observable, object defaultValue)
        {
            observable.Parameters = new Literal[] { new Literal(defaultValue) };
            return observable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observable"></param>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Observable ExtendWith(this Observable observable, string name, string options)
        {
            observable.AddExtension(name, options);
            return observable;
        }
    }
}
