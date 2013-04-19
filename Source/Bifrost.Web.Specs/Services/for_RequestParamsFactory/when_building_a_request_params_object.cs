using Bifrost.Web.Services;
using Bifrost.Services.Specs.Execution.for_RequestParamsFactory.given;
using Machine.Specifications;

namespace Bifrost.Services.Specs.Execution.for_RequestParamsFactory
{
    [Subject(typeof(RequestParamsFactory))]
    public class when_building_a_request_params_object : a_request_params_factory
    {
        static RequestParams request_params;

        Because of = () => request_params = request_params_factory.BuildParamsCollectionFrom(http_request_base_mock.Object);

        It should_retrieve_parameters_from_the_querystring_first = () =>
                                                                 {
                                                                     request_params[IN_ALL].ShouldEqual(FROM_QUERYSTRING);
                                                                     request_params[IN_QUERY_STRING_ONLY].ShouldEqual(FROM_QUERYSTRING);
                                                                 };

        It should_retrieve_parameters_from_the_form_second = () =>
                                                          {
                                                              request_params[IN_FORM_ONLY].ShouldEqual(FROM_FORMS);
                                                              request_params[IN_FORMS_INPUT_STREAM_COOKIES_AND_SERVER_VARIABLES].ShouldEqual(FROM_FORMS);
                                                          };

        It should_retrieve_parameters_from_the_input_stream_third = () =>
                                                                  {
                                                                      serializer_mock.VerifyAll();
                                                                      request_params[IN_INPUT_STREAM_ONLY].ShouldEqual(FROM_INPUTSTREAM);
                                                                      request_params[IN_INPUT_STREAM_COOKIES_AND_SERVER_VARIABLES].ShouldEqual(FROM_INPUTSTREAM);
                                                                  };
    }
}