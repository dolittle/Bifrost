Bifrost.namespace("Bifrost.markup", {
    propertyExpander: Bifrost.Singleton(function (valueProviderParser) {
        this.expand = function (element, target) {
            for (var attributeIndex = 0; attributeIndex < element.attributes.length; attributeIndex++) {
                var name = element.attributes[attributeIndex].localName;
                var value = element.attributes[attributeIndex].value;

                if (name in target) {
                    if (valueProviderParser.hasValueProvider(value)) {
                        valueProviderParser.parseFor(target, name, value);
                    }
                }
            }
        };
    })
});