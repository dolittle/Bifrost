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
using System.Linq.Expressions;

namespace Bifrost.Read.Validation
{
#pragma warning disable 1591 // Xml Comments
    public static class MethodCalls
    {
        /*
        public static void CallGenericMethod<T>(this T target, Expression<Func<T, Action>> method)
        {

        }*/

        public static TOut CallGenericMethod<T, TOut>(this T target, Expression<Func<T, Func<TOut>>> method, Type methodTypeArgument)
        {
            throw new NotImplementedException();
        }


        /*
        public static void CallGenericMethod<T, T1>(this T target, Expression<Action<T>> method)
        {

        }*/
    }
#pragma warning restore 1591 // Xml Comments




    /// <summary>
    /// Represents an implementation of <see cref="IQueryValidator"/>
    /// </summary>
    public class QueryValidator : IQueryValidator
    {
        IQueryValidationDescriptors _descriptors;

        /// <summary>
        /// Initializes an instance of <see cref="QueryValidator"/>
        /// </summary>
        /// <param name="descriptors"><see cref="IQueryValidationDescriptors"/> for getting descriptors for queries for running through rules</param>
        public QueryValidator(IQueryValidationDescriptors descriptors)
        {
            _descriptors = descriptors;
        }

#pragma warning disable 1591 // Xml Comments
        public QueryValidationResult Validate(IQuery query)
        {
            // _descriptors.GetType().GetMethod("HasDescriptorFor")

            _descriptors.CallGenericMethod<IQueryValidationDescriptors, bool>(d => d.HasDescriptorFor<IQuery>, query.GetType());


            throw new System.NotImplementedException();
        }
#pragma warning restore 1591 // Xml Comments
    }
}
