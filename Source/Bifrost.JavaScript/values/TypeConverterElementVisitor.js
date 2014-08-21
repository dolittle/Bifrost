Bifrost.namespace("Bifrost.values", {
    TypeConverterElementVisitor: Bifrost.markup.ElementVisitor.extend(function () {
        this.visit = function (element, resultActions) {
            /*
            var typeConverterAttribute = element.getAttribute("data-typeconverter");
            if (Bifrost.isNullOrUndefined(typeConverterAttribute)) return;

            var bindingExpression = element.getAttribute("data-bind");
            if (!Bifrost.isNullOrUndefined(bindingExpression)) {
                var bindingString = bindingExpression.toString();
                var context = ko.contextFor(element);

                var bindings = ko.bindingProvider.instance.parseBindingsString(bindingString, context, element);
                for (var property in bindings) {
                    if (property.indexOf("_") == 0) continue;
                    var value = bindings[property];
                    if (ko.isObservable(value)) {
                        value.extend({ typeConverter: typeConverterAttribute.toString() })
                    }
                }
            }*/
        };
    })
});