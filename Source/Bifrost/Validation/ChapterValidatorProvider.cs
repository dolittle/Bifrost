#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
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
using Bifrost.Extensions;
#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Validation
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

#if(NETFX_CORE)
            if (chapterType == null || chapterType.GetTypeInfo().IsInterface)
                return;
#else
            if (chapterType == null || chapterType.IsInterface)
                return;
#endif
            _validators.Add(chapterType, typeToRegister);
        }

        Type GetChapterType(Type typeToRegister)
        {
            var types = from interfaceType in typeToRegister
#if(NETFX_CORE)
                                    .GetTypeInfo().ImplementedInterfaces
#else
                                    .GetInterfaces()
#endif
                        where interfaceType
#if(NETFX_CORE)
                                    .GetTypeInfo().IsGenericType
#else
                                    .IsGenericType
#endif
                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == _validatesType
                        select interfaceType
#if(NETFX_CORE)
                                    .GetTypeInfo().GenericTypeParameters
#else
                                    .GetGenericArguments()
#endif
                            .FirstOrDefault();

            return types.FirstOrDefault();
        }

    }
}