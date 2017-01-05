/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Execution;
using Bifrost.Sagas;
using Bifrost.Extensions;
using Bifrost.Validation;

namespace Bifrost.FluentValidation.Sagas
{

    /// <summary>
    /// Represents a <see cref="IChapterValidatorProvider"/> for providing chapter validators
    /// </summary>
    public class ChapterValidatorProvider : IChapterValidatorProvider
    {
        static ICanValidate NullChapterValidator = new NullChapterValidator();
        static Type _chapterValidatorType = typeof (IChapterValidator);
        static Type _validatesType = typeof (ICanValidate<>);

        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;

        Dictionary<Type, Type> _validators;

        /// <summary>
        /// Initializes an instance of <see cref="ChapterValidatorProvider"/> ChapterValidatorProvider
        /// </summary>
        /// <param name="typeDiscoverer">An instance of ITypeDiscoverer to help identify and register <see cref="IChapterValidator"> IChapterValidator</see> implementations
        /// </param>
        /// <param name="container">An instance of <see cref="IContainer"/> to create concrete instances of validators</param>
        public ChapterValidatorProvider(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;

            Initialize();
        }


#pragma warning disable 1591 // Xml Comments
        public ICanValidate GetValidatorFor(IChapter chapter)
        {
            if (chapter == null)
                return NullChapterValidator;

            var type = chapter.GetType();
            return GetValidatorFor(type);
        }

        public ICanValidate GetValidatorFor(Type type)
        {
            if (type == null)
                return NullChapterValidator;

            Type registeredType;
            _validators.TryGetValue(type, out registeredType);

            var validator = (registeredType != null ? _container.Get(registeredType) : NullChapterValidator) as ICanValidate;
            return validator;
        }
#pragma warning restore 1591 // Xml Comments

        void Initialize()
        {
            _validators = new Dictionary<Type, Type>();

            var validators = _typeDiscoverer.FindMultiple(_chapterValidatorType);

            validators.ForEach(Register);
        }

        void Register(Type typeToRegister)
        {
            var chapterType = GetChapterType(typeToRegister);

            if (chapterType == null || chapterType.GetTypeInfo().IsInterface)
                return;
            _validators.Add(chapterType, typeToRegister);
        }

        Type GetChapterType(Type typeToRegister)
        {
            var types = from interfaceType in typeToRegister.GetTypeInfo().GetInterfaces()
                        where interfaceType.GetTypeInfo().IsGenericType
                        let baseInterface = interfaceType.GetTypeInfo().GetGenericTypeDefinition()
                        where baseInterface == _validatesType
                        select interfaceType.GetTypeInfo().GetGenericArguments().FirstOrDefault();

            return types.FirstOrDefault();
        }

    }
}