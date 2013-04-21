using System.IO;
using System.Text;
using System.Web.Mvc;
using Machine.Specifications;
using Moq;

namespace Bifrost.Web.Mvc.Specs.given
{
    public class an_ajax_helper
    {
        protected static Mock<ViewContext> view_context_mock;
        protected static Mock<IViewDataContainer> view_data_container_mock;
        protected static AjaxHelper ajax_helper;
        protected static StringBuilder string_builder;
        protected static ViewDataDictionary view_data;

        Establish context = () =>
        {
            view_data = new ViewDataDictionary();
            string_builder = new StringBuilder();
            view_context_mock = new Mock<ViewContext>();
            view_context_mock.SetupGet(v => v.Writer).Returns(new StringWriter(string_builder));
            view_context_mock.SetupGet(v => v.ClientValidationEnabled).Returns(false);
            view_context_mock.SetupGet(v => v.ViewData).Returns(view_data);
            view_data_container_mock = new Mock<IViewDataContainer>();
            view_data_container_mock.Setup(v => v.ViewData).Returns(view_data);
            ajax_helper = new AjaxHelper(view_context_mock.Object, view_data_container_mock.Object);
        };

    }
}
