#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Sagas;
using Microsoft.Practices.ServiceLocation;

namespace Bifrost.Validation
{

    /// <summary>
    /// Represents a <see cref="IChapterValidatorProvider"/> for providing chapter validators
    /// </summary>
    public class ChapterValidatorProvider : IChapterValidatorProvider
    {
        static IChapterValidator NullChapterValidator = new NullChapterValidator();
        static Type _chapterValidatorType = typeof (IChapterValidator);
        static Type _transitionValidatorType = typeof (ChapterTranstionValidator<,>);
        static Type _transitionToType = typeof (ICanTransitionTo<>);
        static Type _validatesType = typeof (ICanValidate<>);

        readonly ITypeDiscoverer _typeDiscoverer;
        readonly IServiceLocator _serviceLocator;

        Dictionary<Type, Type> _validators;
        Dictionary<KeyValuePair<Type,Type>, Type> _transitionValidators;

        /// <summary>
        /// Initializes an instance of <see cref="ChapterValidatorProvider"/> ChapterValidatorProvider
        /// </summary>
        /// <param name="typeDiscoverer">An instance of ITypeDiscoverer to help identify and register <see cref="IChapterValidator"> IChapterValidator</see> implementations
        /// </param>
        /// <param name="serviceLocator">An instance of IServiceLocator to return concrete instances of validators</param>
        public ChapterValidatorProvider(ITypeDiscoverer typeDiscoverer, IServiceLocator serviceLocator)
        {
            _typeDiscoverer = typeDiscoverer;
            _serviceLocator = serviceLocator;

            Initialize();
        }


#pragma warning disable 1591 // Xml Comments
        public IChapterValidator GetValidatorFor(IChapter chapter)
        {
            if (chapter == null)
                return NullChapterValidator;

            var type = chapter.GetType();
            return GetValidatorFor(type);
        }

        public IChapterValidator GetValidatorFor(Type type)
        {
            if (type == null)
                return NullChapterValidator;

            Type registeredType;
            _validators.TryGetValue(type, out registeredType);

            var validator = registeredType != null ? _serviceLocator.GetInstance(registeredType) as IChapterValidator : NullChapterValidator;
            return validator;
        }

        public IChapterValidator GetValidatorForTransitionTo<T>(IChapter transitionFromChapter)
        {
            if (transitionFromChapter == null)
                return NullChapterValidator;
            
            var type = transitionFromChapter.GetType();

            var transitionKey = new KeyValuePair<Type, Type>(type, typeof(T));
            Type registeredType;

            _transitionValidators.TryGetValue(transitionKey, out registeredType);

            var validator = registeredType != null ? _serviceLocator.GetInstance(registeredType) as IChapterValidator : NullChapterValidator;

            return validator;
        }
#pragma warning restore 1591 // Xml Comments

        void Initialize()
        {
            _validators = new Dictionary<Type, Type>();
            _transitionValidators = new Dictionary<KeyValuePair<Type, Type>, Type>();

            var validators = _typeDiscoverer.FindMultiple(_chapterValidatorType);
            var transitionValidators = _typeDiscoverer.FindMultiple(_transitionValidatorType);

            Array.ForEach(validators, RegisterChapterValidator);
            Array.ForEach(transitionValidators, RegisterTransitionValidator);
        }

        void RegisterChapterValidator(Type typeToRegister)
        {
            var chapterType = GetChapterType(typeToRegister);

            if (chapterType == null || chapterType.IsInterface)
                return;

            _validators.Add(chapterType, typeToRegister);
        }

        void RegisterTransitionValidator(Type typeToRegister)
        {
            Type transitionToType;
            var chapterType = GetChapterType(typeToRegister, out transitionToType);

            if (chapterType == null || chapterType.IsInterface)
                return;

            _transitionValidators.Add(new KeyValuePair<Type, Type>(chapterType, transitionToType), typeToRegister);
        }

        Type GetChapterType(Type typeToRegister, out Type transitionToType)
        {
            var type = GetChapterType(typeToRegister);
            var transitionTo = from interfaceType in typeToRegister.GetInterfaces()
                        where interfaceType.IsGenericType
                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == _transitionToType
                        select interfaceType.GetGenericArguments();
            transitionToType = transitionTo.FirstOrDefault().FirstOrDefault();

            return type;
        }

        Type GetChapterType(Type typeToRegister)
        {
            var types = from interfaceType in typeToRegister.GetInterfaces()
                        where interfaceType.IsGenericType
                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == _validatesType
                        select interfaceType.GetGenericArguments().FirstOrDefault();

            return types.FirstOrDefault();
        }

    }
}