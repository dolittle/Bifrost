/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Web;
using System.Web.SessionState;
using Bifrost.Configuration;
using Bifrost.Exceptions;
using Bifrost.Execution;
using Bifrost.Security;
using Bifrost.Services;

namespace Bifrost.Web.Services
{
    // Todo : add async support - performance gain! 
    public class RestServiceRouteHttpHandler : IHttpHandler, IRequiresSessionState // IHttpAsyncHandler
    {
        readonly Type _type;
        readonly string _url;
        readonly IRequestParamsFactory _factory;
        readonly IRestServiceMethodInvoker _invoker;
        readonly IContainer _container;
        readonly ISecurityManager _securityManager;
        readonly IExceptionPublisher _exceptionPublisher;

        public RestServiceRouteHttpHandler(Type type, string url) 
            : this(
                type,
                url,
                Configure.Instance.Container.Get<IRequestParamsFactory>(),
                Configure.Instance.Container.Get<IRestServiceMethodInvoker>(),
                Configure.Instance.Container,
                Configure.Instance.Container.Get<ISecurityManager>(),
                Configure.Instance.Container.Get<IExceptionPublisher>())
        {}

        public RestServiceRouteHttpHandler(
            Type type,
            string url,
            IRequestParamsFactory factory,
            IRestServiceMethodInvoker invoker,
            IContainer container,
            ISecurityManager securityManager,
            IExceptionPublisher exceptionPublisher)
        {
            _type = type;
            _url = url;
            _factory = factory;
            _invoker = invoker;
            _container = container;
            _securityManager = securityManager;
            _exceptionPublisher = exceptionPublisher;
        }

        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var form = _factory.BuildParamsCollectionFrom(new HttpRequest(new HttpRequestWrapper(HttpContext.Current.Request)));
                var serviceInstance = _container.Get(_type);

                var authorizationResult = _securityManager.Authorize<InvokeService>(serviceInstance);

                if (!authorizationResult.IsAuthorized)
                {
                    throw new HttpStatus.HttpStatusException(404, "Forbidden");
                }

                var result = _invoker.Invoke(_url, serviceInstance, context.Request.Url, form);
                context.Response.Write(result);
            }
            catch (Exception e)
            {
                _exceptionPublisher.Publish(e);
                if (e.InnerException is HttpStatus.HttpStatusException)
                {
                    var ex = e.InnerException as HttpStatus.HttpStatusException;
                    context.Response.StatusCode = ex.Code;
                    context.Response.StatusDescription = ex.Description;
                }
                else
                {
                    context.Response.StatusCode = 500;
                    context.Response.StatusDescription = e.Message.Substring(0,e.Message.Length >= 512 ? 512: e.Message.Length);
                }
            }
        }
    }
}
