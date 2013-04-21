ko.bindingHandlers.applicationBarContent = {
    init: function (element, valueAccessor, allBindingAccessor, viewModel) {
        $(element).hide();
        Bifrost.messaging.Messenger.global.publish("applicationBarContent", $(element).children());
    }
};

ko.bindingHandlers.applicationBar = {
    init: function (element) {
        Bifrost.messaging.Messenger.global.subscribeTo("applicationBarContent", function (content) {
            $(element).html("");
            if (content.length > 0) {
                var container = $("<div/>");

                var viewModel = ko.dataFor(content[0]);
                $(container).append(content);
                ko.applyBindings(viewModel, container[0]);

                $(element).append(container);
            } 
        });
    }
};