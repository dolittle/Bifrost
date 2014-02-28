Bifrost.namespace("Bifrost.navigation", {
    navigationBindingHandler: Bifrost.Type.extend(function() {
        this.init = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            //return ko.bindingHandlers.template.init(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor));
        };
        this.update = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            //return ko.bindingHandlers.template.update(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor), allBindingsAccessor, viewModel, bindingContext);
        };
    })
});
Bifrost.navigation.navigationBindingHandler.initialize = function () {
    ko.bindingHandlers.navigation = Bifrost.navigation.navigationBindingHandler.create();
    ko.jsonExpressionRewriting.bindingRewriteValidators.view = false; // Can't rewrite control flow bindings
    ko.virtualElements.allowedBindings.view = true;
};

/*
var configurationItems = ko.expressionRewriting.parseObjectLiteral(configurationString);
var configuration = {};

for (var index = 0; index < configurationItems.length; index++) {
    var item = configurationItems[index];
    configuration[item.key.trim()] = item.value.trim();
}

if (typeof configuration.uriMapper !== "undefined") {
    var mapper = Bifrost.uriMappers[configuration.uriMapper];
    var frame = Bifrost.navigation.NavigationFrame.create({
        stringMapper: mapper,
        home: configuration.home || ''
    });
    frame.setContainer(element);

    element.navigationFrame = frame;
}
*/
