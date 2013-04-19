#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
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
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Bifrost.Commands;
using System.Web.Mvc.Ajax;
using System.Globalization;
using System.Text;
using System.IO;

namespace Bifrost.Web.Mvc.Commands
{
    /// <summary>
    /// Provides Ajax helper methods for Commands
    /// </summary>
    public static class CommandAjaxHelper
    {
        const string CommandFormEventsScriptKey = "CommandFormEventsScript";

        /// <summary>
        /// Begins a <see cref="CommandForm{T}"/>, with default <see cref="FormMethod.Post"/> as method
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <typeparam name="TC">Type of controller holding the action to forward to</typeparam>
        /// <param name="AjaxHelper">AjaxHelper to begin a command form within</param>
        /// <param name="ajaxOptions">Ajax Options for the command form</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        /// <remarks>
        /// For the expression that expressed the action to use, it does not care about the parameters for the action, so these
        /// can be set to null. The expression just represents the action strongly typed.
        /// </remarks>
        public static CommandForm<T> BeginCommandForm<T, TC>(this AjaxHelper AjaxHelper, AjaxOptions ajaxOptions = null)
            where T : ICommand, new()
            where TC : ControllerBase
        {
            var action = ControllerHelpers.GetActionForCommand<T, TC>();
            var controllerName = ControllerHelpers.GetControllerNameFromType<TC>();
            var command = AjaxHelper.BeginCommandForm<T>(action.Name, controllerName, ajaxOptions);
            return command;
        }

        /// <summary>
        /// Begins a <see cref="CommandForm{T}"/>, with default <see cref="FormMethod.Post"/> as method
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <typeparam name="TC">Type of controller holding the action to forward to</typeparam>
        /// <param name="AjaxHelper">AjaxHelper to begin a command form within</param>
        /// <param name="ajaxOptions">Ajax Options for the command form</param>
        /// <param name="expression">Expression holding information about the action on the controller to use</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        /// <remarks>
        /// For the expression that expressed the action to use, it does not care about the parameters for the action, so these
        /// can be set to null. The expression just represents the action strongly typed.
        /// </remarks>
        public static CommandForm<T> BeginCommandForm<T, TC>(this AjaxHelper AjaxHelper, Expression<Func<TC, ActionResult>> expression, AjaxOptions ajaxOptions=null)
            where T : ICommand, new()
            where TC : ControllerBase
        {
            var command = AjaxHelper.BeginCommandForm<T, TC>(expression, ajaxOptions, new Dictionary<string, object>());
            return command;
        }


        /// <summary>
        /// Begins a <see cref="CommandForm{T}"/>, with default <see cref="FormMethod.Post"/> as method
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <typeparam name="TC">Type of controller holding the action to forward to</typeparam>
        /// <param name="AjaxHelper">AjaxHelper to begin a command form within</param>
        /// <param name="ajaxOptions">Ajax Options for the command form</param>
        /// <param name="expression">Expression holding information about the action on the controller to use</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to be set for the element</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        /// <remarks>
        /// For the expression that expressed the action to use, it does not care about the parameters for the action, so these
        /// can be set to null. The expression just represents the action strongly typed.
        /// </remarks>
        public static CommandForm<T> BeginCommandForm<T, TC>(this AjaxHelper AjaxHelper, Expression<Func<TC, ActionResult>> expression, AjaxOptions ajaxOptions, object htmlAttributes)
            where T : ICommand, new()
            where TC : ControllerBase
        {
            var command = AjaxHelper.BeginCommandForm<T, TC>(expression, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return command;
        }

        /// <summary>
        /// Begins a CommandForm
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <typeparam name="TC">Type of controller holding the action to forward to</typeparam>
        /// <param name="AjaxHelper">AjaxHelper to begin a command form within</param>
        /// <param name="ajaxOptions">Ajax Options for the command form</param>
        /// <param name="expression">Expression holding information about the action on the controller to use</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to be set for the element</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        /// <remarks>
        /// For the expression that expressed the action to use, it does not care about the parameters for the action, so these
        /// can be set to null. The expression just represents the action strongly typed.
        /// </remarks>
        public static CommandForm<T> BeginCommandForm<T, TC>(this AjaxHelper AjaxHelper, Expression<Func<TC, ActionResult>> expression, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
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
                    var commandForm = BeginCommandForm<T>(AjaxHelper, actionName, controllerName, ajaxOptions, htmlAttributes);
                    return commandForm;
                }
            }

            return null;
        }


        /// <summary>
        /// Begins a CommandForm
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <param name="AjaxHelper">AjaxHelper to begin a command form within</param>
        /// <param name="actionName">Action to call on the controller</param>
        /// <param name="controllerName">Controller the action belongs to</param>
        /// <param name="ajaxOptions">Ajax Options for the command form</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        public static CommandForm<T> BeginCommandForm<T>(this AjaxHelper AjaxHelper, string actionName, string controllerName, AjaxOptions ajaxOptions)
            where T : ICommand, new()
        {
            return AjaxHelper.BeginCommandForm<T>(actionName, controllerName, ajaxOptions, new Dictionary<string, object>());
        }

