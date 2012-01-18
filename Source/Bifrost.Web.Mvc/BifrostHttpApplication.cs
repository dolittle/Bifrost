#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
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

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Validation;
using FluentValidation.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace Bifrost.Web.Mvc
{
    /// <summary>
    /// Represents a HttpApplication that handles initialization of Bifrost and
    /// abstracts some of the tedious tasks needed to configure it
    /// </summary>
    public abstract class BifrostHttpApplication : HttpApplication, IApplication
    {
        public IContainer Container { get; private set; }
        public virtual void OnConfigure(Configure configure) { }
        public virtual void OnStarted() { }
        public virtual void OnStopped() { }

        protected abstract IContainer CreateContainer();

        /// <summary>
        /// Changes the default convention for locations of Views and Master pages for all view engines
        /// </summary>
        /// <param name="siteRelativePath">Relative path within the site from root of site you want your views to be found</param>
        /// <remarks>
        /// By default ASP.net MVC has its views located in the Views folder of your site.
        /// This method can automatically change that location if one likes.
        /// </remarks>
        protected void RelocateViews(string siteRelativePath)
        {
            foreach (var viewEngine in ViewEngines.Engines)
            {
                if (viewEngine is VirtualPathProviderViewEngine)
                {
                    var virtualPathViewEngine = viewEngine as VirtualPathProviderViewEngine;

                    virtualPathViewEngine.MasterLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.MasterLocationFormats, "Views", siteRelativePath);
                    virtualPathViewEngine.ViewLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.ViewLocationFormats, "Views", siteRelativePath);
                    virtualPathViewEngine.PartialViewLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.PartialViewLocationFormats, "Views", siteRelativePath);

                    virtualPathViewEngine.AreaMasterLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.AreaMasterLocationFormats, "Views", siteRelativePath);
                    virtualPathViewEngine.AreaViewLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.AreaViewLocationFormats, "Views", siteRelativePath);
                    virtualPathViewEngine.AreaPartialViewLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.AreaPartialViewLocationFormats, "Views", siteRelativePath);
                }
            }
        }

        protected void Application_Start()
        {
            Container = CreateContainer();
            SetupDependencyResolver();

            var configure = Configure.With(Container, BindingLifecycle.Request).SpecificApplication(this);
            OnConfigure(configure);
            configure.Initialize();

            SetupValidation();
            OnStarted();
        }

        protected void Application_Stop()
        {
            OnStopped();
        }

        void SetupDependencyResolver()
        {
            var dependencyResolver = new ContainerDependencyResolver(Container);
            DependencyResolver.SetResolver(dependencyResolver);
        }

        void SetupValidation()
        {
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            var factory = Container.Get<CommandValidatorFactory>();
            FluentValidationModelValidatorProvider.Configure(p => {
                p.ValidatorFactory = factory;
                p.AddImplicitRequiredValidator = false;
            });

            var validatorFactory = Container.Get<DefaultValidatorFactory>();
            var validatorProvider = new FluentValidationModelValidatorProvider(validatorFactory);
            validatorProvider.AddImplicitRequiredValidator = false;
            ModelValidatorProviders.Providers.Add(validatorProvider);
        }

        static string[] ReplaceInStrings(IEnumerable<string> strings, string partToReplace, string replaceWith)
        {
            return strings.Select(@string => @string.Replace(partToReplace, replaceWith)).ToArray();
        }
    }
}
