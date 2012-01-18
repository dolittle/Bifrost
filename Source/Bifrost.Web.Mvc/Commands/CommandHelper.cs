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
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Bifrost.Commands;

namespace Bifrost.Web.Mvc.Commands
{
    /// <summary>
    /// Provides helper methods for Commands
    /// </summary>
    public static class CommandHelper
    {
        /// <summary>
        /// Begins a <see cref="CommandForm{T}"/>, with default <see cref="FormMethod.Post"/> as method
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <typeparam name="TC">Type of controller holding the action to forward to</typeparam>
        /// <param name="htmlHelper">HtmlHelper to begin a command form within</param>
        /// <param name="expression">Expression holding information about the action on the controller to use</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        /// <remarks>
        /// For the expression that expressed the action to use, it does not care about the parameters for the action, so these
        /// can be set to null. The expression just represents the action strongly typed.
        /// </remarks>
        public static CommandForm<T> BeginCommandForm<T, TC>(this HtmlHelper htmlHelper, Expression<Func<TC, ActionResult>> expression)
            where T : ICommand, new()
            where TC : ControllerBase
        {
            var command = htmlHelper.BeginCommandForm<T, TC>(expression, FormMethod.Post, new Dictionary<string, object>());
            return command;
        }

        /// <summary>
        /// Begins a <see cref="CommandForm{T}"/>, with default <see cref="FormMethod.Post"/> as method
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <typeparam name="TC">Type of controller holding the action to forward to</typeparam>
        /// <param name="htmlHelper">HtmlHelper to begin a command form within</param>
        /// <param name="expression">Expression holding information about the action on the controller to use</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to be set for the element</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        /// <remarks>
        /// For the expression that expressed the action to use, it does not care about the parameters for the action, so these
        /// can be set to null. The expression just represents the action strongly typed.
        /// </remarks>
        public static CommandForm<T> BeginCommandForm<T, TC>(this HtmlHelper htmlHelper, Expression<Func<TC, ActionResult>> expression, IDictionary<string, object> htmlAttributes)
            where T : ICommand, new()
            where TC : ControllerBase
        {
            var command = htmlHelper.BeginCommandForm<T, TC>(expression, FormMethod.Post,htmlAttributes);
            return command;
        }