        /// <summary>
        /// Begins a CommandForm
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <param name="AjaxHelper">AjaxHelper to begin a command form within</param>
        /// <param name="actionName">Action to call on the controller</param>
        /// <param name="controllerName">Controller the action belongs to</param>
        /// <param name="ajaxOptions">Ajax Options for the command form</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to be set for the element</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        public static CommandForm<T> BeginCommandForm<T>(this AjaxHelper AjaxHelper, string actionName, string controllerName, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
            where T : ICommand, new()
        {
            var formAction = UrlHelper.GenerateUrl(null, actionName, controllerName, new RouteValueDictionary(), AjaxHelper.RouteCollection, AjaxHelper.ViewContext.RequestContext, true);
            var form = FormHelper<T>(AjaxHelper, formAction, actionName, controllerName, ajaxOptions, htmlAttributes);
            return form;
        }

        /// <summary>
        /// Begins a CommandForm
        /// </summary>
        /// <typeparam name="T">Type of Command to create form for</typeparam>
        /// <param name="AjaxHelper">AjaxHelper to begin a command form within</param>
        /// <param name="actionName">Action to call on the controller</param>
        /// <param name="controllerName">Controller the action belongs to</param>
        /// <param name="ajaxOptions">Ajax Options for the command form</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to be set for the element</param>
        /// <returns>A <see cref="CommandForm{T}"/></returns>
        public static CommandForm<T> BeginCommandForm<T>(this AjaxHelper AjaxHelper, string actionName, string controllerName, AjaxOptions ajaxOptions, object htmlAttributes)
           where T : ICommand, new()
        {
            return AjaxHelper.BeginCommandForm<T>(actionName, controllerName, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        static CommandForm<T> FormHelper<T>(this AjaxHelper ajaxHelper, string formAction, string action, string controller, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
            where T : ICommand, new()
        {
            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

            RenderCommandFormEventsScriptIfNotOnPage(ajaxHelper);

            var builder = new TagBuilder("form");
            builder.MergeAttribute("action", formAction);
            builder.MergeAttributes<string, object>(htmlAttributes);
            builder.MergeAttribute("method", "post");
            builder.MergeAttribute("data-commandForm","",true);

            if (ajaxHelper.ViewContext.ClientValidationEnabled && !htmlAttributes.ContainsKey("id"))
                builder.GenerateId(typeof (T).Name);

            var idKey = "id";
            var formId = builder.Attributes.ContainsKey(idKey) ? builder.Attributes["id"] : Guid.NewGuid().ToString();
            
            ajaxOptions = ajaxOptions ?? new AjaxOptions();

            ajaxOptions.OnSuccess = string.IsNullOrEmpty(ajaxOptions.OnSuccess) ? "Bifrost.commands.commandFormEvents.onSuccess('#" + formId + "', data)" : ajaxOptions.OnSuccess;
            ajaxOptions.HttpMethod = string.IsNullOrEmpty(ajaxOptions.HttpMethod) ? "Get" : ajaxOptions.HttpMethod;

            if (ajaxHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
                builder.MergeAttributes<string, object>(ajaxOptions.ToUnobtrusiveHtmlAttributes());
            
            ajaxHelper.ViewContext.Writer.Write(builder.ToString(TagRenderMode.StartTag));
            var form = new CommandForm<T>(ajaxHelper.ViewContext);
            form.Action = action;
            form.Controller = controller;

            if (ajaxHelper.ViewContext.ClientValidationEnabled)
                ajaxHelper.ViewContext.FormContext.FormId = formId;
            return form;
        }

        static void RenderCommandFormEventsScriptIfNotOnPage(AjaxHelper ajaxHelper)
        {
            if (ajaxHelper.ViewData.ContainsKey(CommandFormEventsScriptKey))
                return;

            var type = typeof(CommandAjaxHelper);
            var resourceName = string.Format("{0}.commandFormEvents.js",type.Namespace);
            var stream = type.Assembly.GetManifestResourceStream(resourceName);
            var resourceAsBytes = new byte[stream.Length];
            stream.Read(resourceAsBytes,0,resourceAsBytes.Length);
            var script = UTF8Encoding.UTF8.GetString(resourceAsBytes);

            var builder = new TagBuilder("script");
            builder.Attributes["type"] = "text/javascript";
            builder.InnerHtml = script;
            ajaxHelper.ViewContext.Writer.Write(builder.ToString());

            ajaxHelper.ViewData[CommandFormEventsScriptKey] = true;
        }
    }
}
