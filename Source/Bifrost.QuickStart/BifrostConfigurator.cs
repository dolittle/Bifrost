using System.Diagnostics;
using System.Web;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.QuickStart.Concepts.Persons;
using Bifrost.QuickStart.Domain.HumanResources.Employees;
using Bifrost.Validation;

namespace Bifrost.QuickStart
{
    public class BifrostConfigurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            var dataPath = HttpContext.Current.Server.MapPath("~/App_Data");
            configure
                .Serialization
                    .UsingJson()
                .Events
                    .UsingRavenDB(e=>e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))
                    //.UsingRavenDBEmbedded(e=>e.LocatedAt(dataPath).WithManagementStudio())
                .Events
                    .Asynchronous(e=>e.UsingSignalR())
                .DefaultStorage
                    .UsingRavenDB(e => e.WithUrl("http://localhost:8080").WithDefaultDatabase("QuickStart"))
                    //.UsingRavenDBEmbedded(e=>e.LocatedAt(dataPath))
                .Frontend
                    .Web(w=> {
                        w.AsSinglePageApplication();
                        w.PathsToNamespaces.Clear();

                        w.PathsToNamespaces.Add("Visualizer/QualityAssurance", "Bifrost.Visualizer.QualityAssurance");
                        w.PathsToNamespaces.Add("Bifrost/Visualizer/QualityAssurance", "Bifrost.Visualizer.QualityAssurance");
                        w.PathsToNamespaces.Add("/Visualizer/QualityAssurance", "Bifrost.Visualizer.QualityAssurance");
                        w.PathsToNamespaces.Add("/Bifrost/Visualizer/QualityAssurance", "Bifrost.Visualizer.QualityAssurance");

                        w.PathsToNamespaces.Add("Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("Bifrost/Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("/Visualizer", "Bifrost.Visualizer");
                        w.PathsToNamespaces.Add("/Bifrost/Visualizer", "Bifrost.Visualizer");



                        w.PathsToNamespaces.Add("Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("/Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("Bifrost/Visualizer/**/", "Bifrost.Visualizer.**.");
                        w.PathsToNamespaces.Add("/Bifrost/Visualizer/**/", "Bifrost.Visualizer.**.");

                        w.PathsToNamespaces.Add("Features/**/", "Bifrost.QuickStart.Features.**.");
                        w.PathsToNamespaces.Add("/Features/**/", "Bifrost.QuickStart.Features.**.");
                        w.NamespaceMapper.Add("Bifrost.QuickStart.Features.**.", "Bifrost.QuickStart.Domain.HumanResources.**.");

                        w.NamespaceMapper.Add("Bifrost.QuickStart.Domain.HumanResources.**.", "Bifrost.QuickStart.Features.**.");
                        w.NamespaceMapper.Add("Bifrost.QuickStart.Read.HumanResources.**.", "Bifrost.QuickStart.Features.**.");
					})
                .WithMimir();

            var validatorProvider = configure.Container.Get<ICommandValidatorProvider>();
            var inputValidator = validatorProvider.GetInputValidatorFor(typeof(TestCommandWithMultiplePropertiesOfTheSameType));
            var businessValidator = validatorProvider.GetBusinessValidatorFor(typeof(TestCommandWithMultiplePropertiesOfTheSameType));

            var validcommand = new TestCommandWithMultiplePropertiesOfTheSameType()
                {
                    First = "valid",
                    Second = "valid"
                };

            var invalidcommand1 = new TestCommandWithMultiplePropertiesOfTheSameType()
            {
                First = "valid",
                Second = "invalid"
            };

            var invalidcommand2 = new TestCommandWithMultiplePropertiesOfTheSameType()
            {
                First = "invalid",
                Second = "valid"
            };

            var inputResul1 = inputValidator.ValidateFor(validcommand);
            var inputResult2 = inputValidator.ValidateFor(invalidcommand1);
            var inputResult3 = inputValidator.ValidateFor(invalidcommand2);
        }
    }
}