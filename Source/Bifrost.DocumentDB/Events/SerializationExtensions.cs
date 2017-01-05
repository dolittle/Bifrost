/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Bifrost.Serialization;
using Microsoft.Azure.Documents;

namespace Bifrost.DocumentDB.Events
{
    /// <summary>
    /// Extends <see cref="ISerializer"/> with helper functionality
    /// </summary>
    public static class SerializationExtensions
    {
        internal static SerializationOptions SerializationOptions = new SerializationOptions { UseCamelCase = true };

        /// <summary>
        /// Deserialize from a document
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> that is extended</param>
        /// <param name="document"><see cref="Document"/> to deserialize from</param>
        /// <returns>The serialized type</returns>
        public static T FromDocument<T>(this ISerializer serializer, Document document)
        {
            var stream = new MemoryStream();
            document.SaveTo(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var jsonAsString = reader.ReadToEnd();
            var deserialized = (T)serializer.FromJson(typeof(T), jsonAsString, SerializationOptions);
            return deserialized;
        }
    }
}
