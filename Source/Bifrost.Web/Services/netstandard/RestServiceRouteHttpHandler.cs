/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading.Tasks;
using Bifrost.Configuration;
using Bifrost.Exceptions;
using Bifrost.Execution;
using Bifrost.Security;
using Bifrost.Services;
using Microsoft.AspNetCore.Http;

namespace Bifrost.Web.Services
{
    public class RestServiceRouteHttpHandler
    {
        Type _type;
        string _url;
        IRequestParamsFactory _factory;
        IRestServiceMethodInvoker _invoker;
        IContainer _container;
        ISecurityManager _securityManager;
        IExceptionPublisher _exceptionPublisher;

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

        public Task ProcessRequest(HttpContext context)
        {
            try
            {
                var form = _factory.BuildParamsCollectionFrom(new HttpRequest(context.Request));
                var serviceInstance = _container.Get(_type);

                var authorizationResult = _securityManager.Authorize<InvokeService>(serviceInstance);

                if (!authorizationResult.IsAuthorized)
                {
                    throw new HttpStatus.HttpStatusException(404, "Forbidden");
                }

                var request = context.Request;
                var url = $"{request.Scheme}://{request.Host}{request.Path}";
                var result = _invoker.Invoke(_url, serviceInstance, new Uri(url), form);
                return context.Response.WriteAsync(result);
            }
            catch (Exception e)
            {
                _exceptionPublisher.Publish(e);
                var message = string.Empty;
                if (e.InnerException is HttpStatus.HttpStatusException)
                {
                    var ex = e.InnerException as HttpStatus.HttpStatusException;
                    context.Response.StatusCode = ex.Code;
                    message = ex.Description;
                }
                else
                {
                    context.Response.StatusCode = 500;
                    message = e.Message.Substring(0,e.Message.Length >= 512 ? 512: e.Message.Length);
                }
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync($"{{'message':'{message}'}}");
            }
        }
    }
}
