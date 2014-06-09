Bifrost.namespace("Bifrost.QuickStart.Features.Dialog", {
    Index: Bifrost.views.Control.extend(function (configuration) {
        this.configuration = configuration;
    })
});

ko.bindingHandlers.dialog = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var configuration = valueAccessor();

        var messenger = Bifrost.messaging.Messenger.global;

        messenger.subscribeTo(configuration.openmessage, function () {
            var body = $(".modal-body", element)[0];
            var $footer = $(".modal-footer", body);
            if ($footer.length == 1) {
                var content = $(".modal-content", element)[0];
                var footer = $footer[0];

                footer.parentElement.removeChild(footer);
                content.appendChild(footer);
            }

            $(element).modal();
            $(element).on("hidden.bs.modal", function (e) {
            });
        });

        messenger.subscribeTo(configuration.closemessage, function () {
            $(element).modal("hide");
        });
    }
}