        /// <summary>
        /// Begins a <see cref="CommandForm{T}"/>, with default <see cref="FormMethod.Post"/> as method
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <typeparam name="TC">Type of controller holding the action to forward to</typeparam>
        /// <param name="htmlHelper">HtmlHelper to begin a command form within</param>
        /// <param name="expression">Expression holding information about the action on the controller to use</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to be set for the element</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        /// <remarks>
        /// For the expression that expressed the action to use, it does not care about the parameters for the action, so these
        /// can be set to null. The expression just represents the action strongly typed.
        /// </remarks>
        public static CommandForm<T> BeginCommandForm<T, TC>(this HtmlHelper htmlHelper, Expression<Func<TC, ActionResult>> expression, object htmlAttributes)
            where T : ICommand, new()
            where TC : ControllerBase
        {
            var command = htmlHelper.BeginCommandForm<T, TC>(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return command;
        }

        /// <summary>
        /// Begins a CommandForm
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <typeparam name="TC">Type of controller holding the action to forward to</typeparam>
        /// <param name="htmlHelper">HtmlHelper to begin a command form within</param>
        /// <param name="expression">Expression holding information about the action on the controller to use</param>
        /// <param name="formMethod"><see cref="FormMethod"/> to use</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        /// <remarks>
        /// For the expression that expressed the action to use, it does not care about the parameters for the action, so these
        /// can be set to null. The expression just represents the action strongly typed.
        /// </remarks>
        public static CommandForm<T> BeginCommandForm<T, TC>(this HtmlHelper htmlHelper, Expression<Func<TC, ActionResult>> expression, FormMethod formMethod)
            where T : ICommand, new()
            where TC : ControllerBase
        {
            return htmlHelper.BeginCommandForm<T, TC>(expression, formMethod, new Dictionary<string, object>());
        }

        /// <summary>
        /// Begins a CommandForm
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <typeparam name="TC">Type of controller holding the action to forward to</typeparam>
        /// <param name="htmlHelper">HtmlHelper to begin a command form within</param>
        /// <param name="expression">Expression holding information about the action on the controller to use</param>
        /// <param name="formMethod"><see cref="FormMethod"/> to use</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to be set for the element</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        /// <remarks>
        /// For the expression that expressed the action to use, it does not care about the parameters for the action, so these
        /// can be set to null. The expression just represents the action strongly typed.
        /// </remarks>
        public static CommandForm<T> BeginCommandForm<T, TC>(this HtmlHelper htmlHelper, Expression<Func<TC, ActionResult>> expression, FormMethod formMethod, IDictionary<string, object> htmlAttributes)
            where T : ICommand, new()
            where TC : ControllerBase
        {
            var lambda = expression as LambdaExpression;
            if( null != lambda )
            {
                var methodCallExpression = lambda.Body as MethodCallExpression;
                if( null != methodCallExpression )
                {
                    var actionName = methodCallExpression.Method.Name;
                    var controllerName = typeof (TC).Name.Replace("Controller",string.Empty);
                    var commandForm = BeginCommandForm<T>(htmlHelper, actionName, controllerName, formMethod, htmlAttributes);
                    return commandForm;
                }
            }

            return null;
        }

        /// <summary>
        /// Begins a CommandForm
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <typeparam name="TC">Type of controller holding the action to forward to</typeparam>
        /// <param name="htmlHelper">HtmlHelper to begin a command form within</param>
        /// <param name="expression">Expression holding information about the action on the controller to use</param>
        /// <param name="formMethod"><see cref="FormMethod"/> to use</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to be set for the element</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        /// <remarks>
        /// For the expression that expressed the action to use, it does not care about the parameters for the action, so these
        /// can be set to null. The expression just represents the action strongly typed.
        /// </remarks>
        public static CommandForm<T> BeginCommandForm<T, TC>(this HtmlHelper htmlHelper, Expression<Func<TC, ActionResult>> expression, FormMethod formMethod, object htmlAttributes)
            where T : ICommand, new()
            where TC : ControllerBase
        {
            return  htmlHelper.BeginCommandForm<T, TC>(expression, FormMethod.Post, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }


        /// <summary>
        /// Begins a CommandForm
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <param name="htmlHelper">HtmlHelper to begin a command form within</param>
        /// <param name="actionName">Action to call on the controller</param>
        /// <param name="controllerName">Controller the action belongs to</param>
        /// <param name="formMethod"><see cref="FormMethod"/> to use</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        public static CommandForm<T> BeginCommandForm<T>(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod formMethod)
            where T : ICommand, new()
        {
            return htmlHelper.BeginCommandForm<T>(actionName, controllerName, formMethod, new Dictionary<string, object>());
        }

        /// <summary>
        /// Begins a CommandForm
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <param name="htmlHelper">HtmlHelper to begin a command form within</param>
        /// <param name="actionName">Action to call on the controller</param>
        /// <param name="controllerName">Controller the action belongs to</param>
        /// <param name="formMethod"><see cref="FormMethod"/> to use</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to be set for the element</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        public static CommandForm<T> BeginCommandForm<T>(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod formMethod, IDictionary<string, object> htmlAttributes)
            where T : ICommand, new()
        {
            var formAction = UrlHelper.GenerateUrl(null, actionName, controllerName, new RouteValueDictionary(), htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, true);
            var form = FormHelper<T>(htmlHelper, formAction, formMethod, htmlAttributes);
            return form;
        }

        /// <summary>
        /// Begins a CommandForm
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <param name="htmlHelper">HtmlHelper to begin a command form within</param>
        /// <param name="actionName">Action to call on the controller</param>
        /// <param name="controllerName">Controller the action belongs to</param>
        /// <param name="formMethod"><see cref="FormMethod"/> to use</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to be set for the element</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        public static CommandForm<T> BeginCommandForm<T>(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod formMethod, object htmlAttributes)
           where T : ICommand, new()
        {
            return htmlHelper.BeginCommandForm<T>(actionName, controllerName, formMethod, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        private static CommandForm<T> FormHelper<T>(this HtmlHelper htmlHelper, string formAction, FormMethod method, IDictionary<string, object> htmlAttributes)
            where T : ICommand, new()
        {
            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

            var builder = new TagBuilder("form");
            builder.MergeAttribute("action", formAction);
            builder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method), true);
            builder.MergeAttribute("data-commandForm","",true);
            if (htmlHelper.ViewContext.ClientValidationEnabled)
            {
                if (!htmlAttributes.ContainsKey("id"))
                {
                    builder.GenerateId(typeof (T).Name);
                }
            }
            builder.MergeAttributes(htmlAttributes);
            htmlHelper.ViewContext.Writer.Write(builder.ToString(TagRenderMode.StartTag));
            var form = new CommandForm<T>(htmlHelper.ViewContext);
            if (htmlHelper.ViewContext.ClientValidationEnabled)
            {
                htmlHelper.ViewContext.FormContext.FormId = builder.Attributes["id"];
            }
            return form;
        }

    }
}
